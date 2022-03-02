using Core.Utilities.IoC;
using Core.Utilities.Security.JWT;

namespace WebUI.Helpers
{
    public static class AuthHelper
    {
        private static IHttpContextAccessor _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        public static bool IsAuthenticated()
        {
            if (string.IsNullOrWhiteSpace(_httpContextAccessor.HttpContext.Session.GetString("Authentication")))
            {
                return false;
            }
            return true;
        }
    }
}
