export interface UserLoginModel {
    email: string
    password: string
}

export interface CreateUserModel {
    firstname: string
    lastname: string
    username: string
    rmail: string
    password: string
}

export interface UserModel {
    token: string
    firstName: string
    lastName: string
    userName: string
    email: string
    expiration: string
    refreshToken: string
}

export interface ForgotPasswordModel {
    email: string
}