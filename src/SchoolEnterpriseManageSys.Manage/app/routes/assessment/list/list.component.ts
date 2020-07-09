import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'assessment-list',
  templateUrl: './list.component.html',
})
export class AssessmentListComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
