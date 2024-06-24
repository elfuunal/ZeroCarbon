using NeyeTech.ZeroCarbon.Core.Extensions;
using NeyeTech.ZeroCarbon.Entities.Concrete;
using NeyeTech.ZeroCarbon.Entities.DTOs.Companies;
using NeyeTech.ZeroCarbon.Entities.Enums;

namespace NeyeTech.ZeroCarbon.Business.Helpers
{
    public static class EmissionCalculatorHelper
    {
        public static List<CompanyCarbonEmissionDto> GetEmissionCalculate(List<CompanyData> companyDatas)
        {
            List<CompanyCarbonEmissionDto> result = new List<CompanyCarbonEmissionDto>();

            foreach (CompanyData data in companyDatas)
            {
                CompanyCarbonEmissionDto cal = new CompanyCarbonEmissionDto();

                cal.Id = data.Id;

                cal.EmissionScopeId = data.EmissionSource.EmissionSourceScopeId;
                cal.EmissionSourceName = data.EmissionSource.Name;
                cal.FaaliyetVerisi = data.Value.ToCoolString();
                cal.EmissionSourceId = data.EmissionSourceId;

                if (data.EmissionSource != null)
                {
                    switch ((ScopeTypes)data.EmissionSource.EmissionSourceScopeId)
                    {
                        case ScopeTypes.Category1:
                            cal.Total = CalculateCategory1(data, data.EmissionSource);
                            break;
                        case ScopeTypes.Category2:
                            cal.Total = CalculateCategory2(data, data.EmissionSource);
                            break;
                        case ScopeTypes.Category3:
                            cal.Total = CalculateCategory3(data, data.EmissionSource);
                            break;
                        case ScopeTypes.Category4:
                            cal.Total = CalculateCategory4(data, data.EmissionSource);
                            break;
                        case ScopeTypes.Category5:
                            cal.Total = CalculateCategory5(data, data.EmissionSource);
                            break;
                        case ScopeTypes.Category6:
                            cal.Total = CalculateCategory6(data, data.EmissionSource);
                            break;
                    }

                    cal.Total = !cal.Total.HasValue ? 0 : cal.Total.Value;
                    cal.Toplam = cal.Total.HasValue ? cal.Total.Value.ToCoolString() : null;
                }

                result.Add(cal);
            }

            return result;
        }

        private static decimal? CalculateCategory1(CompanyData data, EmissionSource emissionSource)
        {
            switch (data.EmissionSource.GroupCode)
            {
                case "1.1":
                case "1.2":
                    decimal? co2 =
                        (emissionSource.CO2 * emissionSource.CalorificBasic * emissionSource.Density * data.Value)
                        / Convert.ToDecimal(Math.Pow(10, 6));

                    decimal? n2o =
                        (emissionSource.N2O * emissionSource.CalorificBasic * emissionSource.Density * data.Value * 273)
                        / Convert.ToDecimal(Math.Pow(10, 6));

                    decimal? ch4 =
                        (emissionSource.CH4 * emissionSource.CalorificBasic * emissionSource.Density * data.Value * Convert.ToDecimal(27.9))
                        / Convert.ToDecimal(Math.Pow(10, 6));

                    return (co2 + n2o + ch4) / Convert.ToDecimal(Math.Pow(10, 3));
                case "1.4":
                    return (emissionSource.KIP * data.Value) / Convert.ToDecimal(Math.Pow(10, 3));
            }

            return null;
        }

        private static decimal? CalculateCategory2(CompanyData data, EmissionSource emissionSource)
        {
            switch (data.EmissionSource.GroupCode)
            {
                case "2.1.1":
                case "2.1.2":
                    return (data.Value * emissionSource.CalorificBasic) / Convert.ToDecimal(Math.Pow(10, 3));
            }

            return null;
        }

        private static decimal? CalculateCategory3(CompanyData data, EmissionSource emissionSource)
        {
            decimal? co2 = 0, n2o = 0, ch4 = 0;

            switch (data.EmissionSource.GroupCode)
            {
                case "3.1":
                case "3.2":
                    co2 = (data.Value * emissionSource.CO2 * data.YukAgirligi);

                    n2o = (data.Value * emissionSource.N2O * data.YukAgirligi);

                    ch4 = (data.Value * emissionSource.CH4 * data.YukAgirligi);

                    return (co2 + n2o + ch4) / Convert.ToDecimal(Math.Pow(10, 3));
                case "3.3":
                    co2 = (data.Value * emissionSource.CO2);

                    n2o = (data.Value * emissionSource.N2O);

                    ch4 = (data.Value * emissionSource.CH4);

                    return (co2 + n2o + ch4) / Convert.ToDecimal(Math.Pow(10, 3));
                case "3.5":
                    if (emissionSource.Name.Contains("Konaklama"))
                    {
                        co2 = (data.Value * emissionSource.CO2 * data.OdaSayisi);

                        return (co2 + n2o + ch4) / Convert.ToDecimal(Math.Pow(10, 3));
                    }
                    else
                    {
                        co2 = (data.Value * emissionSource.CO2 * data.KisiSayisi) / Convert.ToDecimal(Math.Pow(10, 3));

                        n2o = (data.Value * emissionSource.N2O * data.KisiSayisi) / Convert.ToDecimal(Math.Pow(10, 3));

                        ch4 = (data.Value * emissionSource.CH4 * data.KisiSayisi) / Convert.ToDecimal(Math.Pow(10, 3));

                        return (co2 + n2o + ch4);
                    }
            }

            return null;
        }

        private static decimal? CalculateCategory4(CompanyData data, EmissionSource emissionSource)
        {
            decimal? co2, n2o, ch4;

            switch (data.EmissionSource.GroupCode)
            {
                case "4.1":
                    co2 = (data.Value * emissionSource.CO2);

                    n2o = (data.Value * emissionSource.N2O);

                    ch4 = (data.Value * emissionSource.CH4);

                    return (co2 + n2o + ch4) / Convert.ToDecimal(Math.Pow(10, 3));
            }

            return null;
        }

        private static decimal? CalculateCategory5(CompanyData data, EmissionSource emissionSource)
        {
            decimal? co2, n2o, ch4;

            switch (data.EmissionSource.GroupCode)
            {
                case "5.1":
                    co2 = (data.Value * emissionSource.CO2);

                    n2o = (data.Value * emissionSource.N2O);

                    ch4 = (data.Value * emissionSource.CH4);

                    return (co2 + n2o + ch4) / Convert.ToDecimal(Math.Pow(10, 3));
            }

            return null;
        }

        private static decimal? CalculateCategory6(CompanyData data, EmissionSource emissionSource)
        {
            return null;
        }
    }
}
