import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { listAccountDto } from 'src/modal/listAccount';
import { listJobDto } from 'src/modal/listJob';
import { CampaignService } from 'src/serivce/campaign/campaign.service';
import { TDSTableQueryParams } from 'tmt-tang-ui';


@Component({
  selector: 'app-task-un-finished',
  templateUrl: './task-un-finished.component.html',
  styleUrls: ['./task-un-finished.component.css']
})
export class TaskUnFinishedComponent implements OnInit {
  @Input() seedingName: string = ''
  expandSet = new Set<number>();
  listSeeding: any;
  setOfCheckedId = new Set<string>();
  listOfData: readonly listAccountDto[] = [];
  // listOfCurrentPageData: readonly listAccountDto[] = [];
  disableButtonAction: boolean = true;
  disableButton: boolean = true;
  indeterminate = false;
  currentRouter: any;
  skip = 0;
  totalCount = 0;
  pageIndex = 1;
  pageSize = 10;
  // scriptId: string[] = [];
  jobId: string[] = [];
  //clientId: string[] = [];
  checked = false;
  data: listJobDto[] = []

  constructor(
    private campaignService: CampaignService,
    public router: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this.updateTask();
    this.seedingName = this.router.snapshot.params.seedingName;
  }
  updateCheckedSet(id: string, checked: boolean): void {
    if (checked) {
      this.setOfCheckedId.add(id);
    } else {
      this.setOfCheckedId.delete(id);
      this.data.forEach(item => {
        if (item.jobId == id) {
          this.data = this.data.filter(x => x != item)
          // var index = this.data.indexOf(item);
          // this.data.splice(index, 1)
        }
      })
    }
  }

  onItemChecked(idJob, idScript: string, idClient, checked: boolean, url): void {
    this.updateCheckedSet(idJob, checked);
    if (checked) {
      let dto = {
        jobId: idJob,
        clientId: idClient,
        scriptId: idScript,
        url: url.split("!@#$%^&*()")[0]
      }
      this.data.push(dto);
      this.jobId.push(idJob);
    } else {
      this.jobId = this.jobId.filter((x) => x != idJob);
    }
    this.refreshCheckedStatus();
  }

  onAllChecked(value: boolean): void {
    this.listSeeding.forEach((item) => {
      this.updateCheckedSet(item.jobId, value);
      if (!this.jobId.includes(item.jobId) && value == true) {
        this.jobId.push(item.jobId);
        let dto = {
          jobId: item.jobId,
          clientId: item.clientId,
          scriptId: item.scriptId,
          url: item.value.split("!@#$%^&*()")[0]
        }
        this.data.push(dto);
      } else {
        this.jobId = [];
      }
    });
    this.refreshCheckedStatus();
  }

  refreshCheckedStatus(): void {
    this.checked = this.listSeeding.every((item) =>
      this.setOfCheckedId.has(item.jobId)
    );
    this.indeterminate =
      this.listSeeding.some((item) =>
        this.setOfCheckedId.has(item.jobId)
      ) && !this.checked;
    if (this.jobId.length == 0) {
      this.disableButton = true;
      this.disableButtonAction = false;
    } else {
      this.disableButton = false;
      this.disableButtonAction = true;
    }
  }
  getListSeedingJobUnfinished() {
    this.campaignService.getListScriptUpFinished(this.seedingName, this.skip, this.pageSize).subscribe(res => {
      this.totalCount = res.totalCount;
      this.listSeeding = res.items;
      console.log(this.listSeeding);
    })
  }
  onQueryParamsChange(params: TDSTableQueryParams): void {
    this.skip = (params.pageIndex - 1) * params.pageSize;
    this.getListSeedingJobUnfinished();
  }
  ///check contents
  checkContent(value: string) {
    return value.includes('facebook.com');
  }
  updateTask() {
    this.data.forEach(item => {
      this.campaignService.updateTaskSeeding(item.scriptId, item.clientId, item.url).subscribe(data => {
        this.cancelScript()
      }
      );
    })
  }
  cancelScript() {
    this.jobId = [];
    this.listSeeding.forEach((element) => {
      this.updateCheckedSet(element.jobId, false);
    });
    this.refreshCheckedStatus();
  }
}
