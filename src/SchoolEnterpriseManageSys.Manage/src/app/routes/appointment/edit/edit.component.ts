import { DepartmentService } from 'providers/departmentService';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { NzMessageService, UploadFile } from 'ng-zorro-antd';
import { _HttpClient, SettingsService } from '@delon/theme';
import { ReuseTabService } from '@delon/abc';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AppointmentService } from 'providers/appointmentService';

@Component({
  selector: 'appointment-edit',
  templateUrl: './edit.component.html',
  providers: [AppointmentService],
})
export class AppointmentEditComponent implements OnInit {
  constructor(
    private router: Router,
    public _location: Location,
    public msgSrv: NzMessageService,
    public http: _HttpClient,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private appointmentService: AppointmentService,
    public settingsService: SettingsService,
    private reuseTabService: ReuseTabService,
  ) {
    this.form = this.formBuilder.group({
      id: '',
      visitsTime: [null, [Validators.required]],
      content: [null, [Validators.required]],
    });
  }

  id = '';
  form: FormGroup;
  submitting = false;
  appointment: any = {};

  ngOnInit(): void {
    this.id = this.activatedRoute.snapshot.paramMap.get('id');
    this.getDetail();
  }


  //#region 文件上传
  // fileUpload(res) {
  //   if (
  //     res.type === 'success' &&
  //     res.file.response.result &&
  //     res.file.response.result.length > 0
  //   ) {
  //     const fileIds = [];
  //     res.file.response.result.forEach(item => {
  //       fileIds.push(item.id);
  //       this.fileList.forEach(f => {
  //         if (!f.fileId) {
  //           f.url = item.fileUrl;
  //           f.thumbUrl = '';
  //           f.uid = item.id;
  //           f.fileId = item.id;
  //         }
  //       });
  //       if (this.id) {
  //         this.fileService.insertProjectFile({
  //           projectId: this.id,
  //           fileId: item.id,
  //         });
  //       }
  //     });

  //     this.fileIdList = this.fileIdList.concat(fileIds);
  //   }
  // }

  // fileRemove = (file: UploadFile) => {
  //   if (file.fileId) {
  //     if (this.id) {
  //       this.fileService.deleteProjectFile({
  //         projectId: this.id,
  //         fileId: file.fileId,
  //       });
  //     } else {
  //       this.fileService.deleteFile(file.fileId);
  //       this.fileIdList = this.fileIdList.filter(function (item) {
  //         return item !== file.fileId;
  //       });
  //     }
  //   }
  //   return true;
  // }
  //#endregion

  getDetail() {
    if (!this.id) return;
    this.appointmentService.getAppointment(this.id).then(res => {
      if (!res) return;
      if (!res.success) return;
      if (!res.result) return;
      this.appointment = res.result;
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

    const input = this.form.value;

    input.id = this.id;
    this.appointmentService.editAppointment(input).then(res => {
      this.submitting = false;
      if (!res) return;
      this.msgSrv.success(`提交成功`);
      this.reuseTabService.clear();
      this.router.navigateByUrl('/appointment/list');
    });


  }
}
