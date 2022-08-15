import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  constructor(private shareService: ShareServiceService) {}

  public login(phoneNumber: string, password: string): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/v1/sign-in/password`;
    let dto = {
      phoneNumber: phoneNumber,
      password: password,
    };
    return this.shareService.postHttpClient(url, dto);
  }
}
