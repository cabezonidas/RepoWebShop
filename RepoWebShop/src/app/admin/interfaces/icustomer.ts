export interface ICustomer {
    emails: Array<string>;
    orderIds: Array<string>;
    created: Date;
    registrationId: string;
    name: string;
    facebookNameIdentifier: string;
    googleNameIdentifier: string;
    emailConfirmed: boolean;
    phoneNumberConfirmed: boolean;
    phoneNumber: string;
    phoneNumberDeclared: string;
    orders: number;
    spent: number;
}
