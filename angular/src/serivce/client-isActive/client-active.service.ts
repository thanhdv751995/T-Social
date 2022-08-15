import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root',
})
export class ClientActiveService {
  constructor(private shareService: ShareServiceService) {}
  public createAccount(scriptId : string, listClientId : string[]): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client-using-script/Create-By-ListClient`;
    let dto = {
      scriptId: scriptId,
      listClientId: listClientId,
    };
    return this.shareService.postHttpClient(url, dto);
  }
  public getListProxyIpUsingScript(skip : number, take: number): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client-using-script/Get-List-By-ListClient?skip=${skip}&take=${take}`;
    return this.shareService.returnHttpClient(url);
  }
  public getListClientByIdScript(scriptId): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client/Get-List-Client-By-IDScript?scriptId=${scriptId}`;
    return this.shareService.returnHttpClient(url);
  }
}
