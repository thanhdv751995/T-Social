import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { differenceInCalendarDays } from 'date-fns';
import { ActiviteChartService } from 'src/serivce/ActiviteCharts/activite-chart.service';
import { TDSSafeAny } from 'tmt-tang-ui';

@Component({
  selector: 'app-client-activity-logs',
  templateUrl: './client-activity-logs.component.html',
  styleUrls: ['./client-activity-logs.component.css']
})
export class ClientActivityLogsComponent implements OnInit {
  idUser: any;
  listLogsActivity : any[] = [];
  take = 100;
  skip = 0;
  SDate : Date;
  EDate : Date;
  startDate : string =''
  endDate : string ='';
  choosePageSize : number = 20;
  tempSkip = 0;
  tempTake= 100;
  dateFilter : string = ''
  totalTable : number = 0;
  totalCountLogs :number = 0;
  rangeDate = null;
    listPageSize = [
    { id: 20, name: 20 },
    { id: 50, name: 50 },
    { id: 100, name: 100 },
    { id: 200, name: 200 },
    ]
  constructor(
    public router: ActivatedRoute,
    private activiteChartService: ActiviteChartService
  ) { }

  ngOnInit(): void {
    this.idUser = this.router.snapshot.params.idUser;
    this.getListLogsActivity()
  }
  onChangePageSize(data: any) {
    this.choosePageSize = data
  }
  getListLogsActivity()
  {
    this.activiteChartService.getLogActivity(this.idUser,this.skip, this.take,this.startDate, this.endDate).subscribe(res=>{
      this.totalTable = res.totalCount;
      this.listLogsActivity= this.listLogsActivity.concat(res.items)      
      
    })
  }
  // onStartDateChange(result: TDSSafeAny): void {
  //   if(result == null)
  //   { 
  //     this.startDate = ''
  //     this.activiteChartService.getLogActivity(this.idUser,this.skip, this.take, this.startDate, this.endDate).subscribe(res=>{
  //       this.totalTable = res.totalCount;
  //         this.listLogsActivity= res.items
  //     })
  //   }
  //   else
  //   {
  //     const  temp =new Date(result.getFullYear(),result.getMonth(),result.getDate(),0,0,0,0);
  //     this.startDate = temp.toISOString()
  //     this.activiteChartService.getLogActivity(this.idUser,this.skip, this.take, this.startDate, this.endDate).subscribe(res=>{
  //       this.totalTable = res.totalCount;
  //       this.listLogsActivity= res.items
  //   })
  //   }
  // }
  // onEndDateChange(result: TDSSafeAny): void {
  //   if(result == null)
  //   { 
  //     this.endDate = ''
  //     this.activiteChartService.getLogActivity(this.idUser,this.skip, this.take, this.startDate, this.endDate).subscribe(res=>{
  //       this.totalTable = res.totalCount;
  //       this.listLogsActivity= res.items
  //     })
  //   }
  //   else
  //   {
  //     const  temp =new Date(result.getFullYear(),result.getMonth(),result.getDate(),0,0,0,0);
  //     this.endDate = temp.toISOString()
  //     this.activiteChartService.getLogActivity(this.idUser,this.skip, this.take, this.startDate, this.endDate).subscribe(res=>{
  //       this.totalTable = res.totalCount;
  //       this.listLogsActivity= res.items
  //   })
  //   }
  // }

  disabledDate = (current: Date): boolean =>
    differenceInCalendarDays(current, new Date()) > 0;
  showMoreLogs()
  { 
    if(this.skip <= this.totalTable)
    {
      this.skip = this.tempTake;
      this.tempTake = this.tempTake + this.choosePageSize;
      this.take = this.choosePageSize;
      this.getListLogsActivity()
    }
  }

  //range date picker
  onChange(result: any): void {
    if(result == undefined || result.length == 0)
    {
      console.log(1);
      
      this.startDate =''
      this.endDate = ''
      this.activiteChartService.getLogActivity(this.idUser,this.skip, this.take, this.startDate, this.endDate).subscribe(res=>{
        this.totalTable = res.totalCount;
        this.listLogsActivity= res.items
      })
    }
    else
    {
      this.startDate =new Date(result[0].getFullYear(),result[0].getMonth(),result[0].getDate(),0,0,0,0).toISOString();   
      this.endDate =new Date(result[1].getFullYear(),result[1].getMonth(),result[1].getDate(),0,0,0,0).toISOString();   
      this.activiteChartService.getLogActivity(this.idUser,this.skip, this.take, this.startDate, this.endDate).subscribe(res=>{
        this.totalTable = res.totalCount;
        this.listLogsActivity= res.items
      })
    }
  }
   //checkContent
   checkContent(value : string)
   {
     if(value.includes('facebook.com'))
     {
       return value
     }
     else
     {
       return 'https://www.facebook.com/'+ value
     }
   }

   checkTypeScript(value : string)
   { 
     if(value == 'ReactionPost')
     {
       return 'Reacts'
     }
     else if(value == 'ShareWall' || value == 'CommentPost' || value == 'SharePostToGroup'|| value == 'SharePost')
     {
       return 'Different'
     }
     else
     {
       return 'Default'
     }
   }
}
