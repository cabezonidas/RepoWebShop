import * as fromComments from '../actions/comments.action';

export interface CommentState {
    comments: string;
    loaded: boolean;
    loading: boolean;
    error: any;
}

export const initialState: CommentState = {
    comments: '',
    loaded: false,
    loading: false,
    error: null
};

export function reducer(state = initialState, action: fromComments.CommentActions): CommentState {
    switch (action.type) {
        case fromComments.CommentActionTypes.LoadComments: 
        case fromComments.CommentActionTypes.AddComments:
        return {
            ...state,
            loading: true,
        };

        case fromComments.CommentActionTypes.LoadCommentsSuccess: 
        case fromComments.CommentActionTypes.AddCommentsSuccess: 
        return {
            ...state,
            comments: action.payload,
            loading: false,
            loaded: true,
        }

        case fromComments.CommentActionTypes.LoadCommentsFail: 
        case fromComments.CommentActionTypes.AddCommentsFail: 
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

export const getComments = (state: CommentState) => state.comments;
export const getCommentsLoading = (state: CommentState) => state.loading;
export const getCommentsLoaded = (state: CommentState) => state.loaded;
export const getCommentsError = (state: CommentState) => state.error;
