using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using MediatR;

namespace NeyeTech.ZeroCarbon.Business.Handlers.CompanyDatas.Commands
{
    public class DeleteCompanyDataCommand : IRequest<ResponseMessage<NoContent>>
    {
        public long Id { get; set; }
        public class DeleteCompanyDataCommandHandler : IRequestHandler<DeleteCompanyDataCommand, ResponseMessage<NoContent>>
        {
            private readonly ICompanyDataRepository _companyDataRepository;

            public DeleteCompanyDataCommandHandler(
                ICompanyDataRepository companyDataRepository)
            {
                _companyDataRepository = companyDataRepository;
            }

            public async Task<ResponseMessage<NoContent>> Handle(DeleteCompanyDataCommand request, CancellationToken cancellationToken)
            {
                await _companyDataRepository.DeleteCompanyDataAsync(request.Id);
                return ResponseMessage<NoContent>.Success("Başarılı");
            }
        }
    }
}
