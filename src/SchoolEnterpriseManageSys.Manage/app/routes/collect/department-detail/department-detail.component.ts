import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'collect-department-detail',
  templateUrl: './department-detail.component.html',
})
export class CollectDepartmentDetailComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
