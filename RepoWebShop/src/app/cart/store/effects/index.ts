import { ItemsEffects } from './items.effect';
import { CateringsEffects } from './caterings.effect';
import { CommentsEffects } from './comments.effect';
import { DiscountsEffects } from './discounts.effect';
import { PickupEffects } from './pickup.effect';
import { TotalsEffects } from './totals.effect';
import { CustomCateringEffects } from './custom-catering.effect';

export const effects: any[] = [
    ItemsEffects,
    CateringsEffects,
    CommentsEffects,
    DiscountsEffects,
    PickupEffects,
    TotalsEffects,
    CustomCateringEffects
];

export * from './items.effect';
export * from './caterings.effect';
export * from './comments.effect';
export * from './discounts.effect';
export * from './pickup.effect';
export * from './totals.effect';
export * from './custom-catering.effect';