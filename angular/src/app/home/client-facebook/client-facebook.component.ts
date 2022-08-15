import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { listAccountDto } from 'src/modal/listAccount';
import { updateClientDto } from 'src/modal/updateClientDto';
import { ChromeService } from 'src/serivce/chrome/chrome.service';
import { ClientFacebookService } from 'src/serivce/Client-Facebook/client-facebook.service';
import { ClientFriendService } from 'src/serivce/Client-Friends/client-friend.service';
import { ClientActiveService } from 'src/serivce/client-isActive/client-active.service';
import {
  TDSHelperObject,
  TDSMessageService,
  TDSModalService,
  TDSSafeAny,
  TDSTableQueryParams,
} from 'tmt-tang-ui';
import { GraphService } from 'src/serivce/graph-service/graph.service';
import { ClientInfomationService } from 'src/serivce/Client-Infomation/client-infomation.service';
import { ScriptService } from 'src/serivce/scripts/script.service';
import { SignalRService } from 'src/serivce/signalR/signalR.service';
import { ActiviteChartService } from 'src/serivce/ActiviteCharts/activite-chart.service';
import { ClientBelongToProfileService } from 'src/serivce/client-belong-to-profile/client-belong-to-profile.service';
import { profileDto } from 'src/modal/profileDto';
import { ProfileClientService } from 'src/serivce/Profile-Client/profile-client.service';
import { EnumService } from 'src/serivce/Enum/enum.service';
import { map, switchMap } from 'rxjs/operators';
import readXlsxFile from 'read-excel-file';
import { Workbook } from 'exceljs';

import * as fs from 'file-saver';
@Component({
  selector: 'app-client-facebook',
  templateUrl: './client-facebook.component.html',
  styleUrls: ['./client-facebook.component.scss'],
})
export class ClientFacebookComponent implements OnInit {
  json_data=[{
		"nameFacebook": "HoaDz",
		"password": '123456', 
    "avatarUrl": 'example.com',
    "userId" : 123456789,
    "secretKey(nếu bật 2fa không thì để trống)" : 'abcxyz'
	},
]
  isActive = [true, false];
  idAccount: string[] = [];
  nameUser: string = '';
  passWord: string = '';
  nameFacebook: string = '';
  avatarUrl: string = '';
  secretKey: string = '';
  cookie: string = '';
  idUserTemp: string = '';
  disableButton: boolean = true;
  disableButtonAction: boolean = true;
  isVisibleChart = false;
  isVisibleProfile = false;
  ///
  isVisible = false;
  checked = false;
  loading = false;
  indeterminate = false;
  listOfData: readonly listAccountDto[] = [];
  listOfCurrentPageData: readonly listAccountDto[] = [];
  setOfCheckedId = new Set<string>();
  ///list account
  listAccount: Array<any> = [];
  filterAccount = '';
  skipCountAccount = 0;
  pageIndex = 1;
  pageSize = 13;
  // maxResultCountAccount = 10;
  scriptId: string = '';
  firstValue: number = 1;
  itemShowModal?: any;
  endValue: number = 10;
  //modal update client
  isActiveTest: boolean = false;
  isVisibleUpdateClient = false;
  accessToken = '';
  //info
  InfoLoading = false;
  dataChartClient: any;
  listActivity: any;
  userNameChart: string = '';
  listProfile: any;
  listEnumProfile: any;
  isVisibleAddProfile = false;
  idClient: string = '';
  listProfileCheckedAdd: string[] = [];
  listProfileCheckedDelete: string[] = [];
  listProfileStringJoin : any;
  idAddProfile : any;
  //singal
  connectionHub: any;
  totalTable : number = 0;
  updateSecretKey : string = '';
  listNameProfile : any;
  listSelectedProfile : any;
  idAccountCreated : any;
  listProfileWithId : any;
  selected : Array<string> = [];
  currentListProfile =[];
  //excel
  isVisibleExcel = false;
  nameExcelFile =''
  excelCount = 0;
  fileList: any;
  listAccountExcelAdd = [];
  constructor(
    private clientService: ClientFacebookService,
    private modalService: TDSModalService,
    private viewContainerRef: ViewContainerRef,
    private clientActiveService: ClientActiveService,
    private chromeService: ChromeService,
    private message: TDSMessageService,
    private graphService: GraphService,
    private clientFriendService: ClientFriendService,
    private clientInformationService: ClientInfomationService,
    private scriptService: ScriptService,
    private signalRService: SignalRService,
    private activiteChartService: ActiviteChartService,
    private clientBelongToProfileService: ClientBelongToProfileService,
    private profileClientService: ProfileClientService,
    private enumService: EnumService,
  ) { }
  ngOnInit(): void {
    this.signalRService.setConnection();
    this.connectionHub = this.signalRService.connection;
  }
  updateCheckedSet(id: string, checked: boolean): void {
    if (checked) {
      this.setOfCheckedId.add(id);
    } else {
      this.setOfCheckedId.delete(id);
    }
  }

