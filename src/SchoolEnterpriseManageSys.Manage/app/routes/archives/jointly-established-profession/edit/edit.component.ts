import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'archives-jointly-established-profession-edit',
  templateUrl: './edit.component.html',
})
export class ArchivesJointlyEstablishedProfessionEditComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
