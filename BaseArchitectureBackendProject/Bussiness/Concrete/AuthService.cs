using Bussiness.Abstract;
using Bussiness.ValidationRules.FluentValidation;
using Core.Aspects.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Hashing;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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


        [ValidationAspect(typeof(UserValidator))]
        public IResult Register(RegisterAuthDto authDto, int imageSize)
        {
            imageSize = 2;

            //Bütün iş kurallarımı tek bir metot üzerinden yürüyorum
            IResult result = BusinessRules.Run(CheckIfEmailExists(authDto.Email), CheckIfImageSizeOneMBAbove(imageSize));

            if (result != null)
                return result;

            _userService.Add(authDto);
            return new SuccessResult("Kullanıcı başarıyla oluşturuldu");
        }


        private IResult CheckIfEmailExists(string email)
        {
            var list = _userService.GetByEmail(email);
            return list == null ? new SuccessResult() : new ErrorResult("Bu mail adresi daha önce kullanılmış");
        }

        private IResult CheckIfImageSizeOneMBAbove(int imageSize)
        {
            if (imageSize > 1)
                return new ErrorResult("Resim 1 Mb'den küçük olmalıdır");
            return new SuccessResult();
        }
    }
}
