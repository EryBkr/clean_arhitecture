using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    //Claim listesinde ki işlemlerimiz için oluşturduğumuz bi extension
    public static class ClaimsPrincipalExtensions
    {
        //Name bazlı claim lere eriştik
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipals,string claimType)
        {
            var result = claimsPrincipals?.FindAll(claimType)?.Select(i => i.Value).ToList();
            return result;
        }

        //Role bazlı claim lere eriştik
        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipals)
        {
            var result = claimsPrincipals?.Claims(claimType:ClaimTypes.Role)?.ToList();
            return result;
        }
    }
}
