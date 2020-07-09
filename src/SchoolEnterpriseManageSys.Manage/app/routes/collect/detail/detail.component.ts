import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'collect-detail',
  templateUrl: './detail.component.html',
})
export class CollectDetailComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
