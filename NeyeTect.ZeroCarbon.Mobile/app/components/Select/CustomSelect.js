import React, { useState } from 'react';
import { StyleSheet, TextInput, TouchableOpacity, View } from 'react-native';
import { useTheme } from '@react-navigation/native';
import { FONTS, ICONS, SIZES } from '../../constants/theme';
import { SvgXml } from 'react-native-svg';
import SelectDropdown from 'react-native-select-dropdown';
import RNPickerSelect from 'react-native-picker-select';

const CustomSelect = (props) => {

    return (
        <>
            <View style={{ position: 'relative', justifyContent: 'center' }}>
                <RNPickerSelect
                    placeholder={{ label: "Select you favourite language", value: null }}
                    onValueChange={(value) => console.log(value)}
                    items={[
                        { label: "JavaScript", value: "JavaScript" },
                        { label: "TypeScript", value: "TypeScript" },
                        { label: "Python", value: "Python" },
                        { label: "Java", value: "Java" },
                        { label: "C++", value: "C++" },
                        { label: "C", value: "C" },
                    ]}
                />
            </View>
        </>
    );
};

const styles = StyleSheet.create({

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

export default CustomSelect;