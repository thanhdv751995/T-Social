
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { BrowserModule } from '@angular/platform-browser';
import { TDSButton, TDSButtonModule, TDSCheckBoxModule, TDSFormFieldModule, TDSInputModule, TDSMessageModule, TDSSwitchModule, TDS_I18N, vi_VN } from 'tmt-tang-ui'; 
import { ScrollingModule } from '@angular/cdk/scrolling'; 
import { DragDropModule } from '@angular/cdk/drag-drop';
import { HttpClientModule } from '@angular/common/http';
import { TDSToolTipModule } from 'tmt-tang-ui'
// Đa ngôn ngữ
import localeVi from '@angular/common/locales/vi'; 
import { registerLocaleData } from '@angular/common'; 
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { HomeModule } from './home/home.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { UserComponent } from './user/user.component';
// Thiết lập tiếng Việt
registerLocaleData(localeVi); 
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    UserComponent,
  ],
  imports: [
    FormsModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    DragDropModule,
    ScrollingModule,
    TDSCheckBoxModule,
    HomeModule,
    TDSToolTipModule,
    HttpClientModule,
    TDSFormFieldModule,
    TDSInputModule,
    ReactiveFormsModule,
    TDSMessageModule,
    TDSSwitchModule,
    TDSButtonModule
  ],
  providers: [{ provide: TDS_I18N, useValue: vi_VN }],
  bootstrap: [AppComponent]
})
export class AppModule { }
