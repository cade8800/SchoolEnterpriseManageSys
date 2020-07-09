import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import {
  FormGroup,
  FormBuilder,
  Validators,
  FormControl,
} from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd';
import { UserService } from '../../../../providers/userService';

@Component({
  selector: 'passport-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.less'],
  providers: [UserService],
})
export class UserRegisterComponent implements OnDestroy {
  form: FormGroup;
  error = '';
  type = 0;
  loading = false;
  visible = false;
  status = 'pool';
  progress = 0;
  passwordProgressMap = {
    ok: 'success',
    pass: 'normal',
    pool: 'exception',
  };

  constructor(
    fb: FormBuilder,
    private router: Router,
    public msg: NzMessageService,
    public userService: UserService,
  ) {
    this.form = fb.group({
      email: [null, [Validators.email]],
      password: [
        null,
        [
          Validators.required,
          Validators.minLength(8),
          Validators.maxLength(16),
          UserRegisterComponent.checkPassword.bind(this),
        ],
      ],
      confirmPassword: [
        null,
        [
          Validators.required,
          Validators.minLength(8),
          Validators.maxLength(16),
          UserRegisterComponent.passwordEquar,
        ],
      ],
      // mobilePrefix: ['+86'],
      // mobile: [null, [Validators.required, Validators.pattern(/^1\d{10}$/)]],
      // captcha: [null, [Validators.required]],
    });
  }

  static checkPassword(control: FormControl) {
    if (!control) return null;
    const self: any = this;
    self.visible = !!control.value;
    if (control.value && control.value.length > 9) self.status = 'ok';
    else if (control.value && control.value.length > 5) self.status = 'pass';
    else self.status = 'pool';

    if (self.visible)
      self.progress =
        control.value.length * 10 > 100 ? 100 : control.value.length * 10;
  }

  static passwordEquar(control: FormControl) {
    if (!control || !control.parent) return null;
    if (control.value !== control.parent.get('password').value) {
      return { equar: true };
    }
    return null;
  }

  // region: fields

  get email() {
    return this.form.controls.email;
  }
  get password() {
    return this.form.controls.password;
  }
  get confirmPassword() {
    return this.form.controls.confirmPassword;
  }
  // get mobile() {
  //   return this.form.controls.mobile;
  // }
  // get captcha() {
  //   return this.form.controls.captcha;
  // }

  // endregion

  // region: get captcha

  count = 0;
  interval$: any;

  getCaptcha() {
    this.count = 59;
    this.interval$ = setInterval(() => {
      this.count -= 1;
      if (this.count <= 0) clearInterval(this.interval$);
    }, 1000);
  }

  // endregion

  submit() {
    this.error = '';
    for (const i in this.form.controls) {
      this.form.controls[i].markAsDirty();
      this.form.controls[i].updateValueAndValidity();
    }
    if (this.form.invalid) return;
    // mock http
    this.loading = true;

    this.userService.enterpriseRegist(this.form.value).then(res => {
      this.loading = false;
      if (!res) return;
      if (!res.success) return;
      this.router.navigate([
        '/passport/register-result/' + this.form.value.email,
      ]);
    });
  }

  ngOnDestroy(): void {
    if (this.interval$) clearInterval(this.interval$);
  }
}
