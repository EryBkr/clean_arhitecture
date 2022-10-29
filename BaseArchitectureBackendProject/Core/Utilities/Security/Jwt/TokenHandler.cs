using Core.Extensions;
using Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Core.Utilities.Security.Jwt
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public object JwtSecurityKey { get; private set; }

        public Token CreateToken(User user, List<OperationClaim> operationClaims)
        {
            Token token = new Token();

            //Security Key'in simetriği alınacak
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            //Şifrelenmiş kimliği oluşturuyoruz
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            //Token ayarları
            token.Expiration = DateTime.Now.AddSeconds(60);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                expires: token.Expiration,
                claims: SetClaims(user,operationClaims),
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials);

            //Token Creater
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            token.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

            //Refresh Token Creater
            token.RefreshToken = CreateRefreshToken();
            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using(RandomNumberGenerator random=RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }

        private IEnumerable<Claim> SetClaims(User user,List<OperationClaim> operationClaims) 
        {
            var claims=new List<Claim>();

            //Extension metotlar ile claim nesnemizi oluşturduk
            claims.AddName(user.Name);
            claims.AddRoles(operationClaims.Select(i => i.Name).ToArray());

            return claims;
        }
    }
}
