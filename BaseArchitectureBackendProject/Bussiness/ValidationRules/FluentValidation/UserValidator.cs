using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<RegisterAuthDto>
    {
        public UserValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("İsim alanı boş olamaz");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Mail alanı boş olamaz");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Mail gerekli formata uygun değil");
            RuleFor(p => p.ImageUrl).NotEmpty().WithMessage("Resim alanı boş olamaz");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Şifre alanı boş olamaz");
            RuleFor(p => p.Password).MinimumLength(6).WithMessage("Şifre 6 karakter olmalıdır");
        }
    }
}
