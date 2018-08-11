import { CateringsService } from './caterings.service';
import { CommentsService } from './comments.service';
import { CustomCateringService } from './custom-catering.service';
import { DeliveryService } from './delivery.service';
import { DiscountService } from './discount.service';
import { InvoiceService } from './invoice.service';
import { ItemsService } from './items.service';
import { PickupService } from './pickup.service';
import { TotalsService } from './totals.service';

export const services: any[] = [
    CateringsService,
    CommentsService,
    CustomCateringService,
    DeliveryService,
    DiscountService,
    InvoiceService,
    ItemsService,
    PickupService,
    TotalsService
];

export * from './caterings.service';
export * from './comments.service';
export * from './custom-catering.service';
export * from './delivery.service';
export * from './discount.service';
export * from './invoice.service';
export * from './items.service';
export * from './pickup.service';
export * from './totals.service';