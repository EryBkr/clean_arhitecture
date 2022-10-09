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

        public string Register(RegisterAuthDto authDto)
        {
            if (authDto.Name == "")
                return "Kullanıcı adı boş olamaz";

            if (authDto.Email == "")
                return "Email boş olamaz";

            if (authDto.Password == "")
                return "Parola boş olamaz";

            if (authDto.ImageUrl == "")
                return "Resim boş olamaz";

            if (authDto.Password.Length < 6)
                return "Şifre en az 6 karakter olmalıdır";

            _userService.Add(authDto);
            return "Kullanıcı kaydı başarıyla tamamlandı";
        }
    }
}
