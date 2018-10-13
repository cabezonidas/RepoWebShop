import { Injectable, Optional, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IPhoto } from '../interfaces/iphoto';
import { IAlbum } from '../interfaces/ialbum';
import { APP_BASE_HREF } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class ImagesService {
  public api = 'api';
  constructor(private http: HttpClient, @Optional() @Inject(APP_BASE_HREF) origin: string) {
    this.api = `${origin ? origin : ''}${this.api}`;
  }

  getAlbum = (id): Observable<IAlbum> => this.http.get(this.api + '/_images/album/' + id) as Observable<IAlbum>;

  private getUrl(photo: IPhoto): string {
    return `https://farm${photo.farm}.staticflickr.com/${photo.server}/${photo.id}_${photo.secret}_`;
  }

  squareUrl_75 = (photo: IPhoto): string => this.getUrl(photo) + 's.jpg';
  squareUrl_150 = (photo: IPhoto): string => this.getUrl(photo) + 'q.jpg';
  minUrl_100 = (photo: IPhoto): string => this.getUrl(photo) + 't.jpg';
  smallUrl_240 = (photo: IPhoto): string => this.getUrl(photo) + 'm.jpg';
  med_640 = (photo: IPhoto): string => this.getUrl(photo) + 'z.jpg';
  med_800 = (photo: IPhoto): string => this.getUrl(photo) + 'c.jpg';
  large_1024 = (photo: IPhoto): string => this.getUrl(photo) + 'b.jpg';
  large_1600 = (photo: IPhoto): string => this.getUrl(photo) + 'h.jpg';
  large_2048 = (photo: IPhoto): string => this.getUrl(photo) + 'k.jpg';
}
