import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root'
})
export class ScriptDefaultProfileService {

  constructor(private shareService: ShareServiceService) { }

  public createScriptByList(data: any) {
    const url = `${this.shareService.REST_API_SERVER}/api/script-default-type/Create-By-List`;
    return this.shareService.postHttpClientAnonymous(url, data);
  }
  public createScriptDefaultByList(data: any) {
    const url = `${this.shareService.REST_API_SERVER}/api/script-default-type/Create-By-List-script-profile`;
    return this.shareService.postHttpClientAnonymous(url, data);
  }
  public createProfileByScript(idScript,data: any) {
    let dto = {
      listNameProfiles : data
    }
    const url = `${this.shareService.REST_API_SERVER}/api/script-default-type/Create-by-script?idScript=${idScript}`;
    return this.shareService.postHttpClientAnonymous(url, dto);
  }
  public deleteProfileByScript(idScript,data: string[]) {
    let dto = {
      listNameProfiles : data
    }
    const url = `${this.shareService.REST_API_SERVER}/api/script-default-type/Delete-by-script?idScript=${idScript}`;
    return this.shareService.deleteClientBelongToProfile(url, dto);
  }
}
