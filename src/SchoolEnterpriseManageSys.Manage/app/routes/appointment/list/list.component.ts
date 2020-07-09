import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'appointment-list',
  templateUrl: './list.component.html',
})
export class AppointmentListComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
