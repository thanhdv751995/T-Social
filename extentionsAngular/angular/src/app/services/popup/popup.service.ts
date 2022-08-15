import { NONE_TYPE } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { ShareServiceService } from '../share-service.service';

declare const chrome: any;

@Injectable({
  providedIn: 'root'
})
export class PopupService {

  constructor(private shareService: ShareServiceService) { }

  setCookies(cookie): Promise<any> {
    let promise = new Promise(() => {
      var arr = cookie.split("|");
      if (arr.length > 2) {
        for (var i = 0; i < arr.length; i++) {
          try {
            if (arr[i].indexOf('c_user') > -1) {
              cookie = arr[i];
            }
          } catch (ex) {
          }
        }
      }
      if (!chrome.cookies) {
        chrome.cookies = chrome.experimental.cookies;
      }
      var removeCookie = function (cookie) {
        var url = "http" + (cookie.secure ? "s" : "") + "://" + cookie.domain + cookie.path;
        chrome.cookies.remove({ "url": url, "name": cookie.name });
      };
      chrome.cookies.getAll({ domain: "facebook.com" }, function (all_cookies) {
        for (var i = 0; i < count; i++) {
          removeCookie(all_cookies[i]);
        }
        // callback();
        var ca = cookie.split(';');
        var count = all_cookies.length;
        // debugger
        //  var expires = ";domain=.facebook.com;expires="+ d.toUTCString();
        for (var i = 0; i < ca.length; i++) {
          try {

            var name = ca[i].split('=')[0].trim();
            var val = ca[i].split('=')[1].trim();
            if(name == 'c_user'){
              chrome.storage.sync.set({userName: val}, function(){});
            }
            chrome.cookies.set({ url: "https://www.facebook.com", name: name, value: val });
            chrome.cookies.set({ url: "https://mbasic.facebook.com", name: name, value: val });
          } catch (ex) {
            console.log(ex);
          }
        }
        // chrome.tabs.getSelected(null, function (tab) {
        //   var code = 'window.location.reload();';
        //   chrome.tabs.executeScript(tab.id, { code: code });
        // });
      });
      return "COOKIES_CLEARED_VIA_EXTENSION_API";
    });

    return promise;
  }



}
