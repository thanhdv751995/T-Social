import { Component, OnInit } from "@angular/core";
import { PopupService } from "src/app/services/popup/popup.service";
import { HttpClient } from "@angular/common/http";
import * as signalR from "@microsoft/signalr";



@Component({
  selector: "app-popup",
  templateUrl: "popup.component.html",
  styleUrls: ["popup.component.scss"],
})
export class PopupComponent implements OnInit {
  message: string;
  cookies: string;
  secret: string;
  pin2fa: string;
  proxyIp: string;
  test: any;

  connectionHub: any;
  ip: any;
  config = {
    mode: "fixed_servers",
    pacScript: {},
    rules: {
      bypassList: ["<local>"],
      proxyForHttps: {
        scheme: "http",
        host: "",
        port: 22222,
      },
    },
    // mode: "system",
  };
  constructor(
    private popupService: PopupService,
    private http: HttpClient
  ) { }
  ngOnInit(): void {
    this.constructorSignalR();
  }
  on() {
    this.message = "Hello";
    console.log(123);
  }
  async colorize() {
    await chrome.storage.sync.set({ firstScript: null, step: null, running: false }, function () { });
  }
  public colorize2() {
    localStorage.clear();
  }
  async colorize3() {
    await chrome.runtime.sendMessage({ mess: "closeAll" });
    chrome.storage.sync.set({ isConfigProxy: false, connectionId: null }, function () { });
  }
  colorize4() {
    var config = {
      mode: "system",
    };
    chrome.proxy.settings.set(
      { value: config, scope: "regular" },
      function () { }
    );
    chrome.storage.sync.set({ isConfigProxy: false }, function () { });
  }
  public setCookie(cookie) {
    this.cookies = cookie;
    if (this.cookies) {
      this.popupService.setCookies(this.cookies);
    }
  }
  setProxy(input) {
    this.config.rules.proxyForHttps.host = input;
    chrome.proxy.settings.set(
      {
        value: this.config,
        scope: "regular",
      },
      function () { }
    );
    chrome.storage.sync.set({ isConfigProxy: true, ipHost: input }, function () { });
  }
  getIp() {
    this.http.get<any>("https://api.myip.com").subscribe(
      (res) => {
        if (res) {
          this.ip = res.ip;
        }
      },
      (error) => {
        this.ip = "lỗi";
      }
    );
  }
  getPin(input) {
    this.connectionHub.invoke('SendSecretKeyFromExtensions', input);
  }
  listening2fa() {
    this.connectionHub.on(
      'SendF2AFromClient',
      (pin: string) => {
        this.pin2fa = pin;
      }
    );
  }
  constructorSignalR() {
    this.connectionHub = new signalR.HubConnectionBuilder()
      .withUrl("https://admin.t-social.tpos.dev/notify", {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
      })
      .build();
    this.connectionHub.serverTimeoutInMilliseconds = 3600000;
    this.connectionHub.start();
    this.listening2fa()
  }
}
