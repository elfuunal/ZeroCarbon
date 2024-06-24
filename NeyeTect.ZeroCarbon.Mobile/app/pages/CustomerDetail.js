import React, { useRef, useState, useEffect } from 'react';
import { StyleSheet, Modal, Image, SafeAreaView, ScrollView, Text, TouchableOpacity, View, Alert } from 'react-native';
import { useTheme } from '@react-navigation/native';
import FeatherIcon from 'react-native-vector-icons/Feather';
import { DrawerLayout } from 'react-native-gesture-handler';
import DrawerMenu from '../layout/DrawerMenu';
import CardStyle8 from '../components/card/CardStyle8';
import SearchBar3 from '../components/Search/SearchBar3';
import { GlobalStyleSheet } from '../constants/StyleSheet';
import CompanyListComponent from './components/CompanyList'
import RegisterCompanyModal from '../components/Modal/RegisterCompanyModal';
import { getCompanyEmissionSourceList } from "./core/Company/Requests"
import { getCalculatedCompanyDataList, deleteCompanyData } from "./core/CompanyData/Requests"
import { CompanyModel } from "./core/Company/Models"
import ListStyle1 from "../components/list/ListStyle1";
import FontAwesome from "react-native-vector-icons/FontAwesome";
import { COLORS, FONTS, IMAGES, SIZES } from "../constants/theme";
import { useFocusEffect } from '@react-navigation/native';
import Toast from 'react-native-simple-toast';
import DropShadow from 'react-native-drop-shadow';
import LinearGradient from 'react-native-linear-gradient';
import SwipeBox from "../components/SwipeBox"
import CustomButton from '../components/CustomButton';
import { DataTable, IconButton, MD3Colors } from 'react-native-paper';
import Spinner from 'react-native-loading-spinner-overlay';

