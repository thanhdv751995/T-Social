import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { profileDto } from 'src/modal/profileDto';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root'
})
export class ProfileClientService {

  constructor(private shareService: ShareServiceService) { }
  public createClientProfile(data : any): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client-profile/Create`;
    return this.shareService.postHttpClient(url, data);
  }
  public getListProfile(profileName : string): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client-profile/Get-List-Profile-With-Script?profileName=${profileName}`;
    return this.shareService.returnHttpClient(url);
  }
  public deleteProfile(id: string): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client-profile?name=${id}`;
    return this.shareService.deleteHttpClient(url);
  }
  public updateProfile(profileName: string, dto : any): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client-profile?profileName=${profileName}`;
    return this.shareService.putHttpClient(url, dto);
  }
  public getListProfileStringJoin(): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client-profile/Get-List-Profile-String`;
    return this.shareService.returnHttpClient(url);
  }
  public getListNameProfile(): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client-profile/Get-List-Name-Profile`;
    return this.shareService.returnHttpClient(url);
  }
  public getListNameProfileWithIdClient(idClient : string): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client-profile/Get-List-Name-Profile?clientId=${idClient}`;
    return this.shareService.returnHttpClient(url);
  }
  public getListProfileOfScript(scriptId : string): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client-profile/Get-List-Profile-Of-Script?scriptId=${scriptId}`;
    return this.shareService.returnHttpClient(url);
  }
}
