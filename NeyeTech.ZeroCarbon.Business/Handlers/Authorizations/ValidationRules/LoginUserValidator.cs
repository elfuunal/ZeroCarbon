using NeyeTech.ZeroCarbon.Business.Handlers.Authorizations.Queries;
using FluentValidation;

namespace NeyeTech.ZeroCarbon.Business.Handlers.Authorizations.ValidationRules
{
    public class LoginUserValidator : AbstractValidator<LoginUserQuery>
    {
        public LoginUserValidator()
        {
            RuleFor(m => m.LoginModel.Password).NotNull().WithMessage("Şifre boş olamaz");
            RuleFor(m => m.LoginModel.Email).NotNull().WithMessage("Email adresi boş olamaz");
        }
    }
}
