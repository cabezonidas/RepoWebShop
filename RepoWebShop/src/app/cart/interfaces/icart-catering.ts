import { ILunch } from "./ilunch";

export interface ICartCatering {
    shoppingCartComboCateringId: number;
    amount: number;
    lunchId: number;
    lunch: ILunch;
    bookingId: string;
    created;
}
