import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CollectService } from 'providers/collectService';
import { NzMessageService, UploadFile } from 'ng-zorro-antd';
import { ReuseTabService } from '@delon/abc';

@Component({
  selector: 'collect-edit',
  templateUrl: './edit.component.html',
  providers: [CollectService],
})
export class CollectEditComponent implements OnInit {
  id = '';
  form: FormGroup;
  submitting = false;
  schoolYear = [];

  constructor(
    private http: _HttpClient,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private collectService: CollectService,
    public msgSrv: NzMessageService,
    private router: Router,
    private reuseTabService: ReuseTabService,
  ) {
    this.form = this.formBuilder.group({
      schoolYear: [null, [Validators.required]],
      deadlineSubmission: [null, [Validators.required]],
      description: '',
      baseDescription: '',
      cooperationDescription: '',
    });
  }

  ngOnInit() {
    this.id = this.activatedRoute.snapshot.paramMap.get('id');
    this.getSchoolYear();
    this.getCollectDetail();
  }

  getCollectDetail() {
    if (!this.id) return;
    this.collectService.getCollectDetail(this.id).then(res => {
      if (!res) return;
      this.form.patchValue(res['result']);
    });
  }

  getSchoolYear() {
    const now = new Date();
    const year = now.getFullYear() + 3;

    for (let i = 0; i < 10; i++) {
      this.schoolYear.push(year - 1 - i + '-' + (year - i));
    }
  }

  submit() {
    for (const i in this.form.controls) {
      this.form.controls[i].markAsDirty();
      this.form.controls[i].updateValueAndValidity();
    }
    if (this.form.invalid) return;
    this.submitting = true;
    const input = this.form.value;

    if (this.id) {
      input.id = this.id;
      this.collectService.updateCollect(input).then(res => {
        this.submitting = false;
        if (!res) return;
        this.msgSrv.success(`提交成功`);
        this.reuseTabService.clear();
        this.router.navigateByUrl('/collect/list');
      });
    } else {
      this.collectService.insertCollect(input).then(res => {
        this.submitting = false;
        if (!res) return;
        this.msgSrv.success(`提交成功`);
        this.reuseTabService.clear();
        this.router.navigateByUrl('/collect/list');
      });
    }
  }
}
