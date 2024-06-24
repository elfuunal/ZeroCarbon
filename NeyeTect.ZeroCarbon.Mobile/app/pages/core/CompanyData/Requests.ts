import axios from "axios";
import Config from "react-native-config";
// import {APP_API_URL} from '@env'
import ApiResponse from "../../../Utils/ApiResponse"


const API_URL = Config.APP_API_URL;

export const GET_COMPANYDATA_URL = `${API_URL}/CompanyData/GetCompanyDataList`;
export const GET_CALCULATED_COMPANYDATA_URL = `${API_URL}/CompanyData/GetCalculatedCompanyData`;
export const CREATE_COMPANYDATA_URL = `${API_URL}/CompanyData/CreateCompanyData`;
export const DELETE_COMPANYDATA_URL = `${API_URL}/CompanyData/DeleteCompanyData`;

export async function getCompanyDataList(companyId: number) {
  const response = await axios.get<ApiResponse>(GET_COMPANYDATA_URL + "?companyId=" + companyId);
  return response;
}

export async function getCalculatedCompanyDataList(companyId: number) {
  const response = await axios.get<ApiResponse>(GET_CALCULATED_COMPANYDATA_URL + "?companyId=" + companyId);
  return response;
}

export async function deleteCompanyData(id: number) {
  const response = await axios.delete<ApiResponse>(DELETE_COMPANYDATA_URL + "?id=" + id);
  return response;
}

export async function createCompanyData(
  companyId: number,
  inputs: string,
  kisiSayisiInputs: string,
  yukAgirligiInputs: string,
  odaSayisiInputs: string) {

  console.log("companyId => " + companyId);
  console.log(inputs);
  console.log(kisiSayisiInputs);
  console.log(yukAgirligiInputs);
  console.log(odaSayisiInputs);

  const response = await axios.post<ApiResponse>(CREATE_COMPANYDATA_URL, {
    companyId: companyId,
    inputs: JSON.stringify(inputs),
    kisiSayisiInputs: JSON.stringify(kisiSayisiInputs),
    yukAgirligiInputs: JSON.stringify(yukAgirligiInputs),
    odaSayisiInputs: JSON.stringify(odaSayisiInputs),
  });
  return response;
}