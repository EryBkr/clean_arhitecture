using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Repositories.UserRepository
{
    public interface IUserService
    {
        Task<IDataResult<List<User>>> GetListAsync();
        Task<User> GetByEmailAsync(string email);
        Task AddAsync(RegisterAuthDto register);
        Task<IDataResult<User>> GetByIdAsync(int id);
        Task<IResult> UpdateAsync(User user);
        Task<IResult> ChangePasswordAsync(UserChangePasswordDto userChangePasswordDto);
        Task<IResult> DeleteAsync(User user);
        List<OperationClaim> GetUserOperationClaims(int userId);
    }
}
