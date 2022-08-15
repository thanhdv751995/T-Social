import { Component, Input, OnInit } from '@angular/core';
import { CampaignService } from 'src/serivce/campaign/campaign.service';
import { ProxyUsingScriptService } from 'src/serivce/proxy-using-script/proxy-using-script.service';
import { TDSTableQueryParams } from 'tmt-tang-ui';
import { isBuffer } from 'util';

@Component({
  selector: 'app-seeding-logs',
  templateUrl: './seeding-logs.component.html',
  styleUrls: ['./seeding-logs.component.css']
})
export class SeedingLogsComponent implements OnInit {
  @Input() seedingName : string = ''
  expandSet = new Set<number>();
  listSeeding : any;
  skip = 0;
  totalCount =0;
  pageIndex = 1;
  pageSize = 10;
  filter = [
    { name: 'Đã xử lý', status : 1},
    { name: 'Chưa xử lý', status : 2}
  ]
  isFinish = 0;
  constructor(
    private clientUsingScriptService : ProxyUsingScriptService,
    private campaignService : CampaignService
  ) { }

  ngOnInit(): void {
    // this.getListSeedingJob();
  }
  getListSeedingJob()
  {
    console.log(this.seedingName);
    
    this.campaignService.getListSeedingByJob(this.seedingName,this.skip,this.pageSize,this.isFinish).subscribe(res=>{
      this.totalCount=res.totalCount;
      this.listSeeding = res.items;
    })
  }
  onQueryParamsChange(params: TDSTableQueryParams): void {
    this.skip = (params.pageIndex - 1) * params.pageSize;
    this.getListSeedingJob();
}
  ///check contents
  checkContent(value : string)
  {
    return value.includes('facebook.com');
  }
  changeStatus(value){
    if(value == null)
    {
      this.isFinish = 0
    }
    else
    {
      this.isFinish = value
      this.pageIndex = 1;
      this.pageSize = 10;
    }
    this.getListSeedingJob()
  }
}
