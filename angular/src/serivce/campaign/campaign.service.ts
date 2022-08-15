import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root',
})
export class CampaignService {
  constructor(private shareService: ShareServiceService) { }
  public createCampaign(data: any,): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/seeding/Create`;
    return this.shareService.postHttpClient(url, data);
  }
  public getListSeeding(name: string, skip: number, take: number, conditionValue: number): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/seeding/Get-List?name=${name}&skip=${skip}&take=${take}&conditionValue=${conditionValue}`;
    return this.shareService.returnHttpClientGet(url);
  }
  public deleteCampaign(id: string) {
    const url = `${this.shareService.REST_API_SERVER}/api/seeding?id=${id}`;
    return this.shareService.deleteHttpClient(url)
  }
  public getListEnumGroupName(): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/enum/Get-List-Enum-Group-Type`;
    return this.shareService.returnHttpClientGet(url);
  }
  public getListSeedingByClientId(clientId: string, skip: number, take: number): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/seeding/Clients-Seeding-Dashboard?clientId=${clientId}&skip=${skip}&take=${take}`;
    return this.shareService.returnHttpClientGet(url);
  }

  public getListSeedingByJob(name: string, skip: number, take: number, isFinish: number): Observable<any> {
    let url = `${this.shareService.REST_API_SERVER}/api/hangfire-job/List-Hangfire-Job?MaxResultCount=${take}&SkipCount=${skip}&seedingName=${name}&isFinish=${isFinish}`;
    return this.shareService.returnHttpClientGet(url);
  }
  public GetListJobFuture(seedingName: string): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/hangfire/HangfireJob?seedingName=${seedingName}`;
    return this.shareService.returnHttpClientGet(url);
  }
  public getListScriptUpFinished(name: string, skip: number, take: number): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/hangfire-job/List-Hangfire-Job-UnFinished?MaxResultCount=${take}&SkipCount=${skip}&seedingName=${name}`;
    return this.shareService.returnHttpClientGet(url);
  }
  public updateTaskSeeding(scriptId, clientId, urlActivities): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/client-using-script/Change-Client-Job?ScriptId=${scriptId}&ClientId=${clientId}&Url=${urlActivities}`;
    return this.shareService.postWithURl(url);
  }
}
