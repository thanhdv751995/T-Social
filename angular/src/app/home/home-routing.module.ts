import { VirtualMachineComponent } from './virtual-machine/virtual-machine/virtual-machine.component';
import { ScriptComponent } from './scripts/script/script.component';
import { ProxyClientLogsComponent } from './proxy-client-logs/proxy-client-logs/proxy-client-logs.component';

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClientFacebookComponent } from './client-facebook/client-facebook.component';
import { HomeComponent } from './home.component';
import { IsActiveAccountComponent } from './is-active-account/is-active-account.component';
import { ProxyComponent } from './proxy/proxy.component';
import { ProxyUsingScriptComponent } from './proxy-using-script/proxy-using-script/proxy-using-script.component';
import { SettingComponent } from './setting/setting.component';
import { SharesComponent } from './shares/shares.component';
import { ClientActivityChartComponent } from './client-facebook/Client-Activity-Chart/client-activity-chart/client-activity-chart.component';
import { ProfileClientComponent } from './profile-client/profile-client/profile-client.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FriendsComponent } from './dashboard/friends/friends.component';
import { GroupComponent } from './dashboard/groups/group/group.component';
import { ClientActivityLogsComponent } from './client-facebook/client-activity-logs/client-activity-logs.component';
import { CampaignComponent } from './campaign/campaign.component';
import { HistoryTaskComponent } from './history-task/history-task.component';
import { TaskPreparingComponent } from './campaign/task-preparing/task-preparing.component';
import { TaskUnFinishedComponent } from './campaign/task-un-finished/task-un-finished.component';
import { AuthGuard } from 'src/serivce/Auth/goard.service';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    children: [
      { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'clientFB', component: ClientFacebookComponent , canActivate: [AuthGuard]},
      { path: 'virtual-machine', component: VirtualMachineComponent, canActivate: [AuthGuard] },
      { path: 'proxy', component: ProxyComponent, canActivate: [AuthGuard] },
      { path: 'active', component: IsActiveAccountComponent, canActivate: [AuthGuard] },
      { path: 'logs/:iP', component: ProxyClientLogsComponent, canActivate: [AuthGuard] },
      { path: 'scripts', component: ScriptComponent, canActivate: [AuthGuard] },
      { path: 'proxy-using-script', component: ProxyUsingScriptComponent, canActivate: [AuthGuard] },
      { path: 'shares', component: SharesComponent, canActivate: [AuthGuard] },
      { path: 'setting', component: SettingComponent , canActivate: [AuthGuard]},
      { path: 'profile-client', component: ProfileClientComponent, canActivate: [AuthGuard] },
      { path: 'friends', component: FriendsComponent, canActivate: [AuthGuard] },
      { path: 'groups', component: GroupComponent, canActivate: [AuthGuard] },
      { path: 'campaign', component:CampaignComponent, canActivate: [AuthGuard] },
      { path: 'history', component:HistoryTaskComponent, canActivate: [AuthGuard] },
      { path: 'activityLogs/:idUser', component: ClientActivityLogsComponent, canActivate: [AuthGuard] },
      { path: 'campaign/:seedingName', component: TaskPreparingComponent, canActivate: [AuthGuard] },
      { path: 'campaign/:seedingName/unfinished', component: TaskUnFinishedComponent, canActivate: [AuthGuard] },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class HomeRoutingModule {}
