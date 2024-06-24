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
import { COLORS, FONTS, SIZES, IMAGES, ICONS } from '../../constants/theme';
import { GlobalStyleSheet } from '../../constants/StyleSheet';

const ActivationPage = (props) => {
    return (
        <>
            <SafeAreaView style={{ flex: 1, backgroundColor: '#fff' }}>
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
                                    <Text style={[FONTS.h2, { textAlign: 'center', color: COLORS.white }]}>Üye Oluşturma Başarılı</Text>
                                    <Text style={[FONTS.font, { textAlign: 'center', color: COLORS.white, opacity: .7 }]}>Mail adresinize gelen linki tıklayarak üyeliğinizi aktif etmeniz gerekmektedir.</Text>
                                </View>
                                
                                <View style={{ flexDirection: 'row', justifyContent: 'center', alignItems: 'center', marginBottom: 15, marginTop: 5 }}>
                                    <Text style={[FONTS.font, { color: COLORS.white, opacity: .7 }]}>Giriş yapmak için tıklayınız </Text>
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
    )
}

const styles = StyleSheet.create({

})

export default ActivationPage;