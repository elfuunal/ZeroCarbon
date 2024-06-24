using AutoMapper;
using NeyeTech.ZeroCarbon.Core.Entities.Concrete;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using NeyeTech.ZeroCarbon.Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace NeyeTech.ZeroCarbon.Business.Handlers.Authorizations.Commands
{
    public class ActivateUserCommand : IRequest<ResponseMessage<NoContent>>
    {
        public string EmailCode { get; set; }
        public string UserId { get; set; }
        public class ActivateUserCommandHandler : IRequestHandler<ActivateUserCommand, ResponseMessage<NoContent>>
        {
            public async Task<ResponseMessage<NoContent>> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
            {
                IUserRepository userRepo = ServiceTool.ServiceProvider.GetService<IUserRepository>();
                IMapper mapper = ServiceTool.ServiceProvider.GetService<IMapper>();
                ServiceTool.ServiceProvider.GetService<IMediator>();
                IDataProtector customProtector = ServiceTool.ServiceProvider.GetService<IDataProtectionProvider>().CreateProtector("r8CX8TSXEj3K");

                long userId = Convert.ToInt64(customProtector.Unprotect(request.UserId));

                User user = await userRepo.GetAsync((User s) => s.Id == userId && s.ActivationCode == request.EmailCode && s.ActivationCodeTime > DateTime.Now);

                if (user == null)
                    return ResponseMessage<NoContent>.Fail("Kullanıcı bulunamadı ya da aktivasyon kodunun süresi doldu.");

                user.IsEmailActive = true;
                user.Status = true;
                user.ActivationTime = DateTime.Now;

                userRepo.Update(user);
                await userRepo.SaveChangesAsync();

                return ResponseMessage<NoContent>.Success("Kullanıcı başarıyla aktif hale getirildi. Sisteme giriş yapabilirsiniz.");
            }
        }
    }
}
