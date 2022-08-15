import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root'
})
export class BackgroundJobService {

  constructor(private shareService: ShareServiceService) { }

  public getListRecurringJob(): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/background-job/ListRecurringJob`;
    return this.shareService.returnHttpClientGet(url);
  }

  public TriggerJob(jobId: any): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/background-job/TriggerJob?jobId=${jobId}`;
    return this.shareService.returnHttpClientGet(url);
  }
}
