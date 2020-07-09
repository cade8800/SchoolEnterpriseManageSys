import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'assessment-department-detail',
  templateUrl: './department-detail.component.html',
})
export class AssessmentDepartmentDetailComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
