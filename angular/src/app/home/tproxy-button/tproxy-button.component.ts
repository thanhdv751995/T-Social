import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-tproxy-button',
  templateUrl: './tproxy-button.component.html',
  styleUrls: ['./tproxy-button.component.css'],
})
export class TProxyButtonComponent implements OnInit {
  @Input() text: string = '';
  @Input() color: any;
  @Input() icon: string = '';
  @Input() disabled= false;
  constructor() {}

  ngOnInit(): void {}
}
