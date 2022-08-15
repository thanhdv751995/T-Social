import { Component, Input, OnInit } from '@angular/core';
import { ClientFacebookService } from 'src/serivce/Client-Facebook/client-facebook.service';
import { ClientFriendService } from 'src/serivce/Client-Friends/client-friend.service';
import { TDSBreadCrumbModule, TDSSafeAny, TDSTableQueryParams } from 'tmt-tang-ui';
@Component({
  selector: 'app-friends',
  templateUrl: './friends.component.html',
  styleUrls: ['./friends.component.css']
})
export class FriendsComponent implements OnInit {
  @Input() userNameChart : string = '';
    listFriend: any;
    idUser:string ="";
    listAccount: any;
    totalTable = 0;
    pageIndex = 1;
    pageSize = 10;
    skipCount = 0;
  constructor(private clientFriendService: ClientFriendService,
            private accountService : ClientFacebookService
    ) { }
 
  ngOnInit() {
    this.getListAccount();
  }

  getListFriend(){
    this.idUser= this.userNameChart;
    this.clientFriendService.getListFriend(this.idUser,this.skipCount,this.pageSize).subscribe(data=>{
        this.listFriend = data.items;
        this.totalTable = data.totalCount
    })
  }
  onQueryParamsChange(params: TDSTableQueryParams): void {
    this.skipCount = (params.pageIndex - 1) * params.pageSize;
    this.getListFriend()
}
  getListAccount(){
    this.accountService.getListAccount().subscribe(data=>{
        this.listAccount = data.items;
    })
  }
}
