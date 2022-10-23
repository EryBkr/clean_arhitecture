using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Repositories.UserRepository.Validation.FluentValidation
{
    public class UserValidator:AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("İsim alanı boş olamaz");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Mail alanı boş olamaz");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Mail gerekli formata uygun değil");
            RuleFor(p => p.ImageUrl).NotEmpty().NotNull().WithMessage("Resmi alanı boş olamaz");
        }
    }
}
