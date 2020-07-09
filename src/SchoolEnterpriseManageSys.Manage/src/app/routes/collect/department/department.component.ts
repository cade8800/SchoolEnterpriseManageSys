import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { File_Upload } from 'providers/constants';
import { NzMessageService, UploadFile } from 'ng-zorro-antd';
import { CollectService } from 'providers/collectService';
import { ActivatedRoute, Router } from '@angular/router';
import { FileService } from 'providers/fileService';
import { ReuseTabService, XlsxService } from '@delon/abc';
import { ModalHelper } from '@delon/theme';
import { CollectDepartmentUploadComponent } from './upload/upload.component';


@Component({
  selector: 'collect-department',
  templateUrl: './department.component.html',
  providers: [CollectService, FileService],
})
export class CollectDepartmentComponent implements OnInit {
  form: FormGroup;
  collectId = '';
  collectDepDetail: any = {
    baseList: {},
    collectionId: '',
    cooperation: {},
    id: '',
  };
  // baseFileList: any = [];
  // cooperationFileList: any = [];
  uploadUrl = File_Upload;
  collectDetail: any = { description: '' };
  editIndex = -1;
  tableLoading = false;
  editObj = {};
  isPioneerBase = { '是': true, '否': false };

  constructor(
    private fb: FormBuilder,
    private collectService: CollectService,
    private activatedRoute: ActivatedRoute,
    private fileService: FileService,
    private router: Router,
    public msgSrv: NzMessageService,
    private reuseTabService: ReuseTabService,
    private xlsx: XlsxService,
    private modalHelper: ModalHelper,
  ) { }

  ngOnInit() {
    this.collectId = this.activatedRoute.snapshot.paramMap.get('id');
    this.form = this.fb.group({
      id: '',
      collectionId: '',
      remark: '',

      // baseId: '',
      // baseRemark: '',
      // builtUpTime: [null],
      // baseName: '',
      // departmentNumber: '',
      // departmentName: '',
      // scienceNumber: '',
      // scienceName: '',
      // isPioneerBase: [null],
      // address: '',
      // accepteStudentAverage: 0,
      // yearAccepteStudentTotal: 0,

      cooperationId: '',
      // cooperationRemark: '',
      // alumniAssociationTotal: 0,
      // overseasAlumniAssociationCount: 0,
      // domesticAlumniAssociationCount: 0,
      // cooperationAgencyTotal: 0,
      academicAgencyCount: 0,
      enterpriseCount: 0,
      localGovernmentCount: 0,

      items: this.fb.array([]),
    });

    this.getCollectDetail();
    this.getCollectDepDetail();
  }

  createBase(): FormGroup {
    return this.fb.group({
      id: '',
      baseRemark: '',
      builtUpTime: new Date(),
      baseName: '',
      departmentNumber: '',
      departmentName: '',
      scienceNumber: '',
      scienceName: '',
      isPioneerBase: false,
      address: '',
      accepteStudentAverage: 0,
      yearAccepteStudentTotal: 0,
    });
  }

  get items() {
    return this.form.controls.items as FormArray;
  }

  add() {
    // console.log(this.createBase());

    // field.patchValue(i);

    // this.items.push(this.createBase());

    // console.log(this.collectDepDetail);

    if (this.collectDepDetail.id) {
      this.tableLoading = true;
      const input = {
        collectionDepartmentId: this.collectDepDetail.id,
        id: '',
        baseRemark: '',
        builtUpTime: new Date(),
        baseName: '',
        departmentNumber: '',
        departmentName: '',
        scienceNumber: '',
        scienceName: '',
        isPioneerBase: false,
        address: '',
        accepteStudentAverage: 0,
        yearAccepteStudentTotal: 0,
      };
      this.collectService.insertDepartmentCollectBase(input).then(res => {
        this.tableLoading = false;

        if (!res) return;
        if (!res.result) return;
        input.id = res.result;


        const field = this.createBase();
        field.patchValue(input);
        this.items.push(field);
        this.edit(this.items.length - 1);
      });
    } else {
      this.items.push(this.createBase());
      this.edit(this.items.length - 1);
    }

    return false;
  }

  del(index: number, targetBase: any) {
    // console.log(targetBase);
    if (targetBase.id) {
      this.tableLoading = true;
      this.collectService
        .deleteDepartmentCollectBase(targetBase.id)
        .then(res => {
          this.tableLoading = false;
          if (!res) return;
          this.items.removeAt(index);
        });
    } else {
      this.items.removeAt(index);
    }
  }

  edit(index: number) {
    if (this.editIndex !== -1 && this.editObj) {
      this.items.at(this.editIndex).patchValue(this.editObj);
    }
    this.editObj = { ...this.items.at(index).value };
    this.editIndex = index;
  }

