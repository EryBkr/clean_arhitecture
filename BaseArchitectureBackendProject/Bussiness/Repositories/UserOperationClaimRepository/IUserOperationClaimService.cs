using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Repositories.UserOperationClaimRepository
{
    public interface IUserOperationClaimService
    {
        Task<IResult> AddAsync(UserOperationClaim claim);
        Task<IResult> UpdateAsync(UserOperationClaim claim);
        Task<IResult> DeleteAsync(UserOperationClaim claim);
        Task<IDataResult<List<UserOperationClaim>>> GetListAsync();
        Task<IDataResult<UserOperationClaim>> GetByIdAsync(int id);
    }
}
