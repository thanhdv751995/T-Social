import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from 'src/serivce/Login/login.service';
import { ShareServiceService } from 'src/serivce/share-service.service';
import { TDSMessageService, TDSNotificationService } from 'tmt-tang-ui';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  email = new FormControl('', [Validators.required, Validators.email]);
  phoneNumber: string = '';
  passWord: string = '';
  InfoLoading = false
  form = this.fb.group({
    account: this.fb.array([]),
  });
  constructor(
    private loginService: LoginService,
    private formBuilder: FormBuilder,
    private router: Router,
    private notification: TDSNotificationService,
    private fb: FormBuilder,
    private shareService: ShareServiceService,
    private message: TDSMessageService
  ) {}

  ngOnInit(): void {}
  login() {
    this.InfoLoading = true
      this.loginService.login(this.phoneNumber, this.passWord).subscribe(
        (res) => {
          this.message.success('Login successfully!');
          this.InfoLoading = false
          const promiseLogin = new Promise((resolve, reject) => {
            resolve(
              this.shareService.writeLocalData(
                res.accessToken,
                res.refreshToken,
                JSON.stringify(res.data)
              )
            );
          });
          promiseLogin.then(() => {
            this.router.navigate(['/home']);
          });
        },
        (error) => {
          this.notification.error('Something wrong', error.error.details, {
            placement: 'topLeft',
          });
          this.InfoLoading = false
        }
      );
  }
}
