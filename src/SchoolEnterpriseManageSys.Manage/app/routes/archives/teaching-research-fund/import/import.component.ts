import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'archives-teaching-research-fund-import',
  templateUrl: './import.component.html',
})
export class ArchivesTeachingResearchFundImportComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
