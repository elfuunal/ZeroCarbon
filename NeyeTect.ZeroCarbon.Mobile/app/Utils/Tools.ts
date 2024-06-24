import AsyncStorage from "@react-native-async-storage/async-storage"
import { UserModel } from "../pages/Auth/core/Models";
import navigate from "../Service/Navigation"
import SignIn from "../pages/Auth/SignIn"
import { AxiosError } from "axios";
import Auth from "../Service/Auth";

const AUTH_LOCAL_STORAGE_KEY = 'kt-auth-react-v'

const getAuth = async (): Promise<UserModel | undefined> => {
  try {
    if (!AsyncStorage) {
      return
    }

    const lsValue: string | null = await AsyncStorage.getItem(AUTH_LOCAL_STORAGE_KEY)
    if (!lsValue) {
      return
    }

    try {
      const auth: UserModel = JSON.parse(lsValue) as UserModel
      if (auth) {
        // You can easily check auth_token expiration also
        return auth
      }
    } catch (error) {
      console.error('AUTH LOCAL STORAGE PARSE ERROR', error)
    }
  } catch (error) {
    console.error('getAuth ERROR', error)
  }

}

export function setupAxios(axios: any) {
  axios.defaults.headers.Accept = 'application/json'

  axios.interceptors.request.use(
    async (config: { headers: { Authorization: string } }) => {
      const token = await Auth.getToken()
      if (token) {
        config.headers.Authorization = `Bearer ${token}`
      }

      return config
    },
    (err: any) => Promise.reject(err)
  )

  // Axios için hata interceptor tanımlama
  axios.interceptors.response.use(
    (response: any) => {
      return response;
    },
    async (error: AxiosError) => {
      try {
        if (error.response?.status === 401) {
          navigate.navigate("SignIn")
        }
      } catch (error) {
        console.log(error);
      }


      // Any status codes that falls outside the range of 2xx cause this function to trigger
      return Promise.reject(error);
    }
  );
}