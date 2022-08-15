import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/serivce/User/user.service';
import { TDSMessageService } from 'tmt-tang-ui';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {
  infoUser: any
  userName : string = ''
  email : string =''
  name : string = ''
  surName : string =''
  phoneNumber : string =''
  address : string =''
  dateOfBirth!: Date;
  avatarURL : string =''
  filesAvartar: any;
  constructor(
    private userService : UserService,
    private message: TDSMessageService
  ) { }

  ngOnInit(): void {
    this.getInfoUser()
  }
  getInfoUser()
  {
    this.userService.getInfoToGetAvatar().subscribe((res:any)=>{
        this.userName = res.userName
        this.email = res.email
        this.phoneNumber = res.phoneNumber
        this.surName = res.surname
        this.name = res.name
        this.address = res.address
        this.dateOfBirth = res.dateOfBirth
        this.avatarURL = res.avatarUrl
    })
  }
  updateProfile(){
    this.userService.updateProfile(this.email, this.name, this.address, this.dateOfBirth).subscribe(()=>{
      this.message.success('Update Successfully');
    })
  }
  createAvartar(file: any) {
    this.filesAvartar = file.target.files;
    for (const file of this.filesAvartar) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.userService.UploadAvartar(file).subscribe(
          data => {
            if (data.headers) {
              this.avatarURL = file.name
            }
          },
          error => {
            console.log('err', error);
          }
        );
      };
      reader.readAsDataURL(file);
    }
  }
}
