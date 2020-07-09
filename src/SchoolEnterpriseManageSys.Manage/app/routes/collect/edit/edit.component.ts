import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'collect-edit',
  templateUrl: './edit.component.html',
})
export class CollectEditComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
