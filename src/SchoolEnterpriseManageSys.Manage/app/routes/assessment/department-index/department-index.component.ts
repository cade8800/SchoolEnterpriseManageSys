import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'assessment-department-index',
  templateUrl: './department-index.component.html',
})
export class AssessmentDepartmentIndexComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
