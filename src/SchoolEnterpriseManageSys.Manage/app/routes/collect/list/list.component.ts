import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'collect-list',
  templateUrl: './list.component.html',
})
export class CollectListComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
