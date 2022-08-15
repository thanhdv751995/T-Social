import facebookService from "../shared/scripts/facebook-service";
import mBasicFbService from "./scripts/mbasic-fb-service";

const splitCt = "!@#$%^&*()";
const funcPublic = {
  formatContent(str) {
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
    str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
    str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
    str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
    str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
    str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
    str = str.replace(/Đ/g, "D");
    // Some system encode vietnamese combining accent as individual utf-8 characters
    // Một vài bộ encode coi các dấu mũ, dấu chữ như một kí tự riêng biệt nên thêm hai dòng này
    str = str.replace(/\u0300|\u0301|\u0303|\u0309|\u0323/g, ""); // ̀ ́ ̃ ̉ ̣  huyền, sắc, ngã, hỏi, nặng
    str = str.replace(/\u02C6|\u0306|\u031B/g, ""); // ˆ ̆ ̛  Â, Ê, Ă, Ơ, Ư
    // Remove extra spaces
    // Bỏ các khoảng trắng liền nhau
    str = str.replace(/ + /g, " ");
    str = str.trim();
    // Remove punctuations
    // Bỏ dấu câu, kí tự đặc biệt
    str = str.replace(
      /!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'|\"|\&|\#|\[|\]|~|\$|_|`|-|{|}|\||\\/g,
      " "
    );
    str = str.toLowerCase();
    return str;
  },
  splitContent(str) {
    var str = str.split(" ");
    return str;
  },

  includesContent(arrayContent, content) {
    let result = true;
    for (let index = 0; index < arrayContent.length; index++) {
      if (!content.includes(arrayContent[index])) {
        console.log(
          "Tên nhóm thiếu hoặc không liên quan đến nội dung tìm kiếm : ",
          arrayContent[index]
        );
        result = false;
        break;
      }
    }
    return result;
  },

  receiveScript(data) {
    console.log(data);

    switch (data.type) {
      case 0: {
        // type = Undefined
        break;
      }
      case 1: {
        // type = SurfingNewsfeed
        var time = parseInt(data.value);
        var today = new Date();
        var timeEnd;

        if (time + today.getMinutes() < 60) {
          timeEnd = today.getHours() + ":" + (today.getMinutes() + time);
        } else {
          if (time > 60) {
            for (let i = 0; i < Math.round(time / 60); i++) {
              timeEnd =
                today.getHours() +
                i +
                ":" +
                (today.getMinutes() + time - 60 * i);
            }
          } else {
            timeEnd =
              today.getHours() + 1 + ":" + (today.getMinutes() + time - 60);
          }
        }
        chrome.storage.sync.set({ timeEnd: timeEnd }, function () { });
        setTimeout(() => {
          chrome.runtime.sendMessage({ mess: "openFacebook" });
        }, 1000);
        break;
      }
      case 2: {
        // type = LikeNewsfeed
        // format value
        var char = data.value;
        console.log(char);
        var parts = char.split(splitCt);
        var parts1 = parts[0];
        var parts2 = parts[1];
        chrome.storage.sync.set({ totalLike: parts1 }, function () { });
        setTimeout(() => {
          chrome.runtime.sendMessage({ mess: "openFacebook" });
        }, 1000);
        break;
      }
      case 3: {
        // type = CommentPost
        var char = data.value;
        var parts = char.split(splitCt);
        var parts1 = parts[0]; // id bài viết
        var parts2 = parts[1]; // nội dung bình luận
        console.log(parts1)
        console.log(parts2);
        chrome.storage.sync.set(
          { postId: parts1, contentCmt: parts2, typeName: data.typeName, scriptName: data.scriptName },
          function () { }
        );
        setTimeout(() => {
          chrome.runtime.sendMessage({ mess: "openMbasicFace" });
        }, 1000);
        break;
      }
      case 4: {
        // type = JoinGroupWithURL
        var char = data.value; // url group
        chrome.storage.sync.set(
          { urlGroup: char, typeName: data.typeName, scriptName: data.scriptName },
          function () { }
        );
        setTimeout(() => {
          chrome.runtime.sendMessage({ mess: "openGroup" });
        }, 1000);

        break;
      }
      case 5: {
        // type = ReadNotifications
        var char = data.value; // times notification
        chrome.storage.sync.set(
          { totalNotification: char, typeName: data.typeName, scriptName: data.scriptName },
          function () { }
        );
        setTimeout(() => {
          chrome.runtime.sendMessage({ mess: "openFacebook" });
        }, 1000);
        break;
      }
      case 6: {
        // type = ReadInformationFriend
        chrome.storage.sync.set(
          { typeName: data.typeName, scriptName: data.scriptName },
          function () { }
        );
        setTimeout(() => {
          chrome.runtime.sendMessage({ mess: "openInforFriend" });
        }, 1000);
        break;
      }
      case 7: {
        // type = MakeFriendsSuggestion
        var char = data.value;
        var parts = char.split(splitCt);
        var parts1 = parts[0]; // số lượng kết bạn
        var parts2 = parts[1]; // content search kết bạn
        chrome.storage.sync.set(
          { totalFiendAdd: parts1, contentSearchFriends: parts2, typeName: data.typeName, scriptName: data.scriptName },
          function () { }
        );
        setTimeout(() => {
          chrome.runtime.sendMessage({ mess: "openAddFriend" });
        }, 1000);
        break;
      }
      case 8: {
        // type = AgreeToBeFriends
        var char = data.value;
        console.log(char);
        chrome.storage.sync.set(
          { totalAcceptFriends: char, typeName: data.typeName, scriptName: data.scriptName },
          function () { }
        );
        setTimeout(() => {
          chrome.runtime.sendMessage({ mess: "openAgreetFriend" });
        }, 1000);
        break;
      }
      case 9: {
        // type = SurfWatch
        var time = parseInt(data.value);
        var today = new Date();
        var timeEnd;

        if (time + today.getMinutes() < 60) {
          timeEnd = today.getHours() + ":" + (today.getMinutes() + time);
        } else {
          if (time > 60) {
            for (let i = 0; i < Math.round(time / 60); i++) {
              timeEnd =
                today.getHours() +
                i +
                ":" +
                (today.getMinutes() + time - 60 * i);
            }
          } else {
            timeEnd =
              today.getHours() + 1 + ":" + (today.getMinutes() + time - 60);
          }
        }
        chrome.storage.sync.set({ timeEndWatch: timeEnd }, function () { });
        setTimeout(() => {
          chrome.runtime.sendMessage({ mess: "openFacebookWatch" });
        }, 1000);
        break;
      }
      case 10: {
        // type = LikeVideoWatch
        var char = data.value;
        var parts = char.split(splitCt);
        var parts1 = parts[0];
        var parts2 = parts[1];
        chrome.storage.sync.set({ totalLikeVideo: parts1 }, function () { });
        setTimeout(() => {
          chrome.runtime.sendMessage({ mess: "openFacebookWatch" });
        }, 1000);
        break;
      }
      case 11: {
        // type = SurfStories
        var time = parseInt(data.value);
        chrome.storage.sync.set({ timeSurfStories: time }, function () { });
        setTimeout(() => {
          chrome.runtime.sendMessage({ mess: "openFacebook" });
        }, 1000);
        break;
      }
      case 12: {
        // type = JoinGroup
        var char = data.value;
        var parts = char.split(splitCt);
        var parts1 = parts[0]; // số lượng nhóm sẽ tham gia
        var parts2 = parts[1]; // tên nhóm liên quan sẽ join
        console.log(parts1, parts2);
        chrome.storage.sync.set(
          { totalJoinGrp: parts1, searchGrpName: parts2 },
          function () { }
        );
        setTimeout(() => {
          chrome.runtime.sendMessage({ mess: "openFacebookJoinGrp" });
        }, 1000);
        break;
      }
      case 13: {
        //WatchLivestream
        const randTime = (Math.random() * (15 - 10) + 10).toFixed();
        var char = data.value;
        var parts = char.split(splitCt);
        var parts1 = parts[0]; // url video stream
        var time = parseInt(randTime);// thời gian xem
        var today = new Date();
        var timeEnd;

        if (time + today.getMinutes() < 60) {
          timeEnd = today.getHours() + ":" + (today.getMinutes() + time);
        } else {
          if (time > 60) {
            for (let i = 0; i < Math.round(time / 60); i++) {
              timeEnd =
                today.getHours() +
                i +
                ":" +
                (today.getMinutes() + time - 60 * i);
            }
          } else {
            timeEnd =
              today.getHours() + 1 + ":" + (today.getMinutes() + time - 60);
          }
        }
        chrome.storage.sync.set(
          { urlVideoStream: parts1, timeWatchStream: timeEnd },
          function () { }
        );
        setTimeout(() => {
          chrome.runtime.sendMessage({ mess: "openFacebookWatchStream" });
        }, 1000);
        break;
      } case 14: {
        //read information group
        chrome.storage.sync.set(
          { typeName: data.typeName, scriptName: data.scriptName },
          function () { }
        );
        setTimeout(() => {
          chrome.runtime.sendMessage({ mess: "openInforGroup" });
        }, 1000);
        break;
      }
      case 15: {
        // close chrome done
        break;
      }
      case 16: {
        // type = ReactionPost
        var char = data.value;
        var parts = char.split(splitCt);
        var parts1 = parts[0]; // id bài viết
        var parts2 = parts[2]; // loại hành động(thích , tym , haha ,..)
        chrome.storage.sync.set(
          {
            postId: parts1,
            type: parts2,
            typeName: data.typeName,
            step: "head",
            scriptName: data.scriptName
          },
          function () { }
        );
        setTimeout(() => {
          chrome.runtime.sendMessage({ mess: "openMbasicFace" });
        }, 1000);
        break;
      }
      case 17: {
        // type = PostStatus
        var char = data.value;
        var parts = char.split(splitCt);
        var parts2 = parts[1] // url img
        var parts1 = parts[0]; // content
        chrome.storage.sync.set(
          {
            contentst: parts1,
            urlimg: parts2,
            typeName: data.typeName,
            step: "head",
            scriptName: data.scriptName
          },
          function () { }
        );
        setTimeout(() => {
          chrome.runtime.sendMessage({ mess: "openMbasicFace" });
        }, 1000);
        break;
      }
      case 18: {
        // type = PostGroup
        var char = data.value;
        var parts = char.split(splitCt);
        var parts1 = parts[0]; // url group
        var parts2 = parts[1]; // nội dung đăng bài
        var parts3 = parts[2];// url hình ảnh
        console.log(char);
        chrome.storage.sync.set(
          {
            postId: parts1,
            typeName: data.typeName,
            scriptName: data.scriptName,
            contentgr: parts2,
            urlimg: parts3,
            step: "head"
          },
          function () { }
        );
        setTimeout(() => {
          chrome.runtime.sendMessage({ mess: "openGroupMbasic" });
        }, 1000);
        break;
      }
      case 19: {
        // type = SharePost
        var char = data.value;
        var parts = char.split(splitCt);
        var parts1 = parts[0]; // id bài viết
        var parts2 = parts[1]; // nội dung chia sẻ
        chrome.storage.sync.set(
          {
            postId: parts1,
            contentShare: parts2,
            typeName: data.typeName,
            step: "head",
            scriptName: data.scriptName
          },
          function () { }
        );
        setTimeout(() => {
          chrome.runtime.sendMessage({ mess: "openMbasicFace" });
        }, 1000);
        break;
      }
      case 20: {
        // type = SharePostToGroup

        var char = data.value;
        var parts = char.split(splitCt);
        var parts1 = parts[0]; // id bài viết
        var parts2 = parts[1]; //tên group
        console.log(char);
        console.log(parts1);
        console.log(parts2);
        chrome.storage.sync.set(
          {
            urlPost: parts1,
            nameGroup: parts2,
            typeName: data.typeName,
            scriptName: data.scriptName
            //step: "head",
          },
          function () { }
        );
        setTimeout(() => {
          chrome.runtime.sendMessage({ mess: "openShareGroup" });
        }, 1000);
        break;
      }
      case 21: {
        // type = MessageToPage
        var char = data.value;
        var parts = char.split(splitCt);
        var parts1 = parts[0]; // url page
        var parts2 = parts[1]; //content message
        chrome.storage.sync.set(
          {
            step: "head",
            urlPage: parts1,
            contentMessage: parts2,
            typeName: data.typeName,
            scriptName: data.scriptName
          },
          function () { }
        );
        setTimeout(() => {
          chrome.runtime.sendMessage({ mess: "openPage" });
        }, 1000);
        break;
      }
      case 22: {
        // type = MessageToFriend
        var char = data.value;
        var parts = char.split(splitCt);
        var parts1 = parts[0]; // url friend
        var parts2 = parts[1]; //content message
        chrome.storage.sync.set(
          {
            step: "head",
            urlFriend: parts1,
            contentMessage: parts2,
            typeName: data.typeName,
            scriptName: data.scriptName
          },
          function () { }
        );
        setTimeout(() => {
          chrome.runtime.sendMessage({ mess: "openFriend" });
        }, 1000);
        break;
      }
      default: {
        // do something
      }
    }
  },
  runScript(data) {
    switch (data.type) {
      case 0: {
        // type = Undefined
        break;
      }
      case 1: {
        // type = SurfingNewsfeed
        setTimeout(() => {
          facebookService.surfingNewsfeed();
        }, 1000);
        break;
      }
      case 2: {
        // type = LikeNewsfeed
        // format value
        setTimeout(() => {
          facebookService.likeNewsFeed();
        }, 1000);
        break;
      }
      case 3: {
        // type = CommentPost
        setTimeout(() => {
          chrome.storage.sync.get(["contentCmt"], function (res) {
            mBasicFbService.comments(res.contentCmt);
          });
        }, 1000);
        break;
      }
      case 4: {
        // type = JoinGroupWithURL
        setTimeout(() => {
          facebookService.JoinGroupWithURL();
        }, 1000);
        break;
      }
      case 5: {
        // type = ReadNotifications
        setTimeout(() => {
          facebookService.getNotification();
        }, 1000);
        break;
      }
      case 6: {
        // type = openInformationFriend
        setTimeout(() => {
          facebookService.readInformationFriend();
        }, 1000);
        break;
      }
      case 7: { // bỏ
        // type = MakeFriendsSuggestion
        setTimeout(() => { }, 1000);
        break;
      }
      case 8: {
        // type = AgreeToBeFriends
        setTimeout(() => {
          facebookService.AcceptFriend();
        }, 1000);
        break;
      }
      case 9: {
        // type = SurfWatch
        setTimeout(() => {
          facebookService.SurfWatch();
        }, 1000);
        break;
      }
      case 10: {
        // type = LikeVideoWatch
        setTimeout(() => {
          facebookService.likeVideoWatch();
        }, 1000);
        break;
      }
      case 11: {
        // type = SurfStories
        setTimeout(() => {
          facebookService.SurfStories();
        }, 1000);
        break;
      }
      case 12: {
        // type = JoinGroup
        setTimeout(() => {
          chrome.storage.sync.get(["searchGrpName"], function (res) {
            facebookService.joinGroup(res.searchGrpName);
          });
        }, 1000);
        break;
      }
      case 13: {
        //WatchLivestream
        setTimeout(() => {
          facebookService.watchLiveStream();
        }, 1000);
        break;
      }
      case 14: {
        // read information group
        setTimeout(() => {
          facebookService.groupDataAnalyst();
        }, 1000);
        break;
      }
      case 15: {
        break;
      }
      case 16: {
        // type = ReactionPost
        setTimeout(() => {
          chrome.storage.sync.get(["type", "step"], function (res) {
            mBasicFbService.reaction(res.step, "Bày tỏ cảm xúc", res.type);
          });
        }, 1000);
        break;
      }
      case 17: {

        // type = PostStatus
        setTimeout(() => {
          chrome.storage.sync.get(["contentst", "urlimg", "step"], function (res) {
            mBasicFbService.postImageStatus(res.step, res.urlimg, "aaaa", res.contentst);
          });
        }, 1000);
        break;
      }
      case 18: {
        // type = PostGroup
        setTimeout(() => {
          chrome.storage.sync.get(["postId", "contentgr", "urlimg", "step"], function (res) {
            mBasicFbService.postGroup(res.postId, res.step, res.urlimg, "aaaa", res.contentgr);
          });
        }, 1000);
        break;
      }
      case 19: {
        // type = SharePost
        setTimeout(() => {
          chrome.storage.sync.get(["contentShare", "step"], function (res) {
            mBasicFbService.share(res.contentShare, "Chia sẻ", res.step);
          });
        }, 1000);
        break;
      }
      case 20: {
        // type = SharePostToGroup
        setTimeout(() => {
          chrome.storage.sync.get(["nameGroup"], function (res) {
            facebookService.sharePostInGroup(res.nameGroup);
          });
        }, 1000);
        break;
      }
      case 21: {
        // type = MessageToPage
        setTimeout(() => {
          chrome.storage.sync.get(["step", "contentMessage"], function (res) {
            mBasicFbService.messageToPage(res.step, res.contentMessage);
          });
        }, 1000);
        break;
      }
      case 21: {
        // type = MessageToFriend
        setTimeout(() => {
          chrome.storage.sync.get(["step", "contentMessage"], function (res) {
            mBasicFbService.messageToFriend(res.step, res.contentMessage);
          });
        }, 1000);
        break;
      }
      default: {
        // do something
      }
    }
  },
};

export default funcPublic;
