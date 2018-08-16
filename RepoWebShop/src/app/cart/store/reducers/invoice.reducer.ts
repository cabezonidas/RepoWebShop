import * as fromInvoices from '../actions/invoice.action';
import { IInvoiceInfo } from '../../interfaces/iinvoice-info';

export interface InvoiceState {
    cuit: string;
    taxpayerInfo: string[];
    loaded: boolean;
    loading: boolean;
    error: any;
}

export const initialState: InvoiceState = {
    cuit: '',
    taxpayerInfo: [],
    loaded: false,
    loading: false,
    error: null
};

export function reducer(state = initialState, action: fromInvoices.InvoiceActions): InvoiceState {
    switch (action.type) {
        case fromInvoices.InvoiceActionTypes.AddCuit: 
        case fromInvoices.InvoiceActionTypes.ClearCuit: 
        case fromInvoices.InvoiceActionTypes.IsCuitValid: 
        case fromInvoices.InvoiceActionTypes.GetCuit: 
        return {
            ...state,
            loading: true,
        };

        case fromInvoices.InvoiceActionTypes.AddCuitSuccess:
        return {
            ...state,
            taxpayerInfo: action.payload,
            loading: false,
            loaded: true,
        }

        case fromInvoices.InvoiceActionTypes.GetCuitSuccess: 
        return {
            ...state,
            cuit: action.payload,
            loading: false,
            loaded: true,
        }

        case fromInvoices.InvoiceActionTypes.ClearCuitSuccess: 
        return {
            ...state,
            cuit: '',
            taxpayerInfo: [],
            loading: false,
            loaded: true,
        }

        case fromInvoices.InvoiceActionTypes.IsCuitValidSuccess: 
        return {
            ...state,
            loading: false,
            loaded: true,
        }

        case fromInvoices.InvoiceActionTypes.AddCuitFail: 
        case fromInvoices.InvoiceActionTypes.ClearCuitFail: 
        case fromInvoices.InvoiceActionTypes.IsCuitValidFail: 
        case fromInvoices.InvoiceActionTypes.GetCuitFail: 
        return {
            ...state,
            error: action.payload,
            loading: false,
            loaded: true,
        }
        default:
            return state;
    }
}

export const getCuit = (state: InvoiceState) => state.cuit;
export const getTaxpayer = (state: InvoiceState) => state.taxpayerInfo;
export const getInvoicesLoading = (state: InvoiceState) => state.loading;
export const getInvoicesLoaded = (state: InvoiceState) => state.loaded;
export const getInvoicesError = (state: InvoiceState) => state.error;
