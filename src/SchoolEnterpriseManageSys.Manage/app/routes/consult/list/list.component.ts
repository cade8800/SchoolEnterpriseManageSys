import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'consult-list',
  templateUrl: './list.component.html',
})
export class ConsultListComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
