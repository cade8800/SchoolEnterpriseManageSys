import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'archives-social-service-import',
  templateUrl: './import.component.html',
})
export class ArchivesSocialServiceImportComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
