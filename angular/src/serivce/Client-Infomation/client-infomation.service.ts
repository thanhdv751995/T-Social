import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root'
})
export class ClientInfomationService {

  constructor(
    private shareService: ShareServiceService
  ) { }
  public CreateInformationUser(data : any): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/Information-Client/Create-Update-By-List`;
    return this.shareService.postHttpClient(url, data);
  }
}
