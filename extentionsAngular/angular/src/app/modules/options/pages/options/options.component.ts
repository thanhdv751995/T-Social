import { HttpClient } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import * as $ from "jquery";
import { ProxyService } from "src/app/services/proxy/proxy.service";
@Component({
  selector: "app-options",
  templateUrl: "options.component.html",
  styleUrls: ["options.component.scss"],
})
export class OptionsComponent implements OnInit {
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
  httpHost;
  httpPort;
  httpsHost;
  httpsPort;
  ipBrowser;
  ipHost;
  ipHost2;
  temp = 0;
  userName;

  isProxySuccess;
  constructor(private http: HttpClient, private proxyService: ProxyService) { }
  ngOnInit(): void {
    setTimeout(() => {
      this.allFunc()
    }, 100);
  }

  allFunc() {
    var isCheck = localStorage.getItem('isCheck');
    var isFirstConfig = localStorage.getItem('isFirstConfig');
    var ipProxy = localStorage.getItem('ipProxy');
    if (isCheck == 'true') {
      console.log(isCheck);
      if (isFirstConfig == 'true') {
        console.log(ipProxy);
        this.checkLocal2(ipProxy);
      }
      else {
        this.checkLocal();
      }
    } else {
      console.log('false');
      chrome.storage.sync.get(['isFirstConfig', 'ipHost'], function (res) {
        localStorage.setItem('isCheck', 'true');
        localStorage.setItem('isFirstConfig', res["isFirstConfig"])
        localStorage.setItem('ipProxy', res["ipHost"])
        chrome.runtime.sendMessage({ mess: "openGoogle" });
      });
    }
  }

  checkLocal() {
    const cat = localStorage.getItem('status');
    if (cat) {
      console.log(cat);
      if (cat == "true") {
        this.isProxySuccess = -1;
        this.getIP();
      } else {
        this.isProxySuccess = 1;
        this.userName = localStorage.getItem('userName');
        this.ipHost = localStorage.getItem('ipHost');
        setTimeout(() => {
          this.proxyService.updateActiveProxyConfig(this.userName, this.ipHost).subscribe(
            (res) => {
              if (res) {
                chrome.storage.sync.set({ 'isConfigProxy': true, 'isFirstConfig': true, running: true }, function () { });
                localStorage.clear();
                chrome.runtime.sendMessage({ mess: "openLoginFacebook" });
              }
            }
          );
        }, 1000);
      }
    } else {
      this.isProxySuccess = 0;
      this.getIpProxy();
    }
  }
  checkLocal2(input) {
    const cat = localStorage.getItem('status');
    if (cat) {
      console.log(cat);
      if (cat == "true") {
        this.isProxySuccess = -1;
        this.getIP();
      } else {
        this.isProxySuccess = 1;
        chrome.storage.sync.set({ isConfigProxy: true, isFirstConfig: true }, function () { });
        localStorage.clear();
        chrome.runtime.sendMessage({ mess: "openLoginFacebook" });
      }
    } else {
      this.isProxySuccess = 0;
      this.getIpProxy2(input);
    }
  }
  auth() {
    return {
      authCredentials: {
        username: "yuri",
        password: "pAss1234567890",
      },
    };
  }
  getIpProxy() {
    setTimeout(() => {
      this.proxyService.getProxyConfig().subscribe(
        (res) => {
          if (res) {
            console.log(res);
            this.config.rules.proxyForHttps.host = res.proxyIp;
            this.ipHost = res.proxyIp;
            this.ipHost2 = res.proxyIp;
            this.userName = res.userName;
            this.setIpProxy();
            chrome.storage.sync.set({ 'nameFacebook': res.nameFacebook, 'userName': res.userName, 'clientId': res.clientId, 'password': res.password, 'accesToken': res.accesToken, 'ipHost': res.proxyIp, 'isGetProxy': true, 'f2fa': res.f2FA }, function () { });
            localStorage.setItem('userName', res.userName);
            localStorage.setItem('ipHost', res.proxyIp);
            localStorage.setItem('clientId', res.clientId);
          }
        },
        (error) => {
          console.log(error);
        }
      );
    }, 1000);
  }
  getIpProxy2(input) {
    this.ipHost = input;
    this.config.rules.proxyForHttps.host = input;
    this.setIpProxy();
  }
  setIpProxy() {
    setTimeout(() => {
      chrome.proxy.settings.set(
        {
          value: this.config,
          scope: "regular",
        },
        function () { }
      );
      localStorage.setItem('status', 'true');
      setTimeout(() => {
        chrome.runtime.sendMessage({ mess: "openGoogle" });
      }, 1000);
    }, 1000);
  }
  setAuthProxy() {
  }
  getIP() {
    setTimeout(() => {
      localStorage.setItem('status', 'false');
      setTimeout(() => {
        chrome.runtime.sendMessage({ mess: "reload" });
      }, 1000);
      this.http.get<any>("https://api.myip.com").subscribe((res) => {
        if (res) {
          this.ipBrowser = this.ipHost2;
          // localStorage.setItem('ipHost',this.ipBrowser);
          chrome.storage.sync.get(['ipHost'], function (res) {
            let cat = localStorage.getItem('ipHost');
            let temp = res["ipHost"];
            console.log(temp);
            console.log(cat);
            // if (cat==temp) {
            //   localStorage.setItem('status','false');
            //   setTimeout(() => {
            //     chrome.runtime.sendMessage({mess: "openGoogle"});
            //   }, 1000);
            // }
            localStorage.setItem('status', 'false');
            setTimeout(() => {
              chrome.runtime.sendMessage({ mess: "openGoogle" });
            }, 1000);
          })
        }
      },
        (error) => {
          console.log(error);
        });
    }, 1000);
  }
}
