import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(
    private shareService: ShareServiceService,
    private http: HttpClient
  ) {}


  getInfoToGetAvatar() {
    return this.http.get(
      `${this.shareService.API_ACCOUNT}api/v1/user-profile`,
      {
        headers: this.shareService.setRequestHeader(),
      }
    );
  }

  updateProfile(
    email: string,
    name: string,
    address: string,
    dateOfBirth: Date
  ): Observable<any> {
    let dto = {
      email: email,
      name: name,
      address: address,
      dateOfBirth: dateOfBirth,
    };
    return this.http.put(
      `${this.shareService.API_ACCOUNT}api/v1/user-profile`,
      dto,
      { headers: this.shareService.setRequestHeader() }
    );
  }
  UploadAvartar(file: File): Observable<any> {
    let formData = new FormData();
    formData.append('AvatarImage', file, file.name);
    var reqHeader = new HttpHeaders({
      Authorization: `Bearer ${this.shareService.getToken()}`,
    });
    return this.http.put<any>(`${this.shareService.API_ACCOUNT}api/v1/user-profile/upload-avatar`, formData, {
      reportProgress: true,
      observe: 'events',
      headers: reqHeader,
    });
    }
}
