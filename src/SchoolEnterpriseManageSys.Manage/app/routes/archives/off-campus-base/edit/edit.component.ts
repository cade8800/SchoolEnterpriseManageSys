import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'archives-off-campus-base-edit',
  templateUrl: './edit.component.html',
})
export class ArchivesOffCampusBaseEditComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
