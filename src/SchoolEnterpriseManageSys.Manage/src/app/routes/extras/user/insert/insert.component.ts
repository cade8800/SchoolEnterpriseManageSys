import { UserService } from 'providers/userService';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NzMessageService, NzModalRef } from 'ng-zorro-antd';
import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { DepartmentService } from 'providers/departmentService';

@Component({
  selector: 'app-extras-user-insert',
  templateUrl: './insert.component.html',
  providers: [DepartmentService, UserService],
})
export class ExtrasUserInsertComponent implements OnInit {
  param: any;
  title: any = { 2: '系统管理员', 8: '专家', 16: '系管理员' };
  form: FormGroup;
  errorMsg = '';
  departmentList: any = [];

  constructor(
    private modal: NzModalRef,
    public msgSrv: NzMessageService,
    private departmentService: DepartmentService,
    public http: _HttpClient,
    formBuilder: FormBuilder,
    private userService: UserService,
  ) {
    this.form = formBuilder.group({
      userName: [null, [Validators.required, Validators.maxLength(30), Validators.minLength(5)]],
      actualName: [null, [Validators.maxLength(30)]],
      mobilephone: [null, [Validators.maxLength(20)]],
      fixedTelephone: [null, [Validators.maxLength(20)]],
      email: [null, [Validators.email, Validators.maxLength(60)]],
      departmentId: '',
      position: [null, [Validators.maxLength(15)]],
    });
  }
  get userName() {
    return this.form.controls.userName;
  }
  get actualName() {
    return this.form.controls.actualName;
  }
  get mobilephone() {
    return this.form.controls.mobilephone;
  }
  get fixedTelephone() {
    return this.form.controls.fixedTelephone;
  }
  get email() {
    return this.form.controls.email;
  }
  get departmentId() {
    return this.form.controls.departmentId;
  }
  get position() {
    return this.form.controls.position;
  }

  ngOnInit() { }

  loadDepartment() {
    if (this.departmentList.length > 0) return;
    this.departmentService.getDepartment().then(res => {
      if (!res) return;
      this.departmentList = res['result']['departmentList'];
    });
  }

  submit() {
    this.errorMsg = '';
    for (const i in this.form.controls) {
      this.form.controls[i].markAsDirty();
      this.form.controls[i].updateValueAndValidity();
    }
    if (this.form.invalid) return;
    const input: any = this.form.value;
    input.roleType = this.param;
    if (input.roleType === 16 && !input.departmentId) {
      this.errorMsg = '请选择所属系';
      return;
    }
    this.userService.insertUser(input).then(res => {
      if (!res) return;
      this.msgSrv.success('操作成功');
      this.modal.close(true);
      this.close();
    });
  }

  close() {
    this.modal.destroy();
  }
}
