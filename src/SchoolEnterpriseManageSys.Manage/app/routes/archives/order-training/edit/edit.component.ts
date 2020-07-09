import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'archives-order-training-edit',
  templateUrl: './edit.component.html',
})
export class ArchivesOrderTrainingEditComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
