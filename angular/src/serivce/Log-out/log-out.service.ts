import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root',
})
export class LogOutService {
  constructor(
    private shareService: ShareServiceService,
    private http: HttpClient
  ) {}

  logOut(): Observable<any> {
    return this.http.get(`${this.shareService.API_ACCOUNT}api/account/logout`, {
      headers: this.shareService.setRequestHeader(),
    });
  }
}
