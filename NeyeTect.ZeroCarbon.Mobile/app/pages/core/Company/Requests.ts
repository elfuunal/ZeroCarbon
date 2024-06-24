import axios from "axios";
import Config from "react-native-config";
// import {APP_API_URL} from '@env'
import ApiResponse from "../../../Utils/ApiResponse"


const API_URL = Config.APP_API_URL;

export const GET_COMPANY_URL = `${API_URL}/Companies/GetList`;
export const GET_CITY_LIST_URL = `${API_URL}/Cities/GetList`;
export const GET_COUNTY_LIST_URL = `${API_URL}/Counties/GetList`;
export const GET_COMPANY_EMISSIONSOURCE_LIST_URL = `${API_URL}/CompanyEmissionSources/GetList`;
export const POST_NEW_COMPANY_URL = `${API_URL}/Companies/Create`;
export const POST_NEW_COMPANYEMISSIONSOURCE_URL = `${API_URL}/CompanyEmissionSources/Create`;

export async function getCompanyList() {
  const response = await axios.get<ApiResponse>(GET_COMPANY_URL);
  return response;
}

export async function getCityList() {
  const response = await axios.get<ApiResponse>(GET_CITY_LIST_URL);
  return response;
}

export async function getCountyList(cityId: number) {
  const response = await axios.get<ApiResponse>(GET_COUNTY_LIST_URL + "?cityId=" + cityId);
  return response;
}

export async function getCompanyEmissionSourceList(companyId: number) {
  const response = await axios.get<ApiResponse>(GET_COMPANY_EMISSIONSOURCE_LIST_URL + "?companyId=" + companyId);
  return response;
}

export async function createCompanyEmissionSource(emissionSourceId: number, customer: number) {
  const response = await axios.post<ApiResponse>(POST_NEW_COMPANYEMISSIONSOURCE_URL, {
    companyId: customer,
    emissionSourceId: emissionSourceId
  });
  return response;
}

export async function createCompany(
  countyId: number,
  title: string,
  address: string,
  phoneNumber: string) {
  const response = await axios.post<ApiResponse>(POST_NEW_COMPANY_URL, {
    countyId: countyId,
    title: title,
    address: address,
    phoneNumber: phoneNumber
  });
  return response;
}