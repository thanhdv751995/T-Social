import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { ActiviteChartService } from 'src/serivce/ActiviteCharts/activite-chart.service';

@Component({
  selector: 'app-history-task',
  templateUrl: './history-task.component.html',
  styleUrls: ['./history-task.component.scss'],
})
export class HistoryTaskComponent implements OnInit {
  listHistory;
  InfoLoading = false;
  startDate = null;
  endDate = null;
  rangeDate = [];
  nameSeeding = ''
  myDate = new Date();
  listNameSeeding : any;
  constructor(private activityService: ActiviteChartService, 
              private activatedRoute: ActivatedRoute,
              private router: Router,) {
  }

  ngOnInit(): void {
    this.getListHistory();
    this.getListNameSeeding()
    this.rangeDate = [new Date(this.myDate.getFullYear(),this.myDate.getMonth(),this.myDate.getDate(),0,0,0,0), 
                      new Date(this.myDate.getFullYear(),this.myDate.getMonth(),this.myDate.getDate(),23,59,0,0)] 
  }

  getListHistory() {
    this.InfoLoading = true
    this.activatedRoute
    .queryParams.pipe(switchMap((rout)=>{
      if(rout.seeding != undefined)
      {
        this.nameSeeding = rout.seeding
      }
      else
      {
        this.nameSeeding =''
      }
      return  this.activityService.getHistory(this.startDate, this.endDate,this.nameSeeding)
    })
    ).subscribe((res) => {
      this.listHistory = res;
      this.InfoLoading = false
    }, error => {
      this.InfoLoading = false
    });
  }
  onChange(result: any): void {
    if(result.length > 0) {
      this.startDate =new Date(result[0].getFullYear(),result[0].getMonth(),result[0].getDate(),0,0,0,0).toISOString();   
      this.endDate =new Date(result[1].getFullYear(),result[1].getMonth(),result[1].getDate(),23,59,0,0).toISOString();   
    }
    else
    {
      this.startDate = null
      this.endDate = null
    }
    this.getListHistory();
  }
  getListNameSeeding()
  {
    this.activityService.getListNameSeeding().subscribe(res => {
      this.listNameSeeding = res
    })
  }
  ngModelChangeName(event)

  {
    if(event == null)
    {
      this.nameSeeding = ''
      this.router.navigate(['.'], { relativeTo: this.activatedRoute, queryParams: {} });
    }
    else
    {
      this.nameSeeding = event;
      this.router.navigate(['.'], { relativeTo: this.activatedRoute, queryParams: {seeding :this.nameSeeding}});
    }
    this.getListHistory()
  }
}
