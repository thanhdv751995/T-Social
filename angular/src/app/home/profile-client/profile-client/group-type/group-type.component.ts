import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, Validators } from '@angular/forms';
import { GroupTypeService } from 'src/serivce/Group-Type/group-type.service';
import { TDSMessageService, TDSModalService, TDSSafeAny } from 'tmt-tang-ui';

@Component({
  selector: 'app-group-type',
  templateUrl: './group-type.component.html',
  styleUrls: ['./group-type.component.css']
})
export class GroupTypeComponent implements OnInit {

  listGroupType : any;
  nameGroupType = new FormControl('', [Validators.required]);
  isVisibleGroupType = false
  public listSelected =[];
  public listData = []
  filterNameGroup = ''
  isVisibleUpdateGroupType = false;
  nameUpdate =''
  keyWordUpdate = ''
  listSelectedUpdate =[];
  idGroupUpdate =''

  constructor(private groupTypeService : GroupTypeService, 
              private fb: FormBuilder,
              private message: TDSMessageService,
              private modal: TDSModalService) { }

  ngOnInit(): void { 
    this.getListGroupType()
  }
  getListGroupType()
  {
    this.groupTypeService.getListGroup(this.filterNameGroup).subscribe(res=>{
      this.listGroupType = res
    },err=>{
    })
  }
    /////////////ad group type
    showModalGroupType(): void {
      this.isVisibleGroupType = true;
  }
  
  handleOkGroupType(): void {
    console.log(this.listSelected);
    
      if (this.nameGroupType.value != '' && this.listSelected.length > 0) {
        let key = ''
        this.listSelected.forEach(element => {
          if(key =='')
          {
            key = element
          }
          else
          {
            key = key + ','+element
          }
        })
        this.groupTypeService.postGroupType(this.nameGroupType.value, key).subscribe(res=>{
          this.isVisibleGroupType = false;
          this.listSelected = []
          this.getListGroupType()
        },error =>{
          this.message.warning(error.error.message)
        });
      }
  }
  
  handleCancelGroupType(): void {
      this.isVisibleGroupType = false;
  
  }
  //delete group type
  deleteGroupType(value){
    this.modal.warning({
      title: 'Cảnh báo',
      content: 'Loại nhóm thuộc Hồ sơ cũng sẽ bị xóa !!!!',
      onOk: () =>  
      this.groupTypeService.deleteGroupType(value).subscribe(()=>{
        this.message.success('Xóa thành công');
        this.getListGroupType()
      }),
      onCancel:()=>{console.log('cancel')},
      okText:"Xác nhận",
      cancelText:"Hủy"
  });
   
  }
  //filter group type
  filterChange(e: any) {
    e == undefined ? this.filterNameGroup = '' : this.filterNameGroup = e;
    this.getListGroupType()
  }
  //update group type 
  showModalUpdateGroupType(data): void {
    this.isVisibleUpdateGroupType = true;
    this.listSelectedUpdate = data.keywordsRelative.split(',');
    this.nameUpdate = data.name
    this.idGroupUpdate = data.id
}

  handleOkUpdateGroupType(): void {
      if (this.nameUpdate != '' && this.listSelectedUpdate.length > 0) {
        let key = ''
        this.listSelectedUpdate.forEach(element => {
          if(key =='')
          {
            key = element
          }
          else
          {
            key = key + ','+element
          }
        })
        let dto ={
          name: this.nameUpdate,
          keywordsRelative : key
        }
        this.groupTypeService.updateGroupType(this.idGroupUpdate, dto).subscribe(()=>{
          this.message.success('Chỉnh sửa thành công');
          this.isVisibleUpdateGroupType = false;
          this.getListGroupType()
        },err =>{
          this.message.warning(err.error.message)
          this.isVisibleUpdateGroupType = false;
        })
        // this.groupTypeService.postGroupType(this.nameGroupType.value, key).subscribe(res=>{
        //   this.isVisibleGroupType = false;
        //   this.listSelected = []
        //   this.getListGroupType()
        // },error =>{
        //   this.message.warning(error.error.message)
        // });
      }
  }

  handleCancelUpdateGroupType(): void {
      this.isVisibleUpdateGroupType = false;
  }
}
