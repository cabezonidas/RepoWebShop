import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IAlbum } from '../interfaces/ialbum';
import { IPhoto } from '../interfaces/iphoto';

@Injectable({
  providedIn: 'root'
})
export class ImagesService {
  constructor(private http: HttpClient) { }

  getAlbum(id) {
    return this.http.get('/api/_images/album/' + id) as Observable<IAlbum>;
  }

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
