import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root',
})
export class HistoryService {
  constructor(private shareService: ShareServiceService) {}

  public getListHistory(
    skip: number,
    take: number,
    clientId: string
  ): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/api/history/Get-List?skip=${skip}&take=${take}&clientId=${clientId}`;
    return this.shareService.returnHttpClient(url);
  }
}
