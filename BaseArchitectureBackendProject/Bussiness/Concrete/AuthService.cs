using Bussiness.Abstract;
using Bussiness.ValidationRules.FluentValidation;
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

        public List<string> Register(RegisterAuthDto authDto)
        {
            //Validator'dan gelen response'u handle ettik
            UserValidator userValidator = new UserValidator();
            var validatorResult = userValidator.Validate(authDto);

            if (validatorResult.IsValid)
            {
                _userService.Add(authDto);
                return new List<string> { "Kullanıcı başarıyla oluşturuldu" };
            }

            return validatorResult.Errors.Select(i=>i.ErrorMessage).ToList();
        }
    }
}
