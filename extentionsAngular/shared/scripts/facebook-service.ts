import * as $ from "jquery";
import proxyService from "../proxy-shared-service";
import funcPublic from "../funcPublic";
import signalRService from "../signalR/signalR-service";
import httpRequestService from "../http-request-service";
import { url } from "../environment";

var classVideoNews = "";
var classNews = "";
var classNewsContent = "";
var classVideoContent = "";
var urlVideos = "";
var urlNews = "";
var btnLike = "";
var btnReaction = "";
var btnReactionChild = "";
var divSearchGroup = "";
var groupNameChild = "";
var groupContentChild = "";
var groupTypeChild = "";
var btnJoinGroup = "";
var btnAddFr = "";
var typeGroup = "";
var cardFr = "";
var input2fa = "";
var btnCheckpoint = "";
var cardGroup = "";
var buttonAccept = ""
var firstSurfStory = "";
var closeStory = "";
var cardNotification = "";
var notification = "";
var information = "";
var informationF = "";
var contactCard = "";
var btnShare = "";
var allGroup = "";
var listGroup = "";
var btnPost = "";
var ifGroup = "";
var btnJoin = "";
var nameGroup = "";
var avatarFR = "";
var cardFriend = "";
var avatar = "";
var contentNotification = "";
var timeNotification = "";
var cardChildDrawer = "";
var nameGroup = "";
var informationGroup = "";
var information1Group = "";
var cardMakeFriendsSuggestion = "";
var cardAddFriendsFromGroup = "";
var cardCountFrGr = "";
var btnCardAddFr = "";
var listBTShare = "";
var listBTShareEqualZero = "";
var listBtnShareGroup = "";
var btnIntroduceJoinGroupWithUrl = "";
var contentGroup = "";
var avatarGroup = "";
var urlAvatarReadInformationFriend = "";
var nameReadInformationFriend = "";

setTimeout(() => {
  facebookService.getres();
}, 500)

setInterval(() => {
  facebookService.getres();
}, 180000)

