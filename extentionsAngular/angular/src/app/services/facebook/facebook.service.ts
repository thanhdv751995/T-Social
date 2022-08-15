import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root'
})
export class FacebookService {

  constructor(private shareService:ShareServiceService) {
  }
  public getListClient(Filter,skipCount,PageSize): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client/Get-List?Filter=${Filter}&SkipCount=${skipCount}&MaxResultCount=${PageSize}`;
    return this.shareService.returnHttpClientGet(url);
  }
  public CreateClient(data): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client/Create`;
    return this.shareService.postHttpClientAnonymous(url, data);
  }
}
