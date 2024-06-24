import axios from "axios";
import { CreateUserModel, ForgotPasswordModel, UserLoginModel, UserModel } from './Models'
import Config from "react-native-config";
import ApiResponse from "../../../Utils/ApiResponse"

// import {APP_API_URL} from '@env'

const API_URL = Config.APP_API_URL;

export const LOGIN_URL = `${API_URL}/Auth/Login`;
export const CREATE_USER_URL = `${API_URL}/Auth/Register`;
export const CHANGE_USER_PASSWORD_URL = `${API_URL}/Auth/ForgotPasswordRequest`;

export async function login(email: string, password: string) {
  console.log(Config);
  const response =  await axios.post<ApiResponse>(LOGIN_URL, {
    email: email,
    password: password
  });

  return response;
}

export async function createuser(
  firstname: string,
  lastname: string,
  username: string,
  email: string,
  password: string) {
    console.log(CREATE_USER_URL);
  return await axios.post<CreateUserModel>(CREATE_USER_URL, {
    firstname: firstname,
    lastname: lastname,
    username: username,
    email: email,
    password: password
  });
}

export async function changepassword(
  email: string) {
    console.log(CHANGE_USER_PASSWORD_URL);
  return await axios.post<ForgotPasswordModel>(CHANGE_USER_PASSWORD_URL, {
    email: email
  });
}