import funcPublic from "../funcPublic";
import * as signalR from "@microsoft/signalr";
import facebookService from "../../shared/scripts/facebook-service";
import proxyService from "../proxy-shared-service"
import { url } from "../environment";
const local = url + "/notify"
const hub = new signalR.HubConnectionBuilder()
  .withUrl(local, {
    skipNegotiation: true,
    transport: signalR.HttpTransportType.WebSockets
  })
  .withAutomaticReconnect([0, 0, 10000])
  .build();
hub.serverTimeoutInMilliseconds = 600000;
hub.start();
const checkFirstUrl = window.location.href;

const signalRService = {
  listeningScript: () => {
    hub.on("NewScript", (dto: any) => {
      console.log("New Script")
      
      chrome.storage.sync.get(["userName","scriptId"], function (res) {
        if (res.userName == dto.username) {
          if (dto.type == 15) {
            console.log("closeChrome");
            signalRService.reqUpdateScript(dto.scriptId, dto.clientId);
            const randTime = (Math.random() * (3000 - 1000) + 1000).toFixed();
            setTimeout(() => {
              chrome.storage.sync.set({ running: false, connectionId: null });
              //  chrome.storage.sync.set({ isConfigProxy: false, running: false, connectionId: null });
              chrome.runtime.sendMessage({ mess: "closeAll" });
            }, parseInt(randTime));
          }
          else {
            if(dto.scriptId != res.scriptId){
              chrome.storage.sync.set({ running: true });
              console.log("realtime");
              signalRService.inputRealTime(dto);
            }
            chrome.storage.sync.set({ scriptId: dto.scriptId });
          }
        }
      });
    });
    hub.on("RemoveConnectionId", (connectionID: any) => {
      chrome.storage.sync.get(["clientId"], function (res) {
        if (res.clientId) {
          proxyService.removeConnection(connectionID, res.clientId);
        }
      });
    });
    hub.on("UpdateScript", (username: any) => {
      chrome.storage.sync.get(["userName", "firstScript", "clientId"], function (res) {
        if (res.userName == username) {
          if (res.firstScript != null) {
            console.log("update");
            let scriptId = res.firstScript.scriptId;
            let clientId = res.clientId;
            signalRService.reqUpdateScript(scriptId, clientId);
          }
        }
      });
    });
    hub.on("GetIdConnection", (connectionId: any) => {
      chrome.storage.sync.get(["clientId"], function (res) {
        if (res.clientId) {
          if (checkFirstUrl.includes('facebook')) {
            proxyService.postConnection(connectionId, res.clientId);
          }
          console.log('clientId', res.clientId);
        }
      });
    });
    hub.on("CheckExceptionCloseChrome", (clientId: any) => {
      chrome.storage.sync.get(["clientId"], function (res) {
        if (res.clientId) {
          proxyService.pingClientExtension(clientId);
          console.log('clientId', clientId);
        }
      });
    });
  },
  reqUpdateScript: (scriptId, clientId) => {
    chrome.storage.sync.set(
      {
        firstScript: null,
        timeEnd: null,
        totalLike: null,
        timeSurfStories: null,
        timeEndWatch: null,
        totalLikeVideo: null,
        step: null,
        nameGroup: null,
        urlPost: null,
        totalJoinGrp: null,
        searchGrpName: null,
        timeWatchStream: null,
        totalFiendAdd: null,
        contentMessage: null,
        urlPage: null,
        urlFrriend: null,
        totalNotification: null
      },
      function () { }
    );
    //hub.invoke("UpdateClientUsingScript", scriptId, userName);
    proxyService.updateActiveByScriptIdProxyIp(scriptId, clientId)
  },

  inputRealTime: (input: any) => {
    console.log(input);
    setTimeout(() => {
      chrome.storage.sync.set({ firstScript: input }, function () { });
      funcPublic.receiveScript(input);
    }, 1000);
  },

  get2Fa: (secretKey, userName) => {
    console.log(secretKey);
    hub.invoke("SendSecretKeyFromExtensions", secretKey, userName);
  },

  listen2Fa: () => {
    hub.on("SendF2AFromClient", (key: any, username: any) => {
      chrome.storage.sync.get(["userName"], function (res) {
        if (username == res.userName) {
          facebookService.set2Fa(key);
        }
      })
    });
  },


  // postClientActivity: (userName, content, Url, scriptName) => {
  //   hub.invoke("PostClientActivity", userName, content, Url, scriptName);
  // },

  // postNotification: (idUser, content, avatar, time) => {
  //   hub.invoke("PostNotification", idUser, content, avatar, time);
  // },

  // postGroup: (userName, groupName, avatarGroup, groupUrl, content) => {
  //   hub.invoke("PostGroup", userName, groupName, avatarGroup, groupUrl, content);
  // },
};
export default signalRService;
