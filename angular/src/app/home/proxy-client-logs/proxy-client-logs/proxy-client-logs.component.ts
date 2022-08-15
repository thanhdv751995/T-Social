import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProxyClientLogsServiceService } from 'src/serivce/proxy-client-log/proxyClientLogsService.service';
import { SignalRService } from 'src/serivce/signalR/signalR.service';

@Component({
  selector: 'app-proxy-client-logs',
  templateUrl: './proxy-client-logs.component.html',
  styleUrls: ['./proxy-client-logs.component.css'],
})
export class ProxyClientLogsComponent implements OnInit {
  proxyClientLogs: any = [];
  iP: any;
  date: any = new Date();
  isLoading = false;
  isShowMoreLoading = false;
  take = 200;
  skip = 0;

  constructor(
    private proxyClientLogsService: ProxyClientLogsServiceService,
    public router: ActivatedRoute,
    private signalRService: SignalRService
  ) {}

  ngOnInit() {
    this.iP = this.router.snapshot.params.iP;
    this.date = this.convertDate(this.date);
    this.getListProxyClientLogs(this.iP, this.date);
    // this.listenCreatedLog();
  }

  listenCreatedLog() {
    this.signalRService.connection.on('CreatedLog', (data: any) => {
      if (data.proxyIp === this.iP) {
        this.proxyClientLogs.unshift(data);
      }
    });
  }

  getListProxyClientLogs(iP: any, date: any) {
    this.isLoading = true;
    if (this.proxyClientLogs.length > 0)
      this.skip = this.proxyClientLogs.length;
    this.getListClientLogs(iP, date);
  }

  getListClientLogs(iP: any, date: any) {
    this.proxyClientLogsService
      .getListProxyClientLogs(iP, date, this.skip, this.take)
      .subscribe(
        (data) => {
          this.proxyClientLogs.length > 0
            ? (this.proxyClientLogs = this.proxyClientLogs.concat(data))
            : (this.proxyClientLogs = data);
          this.isLoading = false;
        },
        (err) => (this.isLoading = false)
      );
  }

  onDateChange(date: any) {
    this.resetData();
    if (date != null) {
      this.date = this.convertDate(date);
      this.getListProxyClientLogs(this.iP, this.date);
    }
  }

  //yyyy-MM-dd
  convertDate(date: any) {
    let day = '';
    let month = '';
    date.getDate() < 10 ? (day = '0' + date.getDate()) : (day = date.getDate());
    date.getMonth() + 1 < 10
      ? (month = '0' + (date.getMonth() + 1))
      : (month = date.getMonth() + 1);

    return date.getFullYear() + '-' + month + '-' + day;
  }

  today() {
    this.resetData();
    this.date = this.convertDate(new Date());
    this.getListProxyClientLogs(this.iP, this.date);
  }

  takeChange(event: any) {
    this.take = event;
  }

  resetData() {
    this.proxyClientLogs = [];
    this.skip = 0;
  }
}
