export function validateEmail(email : string) {
    // Email formatını kontrol etmek için regex ifadesi
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(email);
}