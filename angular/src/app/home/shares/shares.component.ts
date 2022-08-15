import { Component, OnInit } from '@angular/core';
import { ClientFacebookService } from 'src/serivce/Client-Facebook/client-facebook.service';
import { GraphService } from 'src/serivce/graph-service/graph.service';

@Component({
  selector: 'app-shares',
  templateUrl: './shares.component.html',
  styleUrls: ['./shares.component.css']
})
export class SharesComponent implements OnInit {
  filterAccount : string ='';
  skipCountAccount : number = 0;
  maxResultCountAccount : number = 20;
  listAccount : any;
  listStatusByClientId : any;
  accessToken : string ='';
  listShared : any;
  constructor(
    private clientService: ClientFacebookService,
    private graphService : GraphService
  ) { }

  ngOnInit(): void {
    this.getListFacebookAccount()
  }
  getListFacebookAccount() {
    this.clientService.getListFacebookAccount(this.filterAccount,this.skipCountAccount,this.maxResultCountAccount)
      .subscribe((res) => {
        this.listAccount = res;
      });
  }
  getListStatusByClientId(accessToken: string){
    this.accessToken = accessToken;
    this.graphService.getListStatusByClientId(accessToken).subscribe(res=>{
      this.listStatusByClientId = res['posts'].data
    })
  }
  getSharedByPostId(postId:string){
    this.graphService.getSharedAPost(this.accessToken,postId).subscribe(res=>{
      this.listShared = res
    })
  }
}
