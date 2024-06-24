using NeyeTech.ZeroCarbon.Business.Handlers.CompanyDatas.Commands;
using NeyeTech.ZeroCarbon.Business.Handlers.CompanyDatas.Queries;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.Entities.DTOs.Companies;
using NeyeTech.ZeroCarbon.Entities.DTOs.CompanyData;
using Microsoft.AspNetCore.Mvc;

namespace NeyeTech.ZeroCarbon.Api.Controllers
{
    public class CompanyDataController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adres"></param>
        /// <param name="ay"></param>
        /// <param name="yil"></param>
        /// <param name="kapsam"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseMessage<List<CompanyDataDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseMessage<List<CompanyDataDto>>))]
        [HttpGet]
        public async Task<IActionResult> GetCompanyDataListAsync(long companyId)
        {
            var model = new CompanyDataDto()
            {
                CompanyId = companyId
            };

            return CreateActionResult(await Mediator.Send(new GetCompanyDataListQuery() { Model = model }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adres"></param>
        /// <param name="ay"></param>
        /// <param name="yil"></param>
        /// <param name="kapsam"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseMessage<List<CompanyDataDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseMessage<List<CompanyDataDto>>))]
        [HttpGet]
        public async Task<IActionResult> GetCalculatedCompanyDataAsync(long companyId)
        {
            var model = new CompanyDataDto()
            {
                CompanyId = companyId
            };

            return CreateActionResult(await Mediator.Send(new GetChosenCompanyDataQuery() { Model = model }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseMessage<List<CompanyDataDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseMessage<List<CompanyDataDto>>))]
        [HttpPost]
        public async Task<IActionResult> CreateCompanyDataAsync([FromBody] CompanyDataDto model)
        {
            return CreateActionResult(await Mediator.Send(new CreateCompanyDataCommand() { Model = model }));
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
        public async Task<IActionResult> DeleteCompanyDataAsync(long id)
        {
            return CreateActionResult(await Mediator.Send(new DeleteCompanyDataCommand() { Id = id }));
        }
    }
}
