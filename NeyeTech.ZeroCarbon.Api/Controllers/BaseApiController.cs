using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//api denetleyicisi
namespace NeyeTech.ZeroCarbon.Api.Controllers
{
    /// <summary>
    /// Base controller
    /// </summary>
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseApiController : Controller
    {
        private IMediator _mediator;

        /// <summary>
        /// It is for getting the Mediator instance creation process from the base controller.
        /// </summary>
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [NonAction]  //eylem olmasını engeller, sınıf içerisinde yardımcı fonksiyon olmasını sağlar.
        public IActionResult CreateActionResult<T>(ResponseMessage<T> response)
        {
            if (response.StatusCode == 204)
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode
                };

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
