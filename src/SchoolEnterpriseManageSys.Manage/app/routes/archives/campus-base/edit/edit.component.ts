import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'archives-campus-base-edit',
  templateUrl: './edit.component.html',
})
export class ArchivesCampusBaseEditComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
