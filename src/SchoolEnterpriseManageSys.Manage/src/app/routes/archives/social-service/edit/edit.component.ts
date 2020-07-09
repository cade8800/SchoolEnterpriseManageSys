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
  selector: 'archives-social-service-edit',
  templateUrl: './edit.component.html',
  providers: [
    ProjectService,
    FileService,
    DepartmentService,
    EnterpriseService,
  ],
})
export class ArchivesSocialServiceEditComponent implements OnInit {
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
      departmentId: '',
      principal: [null, [Validators.required]],
      enterpriseName: [null, [Validators.required]],
      startTime: [null, [Validators.required]],
      amount: [null, [Validators.required]],
      relateProjectId: '',
      remark: '',
      relateProjectType: '',
    });
  }

  id = '';
  form: FormGroup;
  submitting = false;
  typeDetail = { instructions: '', uploadFileDescription: '' };
  uploadUrl = File_Upload;
  fileIdList = [];
  fileList: any = [];
  projectTypeList = [];
  projectList = [];
  depList = [];
  getProjectListInput: any = {};
  moreProjectLoading = false;
  moreProjectLoadingEnd = false;
  project: any = {};
  enterpriseList: any = [];

  ngOnInit(): void {
    this.id = this.activatedRoute.snapshot.paramMap.get('id');
    // this.getEnterprise();
    this.getTypeDetail();
    this.getTypeList();
    this.getDepList();
    this.getProjectDetail();
  }

  getEnterprise() {
    this.enterpriseService
      .getEnterprise({
        pageIndex: 1,
        pageSize: 20,
      })
      .then(res => {
        if (!res) return;
        this.enterpriseList = res.result.enterprises;
      });
  }
  getTypeDetail() {
    this.projectService.getProjectTypeDetail(8).then(res => {
      if (!res) return;
      this.typeDetail = res['result'];
    });
  }
  getDepList() {
    if (this.settingsService.user.role !== 'department') {
      this.departmentService.getDepartment().then(res => {
        if (!res) return;
        this.depList = res['result'].departmentList;
      });
    }
  }
  getTypeList() {
    this.projectService.getProjectType().then(res => {
      if (!res) return;
      this.projectTypeList = res['result'].types.filter(function (item) {
        return item.projectTypeName.indexOf('基地') > 0;
      });
    });
  }
  selectProjectType(type: number) {
    if (!type) return;
    this.getProjectListInput = {
      type: type,
      pageIndex: 1,
      pageSize: 20,
    };
    this.projectList = [];
    this.projectService.getProjectList(this.getProjectListInput).then(res => {
      if (!res) return;
      this.projectList = res['result'].projectList;
      this.projectList.forEach(function(item) {
        item.createTime = new Date(item.createTime).toLocaleDateString();
      });
    });
  }
  loadMoreProject() {
    if (this.moreProjectLoadingEnd) return;
    this.moreProjectLoading = true;
    this.getProjectListInput.pageIndex++;
    this.projectService.getProjectList(this.getProjectListInput).then(res => {
      this.moreProjectLoading = false;
      if (!res) return;
      if (res['result'].projectList.length < this.getProjectListInput.pageSize)
        this.moreProjectLoadingEnd = true;
      this.projectList = [...this.projectList, ...res['result'].projectList];
      this.projectList.forEach(function(item) {
        item.createTime = new Date(item.createTime).toLocaleDateString();
      });
    });
  }

  fileUpload(res) {
    if (
      res.type === 'success' &&
      res.file.response.result &&
      res.file.response.result.length > 0
    ) {
      const fileIds = [];
      res.file.response.result.forEach(item => {
        fileIds.push(item.id);
        this.fileList.forEach(f => {
          if (!f.fileId) {
            f.url = item.fileUrl;
            f.thumbUrl = '';
            f.uid = item.id;
            f.fileId = item.id;
          }
        });
        if (this.id) {
          this.fileService.insertProjectFile({
            projectId: this.id,
            fileId: item.id,
          });
        }
      });

      this.fileIdList = this.fileIdList.concat(fileIds);
    }
  }

  fileRemove = (file: UploadFile) => {
    if (file.fileId) {
      if (this.id) {
        this.fileService.deleteProjectFile({
          projectId: this.id,
          fileId: file.fileId,
        });
      } else {
        this.fileService.deleteFile(file.fileId);
        this.fileIdList = this.fileIdList.filter(function(item) {
          return item !== file.fileId;
        });
      }
    }
    return true;
  };

  getProjectDetail() {
    if (!this.id) return;
    this.projectService.getProjectDetail(this.id).then(res => {
      if (!res) return;
      if (!res.success) return;
      if (!res.result) return;
      this.project = res.result;

      if (res.result.startTime && res.result.endTime)
        res.result.rangeTime = [res.result.startTime, res.result.endTime];

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

    if (this.id) {
      input.id = this.id;
      this.projectService.updateSocialService(input).then(res => {
        this.submitting = false;
        if (!res) return;
        this.msgSrv.success(`提交成功`);
        this.reuseTabService.clear();
        this.router.navigateByUrl('/archives/SocialService');
      });
    } else {
      input.fileIdList = this.fileIdList;
      this.projectService.insertSocialService(input).then(res => {
        this.submitting = false;
        if (!res) return;
        this.msgSrv.success(`提交成功`);
        this.reuseTabService.clear();
        this.router.navigateByUrl('/archives/SocialService');
      });
    }
  }
}
