using Core.DataAccess.EntityFramework;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.UserRepository
{
    public class EfUserDal : EfEntityRepositoryBase<User, BaseArchContext>, IUserDal
    {
        //Kullanıcıya ait operation claim leri aldık
        //Navigation property kullanmadığımız için bu şekilde handle ettik
        public List<OperationClaim> UserOperationClaimByUserId(int userId)
        {
            using(var context = new BaseArchContext())
            {
                var data = from userOperationClaim in context.UserOperationClaims.Where(claim => claim.UserId == userId)
                           join operationClaim in context.OperationClaims on userOperationClaim.OperationClaimId equals operationClaim.Id
                           select new OperationClaim
                           {
                               Id = operationClaim.Id,
                               Name = operationClaim.Name
                           };
                return data.OrderBy(i => i.Name).ToList();
            }
        }
    }
}
