import { Injectable } from '@angular/core';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root',
})
export class ChromeService {
  constructor(private shareService: ShareServiceService) {}

  openChrome(profile: any, urlOpenChrome: any) {
    const url = `${this.shareService.REST_API_SERVER}/api/chrome/Start?profile=${profile}&url=${urlOpenChrome}`;
    return this.shareService.returnHttpClientGet(url);
  }
  updateProfile(data: any) {
    const url = `${this.shareService.REST_API_SERVER}/api/chrome-profile/create-update-profile`;
    return this.shareService.postHttpClientAnonymous(url, data);
  }
  newChromeProfile(profile: any, urlOpenChrome: any) {
    const url = `${this.shareService.REST_API_SERVER}/api/chrome/New-Chrome-Profile?profile=${profile}&url=${urlOpenChrome}`;
    return this.shareService.returnHttpClientGet(url);
  }
}
