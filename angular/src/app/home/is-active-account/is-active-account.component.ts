import { Component, OnInit } from '@angular/core';
import { faLeaf } from '@fortawesome/free-solid-svg-icons';
import { map } from 'rxjs/operators';
import { listAccountActive } from 'src/modal/listAccountActive';
import { listScriptDto } from 'src/modal/listScriptDto';
import { proxyUsingScript } from 'src/modal/proxyUsingScriptDto';
import { CampaignService } from 'src/serivce/campaign/campaign.service';
import { ClientFacebookService } from 'src/serivce/Client-Facebook/client-facebook.service';
import { ClientActiveService } from 'src/serivce/client-isActive/client-active.service';
import { EnumService } from 'src/serivce/Enum/enum.service';
import { ProfileClientService } from 'src/serivce/Profile-Client/profile-client.service';
import { ProxyService } from 'src/serivce/Proxy-Service/proxy.service';
import { ProxyUsingScriptService } from 'src/serivce/proxy-using-script/proxy-using-script.service';
import { ScriptService } from 'src/serivce/scripts/script.service';
import { SignalRService } from 'src/serivce/signalR/signalR.service';
import {
  TDSMessageService,
  TDSModalService,
  TDSNotificationService,
  TDSSafeAny,
  TDSTableQueryParams,
} from 'tmt-tang-ui';

@Component({
  selector: 'app-is-active-account',
  templateUrl: './is-active-account.component.html',
  styleUrls: ['./is-active-account.component.scss'],
})
export class IsActiveAccountComponent implements OnInit {
  //tich chọn input
  firstValue: number = 0;
  endValue: number = 0;
  InfoLoading = false;
  value: number = 0;
  nameAccountSearch = "";
  nameProfileSearch = "";
  proxyIpSearch = "";
  listNameProfile;
  listProxy;
  expandSet = new Set<number>();
  showInfo = false;
  //script
  typeFilter = '';
  seedingFilter = '';
  idScriptRun;
  idScriptSearch = '';
  filter: string = '';
  typeScript: any = null;
  valueScript: number = 0;
  listScript: any;
  listScriptRun: any;
  listSeeding;
  listAccountRun: any;
  listAccountUsingScript: proxyUsingScript[] = [];
  //addscript
  listEnumType: any;
  dataCreateForm = {
    scriptName: '',
    value: '',
    isActive: true,
    isDefault: false,
    type: 'Undefined',
  };
  action = ['Like', 'Sad', 'HaHa', 'Angry', 'Heart'];
  dataValue = {
    url: '',
    time: '',
    times: '',
    content: '',
    action: '',
  };
  boolValue = [
    { id: true, name: 'true' },
    { id: false, name: 'false' },
  ];
  //connectionHub
  connectionHub: any;
  //tick
  idAccount: string[] = [];
  accountActiveList: Array<TDSSafeAny> = [];
  //table
  checked = false;
  loading = false;
  indeterminate = false;
  isLoading = false;
  idScriptToShow: number;
  listOfCurrentPageData: readonly listAccountActive[] = [];
  setOfCheckedId = new Set<string>();
  skipProxyUsingScript: number = 0;
  takeProxyUsingScript: number = 20;
  disableButton: boolean = false;
  continueScript: boolean = false;
  pageSize: number = 12;
  pageIndex = 1
  skipCountScript = 0
  totalCount = 0
  totalCountAccountActive = 0
  pageSizeAccountActive = 12
  pageIndexAccountActive = 0
  skipCountAccountActive = 0
  //list Enum
  type: any;
  constructor(
    private clientAccountService: ClientFacebookService,
    private scriptService: ScriptService,
    private campaignService: CampaignService,
    private signalRService: SignalRService,
    private proxyService: ProxyService,
    private clientActiveService: ClientActiveService,
    private message: TDSMessageService,
    private profileService: ProfileClientService,
    private notification: TDSNotificationService,
    private modal: TDSModalService,
    private proxyUsingScriptService: ProxyUsingScriptService,
    private enumService: EnumService,
  ) { }

