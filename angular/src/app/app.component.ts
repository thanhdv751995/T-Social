import { Component, OnInit } from '@angular/core';
import { ChromeService } from 'src/serivce/chrome/chrome.service';
import { ClientFacebookService } from 'src/serivce/Client-Facebook/client-facebook.service';
import { ProxyUsingScriptService } from 'src/serivce/proxy-using-script/proxy-using-script.service';
import { ShareServiceService } from 'src/serivce/share-service.service';
import { SignalRService } from 'src/serivce/signalR/signalR.service';
import * as authenticator from 'authenticator';
import { ActiviteChartService } from 'src/serivce/ActiviteCharts/activite-chart.service';
import { NotificationService } from 'src/serivce/Notifications/notification.service';
import { GroupService } from 'src/serivce/Groups/group.service';
import { BackgroundJobService } from 'src/serivce/backgroundJob/backgroundJob.service';
import { TDSConfigService, TDSNotificationService } from 'tmt-tang-ui';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  connectionHub: any;
  title = 'abc';

  constructor(
    private shareService: ShareServiceService,
    private chromeService: ChromeService,
    private clientService: ClientFacebookService,
    private signalRService: SignalRService,
    private proxyUsingScriptService: ProxyUsingScriptService,
    private activiteChartService: ActiviteChartService,
    private notificationService: NotificationService,
    private groupService: GroupService,
    private backgroundJobService: BackgroundJobService,
    private notification: TDSNotificationService,
    private readonly tdsConfigService: TDSConfigService
  ) { }

  ngOnInit(): void {
    this.signalRService.setConnection();
    this.connectionHub = this.signalRService.connection;
    this.receiveSecretKey();
    this.postClientActivity();
    this.postGroup();
    this.checkRecurringJob();
    this.listenNewActivity();
    this.tdsConfigService.set('notification', {
      maxStack: 3
    });
  }

  checkRecurringJob() {
    setInterval(() =>
      this.backgroundJobService.getListRecurringJob().subscribe(data => {
        for (let i = 0; i < data.length; i++) {
          if (data[i].state != 'Active') {
            this.backgroundJobService.TriggerJob(data[i].jobId).subscribe();
          }
        }
      })
      , 1800000);
  }

  postClientActivity() {
    this.connectionHub.on(
      'PostClientActivity',
      (userName: string, content: string, Url: string, scriptName: string) => {
        this.activiteChartService
          .postClientActivity(userName, content, Url, scriptName)
          .subscribe();
      }
    );
  }

  listenNewActivity() {
    this.connectionHub.on(
      'NewActivities',
      (data) => {
        this.createNotificationSuccess(data);
      }
    );
  }

  createNotificationSuccess(data): void {
    this.notification.success(
      'Thao tác thành công',
      data,
    );
  }

  postNotifications() {
    this.connectionHub.on(
      'postNotification',
      (idUser: string, content: string, urlAvatar: string, time: string) => {
        this.notificationService
          .postNotification(idUser, content, urlAvatar, time)
          .subscribe();
      }
    );
  }
  postGroup() {
    this.connectionHub.on(
      'PostGroup',
      (
        userName: string,
        groupName: string,
        avatarGroup: string,
        groupUrl: string,
        content: string
      ) => {
        this.groupService
          .postGroup(userName, groupName, avatarGroup, groupUrl, content)
          .subscribe();
      }
    );
  }
  ///generate 2Fa from secret key

  receiveSecretKey() {
    this.connectionHub.on(
      'SendSecretKeyFromExtensions',
      (secretKey: string, username: string) => {
        const key: string = authenticator.generateToken(secretKey);
        this.connectionHub.invoke('SendF2AFromClient', key, username);
      }
    );
  }
  generate2FA(secretKey: string) {
    const key: string = authenticator.generateToken(secretKey);
    this.connectionHub.invoke('SendF2AFromClient', key);
  }
}
