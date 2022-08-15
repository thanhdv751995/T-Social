import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private shareService: ShareServiceService) { }

  public postNotification(idUser: string, content : string , urlAvatar: string , time : string): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/notification/Create`;
    let data = {
      idUser: idUser,
      content: content,
      urlAvatar: urlAvatar,
      time: time
    };
    return this.shareService.postHttpClientAnonymous(url, data);
  }
}
