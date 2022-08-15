import { Component, Input, OnInit } from '@angular/core';
import { CampaignService } from 'src/serivce/campaign/campaign.service';

@Component({
  selector: 'app-client-seeding-dashboard',
  templateUrl: './client-seeding-dashboard.component.html',
  styleUrls: ['./client-seeding-dashboard.component.css']
})
export class ClientSeedingDashboardComponent implements OnInit {
  @Input() clientId: string = '';
  skip = 0;
  take = 10;
  listSeeding : any;
  constructor(
    private campaignService :CampaignService
  ) { }

  ngOnInit(): void {
    this.getListSeeding()
  }
  getListSeeding()
  {
    this.campaignService.getListSeedingByClientId(this.clientId,this.skip,this.take).subscribe(res=>{
      this.listSeeding = res.items;
    })
  }
}
