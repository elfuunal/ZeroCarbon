
import React, { useEffect } from 'react';
import Routes from "./app/Navigations/Route";
import { Provider } from 'react-redux';
import store from './app/Redux/Store';
import { notificationListener, requestUserPermission } from './app/helper/notification_helper';
//import messaging from '@react-native-firebase/messaging';
import { Alert, Platform } from 'react-native';

const App = () => {

  // useEffect(() => {
  //   const unsubscribe = messaging().onMessage(async remoteMessage => {
  //     Alert.alert('A new FCM message arrived!', JSON.stringify(remoteMessage));
  //   });

  //   return unsubscribe;
  // }, []);

  useEffect(() => {
    const navigationRef = React.createRef();

    if (Platform.OS === 'android') {
      requestUserPermission();
      notificationListener();
    }
  }, []);

  return (
    <>
      <Provider store={store}>
        <Routes />
      </Provider>
    </>
  );
};

export default App;
