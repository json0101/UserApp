using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace UserApp.Service.Commons.CurrentUser
{
    public class CurrentUserService : ICurrentUserService
    {
        private const string SystemUser = "system";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserName
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;

                if (user == null || user.Identity?.IsAuthenticated != true)
                {
                    return SystemUser;
                }

                // El login firma el username en NameIdentifier ("nameid"). Como respaldo
                // se prueba el claim "name" / Identity.Name por si cambia el mapeo de claims.
                var userName = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? user.FindFirst(ClaimTypes.Name)?.Value
                    ?? user.Identity?.Name;

                return string.IsNullOrWhiteSpace(userName) ? SystemUser : userName;
            }
        }
    }
}
