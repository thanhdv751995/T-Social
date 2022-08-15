import facebookService from "../../shared/scripts/facebook-service";
import mBasicFbService from "../../shared/scripts/mbasic-fb-service";
import proxyService from "../../shared/proxy-shared-service";
import facebookGraphService from "../../shared/facebook/facebook-graph-api";
import * as signalR from "@microsoft/signalr";
import funcPublic from "../../shared/funcPublic";
import connection from "../../shared/funcPublic";
import signalRService from "../../shared/signalR/signalR-service";
import * as $ from "jquery";
import httpRequestService from "../../shared/http-request-service";

const checkFirstUrl = window.location.href;
const faceURL = "https://www.facebook.com/";
const basicFaceUrl = "https://mbasic.facebook.com/";
const watchUrl = "https://www.facebook.com/watch";
const loginURL = "https://www.facebook.com/login";
const googleURL = "https://www.google.com/";
const checkPoint = "checkpoint";
const urlSearchGroup = "https://www.facebook.com/search/groups";
const urlMakeFriends = "https://www.facebook.com/search/people";
const actionBasic = ["Thích", "Bày tỏ cảm xúc", "Bình luận", "Chia sẻ"];
const urlImg = "https://scontent.fsgn13-2.fna.fbcdn.net/v/t39.30808-6/260099275_634307350915857_3727391928067221823_n.jpg?_nc_cat=106&ccb=1-5&_nc_sid=8bfeb9&_nc_ohc=zflTn0RBYJgAX8uzbcQ&_nc_ht=scontent.fsgn13-2.fna&oh=00_AT-tnKfCdYe2dtCFQsKpPZsl6LgKoOZT1G8NPu_wHq5FBw&oe=62472A67";
var ipHost;
var urlGroup = "https://www.facebook.com/groups/321411216610827/";
const t =
  "sb=VXzmYd926KRu90KZWg2garhS; datr=VnzmYc8aQFL17X8dtjV9mskp; wd=1920x969; c_user=100005047205190; xs=30%3ATSNoOgI1UjT6kA%3A2%3A1645155348%3A-1%3A6187%3A%3AAcWW8RVNtgLY32CyurJWft-D-me5zVJ13jmgpBqSiHk; fr=0HJkAojU4xA5mRIix.AWWwhO6-dASW9Tuot9c6RYbkWL8.BiHtFx.V-.AAA.0.0.BiHtLE.AWXdiE1HZhI; presence=C%7B%22t3%22%3A%5B%5D%2C%22utc3%22%3A1646187990661%2C%22v%22%3A1%7D";
//   function modifyResponse(response) {
//     var original_response, modified_response;
//     console.log('original_response')
//     if (this.readyState === 4) {
//         // we need to store the original response before any modifications
//         // because the next step will erase everything it had
//         original_response = response.target.responseText;
//         console.log('original_response',original_response)
//     }
// }
//   (function(open) {
//     console.log('XMLHttpRequest open')
//     XMLHttpRequest.prototype.open = (method: string, url: any , async?: boolean, username?: string, password?: string) => {
//      console.log('XMLHttpRequest')
//       this.addEventListener("readystatechange", modifyResponse);
//       open.call(this, method, url, async, username, password);
//     };
//   })(XMLHttpRequest.prototype.open);  

checkProxy();
function checkProxy() {
  chrome.storage.sync.get(
    ["isConfigProxy", "userName", "running", "firstScript"],
    function (res) {
      if (res.isConfigProxy == true) {
        if (checkFirstUrl == loginURL) {
          chrome.storage.sync.get(["userName", "password"], function (res) {
            if (res) {
              console.log(res);
              setTimeout(() => {
                facebookService.loginFace(res.userName, res.password);
              }, 2000);
            }
          });
        }
        if (checkFirstUrl == googleURL) {
          setTimeout(() => {
            chrome.runtime.sendMessage({ mess: "openFacebook" });
          }, 2000);
        }
        if (checkFirstUrl.includes(checkPoint)) {
          chrome.storage.sync.get(["f2fa", "userName"], function (res) {
            if (res) {
              signalRService.listen2Fa();
              setTimeout(() => {
                signalRService.get2Fa(res.f2fa, res.userName);
              }, 1000);
            }
          });
        } else {
          signalRService.listeningScript();
          console.log("running: ",res.running);
              
          if (!res.running == true) {
            setTimeout(() => {
              console.log("lay kich ban dau tien");
              proxyService.getFirstScript(res.userName);
            }, 1000);
            // const randTime = (Math.random() * (300000 - 240000) + 240000).toFixed();
            // setTimeout(() => {
            //   chrome.runtime.sendMessage({ mess: "openFacebook" });
            // }, parseInt(randTime));
          } else {
            if (res.firstScript != null) {
             
              console.log("running");
              console.log(res.firstScript);
              setTimeout(() => {
                funcPublic.runScript(res.firstScript);
              }, 1000);
            }
          }
        }
      } else {
        setTimeout(() => {
          chrome.runtime.sendMessage({ mess: "openOptions" });
        }, 1000);
      }
    }
  );
}

function test() {

  setTimeout(() => {
    //facebookService.JoinGroupWithURL();
     facebookService.joinGroup("học sinh");
  }, 2000);
}
setTimeout(() => {
 //  test();
}, 2000);

setInterval(() => {
  facebookService.checkClientOnline();
}, 100000);

chrome.runtime.onMessage.addListener((request, sender, respond) => { });
