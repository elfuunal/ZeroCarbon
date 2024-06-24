
import React, { useState } from 'react';
import {
    ActivityIndicator,
    Image,
    KeyboardAvoidingView,
    Platform,
    SafeAreaView,
    ScrollView,
    StyleSheet,
    Text,
    TextInput,
    TouchableOpacity,
    View,
    Alert
} from 'react-native';
import LinearGradient from 'react-native-linear-gradient';
import { useTheme } from '@react-navigation/native';
import { SvgXml } from 'react-native-svg';
import SelectDropdown from 'react-native-select-dropdown';
import CustomButton from '../../components/CustomButton';
import { GlobalStyleSheet } from '../../constants/StyleSheet';
import { COLORS, FONTS, SIZES, ICONS, IMAGES } from '../../constants/theme';
import { changepassword } from './core/Requests'

const ForgotPassword = (props) => {

    const [loading, setLoading] = useState(false);
    const [email, setEmail] = useState('');

    const forgotPasswordUser = async () => {
        try {
            if (email == "") {
                setLoading(false);
                Toast.show('E-Mail adresi boş olamaz.');
                return false;
            }

            setLoading(true);

            const { data: response } = await changepassword(email);

            console.log(response);

            setLoading(false);

            if (!response.IsSuccess) {
                Alert.alert('Uyarı', response.Message, [
                    { text: 'Tamam', onPress: () => console.log('OK Pressed') },
                ]);
                Toast.show(response.Message);
                return false;
            }

            props.navigation.navigate('ChangePasswordPage')
        } catch (error) {

        }
    }

    return (
        <>
            <SafeAreaView style={{ flex: 1, backgroundColor: '#fff' }}>
                {loading ?
                    <View
                        style={{
                            position: 'absolute',
                            zIndex: 1,
                            height: '100%',
                            width: '100%',
                            alignItems: 'center',
                            justifyContent: 'center',
                            backgroundColor: 'rgba(0,0,0,.3)',
                        }}
                    >
                        <ActivityIndicator size={'large'} color={COLORS.white} />
                    </View>
                    :
                    null
                }

                <KeyboardAvoidingView
                    style={{
                        flex: 1
                    }}
                    behavior={Platform.OS === "ios" ? "padding" : ""}
                >
                    <ScrollView contentContainerStyle={{ flexGrow: 1 }}>

                        <View style={{
                            flex: 1,
                            alignItems: 'center',
                            justifyContent: 'center',
                            minHeight: 200,
                        }}>
                            <LinearGradient
                                colors={['#F7F5FF', 'rgba(255,255,255,0)']}
                                style={{
                                    height: 135,
                                    width: 135,
                                    borderRadius: 135,
                                    position: 'absolute',
                                    top: 20,
                                    right: -50,
                                    transform: [{ rotate: '-120deg' }]
                                }}
                            ></LinearGradient>
                            <LinearGradient
                                colors={['#F7F5FF', 'rgba(255,255,255,0)']}
                                style={{
                                    height: 135,
                                    width: 135,
                                    borderRadius: 135,
                                    position: 'absolute',
                                    bottom: 0,
                                    left: -50,
                                    transform: [{ rotate: '120deg' }]
                                }}
                            ></LinearGradient>
                            <Image
                                style={{
                                    width: 100,
                                    height: 100,
                                    marginBottom: 40,
                                    resizeMode: 'contain',
                                }}
                                source={IMAGES.zeroCarbonLogo}
                            />
                            <Image
                                style={{
                                    position: 'absolute',
                                    bottom: 0,
                                    width: '100%',
                                    resizeMode: 'stretch',
                                    height: 65,
                                    //tintColor:colors.backgroundColor,
                                }}
                                source={IMAGES.loginShape}
                            />
                        </View>
                        <View style={{ backgroundColor: '#332A5E' }}>
                            <View style={[GlobalStyleSheet.container, { paddingTop: 5 }]}>
                                <View style={{ marginBottom: 30 }}>
                                    <Text style={[FONTS.h2, { textAlign: 'center', color: COLORS.white }]}>Şifremi Unuttum</Text>
                                    <Text style={[FONTS.font, { textAlign: 'center', color: COLORS.white, opacity: .7 }]}>Üye olurken kullandığınız mail adresinizi girerek yeni şifre talep edebilirsiniz</Text>
                                </View>
                                <View style={[styles.inputStyle]}>

                                    <TextInput
                                        style={{
                                            ...FONTS.fontLg,
                                            color: COLORS.white,
                                            flex: 1,
                                            top: 1,
                                        }}
                                        keyboardType="email-address"
                                        autoCapitalize='none'
                                        placeholder='Mail Adresinizi giriniz'
                                        onChangeText={value => setEmail(value)}
                                        placeholderTextColor={'rgba(255,255,255,.6)'}
                                    />
                                </View>
                                <View style={{ paddingBottom: 10, flexDirection: 'row' }}>
                                    <TouchableOpacity
                                        onPress={() => props.navigation.navigate('SignIn')}
                                        style={{
                                            backgroundColor: 'rgba(255,255,255,.1)',
                                            width: 50,
                                            borderRadius: SIZES.radius,
                                            alignItems: 'center',
                                            justifyContent: 'center',
                                            marginRight: 10,
                                        }}
                                    >
                                        <SvgXml
                                            style={{ marginLeft: 6 }}
                                            xml={ICONS.back}
                                        />
                                    </TouchableOpacity>
                                    <View style={{ flex: 1 }}>
                                        <CustomButton
                                            onPress={forgotPasswordUser}
                                            title="ŞİFREMİ GÖNDER"
                                        />
                                    </View>
                                </View>
                            </View>
                        </View>
                    </ScrollView>
                </KeyboardAvoidingView>
            </SafeAreaView>
        </>
    );
};



const styles = StyleSheet.create({

    inputStyle: {
        height: 50,
        padding: 5,
        paddingHorizontal: 15,
        borderWidth: 1,
        borderRadius: SIZES.radius,
        marginBottom: 15,
        flexDirection: 'row',
        alignItems: 'center',
        backgroundColor: 'rgba(255,255,255,.05)',
        borderColor: 'rgba(255,255,255,.3)',
    },

})


export default ForgotPassword;
