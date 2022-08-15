import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root',
})
export class ProxyService {
  constructor(private shareService: ShareServiceService) { }

  public getListProxy(
    filter: string,
    skipCount: number,
    maxResultCount: number
  ): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/proxy/Get-List?Filter=${filter}&SkipCount=${skipCount}&MaxResultCount=${maxResultCount}`;
    return this.shareService.returnHttpClient(url);
  }

  public pingHosts(data: any): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/proxy/Ping-Hosts`;
    return this.shareService.postHttpClientAnonymous(url, data);
  }
  public getListProxyConfig(): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/proxy/Get-Account-Proxy-Config`;
    return this.shareService.returnHttpClient(url);
  }
  public updateActive(proxyIp: string): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/proxy/Update-Active?id=${proxyIp}`;
    return this.shareService.putHttpClient(url, null);
  }
  public updateActiveByProxyIp(data: any): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/proxy/Update-Active-By-Proxy-Ip`;
    return this.shareService.putHttpClient(url, data);
  }
}
