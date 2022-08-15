import {
  AfterViewInit,
  Component,
  ElementRef,
  Input,
  OnInit,
  ViewChild,
} from '@angular/core';
import Chart from 'chart.js/auto';
import { ActiviteChartService } from 'src/serivce/ActiviteCharts/activite-chart.service';
import { TDSSafeAny, TDSTableQueryParams } from 'tmt-tang-ui';

@Component({
  selector: 'app-client-activity-chart',
  templateUrl: './client-activity-chart.component.html',
  styleUrls: ['./client-activity-chart.component.css'],
})
export class ClientActivityChartComponent implements OnInit, AfterViewInit {
  @ViewChild('lineCanvas') lineCanvas: ElementRef;
  listActivity: any = null;
  @Input() userNameChart: string = '';
  barChartClient: any;
  isShowChart = false;
  //filter dateOfBirth
  startDateChart: Date;
  endDateChart: Date;
  yearChart: Date;
  startDate :string = ''
  endDate : string =''
  timeShowChart: number = 0;
  height: number = 100;
  width: number = 200;
  logSkip: number = 0;
  logTake: number = 10;
  pageIndex = 1;
  pageSize = 10;
  rangeDate = null;
  logsActivity:Array<TDSSafeAny> = [
      
  ];
  totalTable : number = 0;
  isVisible = false;
  constructor(private activiteChartService: ActiviteChartService) {}
  ngAfterViewInit(): void {}
  ngOnInit(): void {
    this.getListActivityClient();
  }
  lineChartMethod() {
    this.barChartClient = new Chart(this.lineCanvas?.nativeElement, {
      type: 'bar',
      options: {
        animation: {
          duration: 1000,
      },
        // animation: {
        //   onComplete: () => {
        //     delayed = true;
        //   },
        //   delay: (context) => {
        //     let delay = 0;
        //     if (context.type === 'data' && context.mode === 'default' && !delayed) {
        //       delay = context.dataIndex * 300 + context.datasetIndex * 100;
        //     }
        //     return delay;
        //   },
        // },
        plugins: {
          title: {
            display: true,
            text: 'Client - Activity',
          },
        },
      },
      data: {
        labels: this.listActivity?.labels,
        datasets: this.listActivity?.datasets,
      },
    });
  }
  getListActivityClient() {
    const start = Date.now();
    this.activiteChartService
      .getListActivityByClientId(
        this.userNameChart,
        this.startDate,
        this.endDate,
      )
      .subscribe((res) => {
        this.timeShowChart = Date.now() - start;
        this.isShowChart = true;
        this.listActivity = res;
        this.lineChartMethod();
      });
  }
  //modal
  logActivity(idUser: string) {
      window.open(
        '/#/activityLogs/' + idUser,
        '_blank' // <- This is what makes it open in a new window.
      );
    }
    // this.activiteChartService
    //   .getLogActivity(this.userNameChart, this.logSkip, this.logTake)
    //   .subscribe((res) => {
    //     this.totalTable = res.totalCount;
    //     this.logsActivity = res.items;
    //     this.isVisible = true;
    //   });
//   onQueryParamsChange(params: TDSTableQueryParams): void {
//     this.logSkip = (params.pageIndex - 1) * params.pageSize;
//     this.logActivity()
// }
  handleOk(): void {
    this.isVisible = false;
  }

  handleCancel(): void {
    this.isVisible = false;
  }

    //range date picker
    onChange(result: any): void {
      if(result == undefined || result.length == 0)
      {
        this.barChartClient.destroy();
        this.startDate =''
        this.endDate = ''
        this.getListActivityClient()
      }
      else
      {
        this.barChartClient.destroy();
        this.startDate =new Date(result[0].getFullYear(),result[0].getMonth(),result[0].getDate(),0,0,0,0).toISOString();   
        this.endDate =new Date(result[1].getFullYear(),result[1].getMonth(),result[1].getDate(),0,0,0,0).toISOString();   
        this.getListActivityClient()
      }
    }
}
