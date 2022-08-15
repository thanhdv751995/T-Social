import { Component, Input, OnInit } from '@angular/core';
import { ClientFacebookService } from 'src/serivce/Client-Facebook/client-facebook.service';
import { GroupService } from 'src/serivce/Groups/group.service';
import { TDSTableQueryParams } from 'tmt-tang-ui';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css']
})
export class GroupComponent implements OnInit {
  @Input() userNameChart: string = '';
  listAccount: any;
  idUser: string = "";
  listGroup: any;
  skip = 0;
  pageSize = 10;
  pageIndex = 1;
  totalTable = 0;
  constructor(
    private accountService: ClientFacebookService,
    private groupService: GroupService
  ) { }

  ngOnInit(): void {
    this.getlistAccount();
    // this.getListGroup()
  }
  getlistAccount() {
    this.accountService.getListAccount().subscribe(data => {
      this.listAccount = data.items;
    })
  }
  getListGroup() {
    this.groupService.getListGroup(this.userNameChart, this.skip,this.pageSize).subscribe(data => {
      console.log(data);
      this.listGroup = data.items;
      this.totalTable = data.totalCount
    })
  }
  onQueryParamsChange(params: TDSTableQueryParams): void {
    this.skip = (params.pageIndex - 1) * params.pageSize;
    this.getListGroup()
}
}
