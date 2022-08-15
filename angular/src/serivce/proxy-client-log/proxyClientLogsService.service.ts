import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root',
})
export class ProxyClientLogsServiceService {
  constructor(private shareService: ShareServiceService) {}
  public getListProxyClientLogs(
    iP: any,
    date: any,
    skip: any,
    take: any
  ): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/log/Get-Logs?IP=${iP}&date=${date}&skip=${skip}&take=${take}`;
    return this.shareService.returnHttpClient(url);
  }
}
