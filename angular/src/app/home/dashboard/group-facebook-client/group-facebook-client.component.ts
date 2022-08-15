import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-group-facebook-client',
  templateUrl: './group-facebook-client.component.html',
  styleUrls: ['./group-facebook-client.component.css']
})
export class GroupFacebookClientComponent implements OnInit {
  expandSet = new Set<number>();

  listOfData = [
      {
          id: 1,
          name: 'John Brown',
          age: 32,
          expand: false,
          address: 'New York No. 1 Lake Park',
          description: 'My name is John Brown, I am 32 years old, living in New York No. 1 Lake Park.'
      },
      {
          id: 2,
          name: 'Jim Green',
          age: 42,
          expand: false,
          address: 'London No. 1 Lake Park',
          description: 'My name is Jim Green, I am 42 years old, living in London No. 1 Lake Park.'
      },
      {
          id: 3,
          name: 'Joe Black',
          age: 32,
          expand: false,
          address: 'Sidney No. 1 Lake Park',
          description: 'My name is Joe Black, I am 32 years old, living in Sidney No. 1 Lake Park.'
      }
  ];
  constructor() { }

  ngOnInit(): void {
  }
  onExpandChange(id: number, checked: boolean): void {
    if (checked) {
        this.expandSet.add(id);
    } else {
        this.expandSet.delete(id);
    }
}
}
