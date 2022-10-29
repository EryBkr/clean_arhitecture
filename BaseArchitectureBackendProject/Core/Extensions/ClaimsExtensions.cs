using System.Security.Claims;

namespace Core.Extensions
{
    public static class ClaimsExtensions
    {
        //Claim e key-value şeklinde claim atamak için kullandığımız extension
        public static void AddName(this ICollection<Claim> claims, string name) => claims.Add(new Claim(ClaimTypes.Name, name));

        //Claim e key-value şeklinde role atamak için kullandığımız extension
        public static void AddRoles(this ICollection<Claim> claims, string[] roles) => roles.ToList().ForEach(role=>claims.Add(new Claim(ClaimTypes.Role, role)));
    }
}
