import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root'
})
export class GroupTypeService {

  constructor(private shareService: ShareServiceService) { }
  public postGroupType(name: string ,keywordsRelative: string ): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/group-type/Create`;
    let data = {
      name: name,
      keywordsRelative: keywordsRelative,
    };
    return this.shareService.postHttpClientAnonymous(url, data);
  }
  public getListGroup(nameGroup : string): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/group-type/List?nameGroupType=${nameGroup}`;
    return this.shareService.returnHttpClientGet(url);
  }
  public deleteGroupType(id: string): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/group-type?id=${id}`;
    return this.shareService.deleteHttpClient(url);
  }
  public updateGroupType(groupId : string , data : any): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/group-type/Update?Id=${groupId}`;
    return this.shareService.putHttpClient(url, data);
  }
}
