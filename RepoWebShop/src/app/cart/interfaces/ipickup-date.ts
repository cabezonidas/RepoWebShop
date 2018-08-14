export interface IPickupDate {
    shoppingCartPickUpDateId: number;
    bookingId: string;
    from: Date;
    to: Date;
    userSubmitted: boolean;
    message: string;
}
