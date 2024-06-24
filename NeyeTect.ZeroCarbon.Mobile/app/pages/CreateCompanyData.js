import React, { useState } from 'react';
import { Image, Platform, SafeAreaView, ScrollView, Text, TouchableOpacity, View, KeyboardAvoidingView, StyleSheet, TextInput } from 'react-native';
import { useTheme } from '@react-navigation/native';
import FeatherIcon from 'react-native-vector-icons/Feather';
import DropShadow from 'react-native-drop-shadow';
import { GlobalStyleSheet } from '../constants/StyleSheet';
import { COLORS, FONTS, SIZES, IMAGES, ICONS } from '../constants/theme';
import RNPickerSelect from 'react-native-picker-select';
import { useFocusEffect } from '@react-navigation/native';
import { getCompanyDataList } from "./core/CompanyData/Requests"
import CustomButton from '../components/CustomButton';
import Toast from 'react-native-simple-toast';
import { createCompanyData } from "./core/CompanyData/Requests"
import Accordion from 'react-native-collapsible/Accordion';
import FontAwesome from "react-native-vector-icons/FontAwesome";
import CustomInput from '../components/Input/CustomInput';

const CreateCompanyData = (props) => {

    const [loading, setLoading] = useState(false);
    const { colors } = useTheme();
    const [emissionSourceId, setEmissionSourceId] = useState('');
    const [customer, setCustomer] = useState();

    const [inputs, setInputs] = useState({});
    const [kisiSayisiInputs, setKisiSayisiInputs] = useState({});
    const [yukAgirligiInputs, setYukAgirligiInputs] = useState({});
    const [odaSayisiInputs, setOdaSayisiInputs] = useState({});

    const [companyEmissionSourceList, setCompanyEmissionSourceList] = useState(Array);

    useFocusEffect(
        React.useCallback(() => {
            setCustomer(props.route.params.customer);
            CompanyEmissionSourceList();
        }, [])
    );

    const CompanyEmissionSourceList = async () => {
        try {
            const response = await getCompanyDataList(props.route.params.customer?.id);

            if (response.data.IsSuccess) {
                console.log(response.data.Data)
                setCompanyEmissionSourceList(response.data.Data.emissionSourceDtos);
            }
            else {
                Toast.show(response.data.Message);
            }
        } catch (e) {
            console.error(e)
        }
    }

    const SaveEmissionData = async () => {
        try {
            setLoading(true);

            const { data: response } = await createCompanyData(customer.id, inputs, kisiSayisiInputs, yukAgirligiInputs, odaSayisiInputs);

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

    // TextInput değeri değiştiğinde state'i güncellemek için bir fonksiyon
    const handleInputChange = (key, value) => {
        setInputs({
            ...inputs,
            [key]: value,
        });
    };

    // TextInput değeri değiştiğinde state'i güncellemek için bir fonksiyon
    const handleKisiSayisiInputChange = (key, value) => {
        setKisiSayisiInputs({
            ...kisiSayisiInputs,
            [key]: value,
        });
    };

    // TextInput değeri değiştiğinde state'i güncellemek için bir fonksiyon
    const handleOdaSayisiInputChange = (key, value) => {
        setOdaSayisiInputs({
            ...odaSayisiInputs,
            [key]: value,
        });
    };

    // TextInput değeri değiştiğinde state'i güncellemek için bir fonksiyon
    const handleYukAgirligiInputChange = (key, value) => {
        setYukAgirligiInputs({
            ...yukAgirligiInputs,
            [key]: value,
        });
    };

    const [activeSections, setActiveSections] = useState([0]);
    const setSections = (sections) => {
        setActiveSections(
            sections.includes(undefined) ? [] : sections
        );
    };

    const AccordionHeader = (item, _, isActive) => {
        return (
            <View style={{
                flexDirection: 'row',
                alignItems: 'center',
                paddingVertical: 15,
                paddingHorizontal: 15,
                borderLeftWidth: 4,
                borderLeftColor: item.color,
            }}>
                {/* <FontAwesome style={{marginRight:10}} name={item.icon} size={15} color={item.color}/> */}
                <Text
                    style={[FONTS.font,
                    {
                        color: colors.title, fontSize: 15, flex: 1
                        , fontFamily: 'NunitoSans-SemiBold',
                    }
                        , isActive && {
                        }]}
                >{item.label} ({item.unit})</Text>
                <FontAwesome name={isActive ? 'angle-up' : 'angle-down'} size={20} color={colors.title} />
            </View>
        )
    }
    const AccordionBody = (item, _, isActive) => {
        return (
            <View style={{
                paddingHorizontal: 15,
                paddingBottom: 15,
                borderLeftWidth: 4,
                borderLeftColor: item.color,
            }}>
                <TextInput
                    style={{ fontSize: 14, borderWidth: 1, borderRadius: 5, paddingLeft: 10, height: 40 }}
                    placeholderTextColor={'rgba(128,128,128,.6)'}
                    keyboardType='decimal-pad'
                    placeholder='Faaliyet Verisi Giriniz'
                    key={item.id}
                    value={inputs[item.id]}
                    onChangeText={(value) => handleInputChange(item.id, value)}
                />
                {
                    item.groupCode == '3.1' || item.groupCode == '3.2'
                        ?
                        <TextInput
                            style={{ fontSize: 14, borderWidth: 1, borderRadius: 5, paddingLeft: 10, height: 40, marginTop: 10 }}
                            placeholderTextColor={'rgba(128,128,128,.6)'}
                            keyboardType='numeric'
                            placeholder='Yük Ağırlığını Giriniz'
                            value={yukAgirligiInputs[item.id]}
                            onChangeText={(value) => handleYukAgirligiInputChange(item.id, value)}
                        />
                        :
                        null
                }
                {
                    item.groupCode == '3.5' && item.label.includes("Konaklama")
                        ?
                        <TextInput
                            style={{ fontSize: 14, borderWidth: 1, borderRadius: 5, paddingLeft: 10, height: 40, marginTop: 10 }}
                            placeholderTextColor={'rgba(128,128,128,.6)'}
                            keyboardType='numeric'
                            placeholder='Oda Sayısını Giriniz'
                            value={odaSayisiInputs[item.id]}
                            onChangeText={(value) => handleOdaSayisiInputChange(item.id, value)}
                        />
                        :
                        null
                }
                {
                    item.groupCode == '3.5' && !item.label.includes("Konaklama")
                        ?
                        <TextInput
                            style={{ fontSize: 14, borderWidth: 1, borderRadius: 5, paddingLeft: 10, height: 40, marginTop: 10 }}
                            placeholderTextColor={'rgba(128,128,128,.6)'}
                            keyboardType='numeric'
                            placeholder='Kişi Sayısını Giriniz'
                            value={kisiSayisiInputs[item.id]}
                            onChangeText={(value) => handleKisiSayisiInputChange(item.id, value)}
                        />
                        :
                        null
                }
            </View>
        )
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
                        <Text style={{ ...FONTS.h6, color: colors.title }}>Yeni Emisyon Verisi</Text>
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

                                <View style={{ borderBottomWidth: 1, borderColor: colors.borderColor, paddingBottom: 8, marginBottom: 20 }}>
                                    <Accordion
                                        sections={companyEmissionSourceList}
                                        sectionContainerStyle={{
                                            backgroundColor: '#f1f1f1',
                                            marginBottom: 6,
                                            borderRadius: 6,
                                            overflow: 'hidden',
                                        }}
                                        duration={300}
                                        activeSections={activeSections}
                                        onChange={setSections}
                                        touchableComponent={TouchableOpacity}
                                        renderHeader={AccordionHeader}
                                        renderContent={AccordionBody}
                                    >
                                    </Accordion>
                                </View>

                                <View style={{ paddingBottom: 10 }}>
                                    <CustomButton
                                        onPress={() => SaveEmissionData()}
                                        title="Kaydet"
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
    inputStyle: {
        height: 70,
        paddingLeft: 90,
        fontSize: 10,
        borderWidth: 1,
        backgroundColor: 'rgba(255,255,255,.05)',
        borderColor: 'rgba(255,255,255,.3)',
        color: COLORS.white,
    },
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

export default CreateCompanyData;