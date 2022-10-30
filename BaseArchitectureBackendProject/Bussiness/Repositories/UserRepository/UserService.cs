using Bussiness.Repositories.UserRepository.Constans;
using Bussiness.Repositories.UserRepository.Validation.FluentValidation;
using Bussiness.Utilities.File;
using Core.Aspects.Caching;
using Core.Aspects.Transaction;
using Core.Aspects.Validation;
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

        //Silme işlemi cache'in eklendiği key'e ait olmalı (ya da key'in bir bölümünü içermeli)
        [RemoveCacheAspect("IUserService.GetList")]
        public async Task AddAsync(RegisterAuthDto register)
        {
            //Resim servisi aracılğıyla kayıt işlemini yapıyorum
            var fileName = await _fileService.FileSaveAsync("./Content/img/", register.Image);

            //Create User
            await _userDal.AddAsync(CreateUser(register, fileName));
        }

        [ValidationAspect(typeof(UserChangePasswordValidator))]
        public async Task<IResult> ChangePasswordAsync(UserChangePasswordDto userChangePasswordDto)
        {
            var user =await _userDal.GetAsync(p => p.Id == userChangePasswordDto.UserId);
            bool result = HashingHelper.VerifyPasswordHash(userChangePasswordDto.OldPassword, user.PasswordHash, user.PasswordSalt);

            if (!result)
                return new ErrorResult(UserMessages.WrongCurrentPassword);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePassword(userChangePasswordDto.NewPassword, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _userDal.UpdateAsync(user);

            return new SuccessResult(UserMessages.ChangedPassword);
        }

        public async Task<IResult> DeleteAsync(User user)
        {
            await _userDal.DeleteAsync(user);
            return new SuccessResult(UserMessages.DeletedUser);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _userDal.GetAsync(i => i.Email == email);
        }

        public async Task<IDataResult<User>> GetByIdAsync(int id)
        {
            return new SuccessDataResult<User>(await _userDal.GetAsync(i => i.Id == id));
        }

        //public List<User> GetList()
        //{
        //    return _userDal.GetAll();
        //}

        public List<OperationClaim> GetUserOperationClaims(int userId)
        {
            return _userDal.UserOperationClaimByUserId(userId);
        }

        [ValidationAspect(typeof(UserValidator))]
        [TransactionAspect]
        public async Task<IResult> UpdateAsync(User user)
        {
            await _userDal.UpdateAsync(user);
            return new SuccessResult(UserMessages.UpdatedUser);
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

        //Cache sayesinde 60 dk boyunca key'e ait değeri saklamış olacağız
        [CacheAspect(60)]
        public async Task<IDataResult<List<User>>> GetListAsync()
        {
            return new SuccessDataResult<List<User>>(await _userDal.GetAllAsync());
        }
    }
}
