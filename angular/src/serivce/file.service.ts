import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from './share-service.service';

@Injectable({
  providedIn: 'root'
})
export class FileService {

  constructor(
    private http: HttpClient, 
    private shareService: ShareServiceService
  ) { }
  createAttachment(file: File): Observable<any> {
    let formData = new FormData();
    formData.append('dto.File', file, file.name);
    return this.http.post<any>(`${this.shareService.REST_API_SERVER}/api/files`, formData ,{
      reportProgress: true,
      observe: 'events'
  });
  }
}
