import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'assessment-department',
  templateUrl: './department.component.html',
})
export class AssessmentDepartmentComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
