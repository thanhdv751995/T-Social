import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root',
})
export class ClientFacebookService {
  constructor(private shareService: ShareServiceService) { }
  public getListFacebookAccount(
    filter: string,
    skipCount: number,
    maxResultCount: number
  ): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client/Get-List?Sorting=creationTime%20asc&Filter=${filter}&SkipCount=${skipCount}&MaxResultCount=${maxResultCount}`;
    return this.shareService.returnHttpClient(url);
  }

  public getListFacebookAccountActive(nameFacebook : string , nameProfile :string, proxyIp: string, skip : number, take : number): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client/Get-List-Account-Active?nameClient=${nameFacebook}&profileName=${nameProfile}&proxyIp=${proxyIp}&skip=${skip}&take=${take}`;
    return this.shareService.returnHttpClientGet(url);
  }
  public getListAccount(): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client/Get-List`;
    return this.shareService.returnHttpClientGet(url);
  }
  public createAccount(
    userName: string,
    passWord: string,
    secretKey: string,
    cookie: string,
    accessToken: string,
    nameFacebook: string,
    avatarUrl: string
  ): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client/Create`;
    let dto = {
      nameFacebook: nameFacebook,
      avatarUrl: avatarUrl,
      userName: userName,
      passWord: passWord,
      secretKey: secretKey,
      cookie: cookie,
      accessToken: accessToken,
    };
    return this.shareService.postHttpClient(url, dto);
  }
  public createAccountWithExcel(data : any): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client/Create-with-excel`;
    return this.shareService.postHttpClient(url, data);
  }
  public deleteAccount(id: string[]): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client/Delete-Many`;
    return this.shareService.deleteHttpClientParam(url, id);
  }
  public deleteSingle(id: string): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client/Delete?id=${id}`;
    return this.shareService.deleteHttpClient(url);
  }
  public updateAccount(clientId: string, data: any): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client?id=${clientId}`;
    return this.shareService.putHttpClient(url, data);
  }

  public GetListClient(): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client/Get-List-Client`;
    return this.shareService.returnHttpClientGet(url);
  }
  public updateClientOnline(data: any): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client/Update-Online`;
    return this.shareService.putHttpClient(url, data);
  }
  public updateClientOnlineByChromeProfile(data: any): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client/Update-Client-Online-By-Chrome-Profile`;
    return this.shareService.putHttpClient(url, data);
  }

  ///////////////
  public GetListAccountFacebook(skip : number, take : number ,  conditionNumber : number): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client/Get-List-Account-Facebook?skip=${skip}&take=${take}&conditionNumber=${conditionNumber}`;
    return this.shareService.returnHttpClientGet(url);
  }
}
