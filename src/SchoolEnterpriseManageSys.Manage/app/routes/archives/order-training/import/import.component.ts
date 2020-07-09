import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'archives-order-training-import',
  templateUrl: './import.component.html',
})
export class ArchivesOrderTrainingImportComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
