import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

  constructor(private shareService: ShareServiceService) { }

  public postGroup(userName: string, groupName : string , avatarGroup :string,  groupUrl: string , content : string): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/group-join/Create`;
    let data = {
      userName: userName,
      groupName: groupName,
      avatarGroup: avatarGroup,
      groupUrl: groupUrl,
      content : content
    };
    return this.shareService.postHttpClientAnonymous(url, data);
  }
  public getListGroup(userId: string, skip : number, take : number): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/group-join/Get-List?userName=${userId}&take=${take}&skip=${skip}`;
    return this.shareService.returnHttpClientGet(url);
  }
}
