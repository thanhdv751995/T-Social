import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root',
})
export class GraphService {
  constructor(
    private http: HttpClient,
    private shareService: ShareServiceService
  ) {}
  public getListFriend(token:string): Observable<any> {
    var reqHeader = new HttpHeaders({
      Authorization: `Bearer ${token}`,
      'Access-Control-Allow-Origin':'*',
      'Access-Control-Allow-Credentials': 'true',
      'Access-Control-Allow-Methods': 'GET, DELETE, HEAD, OPTIONS'
    });
    return this.http.get(
      `https://graph.facebook.com/me?fields=friends.limit(1000){picture,name}`,
      { headers: reqHeader }
    );
  }
  public getInfoUser(token:string): Observable<any> {
    var reqHeader = new HttpHeaders({
      Authorization: `Bearer ${token}`,
      'Access-Control-Allow-Origin':'*',
      'Access-Control-Allow-Credentials': 'true',
      'Access-Control-Allow-Methods': 'GET, DELETE, HEAD, OPTIONS'
    });
    return this.http.get(
      `https://graph.facebook.com/me?fields=email,id,name,birthday`,
      { headers: reqHeader }
    );
  }
  public getListStatusByClientId(token:string): Observable<any> {
    var reqHeader = new HttpHeaders({
      Authorization: `Bearer ${token}`,
      'Access-Control-Allow-Origin':'*',
      'Access-Control-Allow-Credentials': 'true',
      'Access-Control-Allow-Methods': 'GET, DELETE, HEAD, OPTIONS'
    });
    return this.http.get(
      `https://graph.facebook.com/v12.0/me?fields=posts`,
      { headers: reqHeader }
    );
  }
  public getSharedAPost(token:string, postId : string): Observable<any> {
    var reqHeader = new HttpHeaders({
      Authorization: `Bearer ${token}`,
      'Access-Control-Allow-Origin':'*',
      'Access-Control-Allow-Credentials': 'true',
      'Access-Control-Allow-Methods': 'GET, DELETE, HEAD, OPTIONS'
    });
    return this.http.get(
      `https://graph.facebook.com/${postId}/sharedposts`,
      { headers: reqHeader }
    );
  }
}
