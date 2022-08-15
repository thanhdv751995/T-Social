import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { OptionsRoutingModule } from './options-routing.module';
import { OptionsComponent } from './pages/options/options.component';
import { HttpClientModule } from '@angular/common/http';
import { NzResultModule } from 'ng-zorro-antd/result';
import { NzButtonModule } from 'ng-zorro-antd/button';
@NgModule({
  declarations: [OptionsComponent],
  imports: [CommonModule, OptionsRoutingModule,FormsModule,HttpClientModule,NzResultModule,NzButtonModule]
})
export class OptionsModule {}
