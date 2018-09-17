import { IOrder } from './iorder';
import { IInvoiceDetail } from './iinvoice-detail';
import { ICae } from './icae';

export interface IInvoiceData {
    invoiceDataId: number;
    created: Date;
    order: IOrder;
    orderId: number;
    cuit: string;
    ptoVta: number;
    cbteTipo: number;
    fchProceso: string;
    cantReg: number;
    resultado: string;
    reproceso: string;
    invoiceDetails: IInvoiceDetail[];
    caes: ICae[];
    factura: string;
    friendlyResultado: string;
}
