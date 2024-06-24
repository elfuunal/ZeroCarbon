import Storage from '@Utils/Storage';
import AsyncStorage from '@react-native-async-storage/async-storage';

async function getAccount() {
  return await Storage.get('account');
}

async function setAccount(data) {
  return await Storage.set('account', data);
}

async function isAuthenticated() {
  var authStatus = await getAccount();
  if (authStatus == null) {
    return false;
  }
  return true;
}

async function logout() {
  return await Storage.set('account', null);
}

// Token kaydetme
const saveToken = async (token) => {
  try {
    await AsyncStorage.setItem('userToken', token);
  } catch (error) {
    console.error('Token kaydedilemedi:', error);
  }
};

// Token'i almak
const getToken = async () => {
  try {
    const token = await AsyncStorage.getItem('userToken');
    return token;
  } catch (error) {
    console.error('Token alınamadı:', error);
    return null;
  }
};

// Token'i kaldırmak
const removeToken = async () => {
  try {
    await AsyncStorage.removeItem('userToken');
  } catch (error) {
    console.error('Token kaldırılamadı:', error);
  }
};

// Kullanıcı bilgilerini kaydetme
const saveUserInfo = async (userInfo) => {
  try {
    await AsyncStorage.setItem('userInfo', JSON.stringify(userInfo));
  } catch (error) {
    console.error('Kullanıcı bilgileri kaydedilemedi:', error);
  }
};

// Kullanıcı bilgilerini almak
const getUserInfo = async () => {
  try {
    const userInfo = await AsyncStorage.getItem('userInfo');
    return JSON.parse(userInfo);
  } catch (error) {
    console.error('Kullanıcı bilgileri alınamadı:', error);
    return null;
  }
};

// Kullanıcı bilgilerini kaldırmak
const removeUserInfo = async () => {
  try {
    await AsyncStorage.removeItem('userInfo');
  } catch (error) {
    console.error('Kullanıcı bilgileri kaldırılamadı:', error);
  }
};

export default {
  logout,
  getAccount,
  setAccount,
  isAuthenticated,
  saveToken,
  removeUserInfo,
  getUserInfo,
  saveUserInfo,
  removeToken,
  getToken
};