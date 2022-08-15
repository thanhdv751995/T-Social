var token = "EAABwzLixnjYBAKIVnViLFKu0mCxqkhik25ZAH9qcaojoPoJ0ZA4OVjAFeWrtVwrOUEZCHXaZAytemirOHjtgjhmSROqUgbztFdqoKHxxnRMrWE8ZBsny3hTHYldaMnwHLuqYd0ZCn48ZB8r1pZCHVrUJ4yeiWZC7KlVEzEaH2LLTZBRWNgHuCRtM6e9hkchmix9hQZD";
const httpRequestService = {
    requestService: (data, method, url, functionOnload, IsHaveBearer) => {
        var xmlhttp = new XMLHttpRequest();
        xmlhttp.open(
            method,
            url,
        );
        if (method == "POST") {
            if (IsHaveBearer == true) {
                xmlhttp.setRequestHeader("Authorization", "Bearer " + token);
            }
            xmlhttp.setRequestHeader("Content-type", "application/json");
            xmlhttp.setRequestHeader("Accept", "application/json");
            xmlhttp.setRequestHeader("Access-Control-Allow-Origin", "*");
            xmlhttp.responseType = "json";
            xmlhttp.send(JSON.stringify(data));
            xmlhttp.onload = functionOnload;

        }
        else if (method = "PUT") {
            var json = JSON.stringify(data);
            xmlhttp.setRequestHeader("Content-type", "application/json; charset=utf-8");
            xmlhttp.onload = functionOnload;
            xmlhttp.send(json);
        }
        else if (method == "GET") {
            if (IsHaveBearer == true) {
                xmlhttp.setRequestHeader("Authorization", "Bearer " + token);
            }
            xmlhttp.overrideMimeType("application/json");
            xmlhttp.responseType = "json";
            xmlhttp.onload = functionOnload;
            xmlhttp.send();
        }
        return xmlhttp;
    },

};
export default httpRequestService;