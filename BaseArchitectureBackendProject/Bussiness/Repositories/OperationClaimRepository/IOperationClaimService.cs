using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Bussiness.Repositories.OperationClaimRepository
{
    public interface IOperationClaimService
    {
        IResult Add(OperationClaim claim);
        IResult Update(OperationClaim claim);
        IResult Delete(OperationClaim claim);
        IDataResult<List<OperationClaim>> GetList();
        IDataResult<OperationClaim> GetById(int id);
    }
}
