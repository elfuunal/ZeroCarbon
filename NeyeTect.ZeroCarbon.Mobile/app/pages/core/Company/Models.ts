export interface CompanyModel {
    title: string
    recordDate: Date
    companyAddresses: Array<CompanyAddressModel>
}

export interface CompanyAddressModel {
    name: string
}