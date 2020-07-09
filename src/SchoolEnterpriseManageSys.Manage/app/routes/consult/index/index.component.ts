import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'consult-index',
  templateUrl: './index.component.html',
})
export class ConsultIndexComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
