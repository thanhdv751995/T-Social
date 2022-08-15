import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShareServiceService } from '../share-service.service';

@Injectable({
  providedIn: 'root'
})
export class VirtualMachineService {

  constructor(private shareService: ShareServiceService) { }

  public getListVirtualMachine(): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/virtual-machine/list`;
    return this.shareService.returnHttpClient(url);
  }

  public updateActive(data: any): Observable<any> {
    const url = `${this.shareService.REST_API_SERVER}/virtual-machine/Active-VM`;
    return this.shareService.putHttpClient(url, data);
  }
}