const CustomerDetail = (props) => {

    const { colors } = useTheme();
    const [companyEmissionSourceList, setCompanyEmissionSourceList] = useState([]);
    const [calculatedCompanyDataList, setCalculatedCompanyDataList] = useState([]);
    const [customer, setCustomer] = useState();


    this.state = {
        spinner: false
      };

    useFocusEffect(
        React.useCallback(() => {
            setCustomer(props.route.params.customer);
            CompanyEmissionSourceList();
            CalculatedCompanyDataList();
        }, [])
    );

    const CompanyEmissionSourceList = async () => {
        try {
            this.state.visible = true;
            const response = await getCompanyEmissionSourceList(props.route.params.customer?.id);

            if (response.data.IsSuccess) {
                setCompanyEmissionSourceList(response.data.Data);
            }
            else {
                Toast.show(response.data.Message);
            }
        } catch (e) {
            console.error(e)
        }
    }

    const CalculatedCompanyDataList = async () => {
        try {
            const response = await getCalculatedCompanyDataList(props.route.params.customer?.id);
            if (response.data.IsSuccess) {
                setCalculatedCompanyDataList(response.data.Data.emissionData);
            }
            else {
                Toast.show(response.data.Message);
            }
        } catch (e) {
            console.error(e)
        }
    }

    const selectEmissionSource = async () => {
        props.navigation.navigate('CreateCompanyEmissionSource', {
            customer: props.route.params.customer
        });
    }

    const selectEmissionItem = async () => {
        props.navigation.navigate('CreateCompanyData', {
            customer: props.route.params.customer
        });
    }

    const deleteItem = (index) => {

    };

    const deleteEmissionDataItem = (item) => {
        Alert.alert('Kayıt Silinecek', 'Devam etmek istiyor musunuz ?', [
            {
                text: 'Vazgeç',
                onPress: () => console.log('Cancel Pressed'),
                style: 'cancel',
            },
            {
                text: 'SİL', onPress: async () => {
                    try {
                        setLoading(true);
                        const response = await deleteCompanyData(item.id);

                        if (response.data.IsSuccess) {
                            await CalculatedCompanyDataList();
                        }
                        else {
                            Toast.show(response.data.Message);
                        }
                    } catch (e) {
                        console.error(e)
                    }
                }
            },
        ]);

    };

    return (
        <>
            <SafeAreaView
                style={{
                    flex: 1,
                    backgroundColor: colors.background2,
                }}
            >
                <Spinner
                    visible={this.state.spinner}
                    textStyle={styles.spinnerTextStyle}
                />
                <View
                    style={{
                        flexDirection: 'row',
                        alignItems: 'center',
                        paddingHorizontal: 15,
                        paddingVertical: 5,
                    }}
                >
                    <TouchableOpacity
                        onPress={() => props.navigation.goBack()}
                        style={{
                            padding: 12,
                            marginRight: 5,
                            marginLeft: -8,
                        }}
                    >
                        <FeatherIcon color={COLORS.black} name={'arrow-left'} size={30} />
                    </TouchableOpacity>
                    <Text style={{ ...FONTS.h5, color: colors.title, flex: 1, top: 1 }}>{customer?.title}</Text>
                    <View
                        style={{
                            flexDirection: 'row',
                            alignItems: 'center',
                            width: 140,
                        }}
                    >
                    </View>
                </View>

                <ScrollView contentContainerStyle={{ paddingBottom: 80 }}>
                    <View
                        style={GlobalStyleSheet.container}
                    >
                        <View style={{
                            flexDirection: 'row',
                            alignItems: 'stretch',
                            justifyContent: 'space-between',
                            marginBottom: 15,
                        }}>
                            <View style={[styles.card, {
                                backgroundColor: colors.card,
                                width: "100%"
                            }]}>
                                <View style={{ paddingBottom: 8, marginBottom: 10 }}>
                                    <Text style={styles.cardTitle}>Emisyon Listesi</Text>
                                    <TouchableOpacity
                                        onPress={() => selectEmissionSource()}
                                        style={{
                                            position: 'absolute',
                                            top: 0,
                                            right: 5,
                                        }}
                                    >
                                        <DropShadow
                                            style={[{
                                                shadowColor: COLORS.primary2,
                                                shadowOffset: {
                                                    width: 0,
                                                    height: 2,
                                                },
                                                shadowOpacity: .5,
                                                shadowRadius: 5,
                                            }, Platform.OS === 'ios' && {
                                                borderRadius: 25,
                                                backgroundColor: COLORS.primary2,
                                            }]}
                                        >
                                            <LinearGradient
                                                colors={[COLORS.primary2, '#22cf96']}
                                                style={{
                                                    height: 28,
                                                    width: 28,
                                                    borderRadius: 25,
                                                    alignItems: 'center',
                                                    justifyContent: 'center',
                                                }}
                                            >
                                                <FeatherIcon color={COLORS.white} size={16} name={'plus'} />
                                            </LinearGradient>
                                        </DropShadow>
                                    </TouchableOpacity>
                                    <Text style={{ ...FONTS.font, color: colors.textLight, marginTop: 20, textAlign: 'justify' }}>
                                        Aşağıda seçilen müşteri için veri girişi yapılmak istenen emisyon kalem listesi vardır. Yeni eklemek için (+) butonuna basabilirsiniz.</Text>
                                </View>
                                {
                                    companyEmissionSourceList.length > 0 ?
                                        companyEmissionSourceList.map((item, index) => (
                                            <View key={index}>
                                                <SwipeBox data={item.emissionSource.label} colors={colors} handleDelete={() => deleteItem(index)} />
                                            </View>
                                        )) :
                                        <Text style={styles.noCustomerText}>Henüz kayıtlı emisyon kalemi yok</Text>
                                }
                            </View>
                        </View>
                        <View style={{
                            flexDirection: 'row',
                            alignItems: 'stretch',
                            justifyContent: 'space-between',
                            marginBottom: 15,
                        }}>
                            <View style={[styles.card, {
                                backgroundColor: colors.card,
                                width: "100%"
                            }]}>
                                <View style={{ paddingBottom: 8, marginBottom: 10 }}>
                                    <Text style={styles.cardTitle}>Girilen Veriler</Text>
                                    <TouchableOpacity
                                        onPress={() => selectEmissionItem()}
                                        style={{
                                            position: 'absolute',
                                            top: 0,
                                            right: 5,
                                        }}
                                    >
                                        <DropShadow
                                            style={[{
                                                shadowColor: COLORS.primary2,
                                                shadowOffset: {
                                                    width: 0,
                                                    height: 2,
                                                },
                                                shadowOpacity: .5,
                                                shadowRadius: 5,
                                            }, Platform.OS === 'ios' && {
                                                borderRadius: 25,
                                                backgroundColor: COLORS.primary2,
                                            }]}
                                        >
                                            <LinearGradient
                                                colors={[COLORS.primary2, '#22cf96']}
                                                style={{
                                                    height: 28,
                                                    width: 28,
                                                    borderRadius: 25,
                                                    alignItems: 'center',
                                                    justifyContent: 'center',
                                                }}
                                            >
                                                <FeatherIcon color={COLORS.white} size={16} name={'plus'} />
                                            </LinearGradient>
                                        </DropShadow>
                                    </TouchableOpacity>
                                    <Text style={{ ...FONTS.font, color: colors.textLight, marginTop: 20, textAlign: 'justify' }}>
                                        Aşağıda verisi girilen emisyon kalemlerinin listesi vardır. Yeni eklemek için (+) butonuna basabilirsiniz.</Text>
                                    <ScrollView horizontal>
                                        <DataTable>
                                            <DataTable.Header>
                                                <DataTable.Title style={{ width: 20, justifyContent: 'center' }}>#</DataTable.Title>
                                                <DataTable.Title style={{ width: 300 }}>Emisyon Kalemi</DataTable.Title>
                                                <DataTable.Title style={{ width: 200 }}>Faaliyet Verisi</DataTable.Title>
                                                <DataTable.Title style={{ width: 200 }}>Sonuç</DataTable.Title>
                                            </DataTable.Header>
                                            <ScrollView>
                                                {
                                                    calculatedCompanyDataList && calculatedCompanyDataList.length > 0 ?
                                                        calculatedCompanyDataList.map((item, index) => {
                                                            return (
                                                                <DataTable.Row key={index}>
                                                                    <DataTable.Cell style={{ width: 20, justifyContent: 'center' }}>
                                                                        <IconButton
                                                                            icon="delete"
                                                                            iconColor={MD3Colors.error50}
                                                                            size={20}
                                                                            onPress={() => deleteEmissionDataItem(item)}
                                                                        />
                                                                    </DataTable.Cell>
                                                                    <DataTable.Cell style={{ width: 300, paddingLeft: 10 }}>{item.emissionSourceName}</DataTable.Cell>
                                                                    <DataTable.Cell style={{ width: 200 }}>{item.faaliyetVerisi}</DataTable.Cell>
                                                                    <DataTable.Cell style={{ width: 200 }}>{item.toplam}</DataTable.Cell>
                                                                </DataTable.Row>
                                                            );
                                                        })
                                                        :
                                                        <Text style={styles.noCustomerText}>Henüz kayıtlı veri yok</Text>
                                                }
                                            </ScrollView>
                                        </DataTable>
                                    </ScrollView>
                                </View>
                            </View>
                        </View>
                    </View>
                </ScrollView>
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
    cardTitle: {
        color: '#007F73',
        fontSize: 20
    },
    noCustomerText: {
        color: '#DD761C',
        marginTop: 15
    },
    theadItem: {
        flex: 1,
        alignSelf: 'stretch',
        paddingHorizontal: 10,
        paddingVertical: 12,
        ...FONTS.font,
    },
    tbodyItem: {
        flex: 1,
        alignSelf: 'stretch',
        paddingHorizontal: 10,
        paddingVertical: 12,
        ...FONTS.font,
        ...FONTS.fontBold,
    }
})

export default CustomerDetail;