import { Injectable } from "@angular/core";
import { ShareServiceService } from "src/app/services/share-service.service";
import * as signalR from "@microsoft/signalr";

@Injectable({
  providedIn: "root",
})
export class SignalRService {
  public connection: any;

  constructor(private shareService: ShareServiceService) { }
  setConnection() {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(this.shareService.REST_API_SERVER + "/notify")
      .build();
    this.connection.serverTimeoutInMilliseconds = 360000;
    this.connection.start();
  }

  onCloseConnection() {
    this.connection.onclose(() => {
      this.setConnection();
    });
  }
}
