import {UserLoginModel} from './Models'
import AsyncStorage from "@react-native-async-storage/async-storage";

const AUTH_LOCAL_STORAGE_KEY = 'kt-auth-react-v'

const getAuth = async (): Promise<UserLoginModel | undefined> => {
    if (!AsyncStorage) {
      return
    }
  
    const lsValue: string | null = await AsyncStorage.getItem(AUTH_LOCAL_STORAGE_KEY)
    if (!lsValue) {
      return
    }
  
    try {
      const auth: UserLoginModel = JSON.parse(lsValue) as UserLoginModel
      if (auth) {
        // You can easily check auth_token expiration also
        return auth
      }
    } catch (error) {
      console.error('AUTH LOCAL STORAGE PARSE ERROR', error)
    }
  }