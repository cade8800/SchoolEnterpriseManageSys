import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { NzMessageService, UploadFile } from 'ng-zorro-antd';
import { _HttpClient, SettingsService } from '@delon/theme';
import { File_Upload } from 'providers/constants';
import { ReuseTabService } from '@delon/abc';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FileService } from 'providers/fileService';
import { AssessmentService } from 'providers/assessmentService';

@Component({
  selector: 'assessment-department-edit',
  templateUrl: './department-edit.component.html',
  providers: [AssessmentService, FileService],
})
export class AssessmentDepartmentEditComponent implements OnInit {
  constructor(
    private router: Router,
    public _location: Location,
    public msgSrv: NzMessageService,
    public http: _HttpClient,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private assessmentService: AssessmentService,
    private fileService: FileService,
    public settingsService: SettingsService,
    private reuseTabService: ReuseTabService,
  ) {
    this.form = this.formBuilder.group({
      id: '',
      selfEvaluationScore: [null, [Validators.required]],
      selfEvaluation: '',
    });
  }

  id: string;
  assessmentDepartmentId: string;
  assessmentIndexId: string;

  index: any = {};

  form: FormGroup;
  submitting = false;
  uploadUrl = File_Upload;
  fileList: any = [];

  projectList = [{
    type: 2,
    typeText: '校外实践基地',
    fileList: [],
    total: 0
  }, {
    type: 4,
    typeText: '校内共建基地',
    fileList: [],
    total: 0
  }, {
    type: 8,
    typeText: '社会服务',
    fileList: [],
    total: 0
  }, {
    type: 16,
    typeText: '订单培养',
    fileList: [],
    total: 0
  }, {
    type: 32,
    typeText: '共编教材/课程',
    fileList: [],
    total: 0
  }, {
    type: 64,
    typeText: '教学研基金',
    fileList: [],
    total: 0
  }, {
    type: 128,
    typeText: '学术成果',
    fileList: [],
    total: 0
  }];

  ngOnInit(): void {
    this.id = this.activatedRoute.snapshot.paramMap.get('id');
    this.assessmentDepartmentId = this.activatedRoute.snapshot.paramMap.get('dep');
    this.assessmentIndexId = this.activatedRoute.snapshot.paramMap.get('index');

    this.getIndexDetail();
    this.getDetail();
    this.getDepartmentProjectsOnly();
  }

  fileUpload(res) {
    if (
      res.type === 'success' &&
      res.file.response.result &&
      res.file.response.result.length > 0
    ) {
      res.file.response.result.forEach(item => {
        this.fileList.forEach(f => {
          if (!f.fileId) {
            f.url = item.fileUrl;
            f.thumbUrl = '';
            f.uid = item.id;
            f.fileId = item.id;
          }
        });
        if (this.id) {
          this.fileService.insertAssessmentFile({
            departmentIndexId: this.id,
            fileId: item.id,
          });
        }
      });
    }
  }

  fileRemove = (file: UploadFile) => {
    if (file.fileId) {
      if (this.id) {
        this.fileService.deleteAssessmentFile({
          departmentIndexId: this.id,
          fileId: file.fileId,
        });
      } else {
        this.fileService.deleteFile(file.fileId);
      }
    }
    return true;
  }

  getIndexDetail() {
    if (!this.assessmentIndexId) return;
    this.assessmentService.getIndex(this.assessmentIndexId).then(res => {
      this.index = res.result;
    });
  }

  getDetail() {
    if (!this.id) return;
    this.assessmentService.getAssessmentDepartmentIndex(this.id).then(res => {
      if (!res) return;
      if (!res.success) return;
      if (!res.result) return;
      this.form.patchValue(res.result);
      this.fileList = res.result.fileList;

      this.index = res.result;
      this.projectList.forEach(t => {
        const target = this.index.projectList.filter(function (item) {
          return item.type === t.type;
        });
        t.total = target.length;
        target.forEach(element => {
          t.fileList = [...element.fileList, ...t.fileList];
        });
      });
      // console.log(this.projectList);
    });
  }

  getDepartmentProjectsOnly() {
    if (this.id) return;
    this.assessmentService.getAssessmentDepartmentProjects(this.assessmentDepartmentId).then(res => {
      if (!res) return;
      if (!res.success) return;
      if (!res.result) return;

      this.projectList.forEach(t => {
        const target = res.result.projectList.filter(function (item) {
          return item.type === t.type;
        });
        t.total = target.length;
        target.forEach(element => {
          t.fileList = [...element.fileList, ...t.fileList];
        });
      });
    });
  }

  submit() {
    for (const i in this.form.controls) {
      this.form.controls[i].markAsDirty();
      this.form.controls[i].updateValueAndValidity();
    }
    if (this.form.invalid) return;

    const input = this.form.value;

    if (input.selfEvaluationScore > this.index.standardScore) {
      this.msgSrv.error('评分不可高于标准分');
      return;
    }

    input.assessmentDepartmentId = this.assessmentDepartmentId;
    input.assessmentIndexId = this.assessmentIndexId;
    input.fileList = [];

    this.fileList.forEach(item => {
      if (item.fileId) {
        if (
          input.fileList.filter(function (t) {
            return t.fileId === item.fileId;
          }).length < 1
        )
          input.fileList.push({ fileId: item.fileId });
      }
    });

    this.submitting = true;
    this.assessmentService.selfEvaluation(input).then(res => {
      this.submitting = false;
      if (!res) return;
      this.msgSrv.success(`提交成功`);
      history.go(-1);
    });
  }
}
