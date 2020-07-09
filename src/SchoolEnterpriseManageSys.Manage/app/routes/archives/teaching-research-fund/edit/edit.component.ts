import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'archives-teaching-research-fund-edit',
  templateUrl: './edit.component.html',
})
export class ArchivesTeachingResearchFundEditComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
