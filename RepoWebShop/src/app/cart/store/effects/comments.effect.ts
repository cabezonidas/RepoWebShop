import { Injectable } from '@angular/core';

import { Effect, Actions } from '@ngrx/effects';
import { map, switchMap, catchError, tap } from 'rxjs/operators';
import { of } from 'rxjs';

import * as commentActions from '../actions/comments.action';
import * as fromServices from '../../services';

@Injectable()
export class CommentsEffects {
  constructor(
    private actions$: Actions,
    private commentService: fromServices.CommentsService
  ) {}

  @Effect()
  loadComments$ = this.actions$.ofType(commentActions.CommentActionTypes.LoadComments).pipe(
    switchMap(() => {
      return this.commentService.loadComments()
        .pipe(
          map(comments => new commentActions.LoadCommentsSuccess(comments)),
          catchError(error => {
            return of(new commentActions.LoadCommentsFail(error));
          }
        ));
    })
  );

  @Effect()
  addComment$ = this.actions$.ofType(commentActions.CommentActionTypes.AddComments).pipe(
    map((action: commentActions.AddComments) => action.payload),
    switchMap(commentId => {
      return this.commentService
        .addComments(commentId)
        .pipe(
          map(comments => new commentActions.AddCommentsSuccess(comments)),
          catchError(error => of(new commentActions.AddCommentsFail(error)))
        );
    })
  );
}
