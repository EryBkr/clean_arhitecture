using Bussiness.Repositories.UserRepository;
using Bussiness.ValidationRules.FluentValidation;
using Core.Aspects.Validation;
using Core.Utilities.Business;
using Core.Utilities.Hashing;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Results.Concrete.DataResults;
using Core.Utilities.Security.Jwt;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;

namespace Bussiness.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHandler _tokenHandler;

        public AuthService(IUserService userService, ITokenHandler tokenHandler)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
        }

        public IDataResult<Token> Login(LoginAuthDto loginDto)
        {
            var user = _userService.GetByEmail(loginDto.Email);
            var isVerifyPassword = HashingHelper.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt);

            //User Id kullanarak kullanıcıya ait operationClaim leri aldım
            List<OperationClaim> operationClaims = _userService.GetUserOperationClaims(user.Id);

            if (isVerifyPassword)
            {
                Token token = new Token();
                token = _tokenHandler.CreateToken(user, operationClaims);
                return new SuccessDataResult<Token>(token);
            }
            return new ErrorDataResult<Token>("Kullanıcı mail ya da şifre bilgisi yanlış");
        }


        [ValidationAspect(typeof(AuthValidator))]
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
