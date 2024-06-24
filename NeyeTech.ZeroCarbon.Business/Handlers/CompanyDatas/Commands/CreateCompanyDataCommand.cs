using AutoMapper;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using NeyeTech.ZeroCarbon.Entities.Concrete;

using MediatR;
using NeyeTech.ZeroCarbon.Entities.DTOs.CompanyData;
using Azure.Core;
using NeyeTech.ZeroCarbon.Entities.DTOs.Companies;
using ServiceStack.Web;
using ServiceStack;
using Newtonsoft.Json;

namespace NeyeTech.ZeroCarbon.Business.Handlers.CompanyDatas.Commands
{
    public class CreateCompanyDataCommand : IRequest<ResponseMessage<NoContent>>
    {
        public CompanyDataDto Model { get; set; }
        public class CreateCompanyDataCommandHandler : IRequestHandler<CreateCompanyDataCommand, ResponseMessage<NoContent>>
        {
            private readonly ICompanyDataRepository _companyDataRepository;
            IMapper _mapper;

            public CreateCompanyDataCommandHandler(
                ICompanyDataRepository companyDataRepository,
                IMapper mapper)
            {
                _companyDataRepository = companyDataRepository;
                _mapper = mapper;
            }

            public async Task<ResponseMessage<NoContent>> Handle(CreateCompanyDataCommand request, CancellationToken cancellationToken)
            {
                if (!string.IsNullOrEmpty(request.Model.Inputs))
                {
                    List<CompanyData> companyDataList = new List<CompanyData>();

                    var inputList = ConvertJsonToDictionary(request.Model.Inputs);

                    foreach (var input in inputList)
                    {
                        CompanyData companyData = new CompanyData();
                        companyData.EmissionSourceId = Convert.ToInt64(input.Key);
                        companyData.Value = Convert.ToDecimal(input.Value);
                        companyData.CompanyId = request.Model.CompanyId;
                        companyDataList.Add(companyData);
                    }

                    var kisiSayisiList = ConvertJsonToDictionary(request.Model.KisiSayisiInputs);
                    var yukAgirligiList = ConvertJsonToDictionary(request.Model.YukAgirligiInputs);
                    var odaSayisiList = ConvertJsonToDictionary(request.Model.OdaSayisiInputs);

                    foreach (var item in companyDataList)
                    {
                        if (kisiSayisiList.ContainsKey(item.EmissionSourceId.ToString()))
                        {
                            item.KisiSayisi = Convert.ToInt32(kisiSayisiList[item.EmissionSourceId.ToString()]);
                        }

                        if (odaSayisiList.ContainsKey(item.EmissionSourceId.ToString()))
                        {
                            item.OdaSayisi = Convert.ToInt32(odaSayisiList[item.EmissionSourceId.ToString()]);
                        }

                        if (yukAgirligiList.ContainsKey(item.EmissionSourceId.ToString()))
                        {
                            item.YukAgirligi = Convert.ToInt32(yukAgirligiList[item.EmissionSourceId.ToString()]);
                        }
                    }

                    await _companyDataRepository.SaveCompanyDataAsync(companyDataList);

                    return ResponseMessage<NoContent>.Success("Başarılı");
                }

                return ResponseMessage<NoContent>.Fail("Kaydetme işlemi başarısız.");
            }

            static Dictionary<string, string> ConvertJsonToDictionary(string jsonString)
            {
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
            }

            public List<EmissionInputs> ConvertCompanyData(string inputs)
            {
                List<EmissionInputs> result = new List<EmissionInputs>();

                var inputList = inputs.Split(",");

                foreach (var input in inputList)
                {
                    EmissionInputs companyData = new EmissionInputs();

                    var em = input.Split(":");
                    if (em != null && em.Length == 2)
                    {
                        companyData.EmissionSourceId = Convert.ToInt64(em[0]);
                        companyData.Value = Convert.ToDecimal(em[1]);
                        result.Add(companyData);
                    }
                }

                return result;
            }
        }

        public class EmissionInputs
        {
            public long EmissionSourceId { get; set; }
            public decimal Value { get; set; }
        }
    }
}
