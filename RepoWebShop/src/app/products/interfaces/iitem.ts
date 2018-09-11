export interface IItem {
    productId: number;
    price: number;
    multipleAmount: number;
    sizeDescription: string;
    flavour: string;
    priceInStore: number;
    category: string;
    temperature: string;
    minOrderAmount: number;
    portions: number;
    displayName: string;
    displayDescription: string;
    pickUpAsHtml: string;
    estimation: Date;
}
