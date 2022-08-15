import { HttpClient } from "@microsoft/signalr";
import * as $ from "jquery";
import facebookService from "../../shared/scripts/facebook-service";
import proxyService from "../proxy-shared-service"
import signalRService from "../signalR/signalR-service";


const checkCurrentUrl = window.location.href;
const inputCmtId = "composerInput";
const inputShareContent = "xc_message";
const min = 10000;
const max = 20000;
const ranDom = [true, false, true];
var urlGroup = "";
var countGroup = 0;

const mBasicFbService = {

  // cảm xúc

  reaction: (step, action, type) => {
    $(document).ready(function () {
      if (step == 'head') {
        mBasicFbService.clickAction(action);
      }
      if (step == "next") {
        let btnReaction = document.getElementsByTagName("a");
        setTimeout(() => {
          console.log(btnReaction);
          for (let index = 0; index < btnReaction.length; index++) {
            if (btnReaction[index].innerText.includes(type)) {
              // chrome.storage.sync.set({ typeName: null, step: null }, function () { });
              chrome.storage.sync.get(["firstScript", "clientId", "postId", "userName", "typeName", "scriptName"], function (res) {
                proxyService.postClientActivity(res.userName, res.typeName, res.postId, res.scriptName);
                signalRService.reqUpdateScript(
                  res.firstScript.scriptId,
                  res.clientId
                );
              });
              console.log(btnReaction[index]);
              setTimeout(() => {
                btnReaction[index].click();
              }, 500);
              break;
            }
          }
        }, 1000);
      }
    });
  },

  // bình luận

  comments: (content) => {
    $(document).ready(function () {
      let inputComment = document.getElementById(inputCmtId) as HTMLInputElement;
      console.log(inputComment);
      if(inputComment == null){ 
        chrome.storage.sync.get(["firstScript", "clientId"], function (res) {
          proxyService.updateErrorDetail(res.firstScript.scriptId,res.clientId);
          console.log("bài viết này không bình luận được!!!");
          setTimeout(() => {
            signalRService.reqUpdateScript(
              res.firstScript.scriptId,
              res.clientId
            );
            
          }, 1000);
        });
      }
      else{
        let btnCmt = document.getElementsByTagName("input");
        setTimeout(() => {
          inputComment.scrollIntoView({ behavior: "smooth", block: "center", inline: "center" });
          inputComment.value = content;
          console.log(inputComment);
          for (let index = 0; index < btnCmt.length; index++) {
            if (btnCmt[index].getAttribute("value") == "Bình luận") {
              console.log(btnCmt[index]);
              //  chrome.storage.sync.set({ typeName: null }, function () { });
              chrome.storage.sync.get(["firstScript", "clientId", "contentCmt", "typeName", "postId", "userName", "scriptName"], function (res) {
                var content = res.typeName + "!@#$%^&*()" + res.contentCmt;
                proxyService.postClientActivity(res.userName, content, res.postId, res.scriptName);
                signalRService.reqUpdateScript(
                  res.firstScript.scriptId,
                  res.clientId
                );
              });
              setTimeout(() => {
              //  btnCmt[index].click();
              }, 500);
              break;
            }
          }
        }, 100);
      }
    });
  },

  //chia sẻ

  share: (content, action, step) => {
    if (step == 'head') {
      mBasicFbService.clickAction(action);
    }
    if (step == "next") {
      let textarea = document.getElementsByTagName("textarea")[0];
      setTimeout(() => {
        textarea.value = content;
        console.log(textarea);
        let btnShare = document.getElementsByTagName("input");
        setTimeout(() => {
          for (let index = 0; index < btnShare.length; index++) {
            if (btnShare[index].getAttribute("value") == "Chia sẻ") {
              console.log(btnShare[index]);
              //chrome.storage.sync.set({ typeName: null, step: null }, function () { });
              chrome.storage.sync.get(["firstScript", "clientId", "typeName", "postId", "contentShare", "userName", "scriptName"], function (res) {
                var content = res.typeName + "!@#$%^&*()" + res.contentShare;
                proxyService.postClientActivity(res.userName, content, res.postId, res.scriptName);
                signalRService.reqUpdateScript(
                  res.firstScript.scriptId,
                  res.clientId
                );
              });
              setTimeout(() => {
                btnShare[index].click();
              }, 500);
              break;
            }
          }
        }, 1000);
      }, 100);
    }
  },

  // thao tác lên bài viết (cảm xúc, bình luận, chia sẻ, ...)
  clickAction: (action) => {
    let divListAction = document.querySelector('[id^="actions_"]')
    setTimeout(() => {
      let listAction = divListAction.getElementsByTagName("td");
      setTimeout(() => {
        console.log(listAction);

        for (let index = 0; index < listAction.length; index++) {

          if (listAction[index].innerText == action) {
            setTimeout(() => {
              chrome.storage.sync.set({ step: 'next' }, function () { });
              let click = listAction[index].getElementsByTagName("a")[0];
              click.click();
            }, 500);
            break;
          }
        }
      }, 1000);
    }, 100);
  },

  // đăng status chưa kèm img

  postStatus: (content) => {

    let textarea = document.getElementsByTagName("textarea")[0];
    setTimeout(() => {
      textarea.value = content;
      console.log(textarea);
      let btnPost = document.getElementsByTagName("input");
      setTimeout(() => {
        for (let index = 0; index < btnPost.length; index++) {
          if (btnPost[index].getAttribute("value") == "Đăng") {
            console.log(btnPost[index]);
            chrome.storage.sync.set({ typeName: null }, function () { });
            // facebookService.finishScript();
            setTimeout(() => {
              btnPost[index].click();
            }, 500);
            break;
          }
        }
      }, 1000);
    }, 100);
  },

  //đăng status có img

  postImageStatus: (step, urlImage, nameImg, content) => {
    if (step == "head" /*&& checkCurrentUrl.includes("mbasic.facebook.com/home")*/) {
      mBasicFbService.clickPhoto();
    }
    if (step == "next") {
      mBasicFbService.chooseImg(urlImage, nameImg);
    }
    if (step == "nextDone") {
      mBasicFbService.postImg(content);
    }
    if (step == "lickPersonalPage") {
      mBasicFbService.clickPersonalPage();
    }
    if (step == "nextRound") {
      mBasicFbService.clickAll();
    }
    if (step == "getLink") {
      mBasicFbService.getUrlStatus();
    }
  },
  clickPhoto: () => {
    console.log(123);

    var rand = Math.floor(Math.random() * (15 - 5 + 1) + 5);// random từ 3-7
    let btnImg = document.getElementsByTagName("input");
    for (let i = 0; i < btnImg.length; i++) {
      if (btnImg[i].getAttribute("name") == "view_photo") {
        var btn = btnImg[i] as HTMLElement;
        setTimeout(() => {
          chrome.storage.sync.set({ step: 'next' });
          btn.click();
        }, rand);
      }
    }
  },
  postImg: (content) => {
    var rand = Math.floor(Math.random() * (15 - 5 + 1) + 5);
    setTimeout(() => {
      let textarea = document.getElementsByTagName("textarea")[0];
      setTimeout(() => {
        textarea.value = content;
        console.log(textarea);
        let btnPost = document.getElementsByTagName("input");
        setTimeout(() => {
          for (let index = 0; index < btnPost.length; index++) {
            if (btnPost[index].getAttribute("value") == "Đăng") {
              console.log(btnPost[index]);
              setTimeout(() => {
                chrome.storage.sync.set({ step: 'lickPersonalPage' });
                btnPost[index].click();
              }, 500);
              break;
            }
          }
        }, 2000);
      }, 1000);
    }, rand);
  },
  chooseImg: (urlImage, nameImg) => {
    console.log(urlImage, nameImg);
    // urlImage= "https://scontent.fsgn13-2.fna.fbcdn.net/v/t39.30808-6/260099275_634307350915857_3727391928067221823_n.jpg?_nc_cat=106&ccb=1-5&_nc_sid=8bfeb9&_nc_ohc=zflTn0RBYJgAX8uzbcQ&_nc_ht=scontent.fsgn13-2.fna&oh=00_AT-tnKfCdYe2dtCFQsKpPZsl6LgKoOZT1G8NPu_wHq5FBw&oe=62472A67"
    var rand = Math.floor(Math.random() * (25 - 15 + 1) + 15);// random từ 3-7
    setTimeout(() => {
      // $(document).ready(function () {
      loadURLToInputFiled(urlImage);
      function loadURLToInputFiled(url) {
        getImgURL(url, (imgBlob) => {
          // Load img blob to input
          // WIP: UTF8 character error
          nameImg = mBasicFbService.randomNameImage();
          let fileName = nameImg + '.jpg';
          let file = new File([imgBlob], fileName, { type: "image/jpeg", lastModified: new Date().getTime() });
          let container = new DataTransfer();
          container.items.add(file);
          let input1 = document.getElementsByTagName("input");
          for (let index = 0; index < input1.length; index++) {
            if (input1[index].getAttribute("name") == "file1") {
              console.log(input1[index]);
              var test = input1[index] as HTMLInputElement
              input1[index].files = container.files;
              setTimeout(() => {
                let input2 = document.getElementsByTagName("input");
                for (let j = 0; j < input2.length; j++) {
                  if (input2[j].getAttribute("name") == "add_photo_done") {
                    var btnPreview = input2[j] as HTMLElement;
                    setTimeout(() => {
                      chrome.storage.sync.set({ step: 'nextDone' });
                      console.log(btnPreview);

                      btnPreview.click();
                    }, 1000);
                  }
                }
              }, 5000);
              // test.value = '123';
              break;
            }
          }
        })
      }
      // xmlHTTP return blob respond
      function getImgURL(url, callback) {
        var xhr = new XMLHttpRequest();
        xhr.onload = function () {
          callback(xhr.response);
        };
        xhr.open('GET', url);
        xhr.responseType = 'blob';
        xhr.send();
      }
      // });
    }, rand);
  },
  clickPersonalPage: () => {
    setTimeout(() => {
      var personalPage = document.getElementsByClassName("bh bf bg")[0] as HTMLElement;
      setTimeout(() => {
        chrome.storage.sync.set({ step: 'nextRound' });
        personalPage.click();
      }, 3000);
    }, 2000);
  },
  clickAll: () => {
    setTimeout(() => {
      var status1 = document.getElementsByClassName("_55wo _56bf _5rgl")[0];
      console.log(status1);
      if (status1) {
        var seeAll = status1.getElementsByTagName('a');
        for (let i = 0; i < seeAll.length; i++) {
          if (seeAll[i].innerText.includes("Toàn bộ tin")) {
            setTimeout(() => {
              var a = seeAll[i] as HTMLElement;
              console.log(a);

              chrome.storage.sync.set({ step: 'getLink' });
              a.click()
            }, 1500);
          }
        }
      }
    }, 2000);
  },
  nextRound: (urlGroup) => {

  },
  clickAllGroup: () => {
    setTimeout(() => {
      chrome.storage.sync.get(["contentgr", "userName", "nameFacebook"], function (res) {
        if (res) {
          var status1 = document.getElementsByClassName("dd de df");
          for (let i = 0; i < status1.length; i++) {
            var st = status1[i].getElementsByClassName("dm")[0] as HTMLElement;
            var header = status1[i].getElementsByTagName('header')[0] as HTMLAnchorElement;// name facebook 
            if (st.innerText == res.contentgr && header.innerText.includes(res.nameFacebook)) {// kiểm tra trùng content và tên facebook có trong bài đăng
              var seeAll = status1[i].getElementsByTagName('a');
              for (let i = 0; i < seeAll.length; i++) {
                if (seeAll[i].innerText.includes("Toàn bộ tin")) {
                  setTimeout(() => {
                    var a = seeAll[i] as HTMLElement;
                    console.log(a);
                    chrome.storage.sync.set({ step: 'getLink' });
                    a.click()
                  }, 1500);
                }
              }
            }
          }
        }
      });
    }, 2000);
  },
  getUrlStatus: () => {
    setTimeout(() => {
      var parts = checkCurrentUrl.split("_fbid=");
      var id = parts[1].split("&id=");
      // console.log(id[0]);
      setTimeout(() => {
        chrome.storage.sync.get(["contentst", "userName", "scriptName", "typeName"], function (res) {
          if (res) {
            var content = res.typeName + "!@#$%^&*()" + res.contentst;
            proxyService.postClientActivity(res.userName, content, id[0], res.scriptName);
            chrome.storage.sync.get(["firstScript", "clientId"], function (res) {
              signalRService.reqUpdateScript(
                res.firstScript.scriptId,
                res.clientId
              );
            });
          }
        });
      }, 500);
    }, 3000);
  },

  getUrlGroup: () => {
    setTimeout(() => {
      var parts = checkCurrentUrl;
      // chrome.storage.sync.set(
      //   { contentgr: "contentgr", typeName: "typeName", scriptName: "scriptName",userName:"userName" },
      //   function () { }
      // );
      // console.log(id[0]);
      setTimeout(() => {
        chrome.storage.sync.get(["contentgr", "userName", "scriptName", "typeName"], function (res) {
          if (res) {
            var content = res.typeName + "!@#$%^&*()" + res.contentgr;
            proxyService.postClientActivity(res.userName, content, parts, res.scriptName);
            chrome.storage.sync.get(["firstScript", "clientId"], function (res) {
              signalRService.reqUpdateScript(
                res.firstScript.scriptId,
                res.clientId
              );
            });
          }
        });
      }, 500);
    }, 3000);
  },
  //post comment có img
  postImageComment: (urlImage, nameImg, content) => {
    loadURLToInputFiled(urlImage);
    function loadURLToInputFiled(url) {
      getImgURL(url, (imgBlob) => {
        // Load img blob to input
        // WIP: UTF8 character error
        let fileName = nameImg + '.jpg'
        let file = new File([imgBlob], fileName, { type: "image/jpeg", lastModified: new Date().getTime() });
        let container = new DataTransfer();
        container.items.add(file);
        let input1 = document.getElementsByTagName("input");
        for (let index = 0; index < input1.length; index++) {
          if (input1[index].getAttribute("name") == "photo") {
            console.log(input1[index]);
            var test = input1[index] as HTMLInputElement
            input1[index].files = container.files;
            // test.value = '123';
            break;
          }
        }
      })
    }
    // xmlHTTP return blob respond
    function getImgURL(url, callback) {
      var xhr = new XMLHttpRequest();
      xhr.onload = function () {
        callback(xhr.response);
      };
      xhr.open('GET', url);
      xhr.responseType = 'blob';
      xhr.send();
    }
    setTimeout(() => {
      let textarea = document.getElementsByTagName("textarea")[0];
      setTimeout(() => {
        textarea.value = content;
        console.log(textarea);
        let btnPost = document.getElementsByTagName("input");
        setTimeout(() => {
          for (let index = 0; index < btnPost.length; index++) {
            if (btnPost[index].getAttribute("value") == "Bình luận") {
              console.log(btnPost[index]);
              // chrome.storage.sync.set({ typeName: null }, function () { });
              // facebookService.finishScript();
              // setTimeout(() => {
              //   btnPost[index].click();
              // }, 500);
              // break;
            }
          }
        }, 1000);
      }, 100);
    }, 15000);
  },

  //Đăng bài trong group hàng loạt
  postGroup: (urlGroup, step, urlImage, nameImg, content) => {
    if (step == "head" /*&& checkCurrentUrl.includes("mbasic.facebook.com/home")*/) {
      mBasicFbService.clickPhoto();
    }
    if (step == "next") {
      mBasicFbService.chooseImg(urlImage, nameImg);
    }
    if (step == "nextDone") {
      mBasicFbService.postImg(content);
    }
    if (step == "lickPersonalPage") {
      chrome.storage.sync.set(
        { postId: urlGroup, step: "nextRound" },
        function () { }
      );
      setTimeout(() => {
        chrome.runtime.sendMessage({ mess: "openGroupMbasic" });
      }, 2000);
    }
    if (step == "nextRound") {
      mBasicFbService.clickAllGroup();
    }
    if (step == "getLink") {
      mBasicFbService.getUrlGroup();
    }
  },

  randomNameImage: () => {
    var result = '';
    var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    var charactersLength = characters.length;
    for (var i = 0; i < charactersLength - 30; i++) {
      result += characters.charAt(Math.floor(Math.random() * charactersLength));
    }
    // console.log(result)
    return result;
  },
  // nhắn tin cho trang page
  messageToPage: (step, content) => {
    if (step == "head") {
      $(document).ready(function () {
        const randTime = (Math.random() * (4000 - 2000) + 2000).toFixed();
        var listBT = document.getElementsByClassName("ck");
        var btMessage = listBT[0].getElementsByClassName("cm y ba cn co cp z")[2] as HTMLAnchorElement;// btn nhắn tin
        setTimeout(() => {
          chrome.storage.sync.set({ step: "send" }, function () { });
          btMessage.click();
        }, parseInt(randTime));
      });
    }
    if (step == "send") {
      $(document).ready(function () {
        const randTime = (Math.random() * (4000 - 2000) + 2000).toFixed();
        var input = document.getElementsByTagName('textarea')[0];
        setTimeout(() => {
          input.value = content;
          var btnSend = document.getElementsByTagName('input');
          for (let i = 0; i < btnSend.length; i++) {
            if (btnSend[i].getAttribute('value') == "Gửi") {
              setTimeout(() => {
                chrome.storage.sync.get(["contentMessage", "clientId", "userName", "scriptName", "firstScript", "urlPage", "typeName"], function (res) {
                  if (res) {
                    var content = res.typeName + "!@#$%^&*()" + res.contentMessage;
                    proxyService.postClientActivity(res.userName, content, res.urlPage, res.scriptName);
                    signalRService.reqUpdateScript(
                      res.firstScript.scriptId,
                      res.clientId
                    );
                  }
                });
                btnSend[i].click();
              }, parseInt(randTime) * 2);
            }
          }
        }, parseInt(randTime));
      });
    }
  },
  // nhắn tin cho bạn bè
  messageToFriend: (step, content) => {
    if (step == "head") {
      $(document).ready(function () {
        const randTime = (Math.random() * (4000 - 2000) + 2000).toFixed();
        var listBT = document.getElementsByClassName('cj');
        var btIB = listBT[0].getElementsByTagName('a')[0];// btn nhắn tin
        console.log(btIB);
        setTimeout(() => {
          chrome.storage.sync.set({ step: "send" }, function () { });
          btIB.click();
        }, parseInt(randTime));
      });
    }
    if (step == "send") {
      $(document).ready(function () {
        const randTime = (Math.random() * (4000 - 2000) + 2000).toFixed();
        var input = document.getElementsByTagName('textarea')[0];
        setTimeout(() => {
          input.value = content;
          var btnSend = document.getElementsByTagName('input');
          for (let i = 0; i < btnSend.length; i++) {
            if (btnSend[i].getAttribute('value') == "Gửi") {
              setTimeout(() => {
                chrome.storage.sync.get(["contentMessage", "clientId", "userName", "scriptName", "firstScript", "urlFriend", "typeName"], function (res) {
                  if (res) {
                    var content = res.typeName + "!@#$%^&*()" + res.contentMessage;
                    proxyService.postClientActivity(res.userName, content, res.urlFriend, res.scriptName);
                    signalRService.reqUpdateScript(
                      res.firstScript.scriptId,
                      res.clientId
                    );
                  }
                });
                btnSend[i].click();
              }, parseInt(randTime) * 2);
            }
          }
        }, parseInt(randTime));
      });
    }
  },
  changeAvatar: (step, urlImage) => {
    if (step == "head") {
      var btnEdit = document.getElementsByClassName("ce")[0] as HTMLElement;
      var btn = btnEdit.getElementsByTagName('a')[0] as HTMLAnchorElement;
      console.log(btn);
      if (btnEdit.innerText.includes("Chỉnh sửa ảnh đại diện")) {
        setTimeout(() => {
          btn.click();
        }, 2000);
      }
    }
    if (step == "next") {
      var rand = Math.floor(Math.random() * (25 - 15 + 1) + 15);// random từ 3-7
      setTimeout(() => {
        loadURLToInputFiled(urlImage);
        function loadURLToInputFiled(url) {
          getImgURL(url, (imgBlob) => {
            // Load img blob to input
            // WIP: UTF8 character error
            let fileName = mBasicFbService.randomNameImage() + '.jpg';
            let file = new File([imgBlob], fileName, { type: "image/jpeg", lastModified: new Date().getTime() });
            let container = new DataTransfer();
            container.items.add(file);
            let input1 = document.getElementsByTagName("input");
            for (let index = 0; index < input1.length; index++) {
              if (input1[index].getAttribute("name") == "file1") {
                console.log(input1[index]);
                //  var test = input1[index] as HTMLInputElement
                input1[index].files = container.files;
                setTimeout(() => {
                  for (let j = 0; j < input1.length; j++) {
                    if (input1[j].getAttribute("value") == "Tải lên") {
                      var btnPreview = input1[j] as HTMLElement;
                      setTimeout(() => {
                        //  chrome.storage.sync.set({ step: 'nextDone' });
                        console.log(btnPreview);
                        // btnPreview.click();
                      }, 1000);
                    }
                  }
                }, 5000);
                break;
              }
            }
          })
        }
        // xmlHTTP return blob respond
        function getImgURL(url, callback) {
          var xhr = new XMLHttpRequest();
          xhr.onload = function () {
            callback(xhr.response);
          };
          xhr.open('GET', url);
          xhr.responseType = 'blob';
          xhr.send();
        }
      }, rand);
    }
  }
};
export default mBasicFbService;