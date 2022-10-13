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
        List<User> GetList();
        User GetByEmail(string email);
        void Add(RegisterAuthDto register);
    }
}