  ngOnInit(): void {
    this.getListAccountActive();
    this.getListScript();
    this.getListScriptRun();
    this.getListAccountUsingScript();
    this.getListEnumType();
    this.getListProxy();
    this.getListSeeding();
    this.getListNameProfile();
    this.connectionHub = this.signalRService.connection;
    this.addScript();

  }
  ///
  addScript() {
    this.connectionHub.on('CreateScript', () => {
      this.getListScript();
    });
  }
  onQueryParamsChangeAccountActive(params: TDSTableQueryParams): void {
    this.skipCountAccountActive = (params.pageIndex - 1) * params.pageSize;
    this.getListAccountActive()
}
  getListAccountActive() {
    this.clientAccountService
      .getListFacebookAccountActive(this.nameAccountSearch, this.nameProfileSearch, this.proxyIpSearch, this.skipCountAccountActive, this.pageSizeAccountActive)
      .subscribe((data) => {
        this.accountActiveList = data?.items;
        this.listOfCurrentPageData = data.items;
        this.totalCountAccountActive = data.totalCount;
      });
  }
  getListSeeding() {
    this.campaignService.getListSeeding("", 0, 100, 0).subscribe(data => {
      this.listSeeding = data;
    })
  }
  getListProxy() {
    this.proxyService.getListProxy("", 0, 100).subscribe(data => {
      this.listProxy = data?.items;
    })
  }
  getListNameProfile() {
    this.profileService.getListNameProfile().subscribe(data => {
      this.listNameProfile = data;
    })
  }
  onExpandChange(id: number, checked: boolean): void {
     this.idScriptToShow = id;
    this.showInfo = checked;
    if (checked) {
      this.expandSet.add(id);
      this.clientActiveService.getListClientByIdScript(id).subscribe(data => {
        this.listAccountRun = data;
        console.log(this.listAccountRun);
        
      })
    } else {
      this.expandSet.delete(id);
    }
  }
  changeScript(event: any) {
    if (event == null) {
      this.filter = '';
    }
    else {
      this.filter = event;
    }
    this.getListScript();
  }
  changeNameFacebook(event: any) {
    if (event == null) {
      this.nameAccountSearch = '';
    }
    else {
      this.nameAccountSearch = event;
    }
    this.getListAccountActive();
  }
  changeNameProfile(event: any) {
    if (event == null) {
      this.nameProfileSearch = "";
    }
    else {
      this.nameProfileSearch = event;
    }
    this.getListAccountActive();
  }
  changeProxyIp(event: any) {
    if (event == null) {
      this.proxyIpSearch = "";
    }
    else {
      this.proxyIpSearch = event;
    }
    this.getListAccountActive();
  }
  changeTypeScript(event: any) {
    if (event == null) {
      this.typeFilter = '';
    }
    else {
      this.typeFilter = event;
    }
    this.getListScript();
  }
  changeSeeding(event: any) {
    if (event == null) {
      this.seedingFilter = '';
    }
    else {
      this.seedingFilter = event;
    }
    this.getListScript();
  }
  //checked user
  updateCheckedSet(id: string, checked: boolean): void {
    if (checked) {
      this.setOfCheckedId.add(id);
      if (!this.idAccount.includes(id)) {
        this.idAccount.push(id);
      }
    } else {
      this.setOfCheckedId.delete(id);
      this.idAccount = this.idAccount.filter((x) => x != id);
    }
  }

  onItemChecked(id: string, checked: boolean): void {
    this.updateCheckedSet(id, checked);
    this.refreshCheckedStatus();
  }

  onAllChecked(value: boolean): void {
    this.listOfCurrentPageData.forEach((item) => {
      this.updateCheckedSet(item.id, value);
    });
    this.refreshCheckedStatus();
  }

  onCurrentPageDataChange($event: readonly listAccountActive[]): void {
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
  }

  ///////////////////////script
  onQueryParamsChange(params: TDSTableQueryParams): void {
    this.skipCountScript = (params.pageIndex - 1) * params.pageSize;
    this.getListScript()
  }
  getListScript() {
    this.InfoLoading = true
    this.scriptService.getListScript(this.filter,this.idScriptSearch, this.value, this.typeFilter,this.seedingFilter,this.skipCountScript, this.pageSize).subscribe(
      (data) => {

        if (data) {
          this.listScript = data;
          this.totalCount = data.totalCount
          this.InfoLoading = false
        }
      },
      (err) => (this.InfoLoading = false)
    );
  }
  getListScriptRun() {
    this.scriptService.getListScriptRun().subscribe(data => {
      this.listScriptRun = data;
    })
  }
  /////create and get list accout using cripts
  createAccountWithNotification(scriptId: string) {
    if (this.idAccount.length > 0 && this.idScriptRun != null) {
      this.info(scriptId);
    } else {
      this.createNotificationTopLeft();
    }
  }
  //modal info
  info(scriptId: string): void {
    this.modal.info({
      title: 'Chạy kịch bản',
      content: 'Bạn có muốn chạy kịch bản?',
      onOk: () => this.createAccountUsingScript(scriptId),
      onCancel: () => {
        this.cancelScript();
      },
      okText: 'Oke',
      cancelText: 'Cancel',
    });
  }

