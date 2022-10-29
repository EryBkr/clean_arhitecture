using Entities.Concrete;

namespace Core.Utilities.Security.Jwt
{
    public interface ITokenHandler
    {
        Token CreateToken(User user,List<OperationClaim> operationClaims);
    }
}
