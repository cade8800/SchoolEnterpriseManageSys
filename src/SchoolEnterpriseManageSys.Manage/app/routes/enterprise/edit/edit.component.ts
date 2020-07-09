import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'enterprise-edit',
  templateUrl: './edit.component.html',
})
export class EnterpriseEditComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
