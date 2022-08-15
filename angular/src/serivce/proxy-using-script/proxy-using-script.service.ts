import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root',
})
export class ProxyUsingScriptService {
  constructor(private shareService: ShareServiceService) {}

  public getListProxyUsingScripts(
    clientId: string,
    seedingName: string,
    scriptName: string,
    type: string,
    isActive: string,
    skip: number,
    take: number
  ): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client-using-script/Get-List?clientId=${clientId}&seedingName=${seedingName}&scriptName=${scriptName}&type=${type}&isActive=${isActive}&skip=${skip}&take=${take}`;
    return this.shareService.returnHttpClient(url);
  }

  public updateActive(data: any): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client-using-script/Update-Active`;
    return this.shareService.putHttpClient(url, data);
  }

  public delete(id: any): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client-using-script/Delete?Id=${id}`;
    return this.shareService.deleteHttpClient(url);
  }

  public updateRepeatOrRevokePUS(isActive: any, data: any): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/proxy-using-script/Revoke-Or-Repeat-PUS?isActive=${isActive}`;
    return this.shareService.putHttpClient(url, data);
  }
  public updateContinueProxyUsingScript(scripId: string): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/proxy-using-script/Update-Active-test?ScriptId=${scripId}`;
    return this.shareService.putHttpClient(url, null);
  }
  public updateProxyUsingScript(
    scriptId: string,
    clientId: string
  ): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client-using-script/Update-Active-By-ScriptId-ClientId`;
    let dto = {
      scriptId: scriptId,
      clientId: clientId,
    };
    return this.shareService.putHttpClient(url, dto);
  }

  public create(data: any) {
    const url = `${this.shareService.REST_API_SERVER}/api/client-using-script/Create`;
    return this.shareService.postHttpClientAnonymous(url, data);
  }
  public createByList(data: any){
    const url = `${this.shareService.REST_API_SERVER}/api/client-using-script/Create-By-List`;
    return this.shareService.postHttpClientAnonymous(url, data);
  }
  public getListClientSeeding(seedingName: string, skip : number, take : number,
  ): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/seeding/Clients-Seeding-Job?seedingName=${seedingName}&skip=${skip}&take=${take}`;
    return this.shareService.returnHttpClient(url);
  }
}
