import { Action } from '@ngrx/store';

export enum CommentActionTypes {
    AddComments = '[Comments] Add',
    AddCommentsFail = '[Comments] Add fail',
    AddCommentsSuccess = '[Comments] Add success',
    LoadComments = '[Comments] Load',
    LoadCommentsFail = '[Comments] Load fail',
    LoadCommentsSuccess = '[Comments] Load success'
}

export class AddComments implements Action {
    readonly type = CommentActionTypes.AddComments;
    constructor(public payload: string) {}
}

export class AddCommentsFail implements Action {
    readonly type = CommentActionTypes.AddCommentsFail;
    constructor(public payload: any) {}
}

export class AddCommentsSuccess implements Action {
  readonly type = CommentActionTypes.AddCommentsSuccess;
  constructor(public payload: string) {}
}

export class LoadComments implements Action {
    readonly type = CommentActionTypes.LoadComments;
    constructor() {}
}

export class LoadCommentsFail implements Action {
    readonly type = CommentActionTypes.LoadCommentsFail;
    constructor(public payload: any) {}
}

export class LoadCommentsSuccess implements Action {
  readonly type = CommentActionTypes.LoadCommentsSuccess;
  constructor(public payload: string) {}
}


export type CommentActions =
| AddComments
| AddCommentsFail
| AddCommentsSuccess
| LoadComments
| LoadCommentsFail
| LoadCommentsSuccess;