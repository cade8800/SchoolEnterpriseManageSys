import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'archives-off-campus-base-import',
  templateUrl: './import.component.html',
})
export class ArchivesOffCampusBaseImportComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
