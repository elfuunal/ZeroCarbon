import React from 'react';
import { CardStyleInterpolators, createStackNavigator } from '@react-navigation/stack';
import CreateCompany from './CreateCompany';
import CreateCompanyEmissionSource from './CreateCompanyEmissionSource';
import ItemDetails from './ItemDetails';
import ChatScreen from './Chats/ChatScreen';
import Items from './Items';
import DrawerNavigation from './DrawerNavigation';
import Components from '../Screens/components';
import ForgotPassword from './Auth/ForgotPassword';
import ChangePasswordPage from './Auth/ChangePasswordPage';
import CreateAccount from './Auth/CreateAccount';
import ActivationPage from './Auth/ActivationPage';
import CustomerDetail from './CustomerDetail';
import CreateCompanyData from './CreateCompanyData';
import SignIn from '../pages/Auth/SignIn'

const StackComponent = createStackNavigator();

const Pages = () => {

    return (
        <>
            <StackComponent.Navigator
                initialRouteName={"BootomNavigation"}
                detachInactiveScreens={true}
                screenOptions={{
                    headerShown: false,
                    cardStyle: { backgroundColor: "transparent" },
                    cardStyleInterpolator: CardStyleInterpolators.forHorizontalIOS,
                }}
                >
                <StackComponent.Screen name={"DrawerNavigation"} component={DrawerNavigation} />
                <StackComponent.Screen name={"CreateCompany"} component={CreateCompany} />
                <StackComponent.Screen name={"CreateCompanyData"} component={CreateCompanyData} />
                <StackComponent.Screen name={"CreateCompanyEmissionSource"} component={CreateCompanyEmissionSource} />
                <StackComponent.Screen name={"ItemDetails"} component={ItemDetails} />
                <StackComponent.Screen name={"ChatScreen"} component={ChatScreen} />
                <StackComponent.Screen name={"Items"} component={Items} />
                <StackComponent.Screen name={"Components"} component={Components} />
                <StackComponent.Screen name={"ForgotPassword"} component={ForgotPassword} />
                <StackComponent.Screen name={"ChangePasswordPage"} component={ChangePasswordPage} />
                <StackComponent.Screen name={"CreateAccount"} component={CreateAccount} />
                <StackComponent.Screen name={"ActivationPage"} component={ActivationPage} />
                <StackComponent.Screen name={"CustomerDetail"} component={CustomerDetail} />
                <StackComponent.Screen name={"SignIn"} component={SignIn} />
            </StackComponent.Navigator>
        </>
    );

};


export default Pages;