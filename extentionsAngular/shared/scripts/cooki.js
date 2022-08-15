
function importCookie(cookie) {
	var arr = cookie.split("|");
	if(arr.length>2){
		 for (var i = 0; i < arr.length; i++) {
            try {
				if(arr[i].indexOf('c_user')>-1){
				cookie=arr[i];
				}
            } catch (ex) {
            }
        }
	}
    removeAllCookies(function () {
        var ca = cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            try {
                var name = ca[i].split('=')[0].trim();
                var val = ca[i].split('=')[1].trim();;
                chrome.cookies.set({ url: "https://www.facebook.com", name: name, value: val });
                chrome.cookies.set({ url: "https://web.facebook.com", name: name, value: val });
                chrome.cookies.set({ url: "https://m.facebook.com", name: name, value: val });
                chrome.cookies.set({ url: "https://mbasic.facebook.com", name: name, value: val });
                chrome.cookies.set({ url: "https://developers.facebook.com", name: name, value: val });
                chrome.cookies.set({ url: "https://upload.facebook.com", name: name, value: val });
                chrome.cookies.set({ url: "https://mobile.facebook.com", name: name, value: val });
				chrome.cookies.set({ url: "https://business.facebook.com", name: name, value: val });
				
            } catch (ex) {
                console.log(ex);
            }
        }
        chrome.tabs.getSelected(null, function (tab) {
            var code = 'window.location.reload();';
            chrome.tabs.executeScript(tab.id, { code: code });
        });
    });
    
}

var removeAllCookies = function (callback) {
    if (!chrome.cookies) {
        chrome.cookies = chrome.experimental.cookies;
    }
    var removeCookie = function (cookie) {
        var url = "http" + (cookie.secure ? "s" : "") + "://" + cookie.domain + cookie.path;
        chrome.cookies.remove({ "url": url, "name": cookie.name });
    };
    chrome.cookies.getAll({ domain: "facebook.com" }, function (all_cookies) {
        var count = all_cookies.length;
        for (var i = 0; i < count; i++) {
            removeCookie(all_cookies[i]);
        }
        callback();
    });
    return "COOKIES_CLEARED_VIA_EXTENSION_API";
};
