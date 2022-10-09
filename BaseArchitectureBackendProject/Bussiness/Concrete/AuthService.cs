using Bussiness.Abstract;
using Bussiness.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Hashing;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
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
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }

        public string Login(LoginAuthDto loginDto)
        {
            var user = _userService.GetByEmail(loginDto.Email);
            var isVerifyPassword = HashingHelper.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt);

            if (isVerifyPassword)
                return "Giriş başarılı";
            return "Giriş Başarısız";
        }

        public IResult Register(RegisterAuthDto authDto)
        {
            UserValidator userValidator = new UserValidator();
            ValidationTool.Validate(userValidator,authDto);

            var isExistEmail = CheckIfEmailExists(authDto.Email);

            if (isExistEmail)
            {
                _userService.Add(authDto);
                return new SuccessResult("Kullanıcı başarıyla oluşturuldu");
            }
            else
                return new ErrorResult("Bu mail adresi daha önce kullanılmıştır");
        }

        bool CheckIfEmailExists(string email)
        {
            var list = _userService.GetByEmail(email);
            return list == null ? true : false;
        }
    }
}
