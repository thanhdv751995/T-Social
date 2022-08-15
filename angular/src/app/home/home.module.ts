import { VirtualMachineComponent } from './virtual-machine/virtual-machine/virtual-machine.component';
import { NgModule } from '@angular/core';
import {
  TDSAvatarModule,
  TDSBreadCrumbModule,
  TDSCardModule,
  TDSCheckBoxModule,
  TDSDropDownModule,
  TDSFormFieldModule,
  TDSHeaderModule,
  TDSInputModule,
  TDSSelectModule,
  TDSTableModule,
  TDSTagModule,
  TDSModalModule,
  TDSSpinnerModule,
  TDSDatePickerModule,
  TDSButtonModule,
  TDSMessageModule,
  TDSAlertModule,
  TDSNotificationModule,
  TDSInputNumberModule,
  TDSButtonMenuModule,
  TDSUploadModule,
  TDSTabsModule,
  TDSMenuModule,
  TDSPopoverModule,
  TDSCollapseModule,
  TDSImageModule,
  TDSDividerModule,
  TDSToolTipModule,

} from 'tmt-tang-ui';
import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';
import { ClientFacebookComponent } from './client-facebook/client-facebook.component';
import { IsActiveAccountComponent } from './is-active-account/is-active-account.component';
import { ProxyComponent } from './proxy/proxy.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ProxyClientLogsComponent } from './proxy-client-logs/proxy-client-logs/proxy-client-logs.component';
import { ScriptComponent } from './scripts/script/script.component';
import { ProxyUsingScriptComponent } from './proxy-using-script/proxy-using-script/proxy-using-script.component';
import { TProxyButtonComponent } from './tproxy-button/tproxy-button.component';
import { SettingComponent } from './setting/setting.component';
import { SharesComponent } from './shares/shares.component';
import { AutoFocusDirective } from './auto-focus/auto-focus.directive';
import { ClientActivityChartComponent } from './client-facebook/Client-Activity-Chart/client-activity-chart/client-activity-chart.component';
import { ProfileClientComponent } from './profile-client/profile-client/profile-client.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FriendsComponent } from './dashboard/friends/friends.component';
import { GroupComponent } from './dashboard/groups/group/group.component';
import { GroupFacebookClientComponent } from './dashboard/group-facebook-client/group-facebook-client.component';
import { FanpageFacebookClientComponent } from './dashboard/fanpage-facebook-client/fanpage-facebook-client.component';
import { ListStatusInGroupComponent } from './dashboard/group-facebook-client/list-status-in-group/list-status-in-group.component';
import { ClientActivityLogsComponent } from './client-facebook/client-activity-logs/client-activity-logs.component';
import { SeedingLogsComponent } from './campaign/seeding-logs/seeding-logs.component';
import { CampaignComponent } from './campaign/campaign.component';
import { FomateStringPipe } from '../helper/formatString';
import { ClientSeedingDashboardComponent } from './dashboard/client-seeding-dashboard/client-seeding-dashboard.component';
import { formatNameCampaign } from '../helper/formatNameCampaign';
import { formatAction } from '../helper/formatAction';
import { FormatUrl } from '../helper/formatURL';
import { FormatActivity } from '../helper/formatLogActivity';
import { FormatLogUrl } from '../helper/formatLogUrl';
import { SupportContent } from '../helper/supportContent';
import { NameScriptPiPe } from '../helper/nameScriptPipe';
import { FormatNameClientUsingScript } from '../helper/formatNameClientUsingScript';
import { BrowserModule } from '@angular/platform-browser';
import { PolarChartComponent } from './dashboard/polar-chart/polar-chart.component';
import { HistoryTaskComponent } from './history-task/history-task.component';
import { FormatHistory } from '../helper/formatHistory';
import { formatLink } from '../helper/formatLink';
import { FormatTypeScript } from '../helper/formatTypeScript';
import { TaskPreparingComponent } from './campaign/task-preparing/task-preparing.component';
import { DateAgoPipe } from '../helper/timeAgoPipe';
import { NameSeeding } from '../helper/formatNameSeeding';
import { TaskUnFinishedComponent } from './campaign/task-un-finished/task-un-finished.component';
import { FormatNameProfile } from '../helper/nameProfile';
import { NameScript } from '../helper/formatNameScript';
import { TDSProgressModule } from 'tmt-tang-ui';
import { GroupTypeComponent } from './profile-client/profile-client/group-type/group-type.component';
import { TDSBadgeModule } from 'tmt-tang-ui';
import { LoadingComponent } from './loading/loading.component';
@NgModule({
  declarations: [
    LoadingComponent,
    DateAgoPipe,
    FormatTypeScript,
    formatLink,
    NameSeeding,
    FormatHistory,
    FormatNameClientUsingScript,
    NameScriptPiPe,
    SupportContent,
    FormatLogUrl,
    NameScript,
    FormatActivity,
    FormatNameProfile,
    formatAction,
    FormatUrl,
    formatNameCampaign,
    FomateStringPipe,
    HomeComponent,
    ClientFacebookComponent,
    IsActiveAccountComponent,
    ProxyComponent,
    ProxyClientLogsComponent,
    ScriptComponent,
    ProxyUsingScriptComponent,
    TProxyButtonComponent,
    SettingComponent,
    SharesComponent,
    AutoFocusDirective,
    ClientActivityChartComponent,
    ProfileClientComponent,
    DashboardComponent,
    FriendsComponent,
    GroupFacebookClientComponent,
    FanpageFacebookClientComponent,
    ListStatusInGroupComponent,
    FriendsComponent,
    GroupComponent,
    ClientActivityLogsComponent,
    SeedingLogsComponent,
    CampaignComponent,
    ClientSeedingDashboardComponent,
    PolarChartComponent,
    HistoryTaskComponent,
    TaskPreparingComponent,
    TaskUnFinishedComponent,
    VirtualMachineComponent,
    GroupTypeComponent
  ],
  imports: [
    TDSBadgeModule,
    TDSToolTipModule,
    TDSDividerModule,
    TDSImageModule,
    TDSUploadModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    HomeRoutingModule,
    TDSMenuModule,
    TDSTableModule,
    TDSHeaderModule,
    TDSAvatarModule,
    TDSDropDownModule,
    TDSSelectModule,
    TDSFormFieldModule,
    TDSBreadCrumbModule,
    TDSInputModule,
    TDSCheckBoxModule,
    TDSProgressModule,
    TDSCardModule,
    TDSTagModule,
    TDSModalModule,
    TDSSpinnerModule,
    TDSDatePickerModule,
    TDSButtonModule,
    TDSMessageModule,
    TDSAlertModule,
    TDSNotificationModule,
    TDSInputNumberModule,
    TDSButtonMenuModule,
    TDSUploadModule,
    TDSTabsModule,
    TDSPopoverModule,
    TDSCollapseModule
  ],
})
export class HomeModule { }
