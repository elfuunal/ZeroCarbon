
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
} from 'react-native';
import LinearGradient from 'react-native-linear-gradient';
import { SvgXml } from 'react-native-svg';
import uuid from 'react-native-uuid';
import Toast from 'react-native-simple-toast';
import database from '@react-native-firebase/database';
import messaging from '@react-native-firebase/messaging';
import { COLORS, FONTS, SIZES, IMAGES, ICONS } from '../../constants/theme';
import CustomButton from '../../components/CustomButton';
import { GlobalStyleSheet } from '../../constants/StyleSheet';
import { createuser } from './core/Requests'
import { validateEmail } from '../../Utils/GenericMethods'

const CreateAccount = (props) => {
    const [passwordShow, setPasswordShow] = useState(true);
    const [loading, setLoading] = useState(false);
    const handndleShowPassword = () => {
        setPasswordShow(!passwordShow);
    }

    const [firstname, setFirstname] = useState('');
    const [lastname, setLastname] = useState('');
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [pass, setPass] = useState('');

     const registerUser = async () => {

        try {
            if (firstname == "" ||
                lastname == "" ||
                username == "" ||
                email == "" ||
                pass == "") {
                setLoading(false);
                Toast.show('Bütün alanları doldurmanız gerekmektedir.');
                return false;
            }

            if (!validateEmail(email)) {
                setLoading(false);
                Toast.show('Geçerli bir mail adresi girmediniz', Toast.LONG);
                return false;
            }
            
            setLoading(true);

            const {data : response} = await createuser(firstname, lastname, username, email, pass);
            
            console.log(response);

            setLoading(false);

            if (!response.IsSuccess) {
                Toast.show(response.Message);
                return false;
            }

            setFirstname(null);
            setLastname(null);
            setUsername(null);
            setEmail(null);
            setPass(null);

            props.navigation.navigate('ActivationPage')
        } catch (e) {
            console.log("SignIn");
            console.log(e);
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
                                }}
                                source={IMAGES.loginShape}
                            />
                        </View>
                        <View style={{ backgroundColor: '#332A5E' }}>
                            <View style={GlobalStyleSheet.container}>
                                <View style={{ marginBottom: 30 }}>
                                    <Text style={[FONTS.h2, { textAlign: 'center', color: COLORS.white }]}>Yeni Hesap Oluştur</Text>
                                    <Text style={[FONTS.font, { textAlign: 'center', color: COLORS.white, opacity: .7 }]}>Uygulamamızdaki bütün özellikleri kullanmak için üyelik oluşturmanız yeterli</Text>
                                </View>
                                <View style={{ marginBottom: 15 }}>
                                    <View style={styles.inputIcon}>
                                        <SvgXml
                                            xml={ICONS.user}
                                        />
                                    </View>
                                    <TextInput
                                        style={[styles.inputStyle]}
                                        placeholder='Adınız'
                                        onChangeText={value => setFirstname(value)}
                                        value={firstname}
                                        placeholderTextColor={'rgba(255,255,255,.6)'}
                                    />
                                </View>
                                <View style={{ marginBottom: 15 }}>
                                    <View style={styles.inputIcon}>
                                        <SvgXml
                                            xml={ICONS.user}
                                        />
                                    </View>
                                    <TextInput
                                        style={[styles.inputStyle]}
                                        placeholder='Soyadınız'
                                        onChangeText={value => setLastname(value)}
                                        value={lastname}
                                        placeholderTextColor={'rgba(255,255,255,.6)'}
                                    />
                                </View>
                                <View style={{ marginBottom: 15 }}>
                                    <View style={styles.inputIcon}>
                                        <SvgXml
                                            xml={ICONS.user}
                                        />
                                    </View>
                                    <TextInput
                                        style={[styles.inputStyle]}
                                        placeholder='Kullanıcı Adınız'
                                        onChangeText={value => setUsername(value)}
                                        value={username}
                                        placeholderTextColor={'rgba(255,255,255,.6)'}
                                    />
                                </View>
                                <View style={{ marginBottom: 15 }}>
                                    <View style={styles.inputIcon}>
                                        <SvgXml
                                            xml={ICONS.email}
                                        />
                                    </View>
                                    <TextInput
                                        style={[styles.inputStyle]}
                                        placeholder='E-Mail Adresiniz'
                                        keyboardType="email-address"
                                        autoCapitalize='none'
                                        onChangeText={value => setEmail(value)}
                                        value={email}
                                        placeholderTextColor={'rgba(255,255,255,.6)'}
                                    />
                                </View>
                                <View style={{ marginBottom: 15 }}>
                                    <View style={styles.inputIcon}>
                                        <SvgXml
                                            xml={ICONS.lock}
                                        />
                                    </View>
                                    <TextInput
                                        secureTextEntry={passwordShow}
                                        style={[styles.inputStyle]}
                                        onChangeText={value => setPass(value)}
                                        value={pass}
                                        placeholder='Şifreniz'
                                        placeholderTextColor={'rgba(255,255,255,.6)'}
                                    />
                                    <TouchableOpacity
                                        accessible={true}
                                        accessibilityLabel="Password"
                                        accessibilityHint="Password show and hidden"
                                        onPress={() => handndleShowPassword()}
                                        style={styles.eyeIcon}>
                                        <SvgXml
                                            xml={passwordShow ? ICONS.eyeClose : ICONS.eyeOpen}
                                        />
                                    </TouchableOpacity>
                                </View>
                                <View style={{ paddingBottom: 10 }}>
                                    <CustomButton
                                        onPress={registerUser}
                                        title="ÜYE OL"
                                    />
                                </View>
                                <View style={{ flexDirection: 'row', justifyContent: 'center', alignItems: 'center', marginBottom: 15, marginTop: 5 }}>
                                    <Text style={[FONTS.font, { color: COLORS.white, opacity: .7 }]}>Zaten üye misiniz ?</Text>
                                    <TouchableOpacity
                                        onPress={() => props.navigation.navigate('SignIn')}
                                        style={{ marginLeft: 5 }}>
                                        <Text style={[FONTS.fontLg, { color: COLORS.primary, textDecorationLine: 'underline' }]}>Giriş Yapın</Text>
                                    </TouchableOpacity>
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
        ...FONTS.fontLg,
        height: 50,
        paddingLeft: 60,
        borderWidth: 1,
        borderRadius: SIZES.radius,
        color: COLORS.white,
        backgroundColor: 'rgba(255,255,255,.05)',
        borderColor: 'rgba(255,255,255,.3)',
    },
    inputIcon: {
        height: 40,
        width: 40,
        borderRadius: 10,
        position: 'absolute',
        left: 10,
        top: 5,
        alignItems: 'center',
        justifyContent: 'center',
    },
    eyeIcon: {
        position: 'absolute',
        height: 50,
        width: 50,
        alignItems: 'center',
        justifyContent: 'center',
        right: 0,
        zIndex: 1,
        top: 0,
    }

})


export default CreateAccount;