  onItemChecked(id: string, checked: boolean): void {
    this.updateCheckedSet(id, checked);
    if (checked) {
      this.idAccount.push(id);
    } else {
      this.idAccount = this.idAccount.filter((x) => x != id);
    }
    this.refreshCheckedStatus();
  }

  onAllChecked(value: boolean): void {
    this.listOfCurrentPageData.forEach((item) => {
      this.updateCheckedSet(item.id, value);
      if (!this.idAccount.includes(item.id) && value == true) {
        this.idAccount.push(item.id);
      } else {
        this.idAccount = [];
      }
    });
    this.refreshCheckedStatus();
  }

  onCurrentPageDataChange($event: readonly listAccountDto[]): void {
    this.listOfCurrentPageData = $event;
    this.refreshCheckedStatus();
  }

  refreshCheckedStatus(): void {
    this.checked = this.listOfCurrentPageData.every((item) =>
      this.setOfCheckedId.has(item.id)
    );
    this.indeterminate =
      this.listOfCurrentPageData.some((item) =>
        this.setOfCheckedId.has(item.id)
      ) && !this.checked;
    if (this.idAccount.length == 0) {
      this.disableButton = true;
      this.disableButtonAction = false;
    } else {
      this.disableButton = false;
      this.disableButtonAction = true;
    }
  }
  onQueryParamsChange(params: TDSTableQueryParams): void {
    this.skipCountAccount = (params.pageIndex - 1) * params.pageSize;
    this.getListFacebookAccount()
}
  getListFacebookAccount() {
    this.InfoLoading = true
    this.clientService
      .getListFacebookAccount(
        this.filterAccount,
        this.skipCountAccount,
        this.pageSize
      )
      .subscribe((res) => {      
        this.totalTable = res.totalCount;
        this.listAccount = res.items;
        this.InfoLoading = false
      });
  }
  ///modal add account
  showModal(): void {
    this.getNameProfile()
    this.isVisible = true;
  }
  //add account
  handleOk(): void {
    this.isVisible = false;
    this.clientService
      .createAccount(
        this.nameUser,
        this.passWord,
        this.secretKey,
        this.cookie,
        this.accessToken,
        this.nameFacebook,
        this.avatarUrl
      ).pipe(switchMap((res)=>{
        return this.clientBelongToProfileService.CreateProfileChecked(res.id, this.listSelectedProfile)
      })
      ).subscribe(()=>{
        this.nameUser = '';
        this.passWord = '';
        this.secretKey = '';
        this.cookie = '';
        this.accessToken = '';
        this.nameFacebook = '';
        this.avatarUrl = '';
        this.getListFacebookAccount();
      })
  }

