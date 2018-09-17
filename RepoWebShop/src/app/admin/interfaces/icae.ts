import { IInvoiceData } from './iinvoice-data';

export interface ICae {
    caeId: number;
    impTotal: number;
    concepto: number;
    docTipo: number;
    docNro: string;
    cbteDesde: string;
    cbteHasta: string;
    cbteFch: string;
    resultado: string;
    cae: string;
    caeFchVto: string;
}
