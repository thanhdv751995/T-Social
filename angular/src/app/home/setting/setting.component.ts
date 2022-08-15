import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-setting',
  templateUrl: './setting.component.html',
  styleUrls: ['./setting.component.css']
})
export class SettingComponent implements OnInit {

  color : any;
  constructor() { }

  ngOnInit(): void {
  }
  chooseColor()
  { 
    var x = (<HTMLInputElement>document.getElementById('head')).value
  }
}
