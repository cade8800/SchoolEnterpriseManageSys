import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'assessment-edit',
  templateUrl: './edit.component.html',
})
export class AssessmentEditComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