  handleCancel(): void {
    this.nameUser = '';
    this.passWord = '';
    this.secretKey = '';
    this.cookie = '';
    this.accessToken = '';
    this.nameFacebook = '';
    this.avatarUrl = '';
    this.isVisible = false;
  }
  deleteSingle(id: string) {
    if (this.idAccount.length == 0) {
      this.modalService.warning({
        title: 'Delete Account',
        content: 'Bạn muốn xóa nick Facebook này ?',
        onOk: () => {
          this.clientService.deleteSingle(id).subscribe((res) => {
            this.listAccount = this.listAccount.filter((x) => x.id != id);
            this.cancelScript()
          });
        },
        onCancel: () => {
          console.log('cancel');
        },
        okText: 'Xác nhận',
        cancelText: 'Hủy bỏ',
      });
    }
  }
  //xóa account
  deleteAccount() {
    this.clientService.deleteAccount(this.idAccount).subscribe(() => {
      this.cancelScript()
      this.getListFacebookAccount();
      this.idAccount = [];
    });
  }
  error(): void {
    if (this.idAccount.length > 0) {
      this.modalService.error({
        title: 'Modal error',
        content: 'You want to delete ??',
        onOk: () => {
          this.deleteAccount();
        },
        onCancel: () => {
          console.log('cancel');
        },
        okText: 'Save',
        cancelText: 'Cancel',
      });
    }
  }
  onChange(e: any) {
    console.log('onChange', e);
  }
  chooseUser() {
    if (this.firstValue != 0 && this.endValue != 0) {
      for (
        this.firstValue;
        this.firstValue <= this.endValue;
        this.firstValue++
      ) {
        if (this.listAccount[this.firstValue - 1]?.id != undefined) {
          let id = this.listAccount[this.firstValue - 1]?.id;
          this.updateCheckedSet(id, true);
          this.idAccount.push(id);
        }
      }
      this.firstValue = 0;
      this.endValue = 0;
      this.disableButton = false;
      this.disableButtonAction = true;
    }
  }
  //modal updateScript
  showModalUpdateClient(res: any): void {

    // this. getNameProfile()
    this.getNameProfileWithId(res.id)
    if (this.idAccount.length == 0) {
      this.idClient = res.id
      this.itemShowModal = res;
      this.updateSecretKey = res.secretKey
      this.isVisibleUpdateClient = true;
    }
  }

  handleOkUpdateClient(data: any): void {
    console.log(data);
    
    data.secretKey = this.updateSecretKey
    this.clientService.updateAccount(data.id, data).subscribe(); 
    let listDelete = this.currentListProfile.filter (n => !this.selected.includes(n))
    let listAdd = this.selected.filter (n => !this.currentListProfile.includes(n))
    if(listDelete != [])
    {
      this.clientBelongToProfileService
      .deleteProfileChecked(data.id, listDelete)
      .subscribe(() => {
        this.currentListProfile = []
      });
    }
    if(listAdd != [])
    {
      this.clientBelongToProfileService
      .CreateProfileChecked(data.id, listAdd)
      .subscribe(() => {
        this.currentListProfile = []
      });
    }
    this.isVisibleUpdateClient = false;
  }

  handleCancelUpdateClient(): void {
    this.currentListProfile = []
    this.isVisibleUpdateClient = false;
  }
  openChrome(): void {
    if (this.idAccount.length > 0) {
      this.idAccount.forEach((x) => {
        let account = this.listAccount.find((item) => item.id === x);
        if (account.chromeProfile) {        
          this.chromeService
            .openChrome(account.chromeProfile, 'https://www.google.com')
            .subscribe();
        } else
          this.message.error(
            `Cannot open chrome profile for ${account.userName}`
          );
      });
      this.cancelScript();
    }
  }

  closeChrome() {
    if (this.idAccount.length > 0) {
      let data = {
        scriptName: 'CloseChrome',
        value: this.idAccount.length.toString(),
        isActive: true,
        isDefault: false,
        type: 15,
      };
      this.scriptService.createScript(data).subscribe((res) => {
        this.message.success(
          'Close ' + this.idAccount.length + ' Chrome Successfully'
        );
        this.cancelScript();
      });
    }
  }

  spliceString(str: string) {
    var str1 = str.indexOf('p50x50/');
    var str2 = str.indexOf('.jpg');
    var result = str.slice(str1 + 7, str2 + 4);
    return result;
  }

