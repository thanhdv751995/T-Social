import { GraphApiService } from ".";
const graphApiService = new GraphApiService;
const url = `471654824619945/feed`;
const accessToken = `EAABwzLixnjYBABdkZBYhVqHbBs80XT6TJHmejPmc537WkCgYuHv7vTXuuvlpvbJeznWJbQCb55mZB3tZAbQX7DTmpjFElNG8r6coGos6GHhZAve1mzW08aysNUmfZCFaZBsbylBWh2p67JmNjEQifhJalX1KbUPKDQIJUo5XPADJZABjl3QdXwR`;   

const facebookGraphService = {

    async getInfoUserFaceLogin(): Promise<void> {
        let res = await graphApiService.getGraphApi(url,accessToken);
        return res;
    },

    async postGroup(): Promise<void> {
        // let res = await graphApiService.postGraphApi();
        // return res;
    }
}
export default facebookGraphService;