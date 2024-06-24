using NeyeTech.ZeroCarbon.Business.Handlers.Authorizations.Commands;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NeyeTech.ZeroCarbon.Api.Controllers
{
    //e posta doğrulaması ve şifre değişikliği için 
    public class EmailController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="emailCode"></param> //kullanıcı aktivasyonu
        /// <returns></returns>
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseMessage<AccessToken>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [HttpGet]
        public async Task<IActionResult> Activate(string userId,string emailCode)
        {
            return CreateActionResult(await Mediator.Send(new ActivateUserCommand() 
            { 
                EmailCode = emailCode,
                UserId = userId
            }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="emailCode"></param> //şifre değişikliği
        /// <returns></returns>
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseMessage<AccessToken>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [HttpGet]
        public async Task<IActionResult> ChangeUserPassword(string userId, string forgotPasswordCode)
        {
            return CreateActionResult(await Mediator.Send(new ForgotPasswordCommand()
            {
                ForgotPasswordCode = forgotPasswordCode,
                UserId = userId
            }));
        }
    }
}