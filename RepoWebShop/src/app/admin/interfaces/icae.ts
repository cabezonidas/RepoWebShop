import { IInvoiceData } from './iinvoice-data';

export interface ICae {
    caeId: number;
    impTotal: number;
    invoiceData: IInvoiceData;
    concepto: number;
    docTipo: number;
    docNro: string;
    cbteDesde: string;
    cbteHasta: string;
    cbteFch: string;
    resultado: string;
    cAE: string;
    cAEFchVto: string;
}
