import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'collect-department',
  templateUrl: './department.component.html',
})
export class CollectDepartmentComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
