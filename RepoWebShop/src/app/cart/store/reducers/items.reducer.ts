import * as fromItems from '../actions/items.action';
import { ICartCatalogItem } from '../../interfaces/icart-catalog-item';

export interface ItemState {
    items: ICartCatalogItem[];
    loaded: boolean;
    loading: boolean;
    error: any;
    newItemAdded: boolean;
    newItemRemoved: boolean;
}

export const initialState: ItemState = {
    items: [],
    loaded: false,
    loading: false,
    error: null,
    newItemAdded: false,
    newItemRemoved: false
};

export function reducer(state = initialState, action: fromItems.ItemActions): ItemState {
    switch (action.type) {
        case fromItems.ItemActionTypes.Load:
        case fromItems.ItemActionTypes.Add:
        case fromItems.ItemActionTypes.Remove:
        return {
            ...state,
            loading: true
        };

        case fromItems.ItemActionTypes.LoadSuccess:
        case fromItems.ItemActionTypes.AddSuccess:
        case fromItems.ItemActionTypes.RemoveSuccess:
        return {
            ...state,
            items: action.payload,
            loading: false,
            loaded: true
        };

        case fromItems.ItemActionTypes.LoadFail:
        case fromItems.ItemActionTypes.AddFail:
        case fromItems.ItemActionTypes.RemoveFail:
        return {
            ...state,
            error: action.payload,
            loading: false,
            loaded: true
        };
        default:
            return state;
    }
}

export const getItems = (state: ItemState) => state.items;
export const getItemsLoading = (state: ItemState) => state.loading;
export const getItemsLoaded = (state: ItemState) => state.loaded;
export const getItemsError = (state: ItemState) => state.error;
