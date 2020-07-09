import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'assessment-index-edit',
  templateUrl: './index-edit.component.html',
})
export class AssessmentIndexEditComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
