import { HttpClient, HttpRequest, HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import readXlsxFile from 'read-excel-file';
import { filter } from 'rxjs/operators';
import { CampaignService } from 'src/serivce/campaign/campaign.service';
import { FileService } from 'src/serivce/file.service';
import { GroupTypeService } from 'src/serivce/Group-Type/group-type.service';
import { TDSMessageService, TDSSafeAny, TDSTableQueryParams, TDSUploadChangeParam, TDSUploadFile } from 'tmt-tang-ui';
@Component({
  selector: 'app-campaign',
  templateUrl: './campaign.component.html',
  styleUrls: ['./campaign.component.scss'],
})
export class CampaignComponent implements OnInit {
  typeSeeding = ''
  public campaignForm: FormGroup = new FormGroup({});
  nameCampaignFilter: string = ''
  skipCampaign: number = 0;
  pageSize: number = 10;
  isVisible = false;
  listCampaign: Array<TDSSafeAny> = [];
  listEnumGroupType: any;
  conditionValue: number = 0;
  expandSet = new Set<number>();
  showInfo = false;
  idSeedingToShow: number;
  demoValue: number = 10;
  files: any;
  fileName = ''
  fileURL = ''
  loadingImage = true;
  indexImage;
  totalCount = 0;
  pageIndex = 1
  isVisibleCampaignReady = false
  listSeedingJobFuture: any;
  listDataExcel: any;
  InfoLoading = false;
  listGroupType: any;
  loadingComponent = false
  constructor(
    private campaignService: CampaignService,
    private formBuilder: FormBuilder,
    private msg: TDSMessageService,
    private fileService: FileService,
    private groupTypeService: GroupTypeService) { }

  ngOnInit(): void {
    this.campaignForm = this.formBuilder.group({
      name: new FormControl('', [Validators.required]),
      url: new FormControl('', [Validators.required]),
      postsWall: new FormControl(0, [Validators.required]),
      postsGroup: new FormControl(0, [Validators.required]),
      comments: new FormControl(0, [Validators.required]),
      reacts: new FormControl(0, [Validators.required]),
      sharesWall: new FormControl(0, [Validators.required]),
      sharesGroup: new FormControl(0, [Validators.required]),
      groupType: new FormControl('', [Validators.required]),
    });
    this.addLesson();
    this.addComment();
    this.addShareWall();
  }
  showModalAddCampaign(value): void {
    this.typeSeeding = value;
    this.isVisible = true;
    // this.getListEnumGroupName();
    this.getListGroupType()
  }

  handleOkAddCampaign(): void {
    if (this.listCampaign.some(x => x.name == this.campaignForm.value.name)) {
      this.msg.error('Tên chiến dịch đã tồn tại');
      return;
    }
    // if (this.campaignForm.value.name.includes('++'|| '+'||':+')) {
    //   this.msg.error('Tên chiến dịch chứa kí tự không cho phép!');
    //   return;
    // }
    if (this.listCampaign.some(x => this.campaignForm.value.name.includes(x.name))) {
      var seedingExist = this.listCampaign.find(x => this.campaignForm.value.name.includes(x.name));
      this.msg.error(`Tên chiến dịch không được sử dụng từ khóa đã tạo ở chiến dịch trước: ${seedingExist.name}`);
      return;
    }
    this.InfoLoading = true
    if (this.campaignForm.value.name != '' && this.campaignForm.value.groupType != '') {
      this.isVisible = false;
      let dto = {
        name: this.campaignForm.value.name,
        url: this.campaignForm.value.url,
        postsWall: this.campaignForm.value.postsWall,
        postsGroup: this.campaignForm.value.postsGroup,
        comments: this.campaignForm.value.comments,
        reacts: this.campaignForm.value.reacts,
        sharesWall: this.campaignForm.value.sharesWall,
        sharesGroup: this.campaignForm.value.sharesGroup,
        groupTypeId: this.campaignForm.value.groupType,
        seedingContentDtos: this.lessons.value,
        commentContentList: this.comments.value,
        shareContentList: this.shareWalls.value
      }
      console.log(dto);
      
      this.campaignService.createCampaign(dto).subscribe(() => {
        this.logSeeding(this.campaignForm.value.name)
        this.lessons.reset()
        this.comments.reset()
        this.shareWalls.reset()
        this.getListCampaign()
        this.campaignForm.reset()
        this.InfoLoading = false
      }, error => {
        this.msg.error(error.error.message)
        this.InfoLoading = false
      }
      )
    }
  }
  logSeeding(seedingName: string) {
    window.open(
      '/#/campaign/' + seedingName,
      '_blank'
    );
  }
  logSeedingUnfinished(seedingName: string) {
    window.open(
      '/#/campaign/' + seedingName + '/unfinished',
      '_blank' // <- This is what makes it open in a new window.
    );
  }
  handleCancelAddCampaign(): void {
    this.isVisible = false;
    this.InfoLoading = false

  }
  filterCampaign(event: any) {
    this.nameCampaignFilter = event
    this.getListCampaign()
  }
  onQueryParamsChange(params: TDSTableQueryParams): void {
    this.skipCampaign = (params.pageIndex - 1) * params.pageSize;
    this.getListCampaign()
  }
  //get list campaign
  getListCampaign() {
    this.loadingComponent = true
    this.campaignService.getListSeeding(this.nameCampaignFilter, this.skipCampaign, this.pageSize, this.conditionValue).subscribe(res => {
      this.listCampaign = res.items;
      this.totalCount = res.totalCount
      this.loadingComponent = false
    }, err => {
      this.loadingComponent = false
    })
  }
  deleteCampaign(id: string) {
    this.campaignService.deleteCampaign(id).subscribe(() => {
      this.msg.success('Xóa thành công');
      this.listCampaign = this.listCampaign.filter(x => x.id != id);
    })
  }

  //getListEnum
  // getListEnumGroupName() {
  //   this.campaignService.getListEnumGroupName().subscribe(res => {
  //     this.listEnumGroupType = res;
  //   })
  // }
  getConditionValue(value: string) {
    if (this.conditionValue == parseInt(value)) {
      this.conditionValue = 0
    }
    else {
      this.conditionValue = parseInt(value);
    }
    this.getListCampaign()
  }

  ///sedding logs
  onExpandChange(id: number, checked: boolean): void {
    this.idSeedingToShow = id;
    this.showInfo = checked;
    if (checked) {
      this.expandSet.add(id);
    } else {
      this.expandSet.delete(id);
    }
  }
  /////////////////////form nội dung bài post
  get lessons() {
    return this.form.controls["lessons"] as FormArray;
  }
  form = this.formBuilder.group({
    lessons: this.formBuilder.array([])
  });
  addLesson() {
    const lessonForm = this.formBuilder.group({
      content: [''],
      imageUrl: ['']
    });
    this.lessons.push(lessonForm);
  }

  deleteLesson(lessonIndex: number) {
    this.lessons.removeAt(lessonIndex);
  }
  ////////////////////////form nội dung bình luân
  get comments() {
    return this.formComment.controls["comments"] as FormArray;
  }
  formComment = this.formBuilder.group({
    comments: this.formBuilder.array([])
  });
  addComment() {
    const commentForm = this.formBuilder.group({
      content: ['']
    });
    this.comments.push(commentForm);
    console.log(this.comments);

  }


  deleteCommentForm(commentIndex: number) {
    this.comments.removeAt(commentIndex);
  }
  ///////////////////////nội dung chia sẻ lên tương
  get shareWalls() {
    return this.formShareWall.controls["shareWalls"] as FormArray;
  }
  formShareWall = this.formBuilder.group({
    shareWalls: this.formBuilder.array([])
  });
  addShareWall() {
    const shareWallForm = this.formBuilder.group({
      content: ['']
    });
    this.shareWalls.push(shareWallForm);
  }


  deleteShareWall(shareWallIndex: number) {
    this.shareWalls.removeAt(shareWallIndex);
  }
  ///create attachments
  onFileSelected(event, index) {
    let fileList: FileList = event.target.files;
    if (fileList.length > 0) {
      let file: File = fileList[0];
      this.indexImage = index
      this.loadingImage = false;
      this.fileService.createAttachment(file).subscribe(res => {
        if (res.body) {
          this.loadingImage = true;
          this.form.controls['lessons']['controls'][index].controls['imageUrl'].setValue(res.body[0].content)
        }
      },
        error => {
          this.loadingImage = true;
          this.msg.error(error.error.error.message)
        })
    }
  }
  //handle excel
  handleFileSelect(evt) {
    let fileList: FileList = evt.target.files;
    if (!fileList[0].type.includes('spreadsheetml.sheet')) {
      this.msg.warning('Vui lòng chọn file Excel')
    }
    else {
      readXlsxFile(fileList[0]).then((rows) => {
        rows.forEach((item) => {
          let commentForm = this.formBuilder.group({
            content: [item[0].toString()]
          });
          this.comments.push(commentForm);
          if (this.comments.value[0].content == "") {
            this.comments.removeAt(0)
          }
        })
      })
    }
  }
  getListGroupType() {
    this.groupTypeService.getListGroup('').subscribe(res => {
      this.listGroupType = res
    })
  }
}
