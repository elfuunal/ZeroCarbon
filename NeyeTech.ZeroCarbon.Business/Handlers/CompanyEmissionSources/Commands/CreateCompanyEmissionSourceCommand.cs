using AutoMapper;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using NeyeTech.ZeroCarbon.Entities.Concrete;
using MediatR;
using NeyeTech.ZeroCarbon.Entities.DTOs.CompanyEmissionSources;

namespace NeyeTech.ZeroCarbon.Business.Handlers.CompanyEmissionSources.Commands
{
    public class CreateCompanyEmissionSourceCommand : IRequest<ResponseMessage<AddCompanyEmissionSourceDto>>
    {
        public AddCompanyEmissionSourceDto Model { get; set; }
        public class CreateCompanyEmissionSourceCommandHandler : IRequestHandler<CreateCompanyEmissionSourceCommand, ResponseMessage<AddCompanyEmissionSourceDto>>
        {
            private readonly ICompanyEmissionSourceRepository _cpCompanyEmissionSourceRepository;
            private readonly IMapper _mapper;

            public CreateCompanyEmissionSourceCommandHandler(ICompanyEmissionSourceRepository cpCompanyEmissionSourceRepository, IMapper mapper)
            {
                _cpCompanyEmissionSourceRepository = cpCompanyEmissionSourceRepository;
                _mapper = mapper;
            }

            public async Task<ResponseMessage<AddCompanyEmissionSourceDto>> Handle(CreateCompanyEmissionSourceCommand request, CancellationToken cancellationToken)
            {
                CompanyEmissionSource s = _mapper.Map<CompanyEmissionSource>(request.Model);
                _cpCompanyEmissionSourceRepository.Add(s);
                await _cpCompanyEmissionSourceRepository.SaveChangesAsync();
                return ResponseMessage<AddCompanyEmissionSourceDto>.Success();
            }
        }
    }
}
