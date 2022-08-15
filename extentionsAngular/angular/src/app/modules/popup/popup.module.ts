import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { PopupComponent } from './pages/popup/popup.component';
import { PopupRoutingModule } from './popup-routing.module';

@NgModule({
  declarations: [PopupComponent],
  imports: [CommonModule,FormsModule, PopupRoutingModule]
})
export class PopupModule {}
