using AutoMapper;
using NeyeTech.ZeroCarbon.Business.Handlers.Authorizations.ValidationRules;
using NeyeTech.ZeroCarbon.Core.Aspects.Autofac.Validation;
using NeyeTech.ZeroCarbon.Core.Entities.Concrete;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.Core.Utilities.Security.Hashing;
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
    public class RegisterUserCommand : IRequest<ResponseMessage<CreateUserDto>>
    {
        public CreateUserDto Model { get; set; }
        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ResponseMessage<CreateUserDto>>
        {
            [ValidationAspect(typeof(RegisterUserValidator), Priority = 1)]
            public async Task<ResponseMessage<CreateUserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                IMediator mediator = ServiceTool.ServiceProvider.GetService<IMediator>();
                IUserRepository userRepository = ServiceTool.ServiceProvider.GetService<IUserRepository>();
                IMapper mapper = ServiceTool.ServiceProvider.GetService<IMapper>();
                IOptions<MailSettings> mailOption = ServiceTool.ServiceProvider.GetService<IOptions<MailSettings>>();
                IDataProtectionProvider protector = ServiceTool.ServiceProvider.GetService<IDataProtectionProvider>();
                IDataProtector customProtector = protector.CreateProtector("r8CX8TSXEj3K");

                var isThereAnyUser = await userRepository.GetAsync(u => u.Username == request.Model.Username || u.Email == request.Model.Email);
                if (isThereAnyUser != null)
                    return ResponseMessage<CreateUserDto>.Fail(StatusCodes.Status400BadRequest, "Bu kullanıcı adı zaten eklenmiş.");

                HashingHelper.CreatePasswordHash(request.Model.Password, out var passwordSalt, out var passwordHash);

                string emailCode = GeneratorExtensions.GenerateRandomCode(10);

                var user = new User
                {
                    Username = request.Model.Username,
                    Email = request.Model.Email,
                    Firstname = request.Model.Firstname,
                    Lastname = request.Model.Lastname,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Status = false,
                    LoginType = Core.Enums.LoginType.System,
                    ActivationCode = emailCode,
                    ActivationCodeTime = DateTime.Now.AddDays(1),
                    IsEmailActive = false
                };

                userRepository.Add(user);

                var result = mapper.Map<CreateUserDto>(user);

                await userRepository.SaveChangesAsync();

                string encryptedId = customProtector.Protect(user.Id.ToString());

                var emailTemplate = await File.ReadAllTextAsync("MailTemplates/ActivationTemplate.html", cancellationToken);
                string url = mailOption.Value.EmailActivationLink + $"?userId={encryptedId}&emailCode={emailCode}";
                string link = $"<a href='{url}'>Aktifleştir</a>";

                emailTemplate = emailTemplate.Replace("#ADSOYAD", user.Firstname + " " + user.Lastname);
                emailTemplate = emailTemplate.Replace("#LINK", link);
                emailTemplate = emailTemplate.Replace("#TARIH", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                emailTemplate = emailTemplate.Replace("#MAIL", user.Email);
                emailTemplate = emailTemplate.Replace("#USERNAME", user.Username);
                
                var mailResult = await mediator.Send(new MailSenderCommand()
                {
                    AliciMail = user.Email,
                    Konu = "Zero Carbonizer - Üyelik Aktivasyonu",
                    Icerik = emailTemplate
                }, cancellationToken);

                return ResponseMessage<CreateUserDto>.Success(result);
            }
        }
    }
}