  cancelScript() {
    this.idAccount = [];
    this.idScriptRun = "";
    this.listOfCurrentPageData.forEach((element) => {
      this.updateCheckedSet(element.id, false);
    });
    this.refreshCheckedStatus();
    this.disableButton = false;
    this.firstValue = 0;
    this.endValue = 0;
  }
  createAccountUsingScript(scriptId: string) {
    this.InfoLoading = true;
    let clientIds = [];
    this.idAccount.forEach(element => {
      clientIds.push({ clientId: element });
    });
    const data = {
      scriptId: scriptId,
      clientIds: clientIds
    };
    this.proxyUsingScriptService.createByList(data).subscribe(() => {
      this.idAccount = [];
      this.idScriptRun = "";
      this.getListAccountUsingScript();
      this.getListScript();
      this.InfoLoading = false;
      this.firstValue = 0;
      this.endValue = 0;
      this.listOfCurrentPageData.forEach((element) => {
        this.updateCheckedSet(element.id, false);
      });
      this.refreshCheckedStatus();
    }, error => this.InfoLoading = false)
  }
  getListAccountUsingScript() {
    this.clientActiveService
      .getListProxyIpUsingScript(
        this.skipProxyUsingScript,
        this.takeProxyUsingScript
      )
      .subscribe((res) => {
        this.listAccountUsingScript = res.items;
      });
  }
  //////showmodal add script
  changeAction(event: any) {
    this.dataValue.action = event;
  }
  resetValue() {
    this.dataValue = {
      url: '',
      time: '',
      times: '',
      content: '',
      action: '',
    };
  }
  getErrorMessage(value: any) {
    if (value == '') {
      return 'You must enter a value';
    }
    return value.hasError(value) ? 'Not a valid value' : '';
  }
  getListEnumType() {
    this.enumService.getListEnumType().subscribe((data) => {
      this.listEnumType = data;
    });
  }
  changeEnum(event: any) {
    this.dataCreateForm.type = event;
  }
  ngModelChangeType(event: any) {
    this.typeScript = event;
    if (event != null) {
      this.scriptService
        .getListScriptFilter(this.valueScript, this.typeScript)
        .subscribe((res) => {
          this.listScript = res?.items;
        });
    } else {
      this.getListScript();
    }
  }
  runScript() {
    this.createAccountWithNotification(this.idScriptRun);
  }
  /////////notification warning
  createNotificationTopLeft(): void {
    this.notification.warning(
      'Cảnh báo',
      'Hãy chọn kịch bản trước khi Submit',
      {
        placement: 'topLeft',
      }
    );
  }
  //////////////////tích chọn input user
  onChange(e: any) {
    console.log('onChange', e);
  }
  chooseUser() {
    if (
      this.firstValue != 0 &&
      this.endValue != 0 &&
      this.idAccount.length == 0
    ) {
      let first = this.firstValue;
      for (first; first <= this.endValue; first++) {
        let id = this.accountActiveList[first - 1]?.id;
        if (id != undefined) {
          this.updateCheckedSet(id, true);
          this.idAccount.push(id);
        }
      }
    }
  }
  ///////////////update continue
  updateContinueProxyUsingScript(scriptId: string) {
    this.scriptService.updateActive({ id: scriptId }).subscribe((res) => {
      this.listAccountUsingScript = this.listAccountUsingScript.map((x) => {
        if (x.id == scriptId) x.isActive = !x.isActive;
        return x;
      });
    });
  }
  // isContinue(event: any) {
  //   this.listAccountUsingScript = this.listAccountUsingScript.map(x => {
  //     if (x.id == event) x.isContinue == x.isActive;
  //     return x;
  //   });
  // }
}
