import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'enterprise-detail',
  templateUrl: './detail.component.html',
})
export class EnterpriseDetailComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
