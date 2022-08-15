import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root',
})
export class StatusTypeService {
  constructor(private shareService: ShareServiceService) {}
  
  public getRandomStatus(type: any): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/status-store/status-random?statusType=${type}`;
    return this.shareService.returnHttpClient(url);
  }
}
