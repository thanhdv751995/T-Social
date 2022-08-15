import { Component, OnInit } from '@angular/core';
import { infoTotalAccount } from 'src/modal/infoTotalAccount';
import { ClientFacebookService } from 'src/serivce/Client-Facebook/client-facebook.service';
import {
  TDSContextMenuService,
  TDSDropdownMenuComponent,
  TDSPlacementType,
  TDSSafeAny,
  TDSTableQueryParams,
} from 'tmt-tang-ui';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {
  InfoLoading = false
  selected: any;
  expandSet = new Set<number>();
  infoTotalAccount: infoTotalAccount;
  showInfo = false;
  idClientToShow: number;
  skipListAccountFacebook: number = 0;
  conditionNumber= 0;
  pageIndex = 1;
  pageSize = 13;
  listOfData: Array<TDSSafeAny> = [];
  constructor(
    private clientService: ClientFacebookService,
    private tdsContextMenuService: TDSContextMenuService
  ) {}
  totalTable : number = 0;
  ngOnInit(): void {
  }
  onExpandChange(id: number, checked: boolean): void {
    this.idClientToShow = id;
    this.showInfo = checked;
    if (checked) {
      this.expandSet.add(id);
    } else {
      this.expandSet.delete(id);
    }
  }
  onQueryParamsChange(params: TDSTableQueryParams): void {  
    this.skipListAccountFacebook = (params.pageIndex -1 ) * params.pageSize;
    this.getListAccount()
}
  getListAccount() {
    this.InfoLoading= true
    this.clientService
      .GetListAccountFacebook(
        this.skipListAccountFacebook,
        this.pageSize,
        this.conditionNumber
      )
      .subscribe((res) => {

        this.totalTable = res.accountDtos.totalCount
        this.infoTotalAccount = res;
        this.listOfData = res.accountDtos.items;
        this.InfoLoading = false
      });
  }
  getListActive(test: string) {
    if(this.conditionNumber == parseInt(test))
    {
      this.conditionNumber = 5
    }
    else
    {
      this.conditionNumber = parseInt(test);
    }
    this.getListAccount();
  }

  ///dropdownAvatar
  contextMenu($event: MouseEvent, menu: TDSDropdownMenuComponent): void {
    this.tdsContextMenuService.create($event, menu);
  }

  closeMenu(): void {
    this.tdsContextMenuService.close();
  }
}
