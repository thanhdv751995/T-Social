import { Observable } from "rxjs";
export default class GraphApiService {
  constructor() { }

  public getGraphApi(urlGraph, accessToken): Promise<any> {
    var url =
      `https://graph.facebook.com/` + urlGraph + `&access_token=` + accessToken;

    let promise = new Promise((resolve, reject) => {
      return fetch(url, {
        method: "GET",
        credentials: "same-origin",
      })
        .then((res) => {
          return res.json();
        })
        .then((res) => {
          resolve(res);
        })
        .catch((error) => {
          reject(error);
        });
    });
    return promise;
  }

  public postGraphApi(urlGraph, data, accessToken): Promise<any> {
    let promise = new Promise((resolve, reject) => {
      var xmlhttp = new XMLHttpRequest();
      xmlhttp.open(
        "POST",
        "https://graph.facebook.com" + urlGraph
      );
      xmlhttp.setRequestHeader("Authorization", "Bearer " + accessToken);
      xmlhttp.setRequestHeader("Content-type", "application/json");
      xmlhttp.setRequestHeader("Accept", "application/json");
      xmlhttp.setRequestHeader("Access-Control-Allow-Origin", "*");
      xmlhttp.responseType = "json";
      xmlhttp.send(JSON.stringify(data));
      xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4) {
          if (xmlhttp.status == 200) {
            resolve(xmlhttp.response);
          } else {
            reject('Đã xảy ra lỗi.');
          }
        }
      }
    });

    return promise;
  }
  //  public  getResponse(proxied): Promise<any> {
  //   var xmlhttp = new XMLHttpRequest();
  //     XMLHttpRequest = function() {
  //         //cannot use apply directly since we want a 'new' version
  //         var wrapped = new(Function.prototype.bind.apply(proxied, arguments));

  //         Object.defineProperty(wrapped, 'responseText', {
  //             writable: true
  //         });

  //         return wrapped;
  //     };
  // };
}