//class name
// var classVideoNews = "_6x84";
// const classNews = "du4w35lb k4urcfbm l9j0dhe7 sjgh65i0";
// const classNewsContent =
//   "d2edcug0 hpfvmrgz qv66sw1b c1et5uql lr9zc1uh a8c37x1j keod5gw0 nxhoafnm aigsh9s9 fe6kdd0r mau55g9w c8b282yb d3f4x2em iv3no6db jq4qci2q a3bd9o3v b1v8xokw oo9gr5id hzawbc8m";
// const classVideoContent =
//   "d2edcug0 hpfvmrgz qv66sw1b c1et5uql lr9zc1uh a8c37x1j fe6kdd0r mau55g9w c8b282yb keod5gw0 nxhoafnm aigsh9s9 d3f4x2em mdeji52x a5q79mjw g1cxx5fr lrazzd5p oo9gr5id";
// const urlVideos =
//   "oajrlxb2 g5ia77u1 qu0x051f esr5mh6w e9989ue4 r7d6kgcz rq0escxv nhd2j8a9 nc684nl6 p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso i1ao9s8h esuyzwwr f1sip0of lzcic4wl gmql0nx0 gpro0wi8 b1v8xokw";
// const urlNews =
//   "oajrlxb2 gs1a9yip g5ia77u1 mtkw9kbi tlpljxtp qensuy8j ppp5ayq2 goun2846 ccm00jje s44p3ltw mk2mc5f4 rt8b4zig n8ej3o3l agehan2d sk4xxmp2 rq0escxv nhd2j8a9 mg4g778l pfnyh3mw p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x tgvbjcpo hpfvmrgz jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso l9j0dhe7 i1ao9s8h esuyzwwr f1sip0of du4w35lb n00je7tq arfg74bv qs9ysxi8 k77z8yql btwxx1t3 abiwlrkh p8dawk7l lzcic4wl a8c37x1j tm8avpzi";
// const btnLike =
//   "oajrlxb2 gs1a9yip g5ia77u1 mtkw9kbi tlpljxtp qensuy8j ppp5ayq2 goun2846 ccm00jje s44p3ltw mk2mc5f4 rt8b4zig n8ej3o3l agehan2d sk4xxmp2 rq0escxv nhd2j8a9 mg4g778l pfnyh3mw p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x tgvbjcpo hpfvmrgz jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso l9j0dhe7 i1ao9s8h esuyzwwr du4w35lb n00je7tq arfg74bv qs9ysxi8 k77z8yql pq6dq46d btwxx1t3 abiwlrkh p8dawk7l lzcic4wl gokke00a";
// const btnReaction = "oajrlxb2 gs1a9yip g5ia77u1 mtkw9kbi tlpljxtp qensuy8j ppp5ayq2 goun2846 ccm00jje s44p3ltw mk2mc5f4 rt8b4zig n8ej3o3l agehan2d sk4xxmp2 rq0escxv nhd2j8a9 mg4g778l pfnyh3mw p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x tgvbjcpo hpfvmrgz i1ao9s8h esuyzwwr du4w35lb n00je7tq arfg74bv qs9ysxi8 k77z8yql pq6dq46d btwxx1t3 abiwlrkh p8dawk7l lzcic4wl pphx12oy b4ylihy8 rz4wbd8a b40mr0ww a8nywdso hmalg0qr q45zohi1 g0aa4cga pmk7jnqg gokke00a";
// const btnReactionChild = "oajrlxb2 g5ia77u1 mtkw9kbi tlpljxtp qensuy8j ppp5ayq2 goun2846 ccm00jje s44p3ltw mk2mc5f4 rt8b4zig n8ej3o3l agehan2d sk4xxmp2 rq0escxv mg4g778l pfnyh3mw p7hjln8o kvgmc6g5 oygrvhab tgvbjcpo hpfvmrgz jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso i1ao9s8h esuyzwwr du4w35lb n00je7tq arfg74bv qs9ysxi8 k77z8yql btwxx1t3 abiwlrkh p8dawk7l lzcic4wl bp9cbjyn mu51a5us j1l0snac h9pa7xm5 og22hbeg rr7aif1o nhd2j8a9 j83agx80 cgat1ltu kkf49tns l9j0dhe7 gokke00a";
// const divSearchGroup =
//   "rq0escxv l9j0dhe7 du4w35lb hybvsw6c io0zqebd m5lcvass fbipl8qg nwvqtn77 k4urcfbm ni8dbmo4 stjgntxs sbcfpzgs";
// const groupNameChild =
//   "oajrlxb2 g5ia77u1 qu0x051f esr5mh6w e9989ue4 r7d6kgcz rq0escxv nhd2j8a9 nc684nl6 p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso i1ao9s8h esuyzwwr f1sip0of lzcic4wl gpro0wi8 oo9gr5id lrazzd5p dkezsu63";
// const groupTypeChild =
//   "d2edcug0 hpfvmrgz qv66sw1b c1et5uql lr9zc1uh e9vueds3 j5wam9gi b1v8xokw m9osqain";
// const btnJoinGroup =
//   "oajrlxb2 qu0x051f esr5mh6w e9989ue4 r7d6kgcz nhd2j8a9 p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x i1ao9s8h esuyzwwr f1sip0of abiwlrkh p8dawk7l lzcic4wl bp9cbjyn s45kfl79 emlxlaya bkmhp75w spb7xbtv rt8b4zig n8ej3o3l agehan2d sk4xxmp2 rq0escxv j83agx80 taijpn5t jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso l9j0dhe7 tv7at329 thwo4zme tdjehn4e";
// const btnAddFr =
//   "oajrlxb2 g5ia77u1 qu0x051f esr5mh6w e9989ue4 r7d6kgcz nhd2j8a9 p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso i1ao9s8h esuyzwwr f1sip0of n00je7tq arfg74bv qs9ysxi8 k77z8yql abiwlrkh p8dawk7l lzcic4wl rq0escxv pq6dq46d cbu4d94t taijpn5t l9j0dhe7 k4urcfbm";
// const typeGroup = "Nhóm Công khai";
// const cardFr =
//   "ow4ym5g4 auili1gw rq0escxv j83agx80 buofh1pr g5gj957u i1fnvgqd oygrvhab cxmmr5t8 hcukyx3x kvgmc6g5 hpfvmrgz qt6c0cv9 jb3vyjys l9j0dhe7 du4w35lb bp9cbjyn btwxx1t3 dflh9lhu scb9dxdr nnctdnn4";
// const input2fa = "approvals_code";
// const btnCheckpoint = "checkpointSubmitButton";
// const cardGroup = "rpm2j7zs k7i0oixp gvuykj2m j83agx80 cbu4d94t ni8dbmo4 du4w35lb q5bimw55 ofs802cu pohlnb88 dkue75c7 mb9wzai9 d8ncny3e buofh1pr g5gj957u tgvbjcpo l56l04vs r57mb794 kh7kg01d eg9m0zos c3g1iek1 l9j0dhe7 k4xni2cv";
// var buttonAccept = "oajrlxb2 g5ia77u1 qu0x051f esr5mh6w e9989ue4 r7d6kgcz nhd2j8a9 p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso i1ao9s8h esuyzwwr f1sip0of n00je7tq arfg74bv qs9ysxi8 k77z8yql abiwlrkh p8dawk7l lzcic4wl rq0escxv pq6dq46d cbu4d94t taijpn5t l9j0dhe7 k4urcfbm"
// var firstSurfStory = "oajrlxb2 qu0x051f esr5mh6w e9989ue4 r7d6kgcz nhd2j8a9 p7hjln8o esuyzwwr f1sip0of abiwlrkh p8dawk7l lzcic4wl fni8adji hgaippwi fykbt5ly ns4ygwem ni8dbmo4 stjgntxs dwo3fsh8 g5ia77u1 goun2846 ccm00jje s44p3ltw mk2mc5f4 rt8b4zig n8ej3o3l agehan2d sk4xxmp2 rq0escxv a8c37x1j kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso l9j0dhe7 i1ao9s8h k4urcfbm du4w35lb lsqurvkf sbcfpzgs"
// var closeStory = "oajrlxb2 qu0x051f esr5mh6w e9989ue4 r7d6kgcz nhd2j8a9 p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x i1ao9s8h esuyzwwr f1sip0of abiwlrkh p8dawk7l lzcic4wl bp9cbjyn s45kfl79 emlxlaya bkmhp75w spb7xbtv rt8b4zig n8ej3o3l agehan2d sk4xxmp2 rq0escxv j83agx80 taijpn5t jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso l9j0dhe7 qypqp5cg q676j6op d6rk862h ljqsnud1";
// const cardNotification = "oajrlxb2 qu0x051f esr5mh6w e9989ue4 r7d6kgcz nhd2j8a9 p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x i1ao9s8h esuyzwwr f1sip0of abiwlrkh p8dawk7l lzcic4wl bp9cbjyn s45kfl79 emlxlaya bkmhp75w spb7xbtv rt8b4zig n8ej3o3l agehan2d sk4xxmp2 rq0escxv j83agx80 taijpn5t jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso l9j0dhe7 qypqp5cg q676j6op tdjehn4e";
// const notification = "oajrlxb2 gs1a9yip g5ia77u1 mtkw9kbi tlpljxtp qensuy8j ppp5ayq2 goun2846 ccm00jje s44p3ltw mk2mc5f4 rt8b4zig n8ej3o3l agehan2d sk4xxmp2 rq0escxv nhd2j8a9 mg4g778l pfnyh3mw p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x tgvbjcpo hpfvmrgz jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso l9j0dhe7 i1ao9s8h esuyzwwr f1sip0of du4w35lb btwxx1t3 abiwlrkh p8dawk7l lzcic4wl ue3kfks5 pw54ja7n uo3d90p7 l82x9zwi a8c37x1j";
// const information =
//   "rq0escxv l9j0dhe7 du4w35lb j83agx80 cbu4d94t d2edcug0 hpfvmrgz rj1gh0hx buofh1pr g5gj957u p8fzw8mz pcp91wgn iuny7tx3 ipjc6fyt";
// const informationF =
//   "rq0escxv l9j0dhe7 du4w35lb j83agx80 pfnyh3mw i1fnvgqd gs1a9yip owycx6da btwxx1t3 hv4rvrfc dati1w0a discj3wi b5q2rw42 lq239pai mysgfdmx hddg9phg";
// const contactCard = "dhix69tm sjgh65i0 wkznzc2l tr9rh885";
// const btnShare = "rq0escxv l9j0dhe7 du4w35lb j83agx80 rj1gh0hx buofh1pr g5gj957u hpfvmrgz taijpn5t bp9cbjyn owycx6da btwxx1t3 d1544ag0 tw6a2znq jb3vyjys dlv3wnog rl04r1d5 mysgfdmx hddg9phg qu8okrzs g0qnabr5";
// const allGroup = "rq0escxv l9j0dhe7 du4w35lb j83agx80 cbu4d94t pfnyh3mw d2edcug0 aahdfvyu tvmbv18p";
// const lsgroup = "oajrlxb2 gs1a9yip g5ia77u1 mtkw9kbi tlpljxtp qensuy8j ppp5ayq2 goun2846 ccm00jje s44p3ltw mk2mc5f4 rt8b4zig n8ej3o3l agehan2d sk4xxmp2 rq0escxv nhd2j8a9 mg4g778l pfnyh3mw p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x tgvbjcpo hpfvmrgz jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso l9j0dhe7 i1ao9s8h esuyzwwr f1sip0of du4w35lb btwxx1t3 abiwlrkh p8dawk7l lzcic4wl ue3kfks5 pw54ja7n uo3d90p7 l82x9zwi a8c37x1j";
// const btnPost = "l9j0dhe7 du4w35lb j83agx80 pfnyh3mw taijpn5t bp9cbjyn owycx6da btwxx1t3 kt9q3ron ak7q8e6j isp2s0ed ri5dt5u2 rt8b4zig n8ej3o3l agehan2d sk4xxmp2 rq0escxv d1544ag0 tw6a2znq s1i5eluu tv7at329";
// const ifGroup = "rq0escxv l9j0dhe7 du4w35lb j83agx80 cbu4d94t pfnyh3mw d2edcug0 hpfvmrgz n8tt0mok hyh9befq r8blr3vg jwdofwj8";
// const btnJoin = "l9j0dhe7 du4w35lb j83agx80 pfnyh3mw taijpn5t bp9cbjyn owycx6da btwxx1t3 kt9q3ron ak7q8e6j isp2s0ed ri5dt5u2 rt8b4zig n8ej3o3l agehan2d sk4xxmp2 rq0escxv d1544ag0 tw6a2znq s1i5eluu tv7at329";
// const nameGroup = "oajrlxb2 g5ia77u1 qu0x051f esr5mh6w e9989ue4 r7d6kgcz rq0escxv nhd2j8a9 nc684nl6 p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso i1ao9s8h esuyzwwr f1sip0of lzcic4wl gmql0nx0 gpro0wi8 hnhda86s";
// const avtarFR = "oajrlxb2 gs1a9yip g5ia77u1 mtkw9kbi tlpljxtp qensuy8j ppp5ayq2 goun2846 ccm00jje s44p3ltw mk2mc5f4 rt8b4zig n8ej3o3l agehan2d sk4xxmp2 rq0escxv nhd2j8a9 mg4g778l pfnyh3mw p7hjln8o kvgmc6g5 oygrvhab hcukyx3x tgvbjcpo hpfvmrgz jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso i1ao9s8h esuyzwwr f1sip0of du4w35lb n00je7tq arfg74bv qs9ysxi8 k77z8yql btwxx1t3 abiwlrkh p8dawk7l q9uorilb lzcic4wl pioscnbf wkznzc2l l9j0dhe7 etr7akla";
// const cardFriend = "bp9cbjyn ue3kfks5 pw54ja7n uo3d90p7 l82x9zwi n1f8r23x rq0escxv j83agx80 bi6gxh9e discj3wi hv4rvrfc ihqw7lf3 dati1w0a gfomwglr";
const min = 10000;
const max = 20000;
const ranDom = [true, false, true];
const ranDom1 = [0, 1, 2];
var flagOfType1 = 0;
var flagOfType2 = 0;
var flagOfType10 = 0;
var flagOfType12 = 0;
var countFriend = 0;
var countGroup = 2;
var countFrGr = 12;
var countLikes = 0;
var countJoin = 0;
var countFrAdd = 0;
var totalAccept = 0;
var flagButtonAccept = 0;
var timeSurf = 0;
var tempIndexType2 = 0;
var countNotification = 0;
var countFR = 0;

