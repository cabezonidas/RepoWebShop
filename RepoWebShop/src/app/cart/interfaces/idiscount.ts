export interface IDiscount {
    discountId: number;
    phrase: string;
    validFrom: Date;
    durationDays: number;
    weekly: boolean;
    instancesLeft: number | null;
    percentage: number;
    roof: number;
    base: number;
    comments: string;
    isActive: boolean;
    validTo: Date;
}
