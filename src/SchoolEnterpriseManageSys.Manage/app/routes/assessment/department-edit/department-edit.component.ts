import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'assessment-department-edit',
  templateUrl: './department-edit.component.html',
})
export class AssessmentDepartmentEditComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
