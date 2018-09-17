import { IInvoiceData } from './iinvoice-data';

export interface IInvoiceDetail {
    invoiceDetailId: number;
    invoiceDataId: number;
    type: string;
    code: number;
    msg: string;
}