  ///////////////get friend user
  getFromFb(acc: any) {
    return new Promise((resolve, rej) => {
      let account = this.listAccount.find((x) => x.id == acc);
      if (account.accessToken && account.accessToken != '') {
        this.graphService
          .getListFriend(account.accessToken)
          .subscribe((res) => {
            let friendDto = res.friends.data.map((x: any) => {
              return {
                idUser: acc,
                userName: x.id,
                friendName: x.name,
                avatarUrl: x.picture.data.url,
              };
            });
            resolve(friendDto);
          });
      }
    });
  }
  forLoop = async () => {
    var dataInfo = [];
    for (var i = 0; i < this.idAccount.length; i++) {
      var data = await this.getFromFb(this.idAccount[i]);
      var dataInfoClient = {
        idUser: this.idAccount[i],
        createClientFriendDtos: data,
      };

      dataInfo.push(dataInfoClient);
    }
    return dataInfo;
  };

  //////////get list friend
  async getListFriend() {
    if (this.idAccount.length > 0) {
      var dataInfo = await this.forLoop();
      this.InfoLoading = true;
      this.clientFriendService.createAccount(dataInfo).subscribe(() => {
        this.message.success('Update Successfully ');
        setTimeout(() => {
          this.InfoLoading = false;
        }, 500);
        this.cancelScript();
      });
    }
  }

  //update info user
  getAPIInfoFb(acc: any) {
    return new Promise((resolve, rej) => {
      let account = this.listAccount.find((x) => x.id == acc);
      if (account.accessToken && account.accessToken != '') {
        this.graphService.getInfoUser(account.accessToken).subscribe((res) => {
          const InfoDto = {
            idUser: acc,
            clientId: res.id,
            nameUser: res.name,
            dayOfBirth: res.birthday,
          };
          resolve(InfoDto);
        });
      }
    });
  }

  forLoopInfo = async () => {
    var dataInfo = [];
    for (var i = 0; i < this.idAccount.length; i++) {
      var data = await this.getAPIInfoFb(this.idAccount[i]);
      dataInfo.push(data);
    }
    return dataInfo;
  };

  //////////get list friend
  async getInfoUser() {
    if (this.idAccount.length > 0) {
      this.InfoLoading = true;
      var dataInfo = await this.forLoopInfo();
      this.clientInformationService
        .CreateInformationUser(dataInfo)
        .subscribe(() => {
          this.message.success('Update Successfully ');
          setTimeout(() => {
            this.InfoLoading = false;
          }, 500);
          this.cancelScript();
        });
    }
  }
  cancelScript() {
    this.idAccount = [];
    this.listOfCurrentPageData.forEach((element) => {
      this.updateCheckedSet(element.id, false);
    });
    this.refreshCheckedStatus();
    this.firstValue = 0;
    this.endValue = 0;
    this.disableButton = true;
  }
  /////////modal chart
  /////chart

  showModalChart(item: any): void {
    if(this.idAccount.length == 0)
    { 
      this.isVisibleChart = true;
      this.dataChartClient = item;
      this.userNameChart = item.userName;
    }
  }

  handleOkChart(): void {
    setTimeout(() => {
      this.isVisibleChart = false;
    }, 3000);
  }

  handleCancelChart(): void {
    this.isVisibleChart = false; 
    this.listProfileCheckedAdd = [];
    this.listProfileCheckedDelete =[];
  }
  getProfile(clientId: any) {
    this.InfoLoading = true;
    this.clientBelongToProfileService.getProfile(clientId).subscribe((data) => {
      this.isVisibleProfile = true;
      this.listProfile = data;
      this.InfoLoading = false;
    });
  }
  //////////////profile modal
  // showModalProfile(item: any): void {
  //   if(this.idAccount.length == 0)
  //   { 
  //     this.getProfile(item.id);
  //     this.userNameChart = item.nameFacebook;
  //     this.idClient = item.id;
  //   }
  // }

  // handleOkProfile(): void {
  //   if (this.listProfileCheckedAdd != []) {
  //     this.clientBelongToProfileService
  //       .CreateProfileChecked(this.idClient, this.listProfileCheckedAdd)
  //       .subscribe(() => {
  //         this.listProfileCheckedAdd = [];
  //       });
  //   }
  //   if (this.listProfileCheckedDelete != []) {

