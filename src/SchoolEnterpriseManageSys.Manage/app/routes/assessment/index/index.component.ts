import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'assessment-index',
  templateUrl: './index.component.html',
})
export class AssessmentIndexComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
