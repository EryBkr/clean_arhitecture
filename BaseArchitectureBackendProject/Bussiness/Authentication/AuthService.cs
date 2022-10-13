using Bussiness.Repositories.UserRepository;
using Bussiness.ValidationRules.FluentValidation;
using Core.Aspects.Validation;
using Core.Utilities.Business;
using Core.Utilities.Hashing;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;

namespace Bussiness.Authentication
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
        public IResult Register(RegisterAuthDto authDto)
        {

            //Bütün iş kurallarımı tek bir metot üzerinden yürüyorum
            IResult result = BusinessRules.Run
                   (
                    CheckIfEmailExists(authDto.Email),
                    CheckIfImageSizeOneMBAbove(authDto.Image.Length),
                    CheckIfImageExtensionsAllow(authDto.Image)
                   );

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

        private IResult CheckIfImageSizeOneMBAbove(long imageSize)
        {
            decimal imageMbSize = Convert.ToDecimal(imageSize * 0.000001);
            if (imageMbSize > 1)
                return new ErrorResult("Resim 1 Mb'den küçük olmalıdır");
            return new SuccessResult();
        }

        private IResult CheckIfImageExtensionsAllow(IFormFile file)
        {
            string fileName = file.FileName;
            var fileExtension = Path.GetExtension(fileName).ToLower();

            var allowFileExtensions = new List<string> { ".jpg", ".jpeg", ".gif", ".png" };
            if (!allowFileExtensions.Contains(fileExtension))
                return new ErrorResult("Resim \".jpg\", \".jpeg\", \".gif\", \".png\" türlerinden birisi olmalıdır");
            return new SuccessResult();
        }
    }
}
