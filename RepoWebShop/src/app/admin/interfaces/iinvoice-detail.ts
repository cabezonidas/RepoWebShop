import { IInvoiceData } from './iinvoice-data';

export interface IInvoiceDetail {
    invoiceDetailId: number;
    invoiceDataId: number;
    invoiceData: IInvoiceData;
    type: string;
    code: number;
    msg: string;
}