  save(index: number, targetBase: any) {
    // console.log(targetBase);
    if (targetBase.id) {
      this.tableLoading = true;
      this.collectService.updateDepartmentCollectBase(targetBase).then(res => {
        this.tableLoading = false;
        if (!res) return;
        this.items.at(index).markAsDirty();
        if (this.items.at(index).invalid) return;
        this.editIndex = -1;
      });
    } else {
      this.items.at(index).markAsDirty();
      if (this.items.at(index).invalid) return;
      this.editIndex = -1;
    }
  }

  cancel(index: number) {
    if (!this.items.at(index).value.key) {
      this.del(index, {});
    } else {
      this.items.at(index).patchValue(this.editObj);
    }
    this.editIndex = -1;
  }

  getCollectDetail() {
    this.collectService.getCollectDetail(this.collectId).then(res => {
      if (!res) return;
      this.collectDetail = res['result'];
    });
  }

  getCollectDepDetail() {
    this.collectService
      .getDepartmentCollectDetail({ collectId: this.collectId })
      .then(res => {
        if (!res) return;
        this.collectDepDetail = res['result'];
        // console.log(this.collectDepDetail);

        if (this.collectDepDetail.id) {
          const depCollect = {
            id: this.collectDepDetail.id,
            collectionId: this.collectDepDetail.collectionId,
            remark: this.collectDepDetail.remark,

            // baseId: this.collectDepDetail.base.id,
            // baseRemark: this.collectDepDetail.base.remark,
            // builtUpTime: this.collectDepDetail.base.builtUpTime,
            // baseName: this.collectDepDetail.base.baseName,
            // departmentNumber: this.collectDepDetail.base.departmentNumber,
            // departmentName: this.collectDepDetail.base.departmentName,
            // scienceNumber: this.collectDepDetail.base.scienceNumber,
            // scienceName: this.collectDepDetail.base.scienceName,
            // address: this.collectDepDetail.base.address,
            // accepteStudentAverage: this.collectDepDetail.base.accepteStudentAverage,
            // yearAccepteStudentTotal: this.collectDepDetail.base.yearAccepteStudentTotal,

            cooperationId: this.collectDepDetail.cooperation.id,
            cooperationRemark: this.collectDepDetail.cooperation.remark,
            alumniAssociationTotal: this.collectDepDetail.cooperation
              .alumniAssociationTotal,
            overseasAlumniAssociationCount: this.collectDepDetail.cooperation
              .overseasAlumniAssociationCount,
            domesticAlumniAssociationCount: this.collectDepDetail.cooperation
              .domesticAlumniAssociationCount,
            cooperationAgencyTotal: this.collectDepDetail.cooperation
              .cooperationAgencyTotal,
            academicAgencyCount: this.collectDepDetail.cooperation
              .academicAgencyCount,
            enterpriseCount: this.collectDepDetail.cooperation.enterpriseCount,
            localGovernmentCount: this.collectDepDetail.cooperation
              .localGovernmentCount,
          };
          this.form.patchValue(depCollect);

          this.collectDepDetail.baseList.forEach(i => {
            const field = this.createBase();
            field.patchValue(i);
            this.items.push(field);
          });

          // this.baseFileList = this.collectDepDetail.base.fileList;
          // this.cooperationFileList = this.collectDepDetail.cooperation.fileList;
        }
      });
  }

  //#region upload file

  // cooperationFileUpload(res) {
  //   if (
  //     res.type === 'success' &&
  //     res.file.response.result &&
  //     res.file.response.result.length > 0
  //   ) {
  //     res.file.response.result.forEach(item => {

  //       this.cooperationFileList.forEach(f => {
  //         if (!f.fileId) {
  //           f.url = item.fileUrl;
  //           f.thumbUrl = '';
  //           f.uid = item.id;
  //           f.fileId = item.id;
  //         }
  //       });
  //       if (this.collectDepDetail.cooperation && this.collectDepDetail.cooperation.id) {
  //         this.fileService.insertCollectFile({
  //           collectionItemId: this.collectDepDetail.cooperation.id,
  //           fileId: item.id,
  //         });
  //       }
  //     });
  //   }
  // }

  // cooperationFileRemove = (file: UploadFile) => {
  //   if (file.fileId) {
  //     if (this.collectDepDetail.cooperation && this.collectDepDetail.cooperation.id) {
  //       this.fileService.deleteCollectFile({
  //         collectionItemId: this.collectDepDetail.cooperation.id,
  //         fileId: file.fileId,
  //       });
  //     } else {
  //       this.fileService.deleteFile(file.fileId);
  //     }
  //   }
  //   return true;
  // };

  // baseFileUpload(res) {
  //   if (
  //     res.type === 'success' &&
  //     res.file.response.result &&
  //     res.file.response.result.length > 0
  //   ) {
  //     res.file.response.result.forEach(item => {

  //       this.baseFileList.forEach(f => {
  //         if (!f.fileId) {
  //           f.url = item.fileUrl;
  //           f.thumbUrl = '';
  //           f.uid = item.id;
  //           f.fileId = item.id;
  //         }
  //       });
  //       if (this.collectDepDetail.base && this.collectDepDetail.base.id) {
  //         this.fileService.insertCollectFile({
  //           collectionItemId: this.collectDepDetail.base.id,
  //           fileId: item.id,
  //         });
  //       }
  //     });
  //   }
  // }

