using Bussiness.Repositories.UserRepository.Constans;
using Bussiness.Utilities.File;
using Core.Utilities.Hashing;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Results.Concrete.DataResults;
using DataAccess.Repositories.UserRepository;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Repositories.UserRepository
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IFileService _fileService;

        public UserService(IUserDal userDal, IFileService fileService)
        {
            _userDal = userDal;
            _fileService = fileService;
        }

        public async void Add(RegisterAuthDto register)
        {
            //Resim servisi aracılğıyla kayıt işlemini yapıyorum
            var fileName = await _fileService.FileSave("./Content/img/", register.Image);

            //Create User
            _userDal.Add(CreateUser(register, fileName));
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(Messages.DeletedUser);
        }

        public User GetByEmail(string email)
        {
            return _userDal.Get(i => i.Email == email);
        }

        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(i => i.Id == id));
        }

        public List<User> GetList()
        {
            return _userDal.GetAll();
        }

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Messages.UpdatedUser);
        }

        private User CreateUser(RegisterAuthDto registerEntity, string fileName)
        {
            byte[] passwordHash, passwordSalt;

            //Parola hash'leniyor
            //out keyword'ü tekrar atama işlemi yapmamıza gerek olmamasını sağlıyor
            HashingHelper.CreatePassword(registerEntity.Password, out passwordHash, out passwordSalt);

            User userEntity = new User()
            {
                Email = registerEntity.Email,
                Name = registerEntity.Name,
                ImageUrl = fileName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            return userEntity;
        }

        IDataResult<List<User>> IUserService.GetList()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }
    }
}
