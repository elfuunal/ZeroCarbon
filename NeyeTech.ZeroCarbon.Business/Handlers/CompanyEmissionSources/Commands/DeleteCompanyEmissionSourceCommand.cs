using AutoMapper;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using NeyeTech.ZeroCarbon.Entities.Concrete;
using MediatR;

namespace NeyeTech.ZeroCarbon.Business.Handlers.CompanyEmissionSources.Commands
{
    public class DeleteCompanyEmissionSourceCommand : IRequest<ResponseMessage<NoContent>>
    {
        public long Id { get; set; }
        public class DeleteCompanyEmissionSourceCommandHandler : IRequestHandler<DeleteCompanyEmissionSourceCommand, ResponseMessage<NoContent>>
        {
            private readonly ICompanyEmissionSourceRepository _cpCompanyEmissionSourceRepository;
            private readonly ICompanyDataRepository _companyDataRepository;
            private readonly IMapper _mapper;

            public DeleteCompanyEmissionSourceCommandHandler(
                ICompanyEmissionSourceRepository cpCompanyEmissionSourceRepository, 
                IMapper mapper,
                ICompanyDataRepository companyDataRepository)
            {
                _cpCompanyEmissionSourceRepository = cpCompanyEmissionSourceRepository;
                _mapper = mapper;
                _companyDataRepository = companyDataRepository; 

            }

            public async Task<ResponseMessage<NoContent>> Handle(DeleteCompanyEmissionSourceCommand request, CancellationToken cancellationToken)
            {
                CompanyEmissionSource s = await _cpCompanyEmissionSourceRepository.GetAsync(s => s.Id == request.Id);
                _cpCompanyEmissionSourceRepository.Delete(s);
                await _cpCompanyEmissionSourceRepository.SaveChangesAsync();
                return ResponseMessage<NoContent>.Success();
            }
        }
    }
}
