import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'archives-academic-achievement-import',
  templateUrl: './import.component.html',
})
export class ArchivesAcademicAchievementImportComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
