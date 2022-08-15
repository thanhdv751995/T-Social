import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ProxyService } from 'src/serivce/Proxy-Service/proxy.service';
import { SignalRService } from 'src/serivce/signalR/signalR.service';

@Component({
  selector: 'app-proxy',
  templateUrl: './proxy.component.html',
  styleUrls: ['./proxy.component.css'],
})
export class ProxyComponent implements OnInit, OnDestroy {
  filter = '';
  skipCount = 0;
  maxResultCount = 20;
  listProxy: any;
  timeToPingInterval = 30000; //30s
  hostsAlive: any = [];
  nullLastModified = '-';
  isCurrentComponent = true;
  totalActive = 0;
  hostsDisconnected: any = [];

  constructor(
    private proxyService: ProxyService,
    private signalRService: SignalRService
  ) { }

  ngOnDestroy(): void {
    this.isCurrentComponent = false;
  }

  ngOnInit(): void {
    this.getListProxy();
    this.listenInstallProxyService();
    this.listenPingHost();
  }

  getListProxy() {
    this.proxyService
      .getListProxy(this.filter, this.skipCount, this.maxResultCount)
      .subscribe((res: any) => {
        this.listProxy = res?.items;
        this.totalActive = this.listProxy.filter((x: any) => x.isActive).length;
        this.pingHosts();
      });
  }

  pingHosts() {
    if (this.listProxy) {
      const data = { hosts: this.listProxy.filter((x: any) => x.isActive).map((x: any) => x.proxyIp) };
      setInterval(
        () =>
          this.proxyService.pingHosts(data).subscribe(
            () => { },
            (err: any) => this.hostsAlive.push('error')
          ),
        this.timeToPingInterval
      );
    }
  }

  updateActiveByProxyIp(proxyIp: any) {
    const data = { proxyIp: proxyIp };

    this.proxyService.updateActiveByProxyIp(data).subscribe(() => {
      this.getListProxy();
    })
  }

  listenInstallProxyService() {
    this.signalRService.connection.on('InstallProxyService', () => {
      if (this.isCurrentComponent) {
        this.getListProxy();
      }
    });
  }

  listenPingHost() {
    this.signalRService.connection.on('PingHost', (iP: any, status: any) => {
      if (this.isCurrentComponent) {
        status === 'success'
          ? this.pushIpToHost(iP, this.hostsAlive, this.hostsDisconnected)
          : this.pushIpToHost(iP, this.hostsDisconnected, this.hostsAlive);
      }
    });
  }

  pushIpToHost(iP: any, hostPush: any, hostSplice: any) {
    if (!hostPush.includes(iP)) {
      hostPush.push(iP);
      if (hostSplice.includes(iP)) {
        hostSplice.splice(
          hostSplice.findIndex((x: any) => x == iP),
          1
        );
      }
    }
  }

  showProxyClientLogs(iP: any) {
    window.open(
      '/#/logs/' + iP,
      '_blank' // <- This is what makes it open in a new window.
    );
  }
  updateActive(proxyIp: string) {
    this.proxyService.updateActive(proxyIp).subscribe()
  }
}