  // baseFileRemove = (file: UploadFile) => {
  //   if (file.fileId) {
  //     if (this.collectDepDetail.base && this.collectDepDetail.base.id) {
  //       this.fileService.deleteCollectFile({
  //         collectionItemId: this.collectDepDetail.base.id,
  //         fileId: file.fileId,
  //       });
  //     } else {
  //       this.fileService.deleteFile(file.fileId);
  //     }
  //   }
  //   return true;
  // };

  //#endregion

  _submitForm() {
    for (const i in this.form.controls) {
      this.form.controls[i].markAsDirty();
      this.form.controls[i].updateValueAndValidity();
    }
    if (this.form.invalid) return;

    const formVal = this.form.value;
    // console.log(formVal);

    if (this.collectDepDetail.id) {
      const input = {
        id: formVal.cooperationId,
        remark: formVal.cooperationRemark,
        alumniAssociationTotal: formVal.alumniAssociationTotal,
        overseasAlumniAssociationCount: formVal.overseasAlumniAssociationCount,
        domesticAlumniAssociationCount: formVal.domesticAlumniAssociationCount,
        cooperationAgencyTotal: formVal.cooperationAgencyTotal,
        academicAgencyCount: formVal.academicAgencyCount,
        enterpriseCount: formVal.enterpriseCount,
        localGovernmentCount: formVal.localGovernmentCount,
      };
      // console.log(input);
      this.collectService
        .updateDepartmentCollectCooperation(input)
        .then(res => {
          if (!res) return;
          this.msgSrv.success(`提交成功`);
          this.reuseTabService.clear();
          this.router.navigateByUrl('/collect/list');
        });
    } else {
      const input = {
        id: formVal.id,
        collectionId: this.collectId,
        remark: formVal.remark,
        baseList: formVal.items,
        cooperation: {
          id: formVal.cooperationId,
          remark: formVal.cooperationRemark,
          alumniAssociationTotal: formVal.alumniAssociationTotal,
          overseasAlumniAssociationCount:
            formVal.overseasAlumniAssociationCount,
          domesticAlumniAssociationCount:
            formVal.domesticAlumniAssociationCount,
          cooperationAgencyTotal: formVal.cooperationAgencyTotal,
          academicAgencyCount: formVal.academicAgencyCount,
          enterpriseCount: formVal.enterpriseCount,
          localGovernmentCount: formVal.localGovernmentCount,
        },
      };
      this.collectService.insertDepartmentCollect(input).then(res => {
        if (!res) return;
        this.msgSrv.success(`提交成功`);
        this.reuseTabService.clear();
        this.router.navigateByUrl('/collect/list');
      });
    }
  }

  download() {
    const data = [
      [
        '基地名称', '建立时间', '院系(单位)号', '院系(单位)名称', '面向校内专业', '校内专业代码', '是否是创业实习基地', '地址', '每次可接纳学生数(人)', '当年接纳学生总数(人次)'
      ],
    ];
    this.xlsx.export({
      sheets: [
        {
          data: data,
          name: 'Sheet1',
        },
      ],
      filename: '实习实训基地批量导入模板.xlsx',
    });
  }

  change(e: Event) {
    const file = (e.target as HTMLInputElement).files[0];
    if (!file) return;

    this.xlsx.import(file).then(res => {
      let data = res['Sheet1'];
      if (!data) return;
      data = data.slice(1);

      // console.log(data);
      data.forEach(item => {
        const target = {
          collectionDepartmentId: this.collectDepDetail.id,
          id: '',
          baseRemark: '',
          baseName: item[0],
          builtUpTime: isNaN(item[1]) && !isNaN(Date.parse(item[1])) ? new Date(item[1]) : '',
          departmentNumber: item[2],
          departmentName: item[3],
          scienceName: item[4],
          scienceNumber: item[5],
          isPioneerBase: this.isPioneerBase[item[6]] || false,
          address: item[7],
          accepteStudentAverage: isNaN(item[8]) ? 0 : item[8],
          yearAccepteStudentTotal: isNaN(item[9]) ? 0 : item[9],
        };

        if (this.collectDepDetail.id) {

          this.tableLoading = true;

          this.collectService.insertDepartmentCollectBase(target).then(response => {
            this.tableLoading = false;

            if (!response) return;
            if (!response.result) return;
            target.id = response.result;

            const targetBase = this.createBase();
            targetBase.patchValue(target);
            this.items.push(targetBase);
          });


        } else {
          const targetBase = this.createBase();
          targetBase.patchValue(target);
          this.items.push(targetBase);
        }

      });

    });
  }

  uploadFile(id: string, ) {
    // console.log(id);
    if (!id) return;
    this.modalHelper
      .static(CollectDepartmentUploadComponent, { id: id })
      .subscribe(param => {
        if (!param) return;

      });
  }

}
