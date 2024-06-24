using NeyeTech.ZeroCarbon.Business.Handlers.Authorizations.Commands;
using NeyeTech.ZeroCarbon.Business.Handlers.Authorizations.Queries;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.Core.Utilities.Security.Jwt;
using NeyeTech.ZeroCarbon.Entities.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NeyeTech.ZeroCarbon.Api.Controllers
{
    //kullanıcı kimlik doğrulama için işlev ---> dto 
    public class AuthController : BaseApiController
    {
        /// <summary>
        /// Make it User Login operations
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseMessage<UserDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            return CreateActionResult(await Mediator.Send(new LoginUserQuery() { LoginModel = loginUserDto }));
        }

        /// <summary>
        ///  Make it User Register operations
        /// </summary>
        /// <param name="createUser"></param>r
        /// <returns></returns>
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseMessage<CreateUserDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseMessage<CreateUserDto>))]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateUserDto createUserDto)
        {
            return CreateActionResult(await Mediator.Send(new RegisterUserCommand() { Model = createUserDto }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forgotPasswordDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseMessage<ForgotPasswordDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseMessage<ForgotPasswordDto>))]
        [HttpPost]
        public async Task<IActionResult> ForgotPasswordRequest([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            return CreateActionResult(await Mediator.Send(new ForgotPasswordRequestCommand() { Model = forgotPasswordDto }));
        }
    }
}
