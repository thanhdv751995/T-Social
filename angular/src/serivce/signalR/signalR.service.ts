import { Injectable } from '@angular/core';
import { ShareServiceService } from '../share-service.service';
import { Observable } from 'rxjs';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  public connection: any;
  constructor(private shareService: ShareServiceService) { }

  setConnection() {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(this.shareService.REST_API_SERVER + '/notify')
      .withAutomaticReconnect([0, 0, 10000])
      .build();

    this.connection.serverTimeoutInMilliseconds = 600000;
    this.connection.start();
  }
}
