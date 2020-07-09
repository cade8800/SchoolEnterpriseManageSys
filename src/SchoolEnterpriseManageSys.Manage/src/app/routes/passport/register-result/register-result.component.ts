import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'passport-register-result',
  templateUrl: './register-result.component.html',
})
export class UserRegisterResultComponent implements OnInit {
  constructor(private route: ActivatedRoute) {}
  email = '';
  ngOnInit() {
    this.email = this.route.snapshot.paramMap.get('email');
  }
}
