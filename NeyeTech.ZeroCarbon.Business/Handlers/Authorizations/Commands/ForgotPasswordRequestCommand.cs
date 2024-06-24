using AutoMapper;
using NeyeTech.ZeroCarbon.Business.Handlers.Authorizations.ValidationRules;
using NeyeTech.ZeroCarbon.Core.Aspects.Autofac.Validation;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using NeyeTech.ZeroCarbon.Entities.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using NeyeTech.ZeroCarbon.Core.Extensions;
using NeyeTech.ZeroCarbon.Business.Handlers.MailSenders.Commands;
using NeyeTech.ZeroCarbon.Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NeyeTech.ZeroCarbon.Core.Utilities.Settings;
using Microsoft.AspNetCore.DataProtection;

namespace NeyeTech.ZeroCarbon.Business.Handlers.Authorizations.Commands
{
    public class ForgotPasswordRequestCommand : IRequest<ResponseMessage<CreateUserDto>>
    {
        public ForgotPasswordDto Model { get; set; }
        public class ForgotPasswordRequestCommandHandler : IRequestHandler<ForgotPasswordRequestCommand, ResponseMessage<CreateUserDto>>
        {
            [ValidationAspect(typeof(RegisterUserValidator), Priority = 1)]
            public async Task<ResponseMessage<CreateUserDto>> Handle(ForgotPasswordRequestCommand request, CancellationToken cancellationToken)
            {
                IMediator mediator = ServiceTool.ServiceProvider.GetService<IMediator>();
                IUserRepository userRepository = ServiceTool.ServiceProvider.GetService<IUserRepository>();
                IMapper mapper = ServiceTool.ServiceProvider.GetService<IMapper>();
                IHttpContextAccessor context = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
                IOptions<MailSettings> mailOption = ServiceTool.ServiceProvider.GetService<IOptions<MailSettings>>();
                IDataProtectionProvider protector = ServiceTool.ServiceProvider.GetService<IDataProtectionProvider>();
                IDataProtector customProtector = protector.CreateProtector("r8CX8TSXEj3K");

                var user = await userRepository.GetAsync(u => u.Email == request.Model.Email);
                if (user == null)
                    return ResponseMessage<CreateUserDto>.Fail(StatusCodes.Status200OK, "Mail adresiniz sistemimizde kayıtlı ise şifreniz oluşturularak gönderilmiştir.");

                if (user.LoginType != Core.Enums.LoginType.System)
                    return ResponseMessage<CreateUserDto>.Fail(StatusCodes.Status200OK, "Sosyal medya hesaplarıyla login olunan hesaplarda 'Şifre Değiştirme' işlemi kullanılamaz.");

                if (user.ForgotPasswordCodeTime.HasValue && user.ForgotPasswordCodeTime > DateTime.Now)
                    return ResponseMessage<CreateUserDto>.Fail(StatusCodes.Status200OK, $"Halen aktif bir şifre değiştirme talebiniz bulunmaktadır. {user.ForgotPasswordCodeTime.Value.ToString("dd/MM/yyyy HH:mm:ss")} tarihinde tekrar deneyebilirsiniz.");

                string forgotPasswordCode = GeneratorExtensions.GenerateRandomCode(10);

                user.ForgotPasswordCode = forgotPasswordCode;
                user.ForgotPasswordCodeTime = DateTime.Now.AddMinutes(30);

                userRepository.Update(user);
                await userRepository.SaveChangesAsync();

                string encryptedId = customProtector.Protect(user.Id.ToString());

                var emailTemplate = await File.ReadAllTextAsync("MailTemplates/ForgotPasswordRequestTemplate.html", cancellationToken);
                string url = mailOption.Value.ForgotPasswordLink + $"?userId={encryptedId}&forgotPasswordCode={forgotPasswordCode}";
                string link = $"<a href='{url}'>Şifremi Değiştir</a>";

                emailTemplate = emailTemplate.Replace("#ADSOYAD", user.Firstname + " " + user.Lastname);
                emailTemplate = emailTemplate.Replace("#USERNAME", user.Username);
                emailTemplate = emailTemplate.Replace("#IPADRESS", context.HttpContext.Connection.RemoteIpAddress.ToString());
                emailTemplate = emailTemplate.Replace("#LINK", link);
                emailTemplate = emailTemplate.Replace("#TARIH", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                var mailResult = await mediator.Send(new MailSenderCommand()
                {
                    AliciMail = user.Email,
                    Konu = "Zero Carbonizer - Şifre Değiştirme Talebi",
                    Icerik = emailTemplate
                }, cancellationToken);

                return ResponseMessage<CreateUserDto>.Success();
            }
        }
    }
}
