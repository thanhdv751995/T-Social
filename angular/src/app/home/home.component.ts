import { AfterViewInit, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MenuDto } from 'src/modal/menuDto';
import { ChromeService } from 'src/serivce/chrome/chrome.service';
import { ClientFacebookService } from 'src/serivce/Client-Facebook/client-facebook.service';
import { LogOutService } from 'src/serivce/Log-out/log-out.service';
import { ShareServiceService } from 'src/serivce/share-service.service';
import { SignalRService } from 'src/serivce/signalR/signalR.service';
import { UserService } from 'src/serivce/User/user.service';
import { TDSSafeAny } from 'tmt-tang-ui';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit, AfterViewInit {
  isCollapsed = false;
  lstMenu = MenuDto;
  activeTab = 1;
  active = 1;
  active1 = 'top';
  isChristMast = false;
  avartarUrl: string = '';
  //header
  contact: any;
  //info user
  infoUser: any;
  listIcon = [
    'tdsi-star-2-fill',
    'tdsi-pin-fill',
    'tdsi-domain-fill',
    'tdsi-dashboard-fill',
  ];
  icon: string = 'tdsi-star-2-fill';
  // optionMenu ={ background: 'bg-primary-2 dark:bg-d-neutral-3-200', backgroundItem: 'bg-primary-2 dark:bg-d-neutral-3-200', backgroundItemSelected: 'bg-white dark:bg-d-neutral-3-300', backgroundItemHover:'dark:hover:bg-d-neutral-3-300 hover:bg-neutral-3-50' }
  setActiveTab(event: TDSSafeAny) {
    this.activeTab = event;
  }
  constructor(
    private userService: UserService,
    private logOutService: LogOutService,
    private router: Router,
    private shareService: ShareServiceService,
    private chromeService: ChromeService,
    private clientService: ClientFacebookService,
    private signalRService: SignalRService
  ) { }
  ngAfterViewInit(): void { }

  ngOnInit(): void {
    this.getInfoUser();
    // setInterval(() => {
    //   this.icon = this.randomIcon();
    //   console.log(this.icon);
    // }, 3000);
  }

  toggleCollapsed(): void {
    this.isCollapsed = !this.isCollapsed; 
  }

  onOpenChange(e: boolean) {
    this.isCollapsed = e;
  }
  getInfoUser() {
    this.userService.getInfoToGetAvatar().subscribe((res: any) => {
      this.infoUser = res;
    });
  }
  logOut() {
    this.logOutService.logOut().subscribe(() => {
      this.router.navigate(['/login']);
      this.shareService.deleteLocalData();
    });
  }
  newProfileChrome() {
    let date = new Date();
    let profile =
      this.makeId(32) +
      date.getHours() * 60 * 60 * 1000 +
      date.getMinutes() * 60 * 1000 +
      date.getSeconds() * 1000 +
      date.getMilliseconds();
    this.chromeService.newChromeProfile(profile, 'https://www.google.com').subscribe(() => {
      this.updateProfileChrome({ profile: profile });
    });
  }

  makeId(length: any) {
    var result = '';
    var characters =
      'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    var charactersLength = characters.length;
    for (var i = 0; i < length; i++) {
      result += characters.charAt(Math.floor(Math.random() * charactersLength));
    }
    return result;
  }

  updateProfileChrome(data: any) {
    this.chromeService.updateProfile(data).subscribe();
  }

  randomIcon() {
    let numRandom = this.getRandomInt(4);
    return this.listIcon[numRandom];
  }

  getRandomInt(max: any) {
    return Math.floor(Math.random() * max);
  }
}
