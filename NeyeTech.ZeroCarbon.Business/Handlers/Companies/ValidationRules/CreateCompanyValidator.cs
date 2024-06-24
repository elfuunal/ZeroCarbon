using NeyeTech.ZeroCarbon.Business.Handlers.Companies.Commands;
using FluentValidation;

namespace NeyeTech.ZeroCarbon.Business.Handlers.Companies.ValidationRules
{
    public class CreateCompanyValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyValidator()
        {
            RuleFor(m => m.Model.Title).NotEmpty().WithMessage("{PropertyName} boş olamaz.");
        }
    }
}
