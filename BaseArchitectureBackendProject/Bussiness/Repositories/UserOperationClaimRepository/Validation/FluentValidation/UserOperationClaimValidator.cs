using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Repositories.UserOperationClaimRepository.Validation.FluentValidation
{
    public class UserOperationClaimValidator : AbstractValidator<UserOperationClaim>
    {
        public UserOperationClaimValidator()
        {
            RuleFor(i => i.UserId).Must(IsIdValid).WithMessage("Yetki ataması için kullanıcı seçimi yapmalısınız");
            RuleFor(i => i.OperationClaimId).Must(IsIdValid).WithMessage("Yetki ataması için yetki seçimi yapmalısınız");
        }

        private bool IsIdValid(int id)
        {
            if (id <= 0)
                return false;

            return true;
        }
    }
}
