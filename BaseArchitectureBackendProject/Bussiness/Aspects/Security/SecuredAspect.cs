using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Bussiness.Aspects.Security
{
    public class SecuredAspect : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        //"admin,user" gibi tek bir string içerisinde rolleri alırım daha sonra virgül ile onları ayırabilirim
        //Role için oluşturduğumuz bir yapıcı metot
        public SecuredAspect(string roles)
        {
            _roles=roles.Split(",");
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        //İşin içinde role yoksa bu şekilde kullanabiliriz
        public SecuredAspect()
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
           
            //Role kontrolü gerekiyorsa
            if (_roles != null)
            {
                //ClaimRoles bi extension class'tır
                var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();

                foreach (var role in _roles)
                {
                    if (roleClaims.Contains(role))
                        return;
                }
                throw new Exception("İşlem için yetkiniz bulunmuyor");
            }
            else
            {
                //Role kontrolü gerekmiyorsa
                var claims = _httpContextAccessor.HttpContext.User.Claims;

                //Token var mı
                if (claims.Count() > 0)
                    return;

                throw new Exception("İşlem için yetkiniz bulunmuyor");
            }
        }
    }
}
