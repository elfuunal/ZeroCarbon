import React from 'react';
import { createDrawerNavigator } from '@react-navigation/drawer';
import BottomNavigation from './BottomNavigation';
import SignIn from './Auth/SignIn';
import DrawerMenu from '../layout/DrawerMenu';
import { SafeAreaView } from 'react-native';
import { useTheme } from '@react-navigation/native';
import Home from './Home';

const Drawer = createDrawerNavigator();

const DrawerNavigation = () => {

    const { colors } = useTheme();

    return (
        <SafeAreaView
            style={{
                flex: 1,
                backgroundColor: colors.cardBg,
            }}
        >
            <Drawer.Navigator
                initialRouteName='Home'
                drawerContent={() => <DrawerMenu homeNavigate={'Home'} />}
                screenOptions={{
                    headerShown: false
                }}
            >
                <Drawer.Screen
                    name="Home"
                    component={Home}
                />
            </Drawer.Navigator>
        </SafeAreaView>
    );
};


export default DrawerNavigation;