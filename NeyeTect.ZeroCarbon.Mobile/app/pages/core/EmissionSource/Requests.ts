import axios from "axios";
import Config from "react-native-config";
// import {APP_API_URL} from '@env'
import ApiResponse from "../../../Utils/ApiResponse"


const API_URL = Config.APP_API_URL;

export const GET_EMISSIONSOURCESCOPE_LIST_URL = `${API_URL}/EmissionSourceScopes/GetList`;
export const GET_EMISSIONSOURCE_LIST_URL = `${API_URL}/EmissionSources/GetList`;

export async function getEmissionSourceScopeList() {
  const response = await axios.get<ApiResponse>(GET_EMISSIONSOURCESCOPE_LIST_URL);
  return response;
}

export async function getEmissionSourceList(categoryId: number) {
  const response = await axios.get<ApiResponse>(GET_EMISSIONSOURCE_LIST_URL + "?categoryId=" + categoryId);
  return response;
}