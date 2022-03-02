using Business.Constants;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;

namespace WebUI.UIAspects
{
    public class AuthAspect : MethodInterception
    {
        private IHttpContextAccessor _httpContextAccessor;
        public AuthAspect()
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            if(IsAuthenticated())
            {
                
            }
        }

        private bool IsAuthenticated()
        {
            if (string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Session.GetString("Authentication")))
            {
                return false;
            }
            return true;
            
        }
    }
    
}
