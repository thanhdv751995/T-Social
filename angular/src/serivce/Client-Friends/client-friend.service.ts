import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root'
})
export class ClientFriendService {

  constructor(private shareService: ShareServiceService) { }
  public createAccount(data : any): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/friend/Create-By-List`;
    return this.shareService.postHttpClient(url, data);
  }
  public getListFriend(userId: string, skip:number, take : number): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/friend/Get-List-Friend?idUser=${userId}&skip=${skip}&take=${take}`;
    return this.shareService.returnHttpClientGet(url);
  }
}
