import React, { useState } from 'react';
import { Image, Platform, SafeAreaView, ScrollView, Text, TouchableOpacity, View, KeyboardAvoidingView, StyleSheet, TextInput } from 'react-native';
import { useTheme } from '@react-navigation/native';
import FeatherIcon from 'react-native-vector-icons/Feather';
import DropShadow from 'react-native-drop-shadow';
import { GlobalStyleSheet } from '../constants/StyleSheet';
import { COLORS, FONTS, SIZES, IMAGES, ICONS } from '../constants/theme';
import RNPickerSelect from 'react-native-picker-select';
import { useFocusEffect } from '@react-navigation/native';
import { getEmissionSourceScopeList, getEmissionSourceList } from "./core/EmissionSource/Requests"
import CustomButton from '../components/CustomButton';
import Toast from 'react-native-simple-toast';
import { createCompanyEmissionSource } from "./core/Company/Requests"

const CreateCompanyEmissionSource = (props) => {

    const [loading, setLoading] = useState(false);
    const { colors } = useTheme();
    const [emissionSourceList, setEmissionSourceList] = useState([]);
    const [emissionSourceScopeList, setEmissionSourceScopeList] = useState([]);
    const [categoryId, setCategoryId] = useState('');
    const [emissionSourceId, setEmissionSourceId] = useState('');
    const [customer, setCustomer] = useState();

    useFocusEffect(
        React.useCallback(() => {
            setCustomer(props.route.params.customer);
            EmissionSourceScopeList();
        }, [])
    );

    const EmissionSourceList = async (selectedCategory) => {
        setLoading(true);
        try {
            const response = await getEmissionSourceList(selectedCategory);
            if (response.data.IsSuccess) {
                setEmissionSourceList(response.data.Data);
            }
            else {
                Toast.show('Emission listesi çekilemedi.');
            }
        } catch (e) {
            console.error(e)
        }
        setLoading(false);
    }

    const EmissionSourceScopeList = async () => {
        setLoading(true);
        try {
            const response = await getEmissionSourceScopeList();
            if (response.data.IsSuccess) {
                setEmissionSourceScopeList(response.data.Data);
            }
            else {
                Toast.show('Kategori listesi çekilemedi.');
            }
        } catch (e) {
            console.error(e)
        }
        setLoading(false);
    }

    const CreateNewCompanyEmissionSource = async () => {

        try {
            if (customer.id == "" || customer.id == null) {
                setLoading(false);
                Toast.show('Müşteri seçmediniz.');
                return false;
            }

            if (emissionSourceId == "" || emissionSourceId == null) {
                setLoading(false);
                Toast.show('Emisyon zorunludur.');
                return false;
            }

            setLoading(true);

            const {data : response} = await createCompanyEmissionSource(emissionSourceId, customer.id);

            console.log(response);

            setLoading(false);

            if (!response.IsSuccess) {
                Toast.show(response.Message);
                return false;
            }

            props.navigation.navigate('CustomerDetail', {
                customer: props.route.params.customer
            });

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
                        <Text style={{ ...FONTS.h6, color: colors.title }}>Yeni Emisyon Kalemi Ekleme</Text>
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
                                    <Text style={{ ...FONTS.h5, color: colors.title }}>Emisyon Bilgileri</Text>

                                </View>
                                <View style={{ position: 'relative', justifyContent: 'center' }}>
                                    <RNPickerSelect
                                        placeholder={{ label: "Kategori Seçiniz", value: null }}
                                        onValueChange={(value) => {
                                            console.log("kategori " + value)
                                            if (value != null) {
                                                setCategoryId(value);
                                                EmissionSourceList(value);
                                            }
                                        }}
                                        items={emissionSourceScopeList}
                                    />
                                </View>
                                <View style={{ position: 'relative', justifyContent: 'center' }}>
                                    <RNPickerSelect
                                        placeholder={{ label: "Emisyon Kalemi Seçiniz", value: null }}
                                        onValueChange={(value) => setEmissionSourceId(value)}
                                        items={emissionSourceList}
                                    />
                                </View>
                                <View style={{ paddingBottom: 10 }}>
                                    <CustomButton
                                        onPress={CreateNewCompanyEmissionSource}
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

export default CreateCompanyEmissionSource;