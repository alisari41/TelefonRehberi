using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class TelephoneDirectoryValidator : AbstractValidator<TelephoneDirectories>
    {
        public TelephoneDirectoryValidator()
        {
            RuleFor(p => p.AddressId).GreaterThan(0).WithMessage(Messages.AddressIdNotNull);

            RuleFor(p => p.FirstName).NotEmpty().WithMessage(Messages.FirstNameNotNull);
            RuleFor(p => p.FirstName).Length(3, 50).WithMessage(Messages.KarakterUzunlugu50);

            RuleFor(p => p.LastName).NotEmpty().WithMessage(Messages.LastNameNotNull);
            RuleFor(p => p.LastName).Length(3, 50).WithMessage(Messages.KarakterUzunlugu50);

            RuleFor(p => p.Title).NotEmpty().WithMessage(Messages.TitleNotNull);
            RuleFor(p => p.Title).Length(3, 50).WithMessage(Messages.KarakterUzunlugu50);

            RuleFor(p => p.Email).NotEmpty().WithMessage(Messages.EmailNotNull);
            RuleFor(p => p.Email).EmailAddress().WithMessage(Messages.EmailNot).When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(p => p.PhotoUrl).NotEmpty().WithMessage(Messages.PhotoNotNull);

            RuleFor(p => p.PhoneNumber).NotEmpty().WithMessage(Messages.PhoneNumberNotNull);
            RuleFor(p => p.PhoneNumber).Length(16,16).WithMessage(Messages.PhoneNumberFormat);
            RuleFor(p => p.PhoneNumber).Must(IsPhoneValid).WithMessage(Messages.PhoneNumberFormat);


            //RuleFor(p => p.Fax).NotEmpty().WithMessage(Messages.FaxNotNull);
            //RuleFor(p => p.Fax).Must(IsFaxValid).WithMessage(Messages.FaxFormat);

            //RuleFor(p => p.InternalNumber).NotEmpty().WithMessage(Messages.InternalNumberNotNull);
            RuleFor(p => p.InternalNumber).Length(1, 4).WithMessage(Messages.InternalNumberUzunluğu);



        }

        private bool IsPhoneValid(string arg)
        {
            Regex regex = new Regex(@"((\+90)|0)[.\- ]?[0-9][.\- ]?[0-9][.\- ]?[0-9]");

            //  +90 -345 -123 -4567
            //  +90 333 123 4567
            //  +90 300 123 4567
            //  +90 321 123 -4567
            //  +90 345 -540 -5883


            return regex.IsMatch(arg);
        }
        private bool IsFaxValid(string arg)
        {
            Regex regex = new Regex(@"\(?\d+\)?[-.\s]?\d+[-.\s]?\d+");

            // (555) 444 - 6789

            // 555 - 444 - 6789

            // 555.444.6789

            // 555 444 6789


            return regex.IsMatch(arg);
        }
    }
}
