import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Chart } from 'chart.js';
import { map, switchMap } from 'rxjs/operators';
import { ActiviteChartService } from 'src/serivce/ActiviteCharts/activite-chart.service';

@Component({
  selector: 'app-polar-chart',
  templateUrl: './polar-chart.component.html',
  styleUrls: ['./polar-chart.component.css']
})
export class PolarChartComponent implements OnInit,AfterViewInit {
  @ViewChild('barCanvas') barCanvas: ElementRef;
  barChart : any;
  dataChart : any;
  rangeDate = null;
  startDate = null;
  endDate = null;
  ingredientList : any;
  constructor(
    private activiteChartService : ActiviteChartService
  ) { }
  ngAfterViewInit(): void {
  }

  ngOnInit(): void {
    this.getDataChart()
    this.getDataBarChart()
  }
  getDataChart()
  {
    this.activiteChartService.getListJobForChart(this.startDate, this.endDate).subscribe(res=>{
      this.lineChartMethod(res)
    })
  }
  getDataBarChart()
  {
    this.activiteChartService.getDataBarChart().subscribe(res=>{
      this.ingredientList = res
    })
  }
  addAlpha(color: string, opacity: number): string {
    // coerce values so ti is between 0 and 1.
    const _opacity = Math.round(Math.min(Math.max(opacity || 1, 0), 1) * 255);
    return color + _opacity.toString(16).toUpperCase();
  }
  lineChartMethod(dataChart) {
    
    this.barChart = new Chart(this.barCanvas.nativeElement, {
      type: 'bar',
      options: {
        plugins: {
          title: {
            display: true,
            text: 'Biểu đồ cột thể hiện số tác vụ',
          },
        },
      },
      data: {
        labels: [
          "Tổng số tác vụ",
          "Tác vụ seeding",
          "Tác vụ chạy kịch bản"
      ],
        datasets: [{
          label: 'Ẩn cột biểu đồ',
          data: dataChart,
          backgroundColor: [
            'rgba(255, 99, 132, 0.2)',
            'rgba(255, 159, 64, 0.2)',
            'rgba(75, 192, 192, 0.2)',
          ],
          borderColor: [
            'rgb(255, 99, 132)',
            'rgb(255, 159, 64)',
            'rgb(75, 192, 192)',
          ],
          borderWidth: 1
        }]
      },
    });
  }
  onChange(result: any): void {
    if(result.length > 0) {
      this.barChart.destroy();
      this.startDate =new Date(result[0].getFullYear(),result[0].getMonth(),result[0].getDate(),0,0,0,0).toISOString();   
      this.endDate =new Date(result[1].getFullYear(),result[1].getMonth(),result[1].getDate(),23,59,0,0).toISOString();   
    }
    else
    {
      this.barChart.destroy();
      this.startDate = null
      this.endDate = null
    }
    this.getDataChart()
  }
}
