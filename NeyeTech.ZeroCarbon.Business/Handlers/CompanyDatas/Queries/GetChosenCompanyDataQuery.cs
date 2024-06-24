using NeyeTech.ZeroCarbon.Business.Helpers;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using NeyeTech.ZeroCarbon.Entities.DTOs.Companies;

using MediatR;
using NeyeTech.ZeroCarbon.Entities.DTOs.CompanyData;

namespace NeyeTech.ZeroCarbon.Business.Handlers.CompanyDatas.Queries
{
    public class GetChosenCompanyDataQuery : IRequest<ResponseMessage<CompanyEmissionDataDto>>
    {
        public CompanyDataDto Model { get; set; }
        public class GetChosenCompanyDataQueryHandler : IRequestHandler<GetChosenCompanyDataQuery, ResponseMessage<CompanyEmissionDataDto>>
        {
            private readonly ICompanyDataRepository _companyDataRepository;
            private readonly IEmissionSourceRepository _emissionSourceRepository;

            public GetChosenCompanyDataQueryHandler(ICompanyDataRepository companyDataRepository,
                IEmissionSourceRepository emissionSourceRepository)
            {
                _companyDataRepository = companyDataRepository;
                _emissionSourceRepository = emissionSourceRepository;
            }

            public async Task<ResponseMessage<CompanyEmissionDataDto>> Handle(GetChosenCompanyDataQuery request, CancellationToken cancellationToken)
            {
                CompanyEmissionDataDto returnModel = new CompanyEmissionDataDto();

                var companyDatas = await _companyDataRepository.GetCompanyDataAsync(request.Model);

                returnModel.EmissionData = EmissionCalculatorHelper.GetEmissionCalculate(companyDatas);

                return ResponseMessage<CompanyEmissionDataDto>.Success(returnModel);
            }
        }
    }
}
