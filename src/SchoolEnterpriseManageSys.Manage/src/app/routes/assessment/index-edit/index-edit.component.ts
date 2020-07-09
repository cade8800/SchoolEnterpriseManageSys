import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { NzMessageService, UploadFile } from 'ng-zorro-antd';
import { _HttpClient, SettingsService } from '@delon/theme';
import { ReuseTabService } from '@delon/abc';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AssessmentService } from 'providers/assessmentService';


@Component({
  selector: 'assessment-index-edit',
  templateUrl: './index-edit.component.html',
  providers: [AssessmentService],
})
export class AssessmentIndexEditComponent implements OnInit {
  constructor(
    private router: Router,
    public _location: Location,
    public msgSrv: NzMessageService,
    public http: _HttpClient,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private assessmentService: AssessmentService,
    public settingsService: SettingsService,
    private reuseTabService: ReuseTabService,
  ) {
    this.form = this.formBuilder.group({
      id: '',
      associatProjectType: '',
      completeStandard: [null, [Validators.required]],
      content: [null, [Validators.required]],
      indexType: [null, [Validators.required]],
      standardScore: [null, [Validators.required]],
      remark: ''
    });
  }

  id = '';
  form: FormGroup;
  submitting = false;
  index: any = {};
  projectTypeList: any = [
    { key: '校外实践基地', value: '2' },
    { key: '校内共建基地', value: '4' },
    { key: '社会服务', value: '8' },
    { key: '订单培养', value: '16' },
    { key: '共编教材/课程', value: '32' },
    { key: '教学研基金', value: '64' },
    { key: '学术成果', value: '128' },
    { key: '共建专业', value: '256' },
  ];


  ngOnInit(): void {
    this.id = this.activatedRoute.snapshot.paramMap.get('id');
    this.getDetail();
  }

  getDetail() {
    if (!this.id) return;
    this.assessmentService.getIndex(this.id).then(res => {
      if (!res) return;
      if (!res.success) return;
      if (!res.result) return;
      this.index = res.result;
      this.form.patchValue(res.result);
    });
  }

  submit() {
    for (const i in this.form.controls) {
      this.form.controls[i].markAsDirty();
      this.form.controls[i].updateValueAndValidity();
    }
    if (this.form.invalid) return;
    this.submitting = true;

    this.assessmentService.editIndex(this.form.value).then(res => {
      this.submitting = false;
      if (!res) return;
      this.msgSrv.success(`提交成功`);
      this.reuseTabService.clear();
      this.router.navigateByUrl('/assessment/index');
    });

  }
}
