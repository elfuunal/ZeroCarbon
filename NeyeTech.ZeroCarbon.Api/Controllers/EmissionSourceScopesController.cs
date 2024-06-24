using Microsoft.AspNetCore.Mvc;
using NeyeTech.ZeroCarbon.Business.Handlers.EmissionSourceScopes.Queries;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.Entities.DTOs.EmissionSourceScopes;

namespace NeyeTech.ZeroCarbon.Api.Controllers
{
    public class EmissionSourceScopesController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseMessage<List<EmissionSourceScopeDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseMessage<List<EmissionSourceScopeDto>>))]
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            return CreateActionResult(await Mediator.Send(new GetEmissionSourceScopesQuery()));
        }
    }
}
