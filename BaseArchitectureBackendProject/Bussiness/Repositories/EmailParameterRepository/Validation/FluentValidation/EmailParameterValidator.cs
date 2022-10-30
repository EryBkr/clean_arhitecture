using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Repositories.EmailParameterRepository.Validation.FluentValidation
{
    public class EmailParameterValidator:AbstractValidator<EmailParameters>
    {
        public EmailParameterValidator()
        {
            RuleFor(i => i.SMTP).NotEmpty().WithMessage("SMTP adresi boş olamaz");
            RuleFor(i => i.Email).EmailAddress().WithMessage("Email formata uygun değil");
            RuleFor(i => i.Port).NotEmpty().WithMessage("Port boş olamaz");
            RuleFor(i => i.Password).NotEmpty().WithMessage("Parola boş olamaz");
        }
    }
}
