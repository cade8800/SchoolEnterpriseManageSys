import { EnterpriseService } from 'providers/enterpriseService';
import { DepartmentService } from 'providers/departmentService';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { NzMessageService, UploadFile } from 'ng-zorro-antd';
import { _HttpClient, SettingsService } from '@delon/theme';
import { ProjectService } from 'providers/projectService';
import { File_Upload } from 'providers/constants';
import { ReuseTabService } from '@delon/abc';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FileService } from 'providers/fileService';

@Component({
  selector: 'enterprise-edit',
  templateUrl: './edit.component.html',
  providers: [
    ProjectService,
    FileService,
    DepartmentService,
    EnterpriseService,
  ],
})
export class EnterpriseEditComponent implements OnInit {
  constructor(
    private router: Router,
    public _location: Location,
    public msgSrv: NzMessageService,
    public http: _HttpClient,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private projectService: ProjectService,
    private fileService: FileService,
    private departmentService: DepartmentService,
    public settingsService: SettingsService,
    private reuseTabService: ReuseTabService,
    private enterpriseService: EnterpriseService,
  ) {
    this.form = this.formBuilder.group({
      id: '',
      fullName: [null, [Validators.required]],
      nameAbbreviation: '',
      legalRepresentative: [null, [Validators.required]],
      fixedTelephone: [null, [Validators.required]],
      contactName: [null, [Validators.required]],
      scale: '',
      companyProfile: ''
    });
  }

  id = '';
  form: FormGroup;
  submitting = false;
  uploadUrl = File_Upload;
  fileList: any = [];
  enterprise: any = {};


  ngOnInit(): void {
    this.id = this.activatedRoute.snapshot.paramMap.get('id');
    this.getDetail();
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

        this.fileService.insertEnterpriseFile({
          enterpriseId: this.id || '',
          fileId: item.id,
        });

      });
    }
  }

  fileRemove = (file: UploadFile) => {
    if (file.fileId) {

      this.fileService.deleteEnterpriseFile({
        enterpriseId: this.id || '',
        fileId: file.fileId,
      });

    }
    return true;
  }

  getDetail() {
    this.enterpriseService.getEnterpriseDetail(this.id).then(res => {
      if (!res) return;
      if (!res.success) return;
      if (!res.result) return;
      this.enterprise = res.result;
      this.form.patchValue(res.result);
      this.fileList = res.result.fileList;
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
    console.log(input);

    this.enterpriseService.editEnterprise(input).then(res => {
      this.submitting = false;
      if (!res) return;
      this.msgSrv.success(`提交成功`);
    });

  }
}
