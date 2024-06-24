import React, { useState } from 'react';
import { Image, Platform, SafeAreaView, ScrollView, Text, TouchableOpacity, View, KeyboardAvoidingView, StyleSheet, TextInput } from 'react-native';
import { useTheme } from '@react-navigation/native';
import FeatherIcon from 'react-native-vector-icons/Feather';
import DropShadow from 'react-native-drop-shadow';
import { GlobalStyleSheet } from '../constants/StyleSheet';
import { COLORS, FONTS, SIZES, IMAGES, ICONS } from '../constants/theme';
import { SvgXml } from 'react-native-svg';
import CustomInput from '../components/Input/CustomInput';
import CustomPhoneInput from '../components/Input/CustomPhoneInput';
import CustomSelect from '../components/Select/CustomSelect';
import FontAwesome from 'react-native-vector-icons/FontAwesome';
import RNPickerSelect from 'react-native-picker-select';
import { useFocusEffect } from '@react-navigation/native';
import { getCityList, getCountyList, createCompany } from "./core/Company/Requests"
import CustomButton from '../components/CustomButton';
import Toast from 'react-native-simple-toast';

const CreateCompany = (props) => {

    const [loading, setLoading] = useState(false);
    const { colors } = useTheme();
    const [title, setTitle] = useState('');
    const [cityId, setCityId] = useState('');
    const [countyId, setCountyId] = useState('');
    const [acikAdres, setAcikAdres] = useState('');
    const [telefonNo, setTelefonNo] = useState('');
    const [cityList, setCityList] = useState([]);
    const [countyList, setCountyList] = useState([]);

    useFocusEffect(
        React.useCallback(() => {
            
            CityList();
        }, [])
    );

    const CityList = async () => {
        setLoading(true);
        try {
            const response = await getCityList();
            if (response.data.IsSuccess) {
                setCityList(response.data.Data);
            }
            else {
                Toast.show('Şehir listesi çekilemedi.');
            }
        } catch (e) {
            console.error(e)
        }
        setLoading(false);
    }

    const CountyList = async (cityId) => {
        setLoading(true);
        try {
            const response = await getCountyList(cityId);
            if (response.data.IsSuccess) {
                setCountyList(response.data.Data);
            }
            else {
                Toast.show('İlçe listesi çekilemedi.');
            }
        } catch (e) {
            console.error(e)
        }
        setLoading(false);
    }

    const CreateNewCompany = async () => {

        try {
            console.log(telefonNo)
            if (countyId == "" || countyId == null) {
                setLoading(false);
                Toast.show('İlçe seçmediniz.');
                return false;
            }

            if (title == "" || title == null) {
                setLoading(false);
                Toast.show('Firma unvanı zorunludur.');
                return false;
            }

            if (acikAdres == "" || acikAdres == null) {
                setLoading(false);
                Toast.show('Firma açık adresi zorunludur.');
                return false;
            }

            if (telefonNo == "" || telefonNo == null) {
                setLoading(false);
                Toast.show('Firma telefon numarası zorunludur.');
                return false;
            }

            setLoading(true);

            const {data : response} = await createCompany(countyId, title, acikAdres, telefonNo);

            console.log(response);

            setLoading(false);

            if (!response.IsSuccess) {
                Toast.show(response.Message);
                return false;
            }

            setTitle(null);
            setCountyId(null);
            setAcikAdres(null);
            setTelefonNo(null);

            props.navigation.navigate('Home')

        } catch (e) {
            console.log("SignIn");
            console.log(e);
        }
    }

    return (
        <>
            <SafeAreaView
                style={{
                    flex: 1,
                    backgroundColor: colors.background2,
                }}
            >
                <DropShadow
                    style={[{
                        shadowColor: "#000",
                        shadowOffset: {
                            width: 4,
                            height: 4,
                        },
                        shadowOpacity: .1,
                        shadowRadius: 5,
                    }, Platform.OS === 'ios' && {
                        backgroundColor: colors.cardBg,
                    }]}
                >
                    <View
                        style={{
                            paddingHorizontal: 15,
                            paddingVertical: 8,
                            backgroundColor: colors.cardBg,
                            flexDirection: 'row',
                            alignItems: 'center',
                        }}
                    >
                        <TouchableOpacity
                            onPress={() => props.navigation.goBack()}
                            style={{
                                padding: 10,
                                marginRight: 5,
                            }}
                        >
                            <FeatherIcon color={colors.title} size={24} name="x" />
                        </TouchableOpacity>
                        <Text style={{ ...FONTS.h6, color: colors.title }}>Yeni Müşteri Ekleme</Text>
                    </View>
                </DropShadow>

                <KeyboardAvoidingView
                    style={{
                        flex: 1
                    }}
                    behavior={Platform.OS === "ios" ? "padding" : ""}
                >
                    <ScrollView>
                        <View
                            style={GlobalStyleSheet.container}
                        >
                            <View style={[styles.card, {
                                backgroundColor: colors.cardBg,
                            }]}>
                                <View style={{ borderBottomWidth: 1, borderColor: colors.borderColor, paddingBottom: 8, marginBottom: 20 }}>
                                    <Text style={{ ...FONTS.h5, color: colors.title }}>Müşteri Bilgileri</Text>
                                    
                                </View>
                                <View style={{ position: 'relative', justifyContent: 'center' }}>
                                    <RNPickerSelect
                                        placeholder={{ label: "Şehir Seçiniz", value: null }}
                                        onValueChange={(value) => {
                                            if (value != null) {
                                                console.log("city change")
                                                console.log(value)
                                                setCityId(value);
                                                CountyList(value);    
                                            }
                                        }}
                                        items={cityList}
                                    />
                                </View>
                                <View style={{ position: 'relative', justifyContent: 'center' }}>
                                    <RNPickerSelect
                                        placeholder={{ label: "İlçe Seçiniz", value: null }}
                                        onValueChange={(value) => setCountyId(value)}
                                        items={countyList}
                                    />
                                </View>
                                <View style={{ marginBottom: 15 }}>
                                    <CustomInput
                                        icon={<FontAwesome name={'building'} size={20} color={colors.textLight} />}
                                        value={title}
                                        placeholder="Firma Unvanı"
                                        onChangeText={(value) => setTitle(value)}
                                    />
                                </View>
                                <View style={{ marginBottom: 15 }}>
                                    <CustomInput
                                        icon={<FontAwesome name={'map'} size={20} color={colors.textLight} />}
                                        value={acikAdres}
                                        placeholder="Açık Adres"
                                        onChangeText={(value) => setAcikAdres(value)}
                                    />
                                </View>
                                <View style={{ marginBottom: 15 }}>
                                    <CustomPhoneInput
                                        icon={<FontAwesome name={'phone'} size={20} color={colors.textLight} />}
                                        value={telefonNo}
                                        placeholder="İletişim Numarası"
                                        onChangeText={(value) => setTelefonNo(value)}
                                    />
                                </View>
                                <View style={{ paddingBottom: 10 }}>
                                    <CustomButton
                                        onPress={CreateNewCompany}
                                        title="KAYDET"
                                    />
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
    card: {
        padding: 15,
        borderRadius: SIZES.radius,
        marginBottom: 15,
        shadowColor: "rgba(0,0,0,.6)",
        shadowOffset: {
            width: 0,
            height: 4,
        },
        shadowOpacity: 0.30,
        shadowRadius: 4.65,

        elevation: 8,
    },
    inputStyle: {
        ...FONTS.fontLg,
        height: 50,
        paddingLeft: 60,
        borderWidth: 1,
        borderRadius: SIZES.radius,
    },
    inputIcon: {
        backgroundColor: COLORS.yellow,
        height: 40,
        width: 40,
        borderRadius: 10,
        position: 'absolute',
        left: 5,
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

export default CreateCompany;