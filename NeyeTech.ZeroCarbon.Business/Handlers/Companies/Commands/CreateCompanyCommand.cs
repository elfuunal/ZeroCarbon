using AutoMapper;
using NeyeTech.ZeroCarbon.Business.Handlers.Companies.ValidationRules;
using NeyeTech.ZeroCarbon.Core.Aspects.Autofac.Validation;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using NeyeTech.ZeroCarbon.Entities.Concrete;
using NeyeTech.ZeroCarbon.Entities.DTOs.Companies;
using MediatR;
using Microsoft.AspNetCore.Http;
using NeyeTech.ZeroCarbon.Core.Extensions;

namespace NeyeTech.ZeroCarbon.Business.Handlers.Companies.Commands
{
    public class CreateCompanyCommand : IRequest<ResponseMessage<AddCompanyDto>>
    {
        public AddCompanyDto Model { get; set; }
        public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, ResponseMessage<AddCompanyDto>>
        {
            private readonly ICompanyRepository _companyRepository;
            private readonly IMapper _mapper;

            public CreateCompanyCommandHandler(ICompanyRepository companyRepository, IMapper mapper)
            {
                _companyRepository = companyRepository;
                _mapper = mapper;
            }

            [ValidationAspect(typeof(CreateCompanyValidator), Priority = 1)]
            public async Task<ResponseMessage<AddCompanyDto>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
            {
                var isThereAnyCompany = await _companyRepository.GetAsync(s => s.Title == request.Model.Title.Trim());
                if (isThereAnyCompany != null)
                    return ResponseMessage<AddCompanyDto>.Fail(StatusCodes.Status400BadRequest, "Kayıt daha önce eklenmiş.");

                var entity = _mapper.Map<Company>(request.Model);
                entity.Status = true;

                entity.Title = entity.Title.Trim().ToUpper();

                entity.UserId = Utils.UserId;
                _companyRepository.Add(entity);
                await _companyRepository.SaveChangesAsync();
                return ResponseMessage<AddCompanyDto>.Success();
            }
        }
    }
}
