using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Bussiness.Repositories.OperationClaimRepository
{
    public interface IOperationClaimService
    {
        Task<IResult> AddAsync(OperationClaim claim);
        Task<IResult> UpdateAsync(OperationClaim claim);
        Task<IResult> DeleteAsync(OperationClaim claim);
        Task<IDataResult<List<OperationClaim>>> GetListAsync();
        Task<IDataResult<OperationClaim>> GetByIdAsync(int id);
    }
}
