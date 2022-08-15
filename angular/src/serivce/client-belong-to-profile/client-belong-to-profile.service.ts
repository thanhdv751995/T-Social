import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root'
})
export class ClientBelongToProfileService {

  constructor(private shareService: ShareServiceService) { }

  getProfile(clientId: any) {
    const url = `${this.shareService.REST_API_SERVER}/api/client-profile/Get-List?clientId=${clientId}`;
    return this.shareService.returnHttpClientGet(url);
  }
  public CreateProfileByListClient(data : any): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client-belong-to-profile/Create-By-List-ClientId`;
    return this.shareService.postHttpClient(url, data);
  }
  public deleteProfileChecked(clientId : string ,listProfileName: string[] ): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client-belong-to-profile/Delete-Profile-Checked?ClientId=${clientId}`;
    let dto = {
      listProfileName: listProfileName,
    };
    return this.shareService.deleteClientBelongToProfile(url, dto);
  }
  public CreateProfileChecked(clientId : string, listProfileName: string[] ): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client-belong-to-profile/Create-Profile-Checked?ClientId=${clientId}`;
    let dto = {listProfileName: listProfileName};
    return this.shareService.postHttpClient(url, dto);
  }
  getListEnumProfile(){
    const url =`${this.shareService.REST_API_SERVER}/api/client-profile/Get-List-Enum-Profile`;
    return this.shareService.returnHttpClientGet(url)
  }
}
