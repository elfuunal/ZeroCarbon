import React, { useRef, useState, useEffect } from 'react';
import { StyleSheet, Modal, Image, SafeAreaView, ScrollView, Text, TouchableOpacity, View } from 'react-native';
import { useTheme } from '@react-navigation/native';
import FeatherIcon from 'react-native-vector-icons/Feather';
import { DrawerLayout } from 'react-native-gesture-handler';
import DrawerMenu from '../layout/DrawerMenu';
import CardStyle8 from '../components/card/CardStyle8';
import SearchBar3 from '../components/Search/SearchBar3';
import { GlobalStyleSheet } from '../constants/StyleSheet';
import CompanyListComponent from './components/CompanyList'
import RegisterCompanyModal from '../components/Modal/RegisterCompanyModal';
import { getCompanyList } from "./core/Company/Requests"
import { CompanyModel } from "./core/Company/Models"
import ListStyle1 from "../components/list/ListStyle1";
import FontAwesome from "react-native-vector-icons/FontAwesome";
import { COLORS, FONTS, IMAGES, SIZES } from "../constants/theme";
import { useFocusEffect } from '@react-navigation/native';
import Toast from 'react-native-simple-toast';
import LinearGradient from 'react-native-linear-gradient';
import DropShadow from 'react-native-drop-shadow';
import Auth from "../Service/Auth";

const Home = (props) => {

    const { colors } = useTheme();
    const drawerRef = useRef();
    const [modalVisible, setModalVisible] = useState(false);
    const [companyList, setCompanyList] = useState([]);
    const [loading, setLoading] = useState(false);

    useFocusEffect(
        React.useCallback(() => {
            CompanyList();
        }, [])
    );

    const CompanyList = async () => {
        try {
            setLoading(true);

            const response = await getCompanyList();
            if (response.data.IsSuccess) {
                setCompanyList(response.data.Data);
            }
            else {
                Toast.show(response.data.Message);
            }
            setLoading(false);
        } catch (e) {
            setLoading(false);

            await Auth.removeToken();

            await Auth.removeUserInfo();

            props.navigation.navigate('SignIn');
        }
    }

    const selectCustomer = async (selectedCustomer) => {
        if (selectCustomer) {
            props.navigation.navigate('CustomerDetail', {
                customer: selectedCustomer
            })
        }
    }

    return (
        <>
            <DrawerLayout
                ref={drawerRef}
                drawerWidth={280}
                drawerPosition={DrawerLayout.positions.Left}
                drawerType="front"
                drawerBackgroundColor="#ddd"
                renderNavigationView={() => <DrawerMenu drawer={drawerRef} />}
            >
                <SafeAreaView
                    style={{
                        flex: 1,
                        backgroundColor: colors.background2,
                    }}
                >
                    <View
                        style={{
                            flexDirection: 'row',
                            alignItems: 'center',
                            paddingHorizontal: 15,
                            paddingVertical: 5,
                        }}
                    >
                        <TouchableOpacity
                            onPress={() => props.navigation.openDrawer()}
                            style={{
                                padding: 12,
                                marginRight: 5,
                                marginLeft: -8,
                            }}
                        >
                            <FeatherIcon size={20} color={colors.title} name='menu' />
                        </TouchableOpacity>
                        <Text style={{ ...FONTS.h5, color: colors.title, flex: 1, top: 1 }}>Ana Sayfa</Text>
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
                                        <Text style={styles.cardTitle}>Müşteri Listesi</Text>
                                        <TouchableOpacity
                                            onPress={() => props.navigation.navigate("CreateCompany")}
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
                                        <Text style={{ ...FONTS.font, color: colors.textLight, marginTop: 20, textAlign: 'justify' }}>Aşağıda sistemde kayıtlı olan müşterilerinizin listesini görebilirsiniz. Yeni müşteri eklemek için yukarıdaki (+) butonuna basabilirsiniz.</Text>
                                    </View>
                                    {
                                        companyList.length > 0 ?
                                            companyList.map((item, index) => (
                                                <ListStyle1
                                                    arrowRight
                                                    onPress={() => selectCustomer(item)}
                                                    key={index}
                                                    icon={<FontAwesome name={'building'} size={15} />}
                                                    title={item.title.length >= 30 ? item.title.substring(0, 30) + "..." : item.title}
                                                />
                                            )) :
                                            <Text style={styles.noCustomerText}>Henüz kayıtlı müşteri yok</Text>
                                    }
                                </View>
                            </View>
                        </View>
                    </ScrollView>
                </SafeAreaView>
            </DrawerLayout>
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
    }
})

export default Home;