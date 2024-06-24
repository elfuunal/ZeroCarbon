using AutoMapper;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using NeyeTech.ZeroCarbon.Entities.Concrete;
using NeyeTech.ZeroCarbon.Entities.DTOs.Companies;
using MediatR;
using NeyeTech.ZeroCarbon.Core.Extensions;

namespace NeyeTech.ZeroCarbon.Business.Handlers.Companies.Queries
{
    public class GetCompaniesQuery : IRequest<ResponseMessage<List<CompanyDto>>>
    {
        public class GetCompaniesQueryHandler : IRequestHandler<GetCompaniesQuery, ResponseMessage<List<CompanyDto>>>
        {
            ICompanyRepository _companyRepository;
            IMapper _mapper;

            public GetCompaniesQueryHandler(ICompanyRepository companyRepository, IMapper mapper)
            {
                _companyRepository = companyRepository;
                _mapper = mapper;
            }

            public async Task<ResponseMessage<List<CompanyDto>>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Company> companies = await _companyRepository.GetCompanyListAsync(Utils.UserId);
                var dto = _mapper.Map<List<CompanyDto>>(companies);
                return ResponseMessage<List<CompanyDto>>.Success(dto);
            }
        }
    }
}
