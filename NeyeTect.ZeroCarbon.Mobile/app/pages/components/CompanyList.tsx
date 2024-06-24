import React, { useState, useEffect } from 'react';
import { StyleSheet, Text, TouchableOpacity, View } from 'react-native';
import { useTheme } from '@react-navigation/native';
import { COLORS, FONTS, IMAGES, SIZES } from '../../constants/theme';
import FontAwesome from "react-native-vector-icons/FontAwesome";
import ListStyle1 from '../../components/list/ListStyle1';
import { CompanyModel } from '../core/Company/Models';

const CompanyListComponent: React.FC<Array<CompanyModel>> = (companyList : Array<CompanyModel>) => {
    const [loading, setLoading] = useState<boolean>(false);
    const { colors } = useTheme();
    const [activeSections, setActiveSections] = useState<number[]>([0]);
    
    return (
        <>
            <View style={[styles.card, {
                backgroundColor: colors.card,
                width: "100%"
            }]}>
                <View style={{ borderBottomWidth: 1, paddingBottom: 8, marginBottom: 10 }}>
                    <Text style={{ ...FONTS.h5, color: colors.text }}>Müşteri Listesi</Text>
                </View>

                {companyList.map((item, index) => (
                    <ListStyle1
                        arrowRight
                        key={index}
                        icon={<FontAwesome name={'table'} size={15} color={COLORS.primary} />}
                        title={item.title.length >= 30 ?  item.title.substring(0,30) + "..." : item.title}
                    />
                ))}
            </View>
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
})

export default CompanyListComponent;