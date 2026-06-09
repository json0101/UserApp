using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using service.Commons.Exceptions;
using UserApp.Domain;
using UserApp.Domain.Entities;
using UserApp.Service.Commons;
using UserApp.Service.Services.Emails;
using UserApp.Service.Services.PasswordReset.Dtos;

namespace UserApp.Service.Services.PasswordReset
{
    public class PasswordResetService : IPasswordResetService
    {
        private readonly UserAppContext _context;
        private readonly IEmailService _emailService;
        private readonly EmailSetting _emailSetting;
        private static readonly PasswordHasher<User> _passwordHasher = new();

        public PasswordResetService(
            UserAppContext context,
            IEmailService emailService,
            IOptions<EmailSetting> emailSetting
        )
        {
            _context = context;
            _emailService = emailService;
            _emailSetting = emailSetting.Value;
        }

        public string RequestPasswordReset(RequestPasswordResetDto dto)
        {
            var email = (dto.email ?? "").Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(_emailSetting.AllowedDomain)
                && !email.EndsWith(_emailSetting.AllowedDomain.ToLower()))
            {
                throw new BadRequestException($"El correo debe de terminar en {_emailSetting.AllowedDomain}");
            }

            var user = GetUserByEmployeeCode(dto.employeeCode);

            if (user == null)
            {
                throw new NotFoundException("El codigo del empleado no fue encontrado");
            }

            // Equivalente a datosValidosEnvioCorreo: el correo enviado debe coincidir con el del usuario.
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                throw new NotFoundException("El empleado no cuenta con correo");
            }

            if (!string.Equals(user.Email.Trim(), email, StringComparison.OrdinalIgnoreCase))
            {
                throw new NotFoundException("Credenciales incorrectas");
            }

            var pin = GeneratePin4Digits();

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                DeactivateActiveRequests(user.Id);

                var request = new PasswordResetRequest
                {
                    UserId = user.Id,
                    Pin = pin,
                    RequestedAt = DateTime.Now.ToUniversalTime(),
                    ExpiresAt = DateTime.Now.ToUniversalTime().AddDays(1),
                    EmailSentTo = email,
                    Active = true,
                };

                _context.PasswordResetRequest.Add(request);
                _context.SaveChanges();

                _emailService.SendPasswordResetPin(email, pin);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }

            return "Correo enviado";
        }

        // Paso 2: validar que el PIN es correcto y no ha vencido (no lo consume todavia).
        public void ValidatePin(ValidatePinDto dto)
        {
            var user = GetUserByEmployeeCode(dto.employeeCode);

            if (user == null)
            {
                throw new NotFoundException("No se encontro el empleado");
            }

            GetValidRequestOrThrow(user.Id, dto.pin);
        }

        // Paso 3: cambiar la contrasena. Se re-valida el PIN para asegurar el flujo.
        public string ChangePassword(ChangePasswordWithPinDto dto)
        {
            if (dto.password != dto.confirmPassword)
            {
                throw new BadRequestException("Valide bien la contrasena");
            }

            if ((dto.password ?? "").Length < 8)
            {
                throw new BadRequestException("La contrasena no puede tener menos de 8 caracteres");
            }

            var user = GetUserByEmployeeCode(dto.employeeCode);

            if (user == null)
            {
                throw new NotFoundException("El codigo del empleado no fue encontrado");
            }

            var request = GetValidRequestOrThrow(user.Id, dto.pin);

            user.Password = _passwordHasher.HashPassword(user, dto.password);

            request.Active = false;
            request.PasswordChangedAt = DateTime.Now.ToUniversalTime();
            request.DeactivatedAt = DateTime.Now.ToUniversalTime();

            _context.SaveChanges();

            return "La contrasena se cambio exitosamente";
        }

        private PasswordResetRequest GetValidRequestOrThrow(int userId, string pin)
        {
            var request = _context.PasswordResetRequest
                .Where(x => x.UserId == userId && x.Pin == pin && x.Active)
                .FirstOrDefault();

            if (request == null)
            {
                throw new NotFoundException("El PIN es incorrecto");
            }

            if (request.ExpiresAt < DateTime.Now.ToUniversalTime())
            {
                throw new BadRequestException("El PIN ha vencido, solicite uno nuevo");
            }

            return request;
        }

        private User? GetUserByEmployeeCode(string employeeCode)
        {
            return _context.User
                .Where(x => x.EmployeeCode == employeeCode && x.Active)
                .FirstOrDefault();
        }

        private void DeactivateActiveRequests(int userId)
        {
            var requests = _context.PasswordResetRequest
                .Where(x => x.UserId == userId && x.Active)
                .ToList();

            foreach (var r in requests)
            {
                r.Active = false;
                r.DeactivatedAt = DateTime.Now.ToUniversalTime();
            }
        }

        private static string GeneratePin4Digits()
        {
            var pin = "";
            for (int i = 0; i < 4; i++)
            {
                pin += RandomNumberGenerator.GetInt32(0, 10).ToString();
            }
            return pin;
        }
    }
}
