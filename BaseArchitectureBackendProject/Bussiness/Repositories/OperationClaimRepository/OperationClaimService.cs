using DataAccess.Repositories.OperationClaimRepository;
using Entities.Concrete;

namespace Bussiness.Repositories.OperationClaimRepository
{
    public class OperationClaimService : IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;

        public OperationClaimService(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }

        public void Add(OperationClaim claim)
        {
            _operationClaimDal.Add(claim);
        }
    }
}
