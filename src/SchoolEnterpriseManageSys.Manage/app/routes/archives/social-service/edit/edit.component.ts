import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'archives-social-service-edit',
  templateUrl: './edit.component.html',
})
export class ArchivesSocialServiceEditComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
