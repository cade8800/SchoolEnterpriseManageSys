import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'assessment-expert-edit',
  templateUrl: './expert-edit.component.html',
})
export class AssessmentExpertEditComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