const facebookService = {
  // Undefined = 0,

  // SurfingNewsfeed = 1,
  surfingNewsfeed: () => {
    const randTime = (Math.random() * (max - min) + min).toFixed();
    $(document).ready(function () {
      var divNews = document.getElementsByClassName(classNews);
      if (divNews.length > flagOfType1) {
        divNews[flagOfType1++].scrollIntoView({
          behavior: "smooth",
          block: "center",
          inline: "center",
        });
      }
    });
    setTimeout(() => {
      var today = new Date();
      var time = today.getHours() + ":" + today.getMinutes();
      chrome.storage.sync.get(["timeEnd", "firstScript", "clientId"], function (res) {
        console.log( "time end: ",res.timeEnd);
        if (res.timeEnd > time) {
          // kiểm tra thời gian kết thúc có lớn hơn thời gian hiện tại không
          facebookService.surfingNewsfeed();
        } else {
          // nếu đã chạy xog kịch bản hoặc có kịch bản mới từ server
          console.log("hết kịch bản, hoặc đã có kịch bản mới từ server!");
          signalRService.reqUpdateScript(
            res.firstScript.scriptId,
            res.clientId
          );
        }
      });
    }, parseInt(randTime));
  },

  // LikeNewsfeed = 2,
  reactionPostTest: (input) => {
    const rand = Math.floor(Math.random() * ranDom1.length);
    console.log(ranDom1[rand]);
    let news = document.getElementsByClassName(classNews)[input];
    let btnReact = news.getElementsByClassName(
      btnReaction
    )[0] as HTMLElement;
    if (btnReact) {
      btnReact.scrollIntoView({
        behavior: "smooth",
        block: "center",
        inline: "center",
      });
      btnReact.click();
      console.log(btnReact);
      setTimeout(() => {
        let btn = document.getElementsByClassName(btnReactionChild)[ranDom1[rand]] as HTMLElement;
        if (btn) {
          setTimeout(() => {
            btn.focus();
            setTimeout(() => {
              btn.click();
            }, 1000);
          }, 1000);
        }
      }, 1000);
    }
  },

  likeNewsFeed: () => {
    const randTime = (Math.random() * (max - min) + min).toFixed();
    const randJoin = Math.floor(Math.random() * ranDom.length);
    $(document).ready(function () {
      let news = document.getElementsByClassName(classNews);
      if (news.length - 1 >= flagOfType2) {
        news[flagOfType2].scrollIntoView({
          behavior: "smooth",
          block: "center",
          inline: "center",
        });
        let urlNEWS = news[flagOfType2].getElementsByClassName(
          urlNews
        ) as HTMLCollectionOf<HTMLAnchorElement>;
        setTimeout(() => {
          let contentNews = news[flagOfType2].getElementsByClassName(
            classNewsContent
          ) as HTMLCollectionOf<HTMLAnchorElement>;
          if (contentNews.length > 0) {
            chrome.storage.sync.set(
              { content: contentNews[0].textContent },
              function () { }
            );
          } else {
            chrome.storage.sync.set({ content: "null" }, function () { });
          }
        }, 0.2 * parseInt(randTime));
        setTimeout(() => {
          if (urlNEWS.length > 0) {
            chrome.storage.sync.set({ Url: urlNEWS[0].href }, function () { });
          } else {
            let test = news[flagOfType2].getElementsByClassName(
              urlVideos
            )[0] as HTMLAnchorElement;
            if (test) {
              var event = new FocusEvent("focusin", {
                view: window,
                bubbles: true,
                cancelable: true,
              });
              test.dispatchEvent(event);
              setTimeout(() => {
                chrome.storage.sync.set({ Url: test.href }, function () { });
              }, 500);
            }
          }
        }, 0.3 * parseInt(randTime));
        setTimeout(() => {
          if (ranDom[randJoin] == true && tempIndexType2 != flagOfType2) {
            tempIndexType2 = flagOfType2;
            facebookService.reactionPostTest(flagOfType2);
            countLikes++;
            setTimeout(() => {
              chrome.storage.sync.get(
                ["userName", "content", "firstScript", "Url"],
                function (res) {
                  if (res) {
                    proxyService.postClientActivity(
                      res.userName,
                      res.firstScriptres.typeName + "!@#$%^&*()" + res.content,
                      res.Url,
                      res.firstScript.scriptName
                    );
                  } else {
                    console.log("khong nhan duoc res");
                  }
                }
              );
            }, 0.6 * parseInt(randTime));
          }
          flagOfType2++;
        }, 0.4 * parseInt(randTime));
      }
    });
    setTimeout(() => {
      chrome.storage.sync.get(["totalLike", "firstScript", "clientId"], function (res) {
        console.log(countLikes);
        console.log(res.totalLike);
        console.log("Random like: ", ranDom[randJoin]);
        if (res.totalLike > countLikes) {
          // kiểm tra số lượng like đã thực thi và có kịch bản mới hay không
          facebookService.likeNewsFeed();
        } else {
          // nếu đã chạy xog kịch bản hoặc có kịch bản mới từ server
          console.log("hết kịch bản, hoặc đã có kịch bản mới từ server!");
          signalRService.reqUpdateScript(
            res.firstScript.scriptId,
            res.clientId
          );
        }
      });
    }, parseInt(randTime));
  },

  // CommentPost = 3,

  // ReadMessage = 4,

  // test.................................................................

  //type = 5 :::get information notification
  getNotification: () => {
    const randTime = (Math.random() * (8000 - 5000) + 5000).toFixed();
    $(document).ready(function () {
      console.log('Wait for ' + randTime + ' seconds');
      var card = document.getElementsByClassName(cardNotification)[1] as HTMLElement;
      if (card) {
        setTimeout(() => {
          card.click();
        }, 3000);
      }
      setTimeout(() => {
        facebookService.cardNotification();
      }, parseInt(randTime));
    });
  },
  cardNotification: () => {
    const randTime = (Math.random() * (10000 - 5000) + 5000).toFixed();
    console.log('Wait for ' + randTime + ' seconds');
    // setTimeout(() => {
    var notf = document.getElementsByClassName(notification);
    var test = notf[countNotification] as HTMLAnchorElement;
    var head = test.getElementsByTagName('strong');
    if (head.length > 0) {
      var avatarValueDocument = document.getElementsByClassName(avatar)[countNotification];
      var avatar1 = avatarValueDocument.getElementsByTagName('image')[0];
      console.log(avatar1.getAttribute('xlink:href'));
      var urlAvartar = avatar1.getAttribute('xlink:href');
      if (notf.length > countNotification) {
        var content = notf[countNotification].getElementsByClassName(contentNotification)[0] as HTMLElement;
        var time = notf[countNotification].getElementsByClassName(timeNotification)[1] as HTMLElement;
        console.log(content.innerText);
        var contentNT = content.innerText;
        var timeNT = time.innerText;
        console.log(time.innerText);
        notf[countNotification].scrollIntoView({ behavior: "smooth", block: "center", inline: "center" });
        chrome.storage.sync.get(["userName"], function (res) {
          setTimeout(() => {
            proxyService.postNotification(res.userName, contentNT, urlAvartar, timeNT);
          }, 1000);
        });
      }
    }
    // }, 2000);
    setTimeout(() => {
      chrome.storage.sync.get(["totalNotification", "firstScript", "clientId"], function (res) {
        if (parseInt(res.totalNotification) > countNotification-1) {
          countNotification++;
          facebookService.cardNotification();
        }
        else {
          console.log("hết kịch bản");
          signalRService.reqUpdateScript(
            res.firstScript.scriptId,
            res.clientId
          );
        }
      });

    }, parseInt(randTime));
  },

  // Read information group
  groupDataAnalyst: () => {
    const randTime = (Math.random() * (35000 - 25000) + 25000).toFixed();
    $(document).ready(function () {
      console.log('Wait for ' + randTime + ' seconds');
      var card = document.getElementsByClassName(cardGroup)[0];
      if (card) {
        var cardChild = card.childNodes[0] as HTMLAnchorElement;
        var cardChildDrawerL = cardChild.getElementsByClassName(cardChildDrawer);
        console.log(cardChildDrawerL);
        if (cardChildDrawerL.length > 0) {
          cardChildDrawerL[countGroup].scrollIntoView({ behavior: "smooth", block: "center", inline: "center" });
          var a = cardChildDrawerL[countGroup] as HTMLElement;

          console.log(a);
          var hr = cardChildDrawerL[countGroup] as HTMLAnchorElement;
          var urlGroup = hr.href;
          var avartar1 = a.getElementsByTagName('image')[0];
          var name = a.getElementsByClassName(nameGroup)[0] as HTMLElement;

          var groupName = name.innerText;
          var avartarGroup = avartar1.getAttribute('xlink:href');
          setTimeout(() => {
            a.click();
            setTimeout(() => {
              var information = document.getElementsByClassName(informationGroup)[1] as HTMLElement;
              //console.log(Information);
              information.click();
              setTimeout(() => {
                var information1 = document.getElementsByClassName(information1Group);
                var b = information1[information1.length - 1] as HTMLElement;
                console.log(b.innerText);
                var content = b.innerText;
                chrome.storage.sync.get(["userName"], function (res) {
                  //post group to database
                  setTimeout(() => {
                    proxyService.postGroup(res.userName, groupName, avartarGroup, urlGroup, content);
                  }, 2000);
                });
              }, 10000);
            }, 5000);
          }, 2000);

        }
        setTimeout(() => {
          chrome.storage.sync.get(["firstScript", "clientId"], function (res) {
            if (cardChildDrawerL.length - 1 > countGroup) {
              countGroup++;
              facebookService.groupDataAnalyst();
            }
            else {
              console.log("hết kịch bản hoặc có kịch bản mới");
              signalRService.reqUpdateScript(
                res.firstScript.scriptId,
                res.clientId
              );
            }
          });
        }, parseInt(randTime));

      }
      //var rand = Math.floor(Math.random() * (10 - 5 + 1) + 5);// random từ 5-10
    });
  },

  // TextingFriends = 6,

  // MakeFriendsSuggestion = 7,
  makeFriends: () => {
    const randTime = (Math.random() * (max - min) + min).toFixed();
    $(document).ready(function () {
      console.log("Wait for " + randTime + " seconds");
      var card = document.getElementsByClassName(contactCard);
      if (card.length > countFriend) {
        card[countFriend].scrollIntoView({
          behavior: "smooth",
          block: "center",
          inline: "center",
        });
        let ct: HTMLElement = card[countFriend].getElementsByClassName(
          cardFr
        )[0] as HTMLElement;
        console.log(ct.innerText);
        if (ct.innerText.includes("bạn chung")) {
          setTimeout(() => {
            ct.click();
            setTimeout(() => {
              var content = document.getElementsByClassName(informationF);
              let ct1: HTMLElement = content[0].getElementsByClassName(
                information
              )[0] as HTMLElement;
              console.log(ct1.innerText);
              if (ct1.innerText.includes("Sống tại Thành phố Hồ Chí Minh")) {
                console.log("true");
                countFrAdd++;
                console.log("Số người đã kết bạn :" + countFrAdd);
                let btnAdd: HTMLElement = card[
                  countFriend
                ].getElementsByClassName(btnAddFr)[0] as HTMLElement;
                console.log(btnAdd.innerText);
                btnAdd.style.background = "red";
              }
            }, 3000);
            // let content: HTMLElement =
          }, 1000);
        }
      }
      //var rand = Math.floor(Math.random() * (10 - 5 + 1) + 5);// random từ 5-10
    });
    setTimeout(() => {
      countFriend++;
      facebookService.makeFriends();
    }, parseInt(randTime));

  },

  makeFriendsSuggestion: (content) => {
    $(document).ready(function () {
      var card = document.getElementsByClassName(contactCard);
      // console.log(card);
      if (card.length > countFriend) {
        card[countFriend].scrollIntoView({
          behavior: "smooth",
          block: "end",
          inline: "nearest",
        });
        let ct: HTMLElement = card[countFriend].getElementsByClassName(cardMakeFriendsSuggestion)[0] as HTMLElement;
        console.log(ct.innerText);
        var context = ct.innerText;
        if (context.includes(content) /*|| context.includes("University")*/) {
          console.log("true");
          countFrAdd++;
          console.log("Số người đã kết bạn :" + countFrAdd);
          let btnAddFr: HTMLElement = card[countFriend].getElementsByClassName(
            "btnAdd"
          )[0] as HTMLElement;
          btnAddFr.style.background = "blue";
        } else {
          console.log("false");
        }
      }
    });
    var rand = Math.floor(Math.random() * (15 - 5 + 1) + 5); // random từ 5-15s
    console.log("Wait for " + rand + " seconds");
    setTimeout(() => {
      chrome.storage.sync.get(
        ["totalFiendAdd", "firstScript", "ipHost", "isChangeScript", "clientId"],
        function (res) {
          if (res.totalFiendAdd > countFrAdd && res.isChangeScript != true) {
            // kiểm tra số lượng kết bạn đã thực thi và có kịch bản mới hay không
            countFriend++;
            facebookService.makeFriendsSuggestion(content);
          } else {
            // nếu đã chạy xog kịch bản hoặc có kịch bản mới từ server
            console.log("hết kịch bản, hoặc có kịch bản mới từ server !");
            signalRService.reqUpdateScript(
              res.firstScript.scriptId,
              res.clientId);
          }
        }
      );
    }, rand * 1000);
  },
  // thêm bạn từ group
  addFriendsFromGroup: () => {
    $(document).ready(function () {
      var card = document.getElementsByClassName(cardAddFriendsFromGroup);
      // console.log(card);
      if (card.length > countFrGr) {
        card[countFrGr].scrollIntoView({
          behavior: "smooth",
          block: "end",
          inline: "nearest",
        });
        let ct: HTMLElement = card[countFrGr].getElementsByClassName(cardCountFrGr)[0] as HTMLElement;
        console.log(ct.innerText);
        var random = Math.floor(Math.random() * 3);
        console.log(random);
        if (random == 1) {
          let btnAddFr: HTMLElement = card[countFrGr].getElementsByClassName(btnCardAddFr)[0] as HTMLElement;
          if (btnAddFr) {
            btnAddFr.style.background = "blue";
            countFrAdd++;
            console.log("Số người đã kết bạn :" + countFrAdd);
          }
        }
      } else {
        window.scrollBy(0, 200);
      }
    });
    var rand = Math.floor(Math.random() * (15 - 5 + 1) + 5); // random từ 5-15s
    console.log("Wait for " + rand + " seconds");
    setTimeout(() => {
      countFrGr++;
      facebookService.addFriendsFromGroup();
    }, rand * 1000);
  },

  // AgreeToBeFriends = 8,

  AcceptFriend: () => {
    $(document).ready(function () {
      chrome.storage.sync.get(["totalAcceptFriends"],
        function (res) {
          totalAccept = res.totalAcceptFriends;
        }
      );
     // totalAccept = 10;
      var x = document.getElementsByClassName(buttonAccept);
      setTimeout(() => {
        if (x.length >= totalAccept) {
          if (
            x[flagButtonAccept]?.textContent == "Xác nhận" &&
            x[flagButtonAccept]?.children.length > 0
          ) {
            // bắt content và kiểm tra xem có class con hay k
            x[flagButtonAccept].scrollIntoView({
              behavior: "smooth",
              block: "end",
              inline: "nearest",
            }); // scroll into view
            var clickItem = document.getElementsByClassName(buttonAccept)[
              flagButtonAccept
            ] as HTMLElement;
            clickItem.click();
            
          }
          if(x[flagButtonAccept]?.textContent == "Xác nhận" &&
          x[flagButtonAccept]?.textContent == "Xóa"){
            flagButtonAccept = x.length;
          }
        }
      }, 2000);
      setTimeout(() => {
        chrome.storage.sync.get(
          ["totalAcceptFriends", "firstScript", "isChangeScript","clientId"],
          function (res) {
            console.log(res);
            if (
              totalAccept > flagButtonAccept*2 && flagButtonAccept <= x.length -1 &&
              res.isChangeScript != true
            ) {
              // kiểm tra số lượng kết bạn đã thực thi và có kịch bản mới hay không
              flagButtonAccept++;
              facebookService.AcceptFriend();
            } else {
              // nếu đã chạy xog kịch bản hoặc có kịch bản mới từ server
              console.log("hết kịch bản, hoặc có kịch bản mới từ server !");
              setTimeout(() => {
                signalRService.reqUpdateScript(res.firstScript.scriptId,res.clientId);
              }, 500);
            }
          }
        );
      }, 3000);
    });
  },

  // SurfWatch = 9,

  SurfWatch: () => {
    const randTime = (Math.random() * (max - min) + min).toFixed();
    var listVideoNews = document.getElementsByClassName(classVideoNews);
    setTimeout(() => {
      if (listVideoNews.length - 1 >= flagOfType10) {
        listVideoNews[flagOfType10++].scrollIntoView({
          behavior: "smooth",
          block: "center",
          inline: "center",
        });
      }
    }, parseInt(randTime) * 0.1);
    setTimeout(() => {
      var today = new Date();
      var time = today.getHours() + ":" + today.getMinutes();
      chrome.storage.sync.get(["timeEndWatch", "firstScript", "clientId"], function (res) {
        console.log("time end:",res.timeEndWatch);
        if (res.timeEndWatch >= time) {
          // kiểm tra số lượng like đã thực thi và có kịch bản mới hay không
          facebookService.SurfWatch();
        } else {
          // nếu đã chạy xog kịch bản hoặc có kịch bản mới từ server
          console.log("hết kịch bản, hoặc có kịch bản mới từ server !");
          signalRService.reqUpdateScript(
            res.firstScript.scriptId,
            res.clientId
          );
        }
      });
    }, parseInt(randTime));
  },

  // LikeVideoWatch = 10,

  likeVideoWatch: () => {
    const randTime = (Math.random() * (max - min) + min).toFixed();
    const randJoin = Math.floor(Math.random() * ranDom.length);
    console.log(flagOfType10);
    $(document).ready(function () {
      var listVideoNews = document.getElementsByClassName(classVideoNews);
      if (listVideoNews.length - 1 >= flagOfType10) {
        listVideoNews[flagOfType10].scrollIntoView({
          behavior: "smooth",
          block: "center",
          inline: "center",
        });
        setTimeout(() => {
          let contentVideo = listVideoNews[flagOfType10].getElementsByClassName(
            classVideoContent
          ) as HTMLCollectionOf<HTMLAnchorElement>;
          if (contentVideo.length > 0) {
            chrome.storage.sync.set(
              { content: contentVideo[0].textContent },
              function () { }
            );
          } else {
            chrome.storage.sync.set({ content: "null" }, function () { });
          }
        }, 0.25 * parseInt(randTime));
        setTimeout(() => {
          let test = listVideoNews[flagOfType10].getElementsByClassName(
            urlVideos
          )[0] as HTMLAnchorElement;
          if (test) {
            setTimeout(() => {
              chrome.storage.sync.set({ Url: test.href }, function () { });
            }, 500);
          } else {
            chrome.storage.sync.set({ Url: null }, function () { });
          }
        }, 0.35 * parseInt(randTime));
        setTimeout(() => {
          console.log(ranDom[randJoin]);
          let btnLikes = listVideoNews[flagOfType10].getElementsByClassName(
            btnLike
          )[0] as HTMLElement;
          if (btnLikes && ranDom[randJoin]) {
            // btnLikes.click();
            // if(btnLikes.textContent == "Thích"){     
            //   facebookService.reactionPostTest(flagOfType10);
            // }
            btnLikes.textContent == "Đã Thích"
            countLikes++;
            setTimeout(() => {
              chrome.storage.sync.get(
                ["userName", "content", "firstScript", "Url"],
                function (res) {
                  if (res) {
                    proxyService.postClientActivity(
                      res.userName,
                      res.firstScriptres.typeName + "!@#$%^&*()" + res.content,
                      res.Url,
                      res.firstScript.scriptName
                    );
                  } else {
                    console.log("khong nhan duoc res");
                  }
                }
              );
            }, 0.6 * parseInt(randTime));
          }
          flagOfType10++;
        }, 0.45 * parseInt(randTime));
      }
    });
    setTimeout(() => {
      chrome.storage.sync.get(
        ["totalLikeVideo", "firstScript", "ipHost", "isChangeScript", "clientId"],
        function (res) {
          if (res.totalLikeVideo > countLikes && res.isChangeScript != true) {
            // kiểm tra số lượng like đã thực thi và có kịch bản mới hay không
            facebookService.likeVideoWatch();
          } else {
            // nếu đã chạy xog kịch bản hoặc có kịch bản mới từ server
            console.log("hết kịch bản, hoặc có kịch bản mới từ server !");
            signalRService.reqUpdateScript(
              res.firstScript.scriptId,
              res.clientId
            );
          }
        }
      );
    }, parseInt(randTime));
  },

  // SurfStories = 11,

  SurfStories: () => {
    $(document).ready(function () {
      chrome.storage.sync.get(
        ["timeSurfStories", "firstScript", "clientId"],
        function (res) {
          timeSurf = parseInt(res.timeSurfStories) * 60 + 20;
          setTimeout(() => {
            var firstStory = document.getElementsByClassName(
              firstSurfStory
            )[0] as HTMLElement;
            firstStory.click();
          }, 1000);
          setTimeout(() => {
            var counter = setInterval(timer, 10000);
            function timer() {
              timeSurf = timeSurf - 10;
              console.log(timeSurf);
              if (timeSurf <= 0) {
                console.log("hết kịch bản, hoặc có kịch bản mới từ server !");
                clearInterval(counter);
                var closeStoryDoc = document.getElementsByClassName(
                  closeStory
                )[0] as HTMLElement;
                setTimeout(() => {
                  if (closeStoryDoc) {
                    closeStoryDoc.click();
                  }
                  signalRService.reqUpdateScript(
                    res.firstScript.scriptId,
                    res.clientId
                  );
                }, 500);
              }
            }
          }, 3000);
        }
      );
    });
  },

  // JoinGroup = 12

  joinGroup: (searchGrpName) => {
    console.log("Đang xử lý nhóm thứ: ", flagOfType12 + 1);
    const randTime = (Math.random() * (max - min) + min).toFixed();
    const randJoin = Math.floor(Math.random() * ranDom.length);
   // console.log("Time random: ", randTime);
    $(document).ready(function () {
      var divsSearchGroup = document.getElementsByClassName(divSearchGroup);
      if (divsSearchGroup.length - 1 >= flagOfType12) {
        divsSearchGroup[flagOfType12].scrollIntoView({
          behavior: "smooth",
          block: "end",
          inline: "nearest",
        });
       // console.log(divsSearchGroup);
        
        let typeChild = divsSearchGroup[flagOfType12].getElementsByClassName(
          groupTypeChild
        )[0] as HTMLAnchorElement;
        var avatar1 = divsSearchGroup[flagOfType12].getElementsByTagName('image')[0];
        var avatarGroup =  avatar1.getAttribute('xlink:href');


        let btnClickJoinGroup = divsSearchGroup[flagOfType12].getElementsByClassName(btnJoinGroup)[0] as HTMLAnchorElement;
        let nameChild = divsSearchGroup[flagOfType12].getElementsByClassName(
          groupNameChild
          )[0] as HTMLAnchorElement;
       let contentChild = divsSearchGroup[flagOfType12].getElementsByClassName(groupContentChild
        )[0] as HTMLAnchorElement;
        if (typeChild) {
          if (typeChild.textContent?.includes(typeGroup)) {
            console.log("Nhóm công khai");
            let inpContent = funcPublic.formatContent(searchGrpName);
            let content = funcPublic.formatContent(nameChild.textContent);
            let arrayContent = funcPublic.splitContent(inpContent);
            let resultIncludes = funcPublic.includesContent(
              arrayContent,
              content
            );
            setTimeout(() => {
              if (resultIncludes) {
                if (ranDom[randJoin]) {
                  console.log(
                    "Join group yes/no: ",
                    ranDom[randJoin]
                  );
                  chrome.storage.sync.get(["userName",], function (res) {
                    setTimeout(() => {
                      proxyService.postGroup(res.userName, nameChild.innerText, avatarGroup, nameChild.href, contentChild.innerText);// post table group-join
                    }, 1000);
                  });
                  btnClickJoinGroup.click();
                  console.log("Nhóm đã tham gia:", ++countJoin);
                } else {
                  console.log(
                    "Join group yes/no: ",
                    ranDom[randJoin]
                  );
                }
              } else {
                console.log("Tên nhóm KHÔNG liên quan đến nội dung tìm kiếm");
              }
            }, 500);
          } else {
            console.log("Nhóm riêng tư");
          }
        }
      }
      setTimeout(() => {
        flagOfType12++;
        setTimeout(() => {
          chrome.storage.sync.get(
            ["totalJoinGrp", "firstScript", "ipHost", "isChangeScript", "clientId"],
            function (res) {
              if (res.totalJoinGrp > countJoin && res.isChangeScript != true) {
                // kiểm tra số lượng like đã thực thi và có kịch bản mới hay không
                facebookService.joinGroup(searchGrpName);
              } else {
                // nếu đã chạy xog kịch bản hoặc có kịch bản mới từ server
                console.log("hết kịch bản");
                signalRService.reqUpdateScript(
                  res.firstScript.scriptId,
                  res.clientId
                );
              }
            }
          );
        }, 1000);
      }, parseInt(randTime));
    });
  },
  
  // WatchLivestream = 13

  watchLiveStream: () => {
    var today = new Date();
    var time = today.getHours() + ":" + today.getMinutes();
    setInterval(function () {
      chrome.storage.sync.get(["timeWatchStream", "firstScript", "clientId"], function (res) {
        console.log("time end:",res.timeWatchStream);
        if (
          parseInt(res.timeWatchStream) > parseInt(time) &&
          res.isChangeScript != true
        ) {
        } else {
          console.log("hết kịch bản, hoặc có kịch bản mới từ server !");
          signalRService.reqUpdateScript(
            res.firstScript.scriptId,
            res.clientId);
        }
      });
    }, 10000);
  },

  // close = 15

  // hàm đăng nhập khi ở trang login
  loginFace: (userName, passWord) => {
    $(document).ready(function () {
      $("#email").val(userName);
      $("#pass").val(passWord);
      setTimeout(() => {
        $("#loginbutton").click();
      }, 1000);
    });
  },

  // 16
  set2Fa: (pin) => {
    var input2Fa = document.getElementById(input2fa) as HTMLInputElement;
    if (input2Fa) {
      input2Fa.value = pin;
      facebookService.clickBtnCheckpoint();
    } else {
      facebookService.clickBtnCheckpoint();
    }
  },

  clickBtnCheckpoint: () => {
    var btn = document.getElementById(btnCheckpoint);
    if (btn) {
      btn.click();
    }
  },
  // chia sẻ bài viết trong nhóm truyền vào id bài viết và tên nhóm
  sharePostInGroup: (groupName) => {
    facebookService.clickBtnShare(groupName);// chọn btn chia sẻ
  },

  clickBtnShare: (groupName) => {
    console.log("test");

    setTimeout(() => {
      $(document).ready(function () {
        var listBT;
        listBT = document.getElementsByClassName(listBTShare);
        if (listBT.length == 0) {
          listBT = document.getElementsByClassName(listBTShareEqualZero);
        }
        console.log(listBT);
        if (listBT.length > 0) {
          var bt = listBT[0].getElementsByClassName(btnShare);
          for (let i = 0; i < bt.length; i++) {
            var btn = bt[i] as HTMLAnchorElement;
            if (btn.innerText == "Chia sẻ") {
              console.log(btn.innerText);
              setTimeout(() => {
                btn.click();
                setTimeout(() => {
                  facebookService.clickBtnShareGroup(groupName);
                }, 4000);
              }, 2000);
            }
          }
        }
      });
    }, 1500);
  },
  clickBtnShareGroup: (groupName) => {
    setTimeout(() => {
      $(document).ready(function () {
        var listBtnShare = document.getElementsByClassName(listBtnShareGroup);
        // console.log(listBtnShare);
        // var btnShGr = listBtnShare[6].getElementsByClassName(btnShareGr);
        var btnShGr = listBtnShare[6] as HTMLAnchorElement;
        // if (btnShGr.length > 0) {
        console.log(btnShGr);
        // var gr = btnShGr[4] as HTMLAnchorElement;
        if (btnShGr.innerText == "Chia sẻ lên nhóm") {
          setTimeout(() => {
            btnShGr.click();
            setTimeout(() => {
              facebookService.clickGroup(groupName);
            }, 4000);
          }, 2000);
        }
        // }
      });
    }, 1000);
  },

  clickGroup: (groupName) => {
    setTimeout(() => {
      $(document).ready(function () {
        var allGr = document.getElementsByClassName(allGroup);
        console.log(allGr);
        var group = allGr[1].getElementsByClassName(listGroup);
        console.log(group);
        if (group.length > 0) {
          for (let i = 0; i < group.length; i++) {
            var gr = group[i] as HTMLMapElement;
            if (gr.innerText.includes(groupName)) {
              console.log(gr.innerText);
              gr.click();
              setTimeout(() => {
                facebookService.postShare();
              }, 2000);
            }
          }
        }
      });
    }, 2000);
  },
  // chia sẻ bài viết trên tường
  postShare: () => {
    $(document).ready(function () {
      var post = document.getElementsByClassName(btnPost)[0] as HTMLAnchorElement;
      if (post.innerText == "Đăng") {
        setTimeout(() => {
          chrome.storage.sync.get(["firstScript", "clientId", "urlPost", "userName", "nameGroup", "typeName", "scriptName"], function (res) {
            var content = res.typeName + "!@#$%^&*()" + res.nameGroup;
            proxyService.postClientActivity(res.userName, content, res.urlPost, res.scriptName);
            signalRService.reqUpdateScript(
              res.firstScript.scriptId,
              res.clientId
            );
          });
          post.click();
        }, 2500);
      }
    });
  },
  // join group with url 
  JoinGroupWithURL: () => {
    $(document).ready(function () {
      var inforGroup = document.getElementsByClassName(ifGroup)[0] as HTMLAnchorElement;
      console.log(inforGroup.innerText);
      if (inforGroup.innerText == "Nhóm Công khai") {
        var btnJoinGroup = document.getElementsByClassName(btnJoin)[0] as HTMLAnchorElement;
        console.log(btnJoinGroup);
        console.log(btnJoinGroup.innerText);
        // var btnIntroduce = document.getElementsByClassName(btnIntroduceJoinGroupWithUrl)[1] as HTMLAnchorElement;// btn giới thiệu
        // if (btnIntroduce.innerText == "Giới thiệu") {
        // setTimeout(() => {
        //btnIntroduce.click();
        setTimeout(() => {
          var name = document.getElementsByClassName(nameGroup)[0] as HTMLAnchorElement;// name group
          var nameGR = name.innerText;
          var inf = document.getElementsByClassName(contentGroup)[5] as HTMLAnchorElement;// content group
          var avata = document.getElementsByClassName(avatarGroup)[1];// avatar group
          var avataGR = avata.getAttribute('src');
          chrome.storage.sync.get(["userName", "urlGroup"], function (res) {
            setTimeout(() => {
              console.log(res.userName, nameGR, avataGR, res.urlGroup, inf.innerText);
              proxyService.postGroup(res.userName, nameGR, avataGR, res.urlGroup, inf.innerText);// post table group-join
            }, 1000);
          });
        }, 500);
        //   }, 1000);
        // }
        if (btnJoinGroup.innerText == "Tham gia nhóm") {
          setTimeout(() => {
            chrome.storage.sync.get(["firstScript", "clientId", "urlGroup", "typeName", "userName", "scriptName"], function (res) {
              var content = res.typeName;
              proxyService.postClientActivity(res.userName, content, res.urlGroup, res.scriptName);
              signalRService.reqUpdateScript(
                res.firstScript.scriptId,
                res.clientId
              );
            });
            btnJoinGroup.click();
          }, 4000);
        }
      }
      else {
        console.log("nhóm riêng tư không thể Join");
      }
    });
  },
  // read information friends   // scripts type = 6
  readInformationFriend: () => {
    $(document).ready(function () {
      const randTime = (Math.random() * (5000 - 4000) + 4000).toFixed();
      var fr = document.getElementsByClassName(cardFriend);
      // console.log(fr);

      if (fr.length > 0) {
        var avt = fr[countFR].getElementsByClassName(avatarFR);
        var urlFR = avt[0].getAttribute('href');
        var urlAvatar = avt[0].getElementsByClassName(urlAvatarReadInformationFriend);
        var urlAVT = urlAvatar[0].getAttribute('src');
        var name = fr[countFR].getElementsByClassName(nameReadInformationFriend)[0] as HTMLAnchorElement;
        console.log(name.innerText);
        fr[countFR].scrollIntoView({ behavior: "smooth", block: "center", inline: "center" });
        chrome.storage.sync.get(["clientId"], function (res) {
          console.log("add friend");
          proxyService.postFriend(res.clientId, urlFR, name.innerText, urlAVT);
        });
      }
      setTimeout(() => {
        chrome.storage.sync.get(["firstScript", "clientId"], function (res) {
          if (fr.length - 1 > countFR) {
            countFR++;
            console.log("length :", fr.length);

            console.log("count friend add,", countFR);

            facebookService.readInformationFriend();
          }
          else {
            console.log("hết kịch bản hoặc có kịch bản mới");
            signalRService.reqUpdateScript(
              res.firstScript.scriptId,
              res.clientId
            );
          }
        });
      }, parseInt(randTime));
    });
  },
  getres: () => {
    var urlRequestServer = url + "/api/extension-variable/List";
    var onload = function (e) {
      if (this.status == 200) {
        var res = JSON.parse(JSON.stringify(this.response));

        classVideoNews = facebookService.getValueExtensionVariableByClassName("classVideoNews", res);
        classNews = facebookService.getValueExtensionVariableByClassName("classNews", res);
        classNewsContent = facebookService.getValueExtensionVariableByClassName("classNewsContent", res);
        classVideoContent = facebookService.getValueExtensionVariableByClassName("classVideoContent", res)
        urlVideos = facebookService.getValueExtensionVariableByClassName("urlVideos", res)
        urlNews = facebookService.getValueExtensionVariableByClassName("urlNews", res);
        btnLike = facebookService.getValueExtensionVariableByClassName("btnLike", res);
        btnReaction = facebookService.getValueExtensionVariableByClassName("btnReaction", res);
        btnReactionChild = facebookService.getValueExtensionVariableByClassName("btnReactionChild", res);
        divSearchGroup = facebookService.getValueExtensionVariableByClassName("divSearchGroup", res)
        groupNameChild = facebookService.getValueExtensionVariableByClassName("groupNameChild", res);
        groupTypeChild = facebookService.getValueExtensionVariableByClassName("groupTypeChild", res);
        btnJoinGroup = facebookService.getValueExtensionVariableByClassName("btnJoinGroup", res);
        btnAddFr = facebookService.getValueExtensionVariableByClassName("btnAddFr", res);
        typeGroup = facebookService.getValueExtensionVariableByClassName("typeGroup", res);
        cardFr = facebookService.getValueExtensionVariableByClassName("cardFr", res);
        input2fa = facebookService.getValueExtensionVariableByClassName("input2fa", res);
        btnCheckpoint = facebookService.getValueExtensionVariableByClassName("btnCheckpoint", res);
        cardGroup = facebookService.getValueExtensionVariableByClassName("cardGroup", res);
        buttonAccept = facebookService.getValueExtensionVariableByClassName("buttonAccept", res);
        firstSurfStory = facebookService.getValueExtensionVariableByClassName("firstSurfStory", res);
        closeStory = facebookService.getValueExtensionVariableByClassName("closeStory", res);
        cardNotification = facebookService.getValueExtensionVariableByClassName("cardNotification", res);
        notification = facebookService.getValueExtensionVariableByClassName("notification", res);
        information = facebookService.getValueExtensionVariableByClassName("information", res);
        informationF = facebookService.getValueExtensionVariableByClassName("informationF", res);
        contactCard = facebookService.getValueExtensionVariableByClassName("contactCard", res);
        btnShare = facebookService.getValueExtensionVariableByClassName("btnShare", res);
        allGroup = facebookService.getValueExtensionVariableByClassName("allGroup", res);
        listGroup = facebookService.getValueExtensionVariableByClassName("listGroup", res);
        btnPost = facebookService.getValueExtensionVariableByClassName("btnPost", res);
        ifGroup = facebookService.getValueExtensionVariableByClassName("ifGroup", res);
        btnJoin = facebookService.getValueExtensionVariableByClassName("btnJoin", res);
        nameGroup = facebookService.getValueExtensionVariableByClassName("nameGroup", res);
        avatarFR = facebookService.getValueExtensionVariableByClassName("avatarFR", res);
        cardFriend = facebookService.getValueExtensionVariableByClassName("cardFriend", res);
        avatar = facebookService.getValueExtensionVariableByClassName("avatar", res);
        contentNotification = facebookService.getValueExtensionVariableByClassName("contentNotification", res);
        timeNotification = facebookService.getValueExtensionVariableByClassName("timeNotification", res);
        cardChildDrawer = facebookService.getValueExtensionVariableByClassName("cardChildDrawer", res);
        nameGroup = facebookService.getValueExtensionVariableByClassName("nameGroup", res);
        informationGroup = facebookService.getValueExtensionVariableByClassName("informationGroup", res);
        information1Group = facebookService.getValueExtensionVariableByClassName("information1Group", res);
        cardMakeFriendsSuggestion = facebookService.getValueExtensionVariableByClassName("cardMakeFriendsSuggestion", res);
        cardAddFriendsFromGroup = facebookService.getValueExtensionVariableByClassName("cardAddFriendsFromGroup", res);
        cardCountFrGr = facebookService.getValueExtensionVariableByClassName("cardCountFrGr", res);
        btnCardAddFr = facebookService.getValueExtensionVariableByClassName("btnCardAddFr", res);
        listBTShare = facebookService.getValueExtensionVariableByClassName("listBTShare", res);
        listBTShareEqualZero = facebookService.getValueExtensionVariableByClassName("listBTShareEqualZero", res);
        listBtnShareGroup = facebookService.getValueExtensionVariableByClassName("listBtnShareGroup", res);
        btnIntroduceJoinGroupWithUrl = facebookService.getValueExtensionVariableByClassName("btnIntroduceJoinGroupWithUrl", res);
        contentGroup = facebookService.getValueExtensionVariableByClassName("contentGroup", res);
        avatarGroup = facebookService.getValueExtensionVariableByClassName("avatarGroup", res);
        urlAvatarReadInformationFriend = facebookService.getValueExtensionVariableByClassName("urlAvatarReadInformationFriend", res);
        nameReadInformationFriend = facebookService.getValueExtensionVariableByClassName("nameReadInformationFriend", res);
        groupContentChild = facebookService.getValueExtensionVariableByClassName("groupContentChild", res);
      }
    };
    httpRequestService.requestService(null, "GET", urlRequestServer, onload, false);
  },
  getValueExtensionVariableByClassName(className: any, res: any) {
    if (res.length > 0) {
      var extensionVariable = res.find(x => x.className == className);

      return extensionVariable.value;
    }
  },
  checkClientOnline() {
    chrome.storage.sync.get(["clientId"], function (res) {
      var urlRequestServer = url + "/api/client/check-client-online?clientId=" + res.clientId;

      var onload = function (e) {
        if (this.status == 200) {
          var res = JSON.parse(JSON.stringify(this.response));

          console.log('online', res);

          if (res == false) {
            setTimeout(() => {
              chrome.runtime.sendMessage({ mess: "closeAll" });
            }, 1000)
          }
        }
      };

      httpRequestService.requestService(null, "GET", urlRequestServer, onload, false);
    });

  }
};

export default facebookService;
