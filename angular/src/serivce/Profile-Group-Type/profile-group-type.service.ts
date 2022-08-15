import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root'
})
export class ProfileGroupTypeService {

  constructor(private shareService: ShareServiceService) { }
  public postGroupType(listProfileId: string[] ,listGroupTypeId: string[] ): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/profile-group-type/Create`;
    let data = {
      listProfileId: listProfileId,
      listGroupTypeId: listGroupTypeId,
    };
    return this.shareService.postHttpClientAnonymous(url, data);
  }
}
