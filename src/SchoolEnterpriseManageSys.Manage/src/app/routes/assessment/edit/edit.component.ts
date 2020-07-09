import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NzMessageService, UploadFile } from 'ng-zorro-antd';
import { ReuseTabService } from '@delon/abc';
import { AssessmentService } from 'providers/assessmentService';
import { DepartmentService } from 'providers/departmentService';

@Component({
  selector: 'assessment-edit',
  templateUrl: './edit.component.html',
  providers: [AssessmentService, DepartmentService],
})
export class AssessmentEditComponent implements OnInit {
  id = '';
  form: FormGroup;
  submitting = false;
  schoolYear: any = [];
  departmentList: any = [];
  checkedDepartmentList: any = [];

  constructor(
    private http: _HttpClient,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private assessmentService: AssessmentService,
    public msgSrv: NzMessageService,
    private router: Router,
    private reuseTabService: ReuseTabService,
    private departmentService: DepartmentService
  ) {
    this.form = this.formBuilder.group({
      id: '',
      schoolYear: [null, [Validators.required]],
      deadline: [null, [Validators.required]],
      rangeTime: [null, [Validators.required]],
      deplist: ''
    });
  }

  ngOnInit() {
    this.id = this.activatedRoute.snapshot.paramMap.get('id');
    this.getSchoolYear();
    this.getDetail();
    this.getDepList();
  }

  getDepList() {
    this.departmentService.getDepartment().then(res => {
      this.departmentList = res['result'].departmentList;
      if (!this.departmentList) return;
      this.departmentList.forEach(item => {
        item.checked = false;
      });
    });
  }

  checkDepartment(value: string[]): void {
    this.checkedDepartmentList = value;
  }

  getDetail() {
    if (!this.id) return;
    this.assessmentService.getAssessment(this.id).then(res => {
      if (!res) return;
      this.form.patchValue(res['result']);

      const deplist = res['result'].departmentList;

      if (deplist.length < 1) return;
      this.departmentList.forEach(item => {
        let target = deplist.filter(function (t) {
          return t.id === item.id
        });
        if (target.length > 0) {
          item.checked = true;
        }
      });

    });
  }

  getSchoolYear() {
    const now = new Date();
    const year = now.getFullYear() + 3;

    for (let i = 0; i < 10; i++) {
      this.schoolYear.push(year - 1 - i + '-' + (year - i));
    }
  }

  clickDepartment(dep: any) {
    if (!this.id) return;
    console.log(dep);
    this.assessmentService.editAssessmentDepartment({
      departmentId: dep.id,
      assessmentId: this.id
    });
  }

  submit() {
    for (const i in this.form.controls) {
      this.form.controls[i].markAsDirty();
      this.form.controls[i].updateValueAndValidity();
    }
    if (this.form.invalid) return;
    this.submitting = true;
    const input = this.form.value;
    input.departmentList = [];
    this.checkedDepartmentList.forEach(item => {
      input.departmentList.push({ id: item, name: '11' });
    });

    this.assessmentService.editAssessment(input).then(res => {
      this.submitting = false;
      if (!res) return;
      this.msgSrv.success(`提交成功`);
      this.reuseTabService.clear();
      this.router.navigateByUrl('/assessment/list');
    });

  }
}
