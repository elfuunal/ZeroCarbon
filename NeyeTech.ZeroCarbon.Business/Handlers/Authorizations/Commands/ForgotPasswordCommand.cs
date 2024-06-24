using AutoMapper;
using NeyeTech.ZeroCarbon.Core.Entities.Concrete;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.Core.Utilities.Security.Hashing;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using MediatR;
using NeyeTech.ZeroCarbon.Core.Extensions;
using Microsoft.AspNetCore.DataProtection;
using NeyeTech.ZeroCarbon.Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NeyeTech.ZeroCarbon.Core.Utilities.Settings;
using NeyeTech.ZeroCarbon.Business.Handlers.MailSenders.Commands;

namespace NeyeTech.ZeroCarbon.Business.Handlers.Authorizations.Commands
{
    public class ForgotPasswordCommand : IRequest<ResponseMessage<NoContent>>
    {
        public string ForgotPasswordCode { get; set; }
        public string UserId { get; set; }
        public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, ResponseMessage<NoContent>>
        {
            public async Task<ResponseMessage<NoContent>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
            {
                IUserRepository userRepo = ServiceTool.ServiceProvider.GetService<IUserRepository>();
                IMapper mapper = ServiceTool.ServiceProvider.GetService<IMapper>();
                ServiceTool.ServiceProvider.GetService<IMediator>();
                IDataProtector customProtector = ServiceTool.ServiceProvider.GetService<IDataProtectionProvider>().CreateProtector("r8CX8TSXEj3K");
                IOptions<MailSettings> mailOption = ServiceTool.ServiceProvider.GetService<IOptions<MailSettings>>();
                IMediator mediator = ServiceTool.ServiceProvider.GetService<IMediator>();

                long userId = Convert.ToInt64(customProtector.Unprotect(request.UserId));

                User user = await userRepo.GetAsync((User s) => s.Id == userId && s.ForgotPasswordCode == request.ForgotPasswordCode && s.ForgotPasswordCodeTime > DateTime.Now);

                if (user == null)
                    return ResponseMessage<NoContent>.Fail("Kullanıcı bulunamadı ya da linkin süresi doldu.");


                var password = GeneratorExtensions.GenerateRandomCode(6);

                HashingHelper.CreatePasswordHash(password, out var passwordSalt, out var passwordHash);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.ForgotPasswordTime = DateTime.Now;

                userRepo.Update(user);
                await userRepo.SaveChangesAsync();

                var emailTemplate = await File.ReadAllTextAsync("MailTemplates/ForgotPasswordTemplate.html", cancellationToken);

                emailTemplate = emailTemplate.Replace("#ADSOYAD", user.Firstname + " " + user.Lastname);
                emailTemplate = emailTemplate.Replace("#EMAIL", user.Email);
                emailTemplate = emailTemplate.Replace("#PASSWORD", password);

                var mailResult = await mediator.Send(new MailSenderCommand()
                {
                    AliciMail = user.Email,
                    Konu = "Zero Carbonizer - Giriş Bilgileriniz",
                    Icerik = emailTemplate
                }, cancellationToken);

                return ResponseMessage<NoContent>.Success("Şifreniz başarıyla değiştirildi. Sisteme giriş yapabilirsiniz.");
            }
        }
    }
}
