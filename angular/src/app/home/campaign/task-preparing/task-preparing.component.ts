import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CampaignService } from 'src/serivce/campaign/campaign.service';

@Component({
  selector: 'app-task-preparing',
  templateUrl: './task-preparing.component.html',
  styleUrls: ['./task-preparing.component.css']
})
export class TaskPreparingComponent implements OnInit {

  seedingName =''
  listSeedingJobFuture : any;
  constructor(
    public router: ActivatedRoute,
    private campaignService : CampaignService
  ) { }

  ngOnInit(): void {
    this.seedingName = this.router.snapshot.params.seedingName;
    this.getListSeeding()
    setInterval(() => {
      this.getListSeeding()
    }, 10000)
  }
  getListSeeding(){
    this.campaignService.GetListJobFuture(this.seedingName).subscribe(res=>{
        this.listSeedingJobFuture = res
    })
  }
}
