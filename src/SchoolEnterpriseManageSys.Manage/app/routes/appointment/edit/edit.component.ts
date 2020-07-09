import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'appointment-edit',
  templateUrl: './edit.component.html',
})
export class AppointmentEditComponent implements OnInit {

  constructor(private http: _HttpClient) { }

  ngOnInit() { }

}
