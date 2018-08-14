import * as fromTotals from '../actions/totals.action';

export interface TotalState {
    total: number;
    totalInStore: number;
    totalWithoutDiscount: number;
    items: number;
    itemsInStore: number;
    customCatering: number;
    customCateringInStore: number;
    caterings: number;
    cateringsInStore: number;
    cateringsSavings: number;
    loaded: boolean;
    loading: boolean;
    error: any;
}

export const initialState: TotalState = {
    total: 0,
    totalInStore: 0,
    totalWithoutDiscount: 0,
    items: 0,
    itemsInStore: 0,
    customCatering: 0,
    customCateringInStore: 0,
    caterings: 0,
    cateringsInStore: 0,
    cateringsSavings: 0,
    loaded: false,
    loading: false,
    error: null
};

export function reducer(state = initialState, action: fromTotals.TotalActions): TotalState {
    switch (action.type) {
        case fromTotals.TotalsActionTypes.GetTotals: 
        return {
            ...state,
            loading: true,
        };

        case fromTotals.TotalsActionTypes.GetTotalsSuccess: 
        return {
            ...state,
            total: action.payload.total,
            totalInStore: action.payload.totalInStore,
            totalWithoutDiscount: action.payload.totalWithoutDiscount,
            items: action.payload.items,
            itemsInStore: action.payload.itemsInStore,
            customCatering: action.payload.customCatering,
            customCateringInStore: action.payload.customCateringInStore,
            caterings: action.payload.caterings,
            cateringsInStore: action.payload.cateringsInStore,
            cateringsSavings: action.payload.cateringsSavings,
            loading: false,
            loaded: true,
        }

        case fromTotals.TotalsActionTypes.GetTotalsFail:  
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

export const getTotal = (state: TotalState) => state.total;
export const getTotalInStore = (state: TotalState) => state.totalInStore;
export const getTotalWithoutDiscount = (state: TotalState) => state.totalWithoutDiscount;
export const getItems = (state: TotalState) => state.items;
export const getItemsInStore = (state: TotalState) => state.itemsInStore;
export const getCustomCatering = (state: TotalState) => state.customCatering;
export const getCustomCateringInStore = (state: TotalState) => state.customCateringInStore;
export const getCaterings = (state: TotalState) => state.caterings;
export const getCateringsInStore = (state: TotalState) => state.cateringsInStore;
export const getCateringsSavings = (state: TotalState) => state.cateringsSavings;
export const getTotalsLoading = (state: TotalState) => state.loading;
export const getTotalsLoaded = (state: TotalState) => state.loaded;
export const getTotalsError = (state: TotalState) => state.error;
