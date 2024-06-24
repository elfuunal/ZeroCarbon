using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using NeyeTech.ZeroCarbon.Entities.DTOs.Companies;
using MediatR;

namespace NeyeTech.ZeroCarbon.Business.Handlers.Companies.Commands
{
    public class DeleteCompanyCommand : IRequest<ResponseMessage<DeleteCompanyDto>>
    {
        public long Id { get; set; }
        public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, ResponseMessage<DeleteCompanyDto>>
        {
            private readonly ICompanyRepository _companyRepository;
            private readonly IMediator _mediator;

            public DeleteCompanyCommandHandler(IMediator mediator, ICompanyRepository companyRepository)
            {
                _mediator = mediator;
                _companyRepository = companyRepository;
            }

            public async Task<ResponseMessage<DeleteCompanyDto>> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
            {
                var user = await _companyRepository.GetAsync(s => s.Id == request.Id);
                _companyRepository.Delete(user);
                await _companyRepository.SaveChangesAsync();

                return ResponseMessage<DeleteCompanyDto>.Success();
            }
        }
    }
}
