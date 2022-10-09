using Bussiness.Abstract;
using Core.Utilities.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;

        public UserService(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void Add(RegisterAuthDto register)
        {
            byte[] passwordHash, passwordSalt;

            //Parola hash'leniyor
            //out keyword'ü tekrar atama işlemi yapmamıza gerek olmamasını sağlıyor
            HashingHelper.CreatePassword(register.Password, out passwordHash, out passwordSalt);

            User userEntity = new User()
            {
                Email = register.Email,
                Name = register.Name,
                ImageUrl = register.ImageUrl,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            _userDal.Add(userEntity);
        }

        public User GetByEmail(string email)
        {
            return _userDal.Get(i => i.Email == email);
        }

        public List<User> GetList()
        {
            return _userDal.GetAll();
        }
    }
}
