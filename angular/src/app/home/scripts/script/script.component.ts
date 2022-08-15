import { HttpClient, HttpRequest, HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { filter } from 'rxjs/operators';
import { ActiviteChartService } from 'src/serivce/ActiviteCharts/activite-chart.service';
import { ClientActiveService } from 'src/serivce/client-isActive/client-active.service';
import { EnumService } from 'src/serivce/Enum/enum.service';
import { ProfileClientService } from 'src/serivce/Profile-Client/profile-client.service';
import { ScriptDefaultProfileService } from 'src/serivce/script-default-profile/script-default-profile.service';
import { ScriptService } from 'src/serivce/scripts/script.service';
import { SignalRService } from 'src/serivce/signalR/signalR.service';
import { StatusTypeService } from 'src/serivce/status-type/status-type.service';
import {
  TDSMessageService,
  TDSModalService,
  TDSSafeAny,
  TDSTableQueryParams,
  TDSUploadChangeParam,
  TDSUploadFile,
} from 'tmt-tang-ui';

@Component({
  selector: 'app-script',
  templateUrl: './script.component.html',
  styleUrls: ['./script.component.scss'],
})
export class ScriptComponent implements OnInit {
  filter: string = '';
  value: number = 0;
  listScript: any;
  totalActive: number = 0;
  totalDefault: number = 0;
  isVisibleModalCreateScript = false;
  isVisible = false;
  listEnumType: any;
  listAccountRun: any;
  isCollapsed = false;
  listStatusType: any;
  profilesApplyScriptDefault: any;
  listNameProfile: any;
  typeFilter = '';
  InfoLoading = false;
  times = [
    '2',
    '5',
    '9',
    '10',
    '20',
    '30',
    '40',
    '50',
    '100',
    '200',
    '300',
    '400',
    '500',
    '600',
    '1000',
  ];
  totalCount = 0;
  connectionHub: any;
  action = [
    'Thích',
    'Yêu thích',
    'Thương thương',
    'Haha',
    'Wow',
    'Buồn',
    'Phẫn nộ',
  ];
  dataValue = {
    url: '',
    time: '',
    times: '',
    content: '',
    action: '',
    statusType: '',
  };
  dataValueEdit = {
    url: '',
    time: '',
    times: '',
    content: '',
    action: '',
    statusType: '',
  };

  dataCreateForm = {
    scriptName: '',
    value: '',
    isActive: true,
    isDefault: false,
    type: 'Undefined',
  };
  dataEditForm = {
    scriptName: '',
    value: '',
    isActive: true,
    isDefault: false,
    type: 'Undefined',
  };

  boolValue = [
    { id: true, name: 'true' },
    { id: false, name: 'false' },
  ];
  isVisibleUpdateProfile = false;
  listProfileOfScript: any;
  listNameProfileUpdate: any;
  currentListProfile: any;
  idScriptForUpdateProfile = '';
  active: string = 'Undefined';
  listNameSeeding: any;
  nameSeeding = '';
  pageSize: number = 12;
  pageIndex = 1;
  skipCountScript = 0;
  constructor(
    private scriptService: ScriptService,
    private message: TDSMessageService,
    private signalRService: SignalRService,
    private modal: TDSModalService,
    private msg: TDSMessageService,
    private http: HttpClient,
    private statusTypeService: StatusTypeService,
    private scriptDefaultProfileService: ScriptDefaultProfileService,
    private enumService: EnumService,
    private profileService: ProfileClientService,
    private clientService: ClientActiveService,
    private activityService: ActiviteChartService
  ) {}

  ngOnInit() {
    this.getNameProfileAddScript();
    this.getListEnumType()
    this.connectionHub = this.signalRService.connection;
    // this.getPageId('https://www.facebook.com/kysuduongpho.official');
    this.getListNameSeeding();
  }

  getRandomStatus() {
    this.statusTypeService
      .getRandomStatus('Philosophy')
      .subscribe((data) => {});
  }
  mapItems(items: any, key: any) {
    return [...new Set(items.map((x: any) => x[key]))];
  }
  onQueryParamsChange(params: TDSTableQueryParams): void {
    this.skipCountScript = (params.pageIndex - 1) * params.pageSize;
    this.getListScript();
  }
  getListScript() {
    this.InfoLoading = true;
    this.scriptService
      .getListScript(
        this.filter,
        '',
        this.value,
        this.typeFilter,
        this.nameSeeding,
        this.skipCountScript,
        this.pageSize
      )
      .subscribe(
        (data) => {
          console.log(data);
          if (data) {
            this.listScript = data;
            this.totalCount = data.totalCount;
            this.totalActive = this.listScript.items.filter(
              (x: any) => x.isActive
            ).length;

            this.totalDefault = this.listScript.items.filter(
              (x: any) => x.isDefault
            ).length;

            this.InfoLoading = false;
          }
        },
        (err) => (this.InfoLoading = false)
      );
  }

  getListEnumType() {
    this.enumService.getListEnumType().subscribe((data) => {
      this.listEnumType = data.filter(
        (x) => x != 'MakeFriendsSuggestion' && x != 'JoinGroup'
      );
    });
  }
  ngModelChangeType(event: any) {
    if (event == null) {
      this.typeFilter = '';
    } else {
      this.typeFilter = event;
    }
    this.getListScript();
  }
  getListEnumStatusType() {
    this.enumService.getListEnumStatusType().subscribe((data: any) => {
      this.listStatusType = data;
    });
  }
  changeEnum(event: any) {
    this.resetValue();
    this.dataCreateForm.type = event;
  }

  updateActive(script: any) {
    this.modal.warning({
      title: 'Warning',
      content: script.isActive
        ? 'If you deactivate script, Proxy which is using this script will stop!'
        : 'Active Script?',
      onOk: () => {
        const data = { id: script.id };
        this.scriptService.updateActive(data).subscribe(() => {
          this.message.success(
            (script.isActive ? 'Deactivate ' : 'Active ') +
              script.scriptName +
              ' successfully'
          );
          let index = this.listScript.items.findIndex(
            (x: any) => x.id == script.id
          );
          script.isActive = !script.isActive;
          this.listScript.items[index] = script;

          script.isActive ? this.totalActive++ : this.totalActive--;
        });
      },
      onCancel: () => {},
      okText: 'Submit',
      cancelText: 'Cancel',
    });
  }

  CreateScript() {
    this.showModal();
  }
  showModal(): void {
    this.getListEnumStatusType();
    this.isVisibleModalCreateScript = true;
  }
  changeColor(value: any) {
    this.active = value;
    this.changeEnum(value);
  }
  handleOk(): void {
    if (
      this.dataValue.time != '' &&
      isNaN(Number.parseInt(this.dataValue.time))
    ) {
      this.message.error('time is not valid');
      return;
    }

    if (
      this.dataValue.times != '' &&
      this.dataValue.times.toUpperCase() != 'ALL' &&
      isNaN(Number.parseInt(this.dataValue.times))
    ) {
      this.message.error('times is not valid');
      return;
    }
    let value: any = [];
    for (let [key, valueOj] of Object.entries(this.dataValue)) {
      if (valueOj != '') {
        if (!isNaN(Number.parseInt(valueOj))) {
          valueOj = Number.parseInt(valueOj).toString();
        }

        value.push(valueOj.toString());
      }
    }
    if (this.dataCreateForm.type == 'PostStatus') {
      this.dataCreateForm.value = value.reverse().join('!@#$%^&*()');
    } else {
      this.dataCreateForm.value = value.join('!@#$%^&*()');
    }

    if (this.dataCreateForm.type == 'Undefined') {
      this.message.error('Type cannot undefined!');
      return;
    }

    if (this.dataCreateForm.scriptName == '') {
      this.message.error(
        Object.keys(this.dataCreateForm)[0].toUpperCase() +
          ' must enter a value'
      );
      return;
    }

    if (
      this.dataCreateForm.value == '' &&
      this.dataCreateForm.type != 'CloseChrome' &&
      this.dataCreateForm.type != 'ReadInformationFriend' &&
      this.dataCreateForm.type != 'ReadInformationGroup'
    ) {
      this.message.error(
        Object.keys(this.dataCreateForm)[1].toUpperCase() +
          ' must enter a value'
      );
      return;
    }
    console.log(this.dataCreateForm);

    this.scriptService.createScript(this.dataCreateForm).subscribe(
      (res) => {
        this.getListScript();
        this.resetValue();
        this.isVisibleModalCreateScript = false;

        if (!res['isDefault']) {
          this.signalRService.connection.invoke(
            'CreateScript',
            res['isDefault']
          );
        }
        if (this.listNameProfile.length > 0) {
          this.createScriptDefaultProfile(res.id);
        }
        this.dataCreateForm = {
          scriptName: '',
          value: '',
          isActive: true,
          isDefault: false,
          type: 'Undefined',
        };
        this.active = 'Undefined';
      },
      (err) => {
        this.message.error(err.error.message),
          (this.isVisibleModalCreateScript = true);
      }
    );
  }
  handleOkAcc(): void {
    this.isVisible = false;
  }

  handleCancelAcc(): void {
    this.isVisible = false;
  }

  getPageId(urlLive: string): void {
    this.fetchBody(urlLive)
      .then((res) => {
        return res.text();
      })
      .then((text) => {
        var pageId = this.regexPageId(text);
        console.log('pageId', pageId);
      })
      .catch((err) => {
        console.log('error fetchBody');
      });
  }

  regexPageId(body: string) {
    var pattern = /{"pageId":"([\s\S]*?)"/gim;
    var res = pattern.exec(body);
    if (res) {
      return res[1];
    }
    return null;
  }

  fetchBody(url: string): Promise<Response> {
    return fetch(`${url}`, {
      method: 'GET',
      credentials: 'same-origin',
      headers: {
        'Content-Type':
          'application/x-www-form-urlencoded, application/x-www-form-urlencoded',
        Accept:
          'text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9',
      },
    });
  }

  getRandomInt(min: number, max: number) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1)) + min;
  }
  resetValue() {
    this.dataValue = {
      url: '',
      time: '',
      times: '',
      content: '',
      action: '',
      statusType: '',
    };
  }

  handleCancel(): void {
    this.resetValue();
    this.isVisibleModalCreateScript = false;
    this.profilesApplyScriptDefault = [];
  }

  createScriptDefaultProfile(scriptId: any) {
    for (var i = 0; i < this.profilesApplyScriptDefault?.length; i++) {
      const data = {
        scriptId: scriptId,
        profileIds: this.profilesApplyScriptDefault[i],
      };
      this.scriptDefaultProfileService
        .createScriptByList(data)
        .subscribe(() => {
          if (i == this.profilesApplyScriptDefault.length - 1) {
            this.profilesApplyScriptDefault = [];
          }
        });
    }
  }
  getScriptById() {
    this.scriptService
      .getScriptById(this.idScriptForUpdateProfile)
      .subscribe((data) => {
        this.dataEditForm.scriptName = data.scriptName;
        this.dataEditForm.type = data.type;
        this.dataEditForm.isActive = data.isActive;
        this.dataEditForm.value = data.value;
        this.dataEditForm.isDefault = data.isDefault;

        var value = data.value.split('!@#$%^&*()');
        for (let i = 0; i < value.length; i++) {
          console.log(value[i]);
          if (data.type == '1' || data.type == '9' || data.type == '11')
            this.dataValueEdit.time = value[0];
          if (
            data.type == '3' ||
            data.type == '13' ||
            data.type == '19' ||
            data.type == '4' ||
            data.type == '21' ||
            data.type == '22' ||
            data.type == '17' ||
            data.type == '18' ||
            data.type == '16' ||
            data.type == '20'
          )
            this.dataValueEdit.url = value[0];
          if (
            data.type == '2' ||
            data.type == '5' ||
            data.type == '8' ||
            data.type == '10' ||
            data.type == '7' ||
            data.type == '12'
          )
            this.dataValueEdit.times = value[0];
          if (data.type == '16' || data.type == '13')
            this.dataValueEdit.times = value[1];
          if (
            data.type == '3' ||
            data.type == '12' ||
            data.type == '7' ||
            data.type == '5' ||
            data.type == '19' ||
            data.type == '21' ||
            data.type == '22' ||
            data.type == '17' ||
            data.type == '18' ||
            data.type == '20'
          )
            this.dataValueEdit.content = value[1];
          if (data.type == '2' || data.type == '10')
            this.dataValueEdit.action = value[1];
          if (data.type == '16') this.dataValueEdit.action = value[2];
        }
        this.isVisibleUpdateProfile = true;
        console.log(this.dataValueEdit.times);
      });
  }
  updateScript() {
    if (
      this.dataEditForm.type == '1' ||
      this.dataEditForm.type == '9' ||
      this.dataEditForm.type == '11'
    )
      this.dataEditForm.value = this.dataValueEdit.time;
    if (
      this.dataEditForm.type == '3' ||
      this.dataEditForm.type == '13' ||
      this.dataEditForm.type == '19' ||
      this.dataEditForm.type == '4' ||
      this.dataEditForm.type == '21' ||
      this.dataEditForm.type == '22' ||
      this.dataEditForm.type == '17' ||
      this.dataEditForm.type == '18' ||
      this.dataEditForm.type == '16' ||
      this.dataEditForm.type == '20'
    )
      this.dataEditForm.value = this.dataValueEdit.url;
    if (
      this.dataEditForm.type == '2' ||
      this.dataEditForm.type == '5' ||
      this.dataEditForm.type == '8' ||
      this.dataEditForm.type == '10' ||
      this.dataEditForm.type == '7' ||
      this.dataEditForm.type == '12'
    )
      this.dataEditForm.value = this.dataValueEdit.times;
    if (this.dataEditForm.type == '16' || this.dataEditForm.type == '13')
      this.dataEditForm.value =
        this.dataEditForm.value + '!@#$%^&*()' + this.dataValueEdit.times;
    if (
      this.dataEditForm.type == '3' ||
      this.dataEditForm.type == '12' ||
      this.dataEditForm.type == '7' ||
      this.dataEditForm.type == '5' ||
      this.dataEditForm.type == '19' ||
      this.dataEditForm.type == '21' ||
      this.dataEditForm.type == '22' ||
      this.dataEditForm.type == '17' ||
      this.dataEditForm.type == '18' ||
      this.dataEditForm.type == '20'
    )
      this.dataEditForm.value =
        this.dataEditForm.value + '!@#$%^&*()' + this.dataValueEdit.content;
    if (this.dataEditForm.type == '2' || this.dataEditForm.type == '10')
      this.dataEditForm.value =
        this.dataEditForm.value + '!@#$%^&*()' + this.dataValueEdit.action;
    if (this.dataEditForm.type == '16')
      this.dataEditForm.value =
        this.dataEditForm.value + '!@#$%^&*()' + this.dataValueEdit.action;
    // console.log(typeof(this.dataEditForm.value.toString()) );
    // console.log(this.dataEditForm);
    this.dataEditForm.value = this.dataEditForm.value.toString();
    //   this.dataEditForm.value.toString();
    this.scriptService
      .updateScript(this.idScriptForUpdateProfile, this.dataEditForm)
      .subscribe(
        (data) => {
          this.message.success('Chỉnh sửa thành công');
          this.dataValueEdit = {
            url: '',
            time: '',
            times: '',
            content: '',
            action: '',
            statusType: '',
          };
          this.getListScript();
        },
        (err) => this.message.error(err.error.message)
      );
  }
  getErrorMessage(value: any) {
    if (value == '') {
      return 'You must enter a value';
    }
    return value.hasError(value) ? 'Not a valid value' : '';
  }

  deleteScript(script: any) {
    this.scriptService.deleteScript(script.id).subscribe(
      () => {
        this.getListScript();
        this.message.success('Delete ' + script.scriptName + ' successfully');
      },
      (err) => this.message.error(err.error.message)
    );
  }

  changeAction(event: any) {
    this.dataValue.action = event;
    this.dataValueEdit.action = event;
  }

  changeTimes(event: any) {
    this.dataValue.times = event;
    this.dataValueEdit.times = event;
  }

  onChange(event: any) {
    this.dataValue.time = event;
    this.dataValueEdit.time = event;
  }

  changeStatusType(event: any) {
    this.dataValue.statusType = event;
  }

  defaultChange(isDefault: boolean) {
    this.dataEditForm.isDefault = isDefault;
    if (!isDefault) {
      this.profilesApplyScriptDefault = [];
    }
  }
  changeDefault(isDefault: boolean) {}

  ///check type Name
  //checkContent
  checkContent(value: string) {
    if (value.includes('facebook.com')) {
      return value;
    } else {
      return 'https://www.facebook.com/' + value;
    }
  }

  checkTypeScript(value: string) {
    if (value == 'ReactionPost') {
      return 'Reacts';
    } else if (
      value == 'ShareWall' ||
      value == 'CommentPost' ||
      value == 'SharePostToGroup' ||
      value == 'SharePost'
    ) {
      return 'Different';
    } else {
      return 'Default';
    }
  }

  ///////get name profile
  getNameProfileAddScript() {
    this.profileService.getListNameProfile().subscribe((res) => {
      this.listNameProfile = res;
    });
  }
  //module update profile
  showModalUpdateProfile(value): void {
    this.idScriptForUpdateProfile = value;
    this.getListNameProfile();
    this.getListProfileOfScript(value);
    this.getScriptById();
  }

  handleOkUpdateProfile(): void {
    let listDelete = this.currentListProfile.filter(
      (n: any) => !this.listNameProfileUpdate.includes(n)
    );
    let listAdd = this.listNameProfileUpdate.filter(
      (n: any) => !this.currentListProfile.includes(n)
    );
    // let deleteResult = this.listProfileOfScript.filter((x: { nameProfile: any; })=> this.currentListProfile.filter ((n: any) => !this.listNameProfileUpdate.includes(n)).includes(x.nameProfile)).map(x=>x.listIdProfile)
    // let addResult = this.listProfileOfScript.filter((x: { nameProfile: any; })=> this.listNameProfileUpdate.filter ((n: any) => !this.currentListProfile.includes(n)).includes(x.nameProfile)).map(x=>x.listIdProfile)
    if (listAdd.length > 0) {
      this.scriptDefaultProfileService
        .createProfileByScript(this.idScriptForUpdateProfile, listAdd)
        .subscribe();
    }
    if (listDelete.length > 0) {
      this.scriptDefaultProfileService
        .deleteProfileByScript(this.idScriptForUpdateProfile, listDelete)
        .subscribe();
    }
    this.updateScript();
    this.isVisibleUpdateProfile = false;
  }

  handleCancelUpdateProfile(): void {
    console.log('Button cancel clicked!');
    this.isVisibleUpdateProfile = false;
    this.dataValueEdit = {
      url: '',
      time: '',
      times: '',
      content: '',
      action: '',
      statusType: '',
    };
  }
  getListProfileOfScript(scriptId) {
    this.profileService.getListProfileOfScript(scriptId).subscribe((res) => {
      this.listNameProfileUpdate = res.map(
        (x: { nameProfile: any }) => x.nameProfile
      );
      this.currentListProfile = res.map(
        (x: { nameProfile: any }) => x.nameProfile
      );
    });
  }
  //get list name profile
  getListNameProfile() {
    this.profileService.getListNameProfile().subscribe(
      (res) => {
        this.listProfileOfScript = res;
      },
      (error) => {}
    );
  }
  //get list seeding
  getListNameSeeding() {
    this.activityService.getListNameSeeding().subscribe((res) => {
      this.listNameSeeding = res;
    });
  }
  ngModelChangeSeeding(value) {
    if (value == null) {
      this.nameSeeding = '';
    } else {
      this.nameSeeding = value;
    }
    this.getListScript();
  }
  ngModelChangeFilter(value) {
    if (value == null) {
      this.filter = '';
    } else {
      this.filter = value;
    }
    this.getListScript();
  }
}
