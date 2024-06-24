using MediatR;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using System.Net.Mail;
using System.Net;
using NeyeTech.ZeroCarbon.Core.Utilities.Settings;
using Microsoft.Extensions.Options;

namespace NeyeTech.ZeroCarbon.Business.Handlers.MailSenders.Commands
{
    public class MailSenderCommand : IRequest<ResponseMessage<bool>>
    {
        public string AliciMail { get; set; }
        public string Konu { get; set; }
        public string Icerik { get; set; }

        public class MailSenderCommandHandler : IRequestHandler<MailSenderCommand, ResponseMessage<bool>>
        {
            MailSettings _mailSettings;

            public MailSenderCommandHandler(IOptions<MailSettings> options)
            {
                _mailSettings = options.Value;
            }

            public async Task<ResponseMessage<bool>> Handle(MailSenderCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    SmtpClient sc = new SmtpClient();
                    sc.Port = _mailSettings.Port;
                    sc.Host = _mailSettings.Host;
                    sc.EnableSsl = true;
                    sc.UseDefaultCredentials = false;

                    sc.Credentials = new NetworkCredential(_mailSettings.Username, _mailSettings.Password);

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(_mailSettings.Username, "Zero Carbonizer");
                    mail.IsBodyHtml = true;
                    mail.Subject = request.Konu;
                    mail.Body = request.Icerik;
                    mail.To.Add(request.AliciMail);

                    await sc.SendMailAsync(mail, cancellationToken);
                }
                catch (Exception ex)
                {
                    throw;
                }

                return ResponseMessage<bool>.Success();
            }
        }
    }
}
