import { NumberFormatStyle } from '@angular/common';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root',
})
export class ScriptService {
  constructor(private shareService: ShareServiceService) { }

  public getListScript(filter: string, id: string, value: number, typeScript: string, seedingName: string, skip : number , take : number): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/scripts/Get-List?Filter=${filter}&ID=${id}&Value=${value}&typeScript=${typeScript}&seedingName=${seedingName}&SkipCount=${skip}&MaxResultCount=${take}`;
    return this.shareService.returnHttpClient(url);
  }
  public getScriptById(id: string): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/scripts/Get-id?id=${id}`;
    return this.shareService.returnHttpClient(url);
  }
  public getListScriptRun(): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/scripts/Get-List-Script`;
    return this.shareService.returnHttpClient(url);
  }
  public updateActive(data: any): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/scripts/Update-Active`;
    return this.shareService.putHttpClient(url, data);
  }
  public updateScript(id: string, data: any): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/scripts?id=${id}`;
    return this.shareService.putHttpClient(url, data);
  }
  public createScript(data: any) {
    const url = `${this.shareService.REST_API_SERVER}/api/scripts/Create`;
    return this.shareService.postHttpClientAnonymous(url, data);
  }

  public deleteScript(id: any) {
    const url = `${this.shareService.REST_API_SERVER}/api/scripts/Delete-Script?Id=${id}`;
    return this.shareService.deleteHttpClient(url);
  }
  public getListScriptNotDefault(value: NumberFormatStyle): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/scripts/Get-List?Value=${value}`;
    return this.shareService.returnHttpClient(url);
  }
  public getListScriptFilter(
    value: number,
    typeScript: string
  ): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/scripts/Get-List?Value=${value}&typeScript=${typeScript}`;
    return this.shareService.returnHttpClient(url);
  }
  public getListNameEnum(): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/enum/Get-List-Type`;
    return this.shareService.returnHttpClient(url);
  }
  public getListScriptDefault(): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/scripts/Get-List-Script-Default`;
    return this.shareService.returnHttpClient(url);
  }
}
