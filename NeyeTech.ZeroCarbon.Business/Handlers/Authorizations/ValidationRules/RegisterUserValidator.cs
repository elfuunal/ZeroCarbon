﻿using NeyeTech.ZeroCarbon.Business.Handlers.Authorizations.Commands;
using FluentValidation;

namespace NeyeTech.ZeroCarbon.Business.Handlers.Authorizations.ValidationRules
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(p => p.Model.Password).NotEmpty().WithMessage("Şifre boş olamaz.")
                    .MinimumLength(6).WithMessage("Şifreniz en az 6 karakterli olmalıdır.")
                    .MaximumLength(12).WithMessage("Şifreniz en fazla 12 karakter olmalıdır.")
                    .Matches(@"[A-Z]+").WithMessage("Şifreniz en az 1 adet büyük harf içermelidir.")
                    .Matches(@"[a-z]+").WithMessage("Şifreniz en az 1 adet küçük harf içermelidir.")
                    .Matches(@"[0-9]+").WithMessage("Şifreniz en az 1 adet rakam içermelidir.");

            RuleFor(m => m.Model.Username).NotNull().WithMessage("Kullanıcı adı boş olamaz");
            RuleFor(m => m.Model.Firstname).NotNull().WithMessage("İsim boş olamaz");
            RuleFor(m => m.Model.Lastname).NotNull().WithMessage("Soyadı boş olamaz");
        }
    }
}