  //     this.clientBelongToProfileService
  //       .deleteProfileChecked(this.idClient, this.listProfileCheckedDelete)
  //       .subscribe(() => {
  //         this.listProfileCheckedDelete = [];
  //       });
  //   }
  //   this.isVisibleProfile = false;
  // }

  // handleCancelProfile(): void {
  //   this.isVisibleProfile = false;
  // }
  getListEnumProfile() {
    this.enumService.getListEnumProfile().subscribe((res) => {
      this.listEnumProfile = res;
    });
  }
  /////////////////////modal add profile
  showModalAddProfile(): void {
    if(this.idAccount.length > 0)
    {
      this.isVisibleAddProfile = true;
      this.getNameProfile()
    }
  }

   handleOkAddProfile(): void {
    this.isVisibleAddProfile = false;
    this.clientBelongToProfileService.CreateProfileByListClient({listClientId : this.idAccount, profileCLientId:this.idAddProfile}).subscribe(res=>{
      this.cancelScript();    
    });
  }

  handleCancelAddProfile(): void {
    this.isVisibleAddProfile = false;
    this.cancelScript();
  }
  getChecked(event: any, profileName: string) {
    if (event.checked) {
      if (!this.listProfileCheckedAdd?.includes(profileName)) {
        this.listProfileCheckedAdd.push(profileName);
      }
    } else {
      if (!this.listProfileCheckedDelete?.includes(profileName)) {
        this.listProfileCheckedDelete.push(profileName);
      }
    }
  }
  //get name profile
  getNameProfile(){
    this.profileClientService.getListNameProfile().subscribe((res)=>{
      this.listNameProfile = res
    })
  }
   getNameProfileWithId(value){
    this.clientBelongToProfileService.getProfile(value).subscribe( res => {
        this.selected = []
       res.map((profile: { status: any; profileName: string; }) => {
         if(profile.status)
         {
          this.selected.push(profile.profileName);
          this.currentListProfile.push(profile.profileName);
         }
      })
      this.listProfileWithId = res;
    })
  }
  /////thêm tài khoản bằng excel
  showModalExcel(): void {
    this.isVisibleExcel = true;
}

handleOkExcel(): void {
  this.fileList.forEach((row) => {
    let dto = {
      nameFacebook : row[0].toString(),
      password: row[1].toString(),
      avatarUrl : row[2].toString(),
      userName: row[3].toString(),
      secretKey: row[4].toString()
    }
    this.listAccountExcelAdd.push(dto)
  })
  this.clientService.createAccountWithExcel(this.listAccountExcelAdd).subscribe(()=>{
    this.isVisibleExcel = false;
    this.getListFacebookAccount()
  })

}

handleCancelExcel(): void {
    this.isVisibleExcel = false;
}
handleFileSelect(evt) {
  if(!evt.target.files[0].type.includes('spreadsheetml.sheet'))
  {
    this.message.warning('Vui lòng chọn file Excel')
  }
  else
  {
    readXlsxFile(evt.target.files[0]).then((rows) => {
      this.nameExcelFile = evt.target.files[0].name
      this.excelCount = rows.length;
      this.fileList = rows
    })
  }
}
 downloadExcel()
  {
    let workbook = new Workbook();
    let worksheet = workbook.addWorksheet("ExampleExcel");
    let header=["Tên nick facebook","Mật khẩu", "Avartar URl", "UserId", "SecretKey"]
    let headerRow = worksheet.addRow(header);
    workbook.views = [
      {
        x: 0, y: 0, width: 10000, height: 20000,
        firstSheet: 0, activeTab: 1, visibility: 'visible'
      }
    ]
    for (let x1 of this.json_data)
    {
      let x2=Object.keys(x1);
      let temp=[]
      for(let y of x2)
      {
        temp.push(x1[y])
      }
      worksheet.addRow(temp)
    }
    let fname="FileExample"
    workbook.xlsx.writeBuffer().then((data) => {
      let blob = new Blob([data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
      fs.saveAs(blob, fname+'.xlsx');
    });
  }
}
