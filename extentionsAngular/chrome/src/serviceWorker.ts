import signalRService from "../../shared/signalR/signalR-service";

var members = "members";
chrome.runtime.onMessage.addListener(function (request, sender, sendResponse) {
  if (!chrome.runtime.lastError) {
    setTimeout(function () {
      sendResponse({ status: true });
    }, 1);
    if (request.mess == "openOptions") {
      setTimeout(() => {
        chrome.tabs.query(
          { active: true, currentWindow: true },
          function (tabs) {
            chrome.runtime.openOptionsPage();
            chrome.tabs.remove(tabs[0].id);
          }
        );
      }, 500);
    }

    if (request.mess == "closeAll") {
      //  offProxy();
      setTimeout(() => {
        chrome.tabs.query(
          { active: true, currentWindow: true },
          function (tabs) {
            tabs.forEach((element) => {
              chrome.tabs.remove(element.id);
            });
          }
        );
      }, 1000);
    }
    if (request.mess == "offProxy") {
      offProxy();
    }

    if (request.mess == "openGoogle") {
      console.log("google");
      let myNewUrl = `https://www.google.com/`;
      openUrl(myNewUrl);
    }

    if (request.mess == "openLoginFacebook") {
      console.log("facebookLogin");
      let myNewUrl = `https://www.facebook.com/login`;
      openUrl(myNewUrl);
    }

    if (request.mess == "openFacebook") {
      console.log("facebook");
      let myNewUrl = `https://www.facebook.com/`;
      openUrl(myNewUrl);
    }

    if (request.mess == "openAgreetFriend") {
      let myNewUrl = `https://www.facebook.com/friends`;
      openUrl(myNewUrl);
    }
    if (request.mess == "openFacebookWatch") {
      console.log("facebookWatch");
      let myNewUrl = `https://www.facebook.com/watch/?ref=tab`;
      openUrl(myNewUrl);
    }
    if (request.mess == "openInforGroup") {
      console.log("openInforGroup");
      let myNewUrl = `https://www.facebook.com/groups/feed/`;
      openUrl(myNewUrl);
    }
    if (request.mess == "openFacebookWatchStream") {
      console.log("facebookWatchStream");
      chrome.storage.sync.get(["urlVideoStream"], function (res) {
        if (res.urlVideoStream) {
          if (res.urlVideoStream.includes("facebook.com")) {
            let myNewUrl = res.urlVideoStream;
            openUrl(myNewUrl);
          } else {
            let myNewUrl = "https://www.facebook.com/" + res.urlVideoStream;
            openUrl(myNewUrl);
          }
        }
      });
    }
    // scripts type = 21
    if (request.mess == "openPage") {
      console.log("Message To Page");
      chrome.storage.sync.get(["urlPage"], function (res) {
        if (res.urlPage) {
          if (res.urlPage.includes("facebook.com")) {
            let myNewUrl = res.urlPage.replace("www", "mbasic")
            openUrl(myNewUrl);
          } else {
            let myNewUrl = "https://mbasic.facebook.com/" + res.urlPage;
            openUrl(myNewUrl);
          }
        }
      });
    }
    // scripts type = 22
    if (request.mess == "openFriend") {
      console.log("Message To Friend");
      chrome.storage.sync.get(["urlFriend"], function (res) {
        if (res.urlFriend) {
          if (res.urlFriend.includes("facebook.com")) {
            let myNewUrl = res.urlFriend.replace("www", "mbasic")
            openUrl(myNewUrl);
          } else {
            let myNewUrl = "https://mbasic.facebook.com/" + res.urlFriend;
            openUrl(myNewUrl);
          }
        }
      });
    }
    // scripts type = 12
    if (request.mess == "openFacebookJoinGrp") {
      console.log("searchGroup");
      chrome.storage.sync.get(
        ["totalJoinGrp", "searchGrpName"],
        function (res) {
          if (res.searchGrpName) {
            let myNewUrl =
              "https://www.facebook.com/search/groups?q=" + res.searchGrpName;
            openUrl(myNewUrl);
          }
        }
      );
    }
    // scripts type = 6
    if (request.mess == "openInforFriend") {
      console.log("openInforFriend");
      chrome.storage.sync.get(
        ["userName"],
        function (res) {
          if (res.userName) {
            let myNewUrl =
              "https://www.facebook.com/" + res.userName + "/friends";
            openUrl(myNewUrl);
          }
        }
      );
    }
    if (request.mess == "openMbasicFace") {
      console.log("openMbasicFace");
      chrome.storage.sync.get(["postId", "type", "typeName"], function (res) {
        if (
          res.typeName == "ReactionPost" ||
          res.typeName == "CommentPost" ||
          res.typeName == "PostGroup" ||
          res.typeName == "SharePost"
        ) {
          let postId = res.postId;
          console.log('postId', postId);
          if (postId.includes("facebook.com")) {
            postId = postId.replace("www", "mbasic")
            let myNewUrl = postId;
            openUrl(myNewUrl);
          } else {
            let myNewUrl = "https://mbasic.facebook.com/" + postId;
            openUrl(myNewUrl);
          }

          if(res.typeName == "CommentPost" && postId.includes("videos")){
            var splitUrl = postId.split('/');
            console.log(splitUrl);
            let myNewUrl = "https://mbasic.facebook.com/" + "story.php?story_fbid=" + splitUrl[5] +"&id=" + splitUrl[3];
            openUrl(myNewUrl);
          }

        } else if (res.typeName == "PostStatus") {
          let myNewUrl = "https://mbasic.facebook.com/me";
          openUrl(myNewUrl);
        }
      });
    }

    if (request.mess == "openGroupMbasic") {
      console.log("openGroupMbasic");
      chrome.storage.sync.get(["postId", "type", "typeName"], function (res) {
        let postId = res.postId;
        if (postId.includes("facebook.com/groups")) {
          postId = postId.replace("www", "mbasic")
          let myNewUrl = postId;
          openUrl(myNewUrl);
        } else {
          let myNewUrl = "https://mbasic.facebook.com/groups/" + postId;
          openUrl(myNewUrl);
        }
      });
    }

    if (request.mess == "openGroup") {
      console.log("openGroup");
      chrome.storage.sync.get(["urlGroup"], function (res) {
        let url = res.urlGroup;
        if (url.includes("facebook.com/groups")) {
          url = url.replace("mbasic", "www")
          let myNewUrl = url+"/about";
          openUrl(myNewUrl);
        } else {
          let myNewUrl = "https://www.facebook.com/groups/" + url +"/about";
          openUrl(myNewUrl);
        }
      });
    }

    if (request.mess == "openShareGroup") {
      console.log("openShareGroup");
      chrome.storage.sync.get(["urlPost", "type", "typeName"], function (res) {
        let urlPost = res.urlPost;
        if (urlPost.includes("facebook.com")) {
          urlPost = urlPost.replace("mbasic", "www")
          let myNewUrl = urlPost;
          openUrl(myNewUrl);
        } else {
          let myNewUrl = "https://www.facebook.com/" + urlPost;
          openUrl(myNewUrl);
        }
      });
    }
    if (request.mess == "openAddFriend") {
      console.log("addFriend");
      // var result = '';
      // var characters = 'abcdefghijklmnopqrstuvwxyz';
      // var charactersLength = characters.length;
      //  result = characters.charAt(Math.floor(Math.random() * charactersLength));

      const names = [
        "thanh",
        "luc",
        "hoa",
        "trung",
        "mai",
        "ky",
        "long",
        "lan",
        "minh",
        "phuong",
        "dao",
        "duy",
        "tuan",
        "vinh",
        "nam",
        "hung",
      ];

      const randomName = names[Math.floor(Math.random() * names.length)];
      chrome.tabs.query({ active: true, currentWindow: true }, function (tabs) {
        //Your code below...
        let myNewUrl = `https://www.facebook.com/search/people/?q=${randomName}`;
        //Update the url here.
        setTimeout(() => {
          // chrome.storage.sync.set({ 'urlAddFriend': myNewUrl }, function () { });
          openUrl(myNewUrl);
        }, 1000);
      });
    }
    if (request.mess == "acceptFriend") {
      console.log("acceptFriend");
      chrome.tabs.query({ active: true, currentWindow: true }, function (tabs) {
        //Your code below...
        let myNewUrl = `https://www.facebook.com/friends`;
        //Update the url here.
        setTimeout(() => {
          openUrl(myNewUrl);
        }, 1000);
      });
    }
    if (request.mess == "reload") {
      console.log("reload");
      chrome.tabs.query({ active: true, currentWindow: true }, function (tabs) {
        chrome.tabs.update(tabs[0].id, { url: tabs[0].url });
      });
    }
    if (request.mess == "onProxy") {
      chrome.storage.sync.get(["ipHost"], function (res) {
        if (res.ipHost) {
          setTimeout(() => {
            onProxy(res.ipHost);
          }, 100);
        }
      });
    }
  }
});

function openUrl(url) {
  chrome.tabs.query({ active: true, currentWindow: true }, function (tabs) {
    setTimeout(() => {
      chrome.tabs.update(tabs[0].id, { url: url });
    }, 1000);
  });
}

function onProxy(ip) {
  var config = {
    mode: "fixed_servers",
    pacScript: {},
    rules: {
      bypassList: ["<local>"],
      proxyForHttps: {
        scheme: "http",
        host: ip,
        port: 22222,
      },
    },
  };
  chrome.proxy.settings.set(
    {
      value: config,
      scope: "regular",
    },
    function () { }
  );
}

function offProxy() {
  var config = {
    mode: "system",
  };
  chrome.proxy.settings.set(
    { value: config, scope: "regular" },
    function () { }
  );
}
function authProxy() {
  return {
    authCredentials: {
      username: "yuri",
      password: "pAss1234567890",
    },
  };
}
