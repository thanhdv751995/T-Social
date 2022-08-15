import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { UserComponent } from './user/user.component';

const routes: Routes = [
  { path: '',
   loadChildren: () => import('./home/home.module').then(m => m.HomeModule),pathMatch: 'prefix' },
  { path: 'login', component : LoginComponent },
  { path: 'profile', component : UserComponent },
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [
    {provide : LocationStrategy , useClass: HashLocationStrategy}
    ],
})
export class AppRoutingModule { }
