import { ILunch } from "./ilunch";

export interface ICartCatering {
    shoppingCartComboCateringId: number;
    amount: number;
    lunchId: number;
    bookingId: string;
    created: Date;
    preparationTime: number;
    title: string;
    description: string;
    price: number;
    priceInStore: number;
    attendants: number;
    eventDuration: number;
}
