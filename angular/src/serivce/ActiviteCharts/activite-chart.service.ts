import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root',
})
export class ActiviteChartService {
  constructor(private shareService: ShareServiceService) {}

  public getListActivityByClientId(
    clientId: string,
    startDate: string,
    endDate: string
  ): Observable<any> {
    const url = `${this.shareService.API_ACTIVITY}/api/activiteChart/Get-List-Activity-By-ClientId?userName=${clientId}&startDate=${startDate}&endDate=${endDate}`;
    return this.shareService.returnHttpClient(url);
  }
  public getLogActivity(
    clientId: string,
    skip: number,
    take: number,
    startDate: string,
    endDate: string
  ): Observable<any> {
    const url = `${this.shareService.API_ACTIVITY}/api/activiteChart/Get-Logs-Activity?userName=${clientId}&skip=${skip}&take=${take}&startDate=${startDate}&endDate=${endDate}`;
    return this.shareService.returnHttpClient(url);
  }
  public postClientActivity(
    userName: string,
    content: string,
    Url: string,
    scriptName: string
  ): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/activity/Create`;
    let data = {
      userName: userName,
      content: content,
      url: Url,
      scriptName: scriptName,
    };
    return this.shareService.postHttpClientAnonymous(url, data);
  }
  public getListJobForChart(startDate : Date, endDate : Date): Observable<any> {
    const url = startDate != null && endDate != null
    ? `${this.shareService.REST_API_SERVER}/api/hangfire-job/Get-Chart-By-Job?startDate=${startDate}&endDate=${endDate}`
    : `${this.shareService.REST_API_SERVER}/api/hangfire-job/Get-Chart-By-Job`;
    return this.shareService.returnHttpClient(url);
  }
  public getDataBarChart(): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/hangfire-job/Get-Data-LineChart`;
    return this.shareService.returnHttpClient(url);
  }
  public getHistory(startDate: any, endDate: any, seedingName: string): Observable<any> {
    const url = startDate != null && endDate != null
        ? `${this.shareService.REST_API_SERVER}/api/activity/Get-List-History-Activity?startDate=${startDate}&endDate=${endDate}&seedingName=${seedingName}`
        : `${this.shareService.REST_API_SERVER}/api/activity/Get-List-History-Activity?seedingName=${seedingName}`;
    return this.shareService.returnHttpClient(url);
  }
  public getListNameSeeding(): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/seeding/List-Name-Seeding`;
    return this.shareService.returnHttpClient(url);
  }
}
