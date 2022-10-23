using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Repositories.UserRepository.Validation.FluentValidation
{
    public class UserChangePasswordValidator:AbstractValidator<UserChangePasswordDto>
    {
        public UserChangePasswordValidator()
        {
            RuleFor(p => p.NewPassword).NotEmpty().WithMessage("Yeni Şifre boş olamaz");
            RuleFor(p => p.OldPassword).NotEmpty().WithMessage("Eski Şifre boş olamaz");
            RuleFor(p => p.UserId).NotEmpty().WithMessage("Lütfen geçerli bir kullanıcı seçiniz");
        }
    }
}
