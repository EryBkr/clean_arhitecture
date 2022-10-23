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
        IDataResult<List<User>> GetList();
        User GetByEmail(string email);
        void Add(RegisterAuthDto register);
        IDataResult<User> GetById(int id);
        IResult Update(User user);
        IResult ChangePassword(UserChangePasswordDto userChangePasswordDto);
        IResult Delete(User user);
    }
}
