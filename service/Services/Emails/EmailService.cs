using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using service.Commons.Exceptions;
using UserApp.Service.Commons;

namespace UserApp.Service.Services.Emails
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetting _emailSetting;
        public EmailService(IOptions<EmailSetting> emailSetting)
        {
            _emailSetting = emailSetting.Value;
        }

        public void Send(string to, string subject, string htmlBody)
        {
            if (string.IsNullOrWhiteSpace(_emailSetting.Host) || string.IsNullOrWhiteSpace(_emailSetting.From))
            {
                throw new BadRequestException("El servicio de correo no esta configurado");
            }

            using var message = new MailMessage
            {
                From = new MailAddress(_emailSetting.From, _emailSetting.FromName ?? _emailSetting.From),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true,
            };
            message.To.Add(to);

            using var client = new SmtpClient(_emailSetting.Host, _emailSetting.Port)
            {
                EnableSsl = _emailSetting.EnableSsl,
            };

            if (!string.IsNullOrWhiteSpace(_emailSetting.User))
            {
                client.Credentials = new NetworkCredential(_emailSetting.User, _emailSetting.Password);
            }

            client.Send(message);
        }

        public void SendPasswordResetPin(string to, string pin)
        {
            var subject = "Cambio de contrasena - PIN de verificacion";
            var htmlBody =
                $"<p>Hola,</p>" +
                $"<p>Su PIN para el cambio de contrasena es: <b style=\"font-size:18px\">{pin}</b></p>" +
                $"<p>Este PIN es valido por 24 horas. Si usted no solicito el cambio, ignore este correo.</p>";

            Send(to, subject, htmlBody);
        }
    }
}
