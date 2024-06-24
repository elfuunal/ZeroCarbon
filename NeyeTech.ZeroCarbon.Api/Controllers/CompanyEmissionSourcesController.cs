using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using Microsoft.AspNetCore.Mvc;
using NeyeTech.ZeroCarbon.Entities.DTOs.CompanyEmissionSources;
using CarbonCalculator.Business.Handlers.CompanyEmissionSources.Queries;
using NeyeTech.ZeroCarbon.Business.Handlers.CompanyEmissionSources.Commands;

namespace NeyeTech.ZeroCarbon.Api.Controllers
{
    public class CompanyEmissionSourcesController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseMessage<List<CompanyEmissionSourceDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseMessage<List<CompanyEmissionSourceDto>>))]
        [HttpGet]
        public async Task<IActionResult> GetListAsync(long companyId)
        {
            return CreateActionResult(await Mediator.Send(new GetCompanyEmissionSourcesQuery() 
            { 
                CompanyId = companyId 
            }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseMessage<AddCompanyEmissionSourceDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseMessage<AddCompanyEmissionSourceDto>))]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddCompanyEmissionSourceDto model)
        {
            return CreateActionResult(await Mediator.Send(new CreateCompanyEmissionSourceCommand()
            { Model = model }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseMessage<NoContent>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseMessage<NoContent>))]
        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            return CreateActionResult(await Mediator.Send(new DeleteCompanyEmissionSourceCommand()
            { Id = id }));
        }
    }
}
