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
    public class OperationClaimValidator : AbstractValidator<OperationClaim>
    {//OperationClaim Entity'sini doğrulayacak ... AbstractValidator için FluentValidation kütüphanesi indirmek gerekir.

        public OperationClaimValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage(Messages.OperationClaimNotNull); //Kural. Name boş olamaz ve  P yi yukardan gelen <OperationClaim> nesnelerinden alır
        }
    }
}
