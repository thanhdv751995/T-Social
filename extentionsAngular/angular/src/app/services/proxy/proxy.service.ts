import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ShareServiceService } from "../share-service.service";

@Injectable({
  providedIn: "root",
})
export class ProxyService {
  constructor(private shareService: ShareServiceService) { }
  public getListProxyById(proxyId): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/proxy/Get-By-Id?ProxyIp=${proxyId}`;
    return this.shareService.returnHttpClientGet(url);
  }
  public getListProxy(skipCount, maxResultCount): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/proxy/Get-List?SkipCount=${skipCount}&MaxResultCount=${maxResultCount}`;
    return this.shareService.returnHttpClientGet(url);
  }
  public getProxyConfig(): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/proxy/Get-Account-Proxy-Config`;
    return this.shareService.returnHttpClientGet(url);
  }
  public getProxyUsingScript(): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/proxy-using-script/Get-List-By-ListProxy`;
    return this.shareService.returnHttpClientGet(url);
  }
  public putProxy(userName, ip): Observable<any> {
    const data = {
      username: userName,
      ip: ip
    }
    const url = `https://49af-116-109-209-125.ngrok.io/api/client/Update-Active`;
    return this.shareService.putHttpClient(url, data);
  }
  public updateActiveProxyConfig(username, proxyIp): Observable<any> {
    const data = {
      username: username,
      ip: proxyIp
    }
    const url = `${this.shareService.REST_API_SERVER}/api/client/Update-Active`;
    return this.shareService.putHttpClient(url, data);
  }
}
