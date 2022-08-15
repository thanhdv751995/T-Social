// const local = "https://admin.t-social.tpos.dev";
var message = "thành công rồi nè";
var token = "EAABwzLixnjYBAKIVnViLFKu0mCxqkhik25ZAH9qcaojoPoJ0ZA4OVjAFeWrtVwrOUEZCHXaZAytemirOHjtgjhmSROqUgbztFdqoKHxxnRMrWE8ZBsny3hTHYldaMnwHLuqYd0ZCn48ZB8r1pZCHVrUJ4yeiWZC7KlVEzEaH2LLTZBRWNgHuCRtM6e9hkchmix9hQZD";
import { SSL_OP_SINGLE_DH_USE } from "constants";
import httpRequestService from "./http-request-service";
import funcPublic from "../shared/funcPublic"
import { url } from "./environment";

const local = url;


const proxyService = {
  // lấy danh sách kịch bản đang active
  getProxyUsingScript: () => {
    var url = local + "/api/client/Get-List-Acount-Active";
    var onload = function (e) {
      if (this.status == 200) {
        console.log("response", this.response); // JSON response
      }
    };
    httpRequestService.requestService(null, "GET", url, onload, false);
  },
  // gửi dữ liệu bài viết khi react xong lên server
  postClientLog: (proxyIp, body) => {
    let data = { proxyIP: proxyIp, body: body };
    var url = "https://8e41-14-186-89-123.ngrok.io/api/kafka/Producer-MQ-Log";
    httpRequestService.requestService(data, "POST", url, null, false);
  },
  postClientActivity: (userName, content, Url, scriptName) => {
    let data = {
      userName: userName,
      content: content,
      url: Url,
      scriptName: scriptName
    };
    var url = local + "/api/activity/Create";
    httpRequestService.requestService(data, "POST", url, null, false);
  },
  //thêm dữ liệu từ thông báo vào database
  postNotification: (idUser, content, urlAvatar, time) => {
    let data = {
      idUser: idUser,
      content: content,
      urlAvatar: urlAvatar,
      time: time,
      href: ''
    };
    var url1 = local + "/api/notification/Create";
    httpRequestService.requestService(data, "POST", url1, null, false);
  },
  //add data to table group
  postGroup: (userName, groupName, avatarGroup, groupUrl, content) => {
    let data = {
      userName: userName,
      groupName: groupName,
      avatarGroup: avatarGroup,
      groupUrl: groupUrl,
      content: content
    };
    var url = local + "/api/group-join/Create";
    httpRequestService.requestService(data, "POST", url, null, false);
  },
  // add friends to table clientFriends
  postFriend: (idUser, userName, friendName, avatarUrl) => {
    let data = {
      idUser: idUser,
      userName: userName,
      friendName: friendName,
      avatarUrl: avatarUrl,
    };
    var url = local + "/api/friend/Create";
    httpRequestService.requestService(data, "POST", url, null, false);
  },
  // post connection
  postConnection: (connectionId, clientId) => {
    let data = {
      connectionId: connectionId,
      groupName: clientId
    };
    var url = local + "/api/signalr/add-connection-to-group";
    httpRequestService.requestService(data, "POST", url, null, false);
  },
  //remove connection
  removeConnection: (connectionId, clientId) => {
    let data = {
      connectionId: connectionId,
      groupName: clientId
    };
    var url = local + "/api/signalr/remove-connection-to-group";
    httpRequestService.requestService(data, "POST", url, null, false);
  },
  // kiểm tra kịch bản ưu tiên từ server
  pingIsChangeScript: (username: any, ipScript) => {

    console.log(username, ipScript);

    var url = local + "/api/client-using-script/First-Script-Client-Using?username=" + username;
    var onload = function (e) {
      if (this.status == 200) {
        let resId = JSON.parse(this.response);
        if (ipScript != resId.id) {
          chrome.storage.sync.set({ isChangeScript: true }, function () { });
          console.log('có kịch bản ưu tiên !');
        }
      }
    };
    httpRequestService.requestService(null, "GET", url, onload, false);
  },
  // lấy kịch bản theo proxyIp
  getFirstScript: (username: any) => {
    console.log("vào scrip");
    var url = local + "/api/client-using-script/First-Script-Client-Using?username=" + username;
    var onload = function (e) {
      if (this.status == 200) {
        var res = JSON.parse(JSON.stringify(this.response));
        console.log('kịch bản đầu tiên', res);
        chrome.storage.sync.set({ firstScript: res }, function () { });
        chrome.storage.sync.set({ running: true }, function () { });
        setTimeout(() => {
          funcPublic.receiveScript(res);
        }, 1000);
      }
      if (this.status == 204) {
        chrome.storage.sync.set({ firstScript: null }, function () { });
        console.log("chưa có kịch bản đầu tiên , chuyển sang lắng nghe bằng realtime");
      }
    };
    httpRequestService.requestService(null, "GET", url, onload, false);
  },
  // cập nhật trạng thái kịch bản khi hoàn thành
  updateActiveByScriptIdProxyIp: (scriptId, clientId) => {
    console.log('done');
    const data = {
      scriptId: scriptId,
      clientId: clientId
    };
    var url = local + "/api/client-using-script/Update-Active-By-ScriptId-ClientId";
    httpRequestService.requestService(data, "PUT", url, onload, true);
  },

  updateErrorDetail: (scriptId: any, clientId: any) => {
    var url = local + "/api/client-using-script/Update-Error-Detail?scriptId=" + scriptId + "&clientId=" + clientId;
    httpRequestService.requestService(null, "PUT", url, onload, true);
  },
  postCommentFacebook: () => {
    let data = { "message": message };
    var url = "https://graph.facebook.com/471761851275909/comments"; //471761851275909 id bài viết
    var onload = function (e) {
      if (this.status == 200) {
        console.log(this.response);
      }
    };
    httpRequestService.requestService(data, "POST", url, onload, true);
  },
  postStatusFacebook: () => {
    let data = { "message": message };
    var url = "https://graph.facebook.com/me/feed";
    var onload = function (e) {
      if (this.status == 200) {
        console.log(this.response);
      }
    };
    httpRequestService.requestService(data, url, "POST", onload, true);
  },
  pingClientExtension: (clientId) => {
    let data = { clientId: clientId };
    var url = local + "/api/ping-client-extension/create-update-ping-client-extension";
    var onload = function (e) {
      if (this.status == 200) {
        console.log(this.response);
      }
    };
    httpRequestService.requestService(data, "POST", url, onload, false);
  },
};

export default proxyService;