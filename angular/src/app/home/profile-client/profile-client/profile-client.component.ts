import { Component, OnInit } from '@angular/core';
import {
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { isThisSecond } from 'date-fns/esm';
import { map, switchMap } from 'rxjs/operators';
import { profileDto } from 'src/modal/profileDto';
import { ClientBelongToProfileService } from 'src/serivce/client-belong-to-profile/client-belong-to-profile.service';
import { GroupTypeService } from 'src/serivce/Group-Type/group-type.service';
import { ProfileClientService } from 'src/serivce/Profile-Client/profile-client.service';
import { ProfileGroupTypeService } from 'src/serivce/Profile-Group-Type/profile-group-type.service';
import { ScriptDefaultProfileService } from 'src/serivce/script-default-profile/script-default-profile.service';
import { ScriptService } from 'src/serivce/scripts/script.service';
import { TDSMessageService, TDSSafeAny } from 'tmt-tang-ui';

@Component({
  selector: 'app-profile-client',
  templateUrl: './profile-client.component.html',
  styleUrls: ['./profile-client.component.scss'],
})
export class ProfileClientComponent implements OnInit {
  nameProfile = new FormControl('', [Validators.required]);
  listIdScriptDefault = new FormControl([], [Validators.required]);
  nameGroupType = new FormControl('', [Validators.required]);
  listIdGroupType = new FormControl([]);
  listEnumProfile: any;
  isVisibleProfile = false;
  isUpdate = false;
  listProfiles: any;
  listTemp: any;
  idProfileSelect: string;
  tempNameProfile = '';
  filterNameProfile: string = '';
  isShowProfile = true;
  isVisibleUpdateProfile = false;
  listNameScript = [];
  listScriptDefault;
  listScriptUpdate = {
    name : [],
    id : []
  };
  listGroupUpdate = {
    name : []
  }
  listScriptNameUpdate : any;
  InfoLoading = false
  isVisibleGroupType = false
  isGroupType = false;
  listGroupType : any;
  listProfilesId : any;
  listGroupSelect =[];
  constructor(
    private profileClientService: ProfileClientService,
    private message: TDSMessageService,
    private fb: FormBuilder,
    private scriptService: ScriptService,
    private scriptDefaultProfileService: ScriptDefaultProfileService,
    private groupTypeService : GroupTypeService,
    private profileGroupTypeService : ProfileGroupTypeService
  ) {}

  ngOnInit() {
    this.getListProfile();
    // this.addKeyWord()
  }
  getListProfile() {
    this.InfoLoading = true
    this.profileClientService
      .getListProfile(this.filterNameProfile)
      .subscribe((res) => {
        this.listProfiles = Object.entries(res.result);
        this.InfoLoading = false
      },error=> {
        this.InfoLoading = false
      });
  }

  ///delete profile
  deleteProfile(id: string) {
    this.profileClientService.deleteProfile(id).subscribe(
      () => {
        this.message.success('Xóa thành công');
        this.getListProfile();
      },
      (error) => {
        this.message.error(error.error.message);
      }
    );
  }
  //modal profile
  showModalProfile(): void {
    this.getListScriptDefault();
    this.getListGroupType()
    if (this.lessons.value.length == 0) {
      this.addLesson();
    }
    this.isVisibleProfile = true;
  }
  handleOkProfile(): void {
    let dto = {
      profileName: this.nameProfile.value,
      timeValue: this.lessons.value,
    };
    this.profileClientService
      .createClientProfile(dto)
      .pipe(
        map(listProfilesId =>{
          this.listProfilesId = listProfilesId
        }),
        switchMap(() => {
            let data = {
              scriptIds: this.listIdScriptDefault.value,
              profileIds: this.listProfilesId,
            };
           return this.scriptDefaultProfileService.createScriptDefaultByList(data);
        })
        ,
        switchMap(() => {
          return this.profileGroupTypeService.postGroupType(this.listProfilesId,this.listIdGroupType.value)
        })
      )
      .subscribe(() => {
          this.getListProfile();
          this.isVisibleProfile = false;
        },
        (err): void => {
          this.message.error(err.error.message);
        }
    );
    // this.profileClientService.createClientProfile(dto).subscribe(
    //   (res) => {
    //     console.log(res);

    //     this.getListProfile();
    //     this.isVisibleProfile = false;
    //   },
    //   (err) => {
    //     console.log(err);
    //     this.message.error(err.error.message);
    //   }
    // );
  }
  handleCancelProfile(): void {
    this.isVisibleProfile = false;
  }
  //update profile
  UpdateProfile(id: string) {
  }
  onModelChange(e: any) {
    this.tempNameProfile = e;
  }
  //filter changeAction
  filterChange(e: any) {
    if (e == null) {
      this.filterNameProfile = '';
      this.getListProfile();
    } else {
      this.filterNameProfile = e;
      this.getListProfile();
    }
  }
  //view profile
  ViewProfile() {
    this.isShowProfile = !this.isShowProfile;
  }
  ////error and add profile
  getErrorMessage(name) {
    if (name.hasError('required')) {
      return 'Bạn chưa điền tên hồ sơ';
    }
    return name.hasError('nameProfile') ? 'Not a valid name' : '';
  }
  form = this.fb.group({
    lessons: this.fb.array([]),
  });

  get lessons() {
    return this.form.controls['lessons'] as FormArray;
  }

  addLesson() {
    const lessonForm = this.fb.group({
      startTime: [0, Validators.required],
      duringMinutes: [0, Validators.required],
      idProfile: ['', Validators.required],
    });
    this.lessons.push(lessonForm);
  }

  deleteLesson(lessonIndex: number) {
    this.lessons.removeAt(lessonIndex);
  }
  ///////modal update profile
  showModalUpdateProfile(data: any): void {
    this.getListScriptDefault()
    this.getListGroupType()
    this.lessons.clear()
    this.nameProfile.setValue(data[0]);
    data[1].forEach((item) => {
      if(item.listScript.length > 0)
      {
        this.listScriptUpdate.name.push(item.listScript);
      }
      if(item.listNameGroupType.length > 0)
      {
        this.listGroupUpdate.name.push(item.listNameGroupType);
        this.listGroupSelect.push(item.listNameGroupType);
      }
      this.listScriptUpdate.id.push(item.id);
      let lessonForm = this.fb.group({
        startTime: [item.startTime],
        duringMinutes: [item.duringMinutes],
        idProfile: [item.id],
      });
      
      this.lessons.push(lessonForm);
      if (
        this.lessons.value[0].duringMinutes == 0 &&
        this.lessons.value[0].startTime == 0
      ) {
        this.lessons.removeAt(0);
      }
    });
    console.log(this.listScriptUpdate);
    
    this.isVisibleUpdateProfile = true;  
  }
  handleOkUpdateProfile(): void {
    // var listDelete = this.listGroupSelect.filter(obj => !this.listGroupUpdate.name.map(x => JSON.stringify(x)).includes(JSON.stringify(obj)))
    // var listAdd = this.listGroupUpdate.name.filter(obj => !this.listGroupSelect.map(x => JSON.stringify(x)).includes(JSON.stringify(obj)))
    console.log(this.listGroupSelect);
    console.log(this.listGroupUpdate?.name);
    
    var listDelete= new Array()

    listDelete = this.listGroupSelect.filter (x => !this.listGroupUpdate?.name.includes(x))

    var listAdd =new Array()

    listAdd = this.listGroupUpdate.name.filter ((x: string) => !this.listGroupSelect.includes(x))

    if(this.listScriptNameUpdate == null){
      this.listScriptNameUpdate =this.listScriptUpdate.name[0]
    }
    console.log(listDelete);
    
        
    this.InfoLoading = true
    let deleteDto= {
      listNameGroupType : listDelete[0]
    }
    let listAddDto= {
      listGroupTypeName : listAdd[0]
    }
    let dto = {
      updateProfileDto : this.lessons.value,
      scriptDefaultName : this.listScriptNameUpdate,
      deleteGroupTypeWithProfileDto : deleteDto,
      addProfileWithGroupTypeDto : listAddDto
    };
    this.profileClientService
      .updateProfile(this.nameProfile.value, dto)
      .subscribe(() => {
        this.listScriptUpdate.id = [];
        this.listScriptUpdate.name = [];
        this.listGroupUpdate.name = [];
        this.lessons.clear();
        this.nameProfile.setValue('');
        this.getListProfile();
        this.InfoLoading = false
        this.isVisibleUpdateProfile = false;
        this.listGroupSelect = []
      }, error => {
        this.InfoLoading = false
        this.isVisibleUpdateProfile = false;
        this.listGroupSelect = []
      });
  }
  handleCancelUpdateProfile(): void {
    this.isVisibleUpdateProfile = false;
    setTimeout(() => {
      this.lessons.clear();
      this.nameProfile.setValue('');
      this.listScriptUpdate.id = [];
      this.listScriptUpdate.name = [];
      this.listGroupUpdate.name = []
    },200)
  }
  //////////get list script default
  getListScriptDefault() {
    this.scriptService.getListScriptDefault().subscribe((res) => {
      this.listScriptDefault = res;
    });
  }
  onModelChangeUpdate(e:TDSSafeAny)
  {
    // e == undefined ? this.listScriptNameUpdate = null : this.listScriptNameUpdate = e;
    if(e == undefined)
    {
      this.listScriptNameUpdate = null
    }
    else
    {
      this.listScriptNameUpdate = e;
    }
  }

getListGroupType(){
  this.groupTypeService.getListGroup('').subscribe(res=>{
    this.listGroupType = res
  })
}
}
