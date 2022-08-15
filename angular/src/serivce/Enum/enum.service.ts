import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root'
})
export class EnumService {

  constructor(private shareService: ShareServiceService) { }

  public getListEnumType(): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/enum/Get-List-Type`;
    return this.shareService.returnHttpClient(url);
  }

  public getListEnumProfile() {
    const url = `${this.shareService.REST_API_SERVER}/api/enum/Get-List-Profile-Type`;
    return this.shareService.returnHttpClientGet(url);
  }

  public getListEnumStatusType(): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/enum/Get-List-Status-Type`;
    return this.shareService.returnHttpClient(url);
  }
}
