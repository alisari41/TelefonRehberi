using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(p => p.Mahalle).NotEmpty().WithMessage(Messages.MahalleNotNull);
            RuleFor(p => p.Mahalle).Length(1,50).WithMessage(Messages.KarakterUzunlugu50);

            RuleFor(p => p.Sokak).NotEmpty().WithMessage(Messages.SokakNotNull);
            RuleFor(p => p.Sokak).Length(1,50).WithMessage(Messages.KarakterUzunlugu50);

            RuleFor(p => p.BinaNo).NotEmpty().WithMessage(Messages.BinaNoNotNull);
            RuleFor(p => p.BinaNo).Length(1,10).WithMessage(Messages.KarakterUzunlugu10);

            RuleFor(p => p.Kat).NotEmpty().WithMessage(Messages.KatNotNull);
            RuleFor(p => p.Kat).GreaterThan(0).WithMessage(Messages.SifirdanBuyukOlmalidir);
            RuleFor(p => p.Kat).LessThan(75).WithMessage(Messages.EnFazla75KatGirebilirsiniz);

            RuleFor(p => p.İlce).NotEmpty().WithMessage(Messages.IlceNotNull);
            RuleFor(p => p.İlce).Length(1,50).WithMessage(Messages.KarakterUzunlugu50);

            RuleFor(p => p.İl).NotEmpty().WithMessage(Messages.IlNotNull);
            RuleFor(p => p.İl).Length(1,50).WithMessage(Messages.KarakterUzunlugu50);

            RuleFor(p => p.PostaKodu).NotEmpty().WithMessage(Messages.PostaKoduNotNull);
            RuleFor(p => p.Kat).GreaterThan(0).WithMessage(Messages.SifirdanBuyukOlmalidir);





        }
    }
}
