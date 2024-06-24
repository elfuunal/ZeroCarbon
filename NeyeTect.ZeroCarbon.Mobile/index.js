/**
 * @format
 */

import 'react-native-gesture-handler';
import axios from 'axios'

import { AppRegistry } from 'react-native';
import App from './App';
import { name as appName } from './app.json';

import messaging from '@react-native-firebase/messaging';
import { setupAxios } from './app/Utils/Tools'
// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getAnalytics } from "firebase/analytics";
import { useNavigation, useTheme } from '@react-navigation/native';

import firebase from '@react-native-firebase/app';



// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
    apiKey: "AIzaSyBn1bvd_A8ti7EsW1MbiLO8DH1YFD7gi_M",
    authDomain: "neyetech-zero-carbon.firebaseapp.com",
    projectId: "neyetech-zero-carbon",
    storageBucket: "neyetech-zero-carbon.appspot.com",
    messagingSenderId: "768294284696",
    appId: "1:768294284696:web:6ac202edb63e8d71c50fed",
    measurementId: "G-S871XLF6QG",
    databaseURL: "https://neyetech-zero-carbon-default-rtdb.firebaseio.com/"
};

if (!firebase.apps.length) {
    firebase.initializeApp(firebaseConfig);
}

setupAxios(axios)

AppRegistry.registerComponent(appName, () => App);
messaging().setBackgroundMessageHandler(async remoteMessage => {
    console.log('Message handled in the background!', remoteMessage);
});