using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Constants;
using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserOperationClaimValidator : AbstractValidator<UserOperationClaim>
    {
        public UserOperationClaimValidator()
        {
            RuleFor(p => p.OperationClaimId).NotEmpty().WithMessage(Messages.OperationClaimIdNotNull); //Kural. Name boş olamaz ve  P yi yukardan gelen 
            RuleFor(p => p.UserId).NotEmpty().WithMessage(Messages.UserIddNotNull); //Kural. Name boş olamaz ve  P yi yukardan gelen 
        }
    }
}
