import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'cms-empty',
  templateUrl: './empty.component.html',
})
export class CmsEmptyComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
