import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalHelper } from '@delon/theme';
import { DepartmentService } from '../../../../providers/departmentService';

import { NzModalRef, NzModalService } from 'ng-zorro-antd';
import { ExtrasDepartmentEditComponent } from './edit/edit.component';

@Component({
  selector: 'department',
  templateUrl: './department.component.html',
  providers: [DepartmentService],
})
export class ExtrasDepartmentComponent implements OnInit {
  constructor(
    private departmentService: DepartmentService,
    private nzModal: NzModalService,
    private modalHelper: ModalHelper,
  ) {}

  departmentList: any = [];
  confirmModal: NzModalRef;

  ngOnInit() {
    this.getDepartment();
  }

  getDepartment() {
    this.departmentService.getDepartment().then(res => {
      const response: any = res;
      this.departmentList = response.result.departmentList;
    });
  }

  deleteDepartment(dep: any) {
    if (!dep) return;
    if (!dep.id) return;

    this.confirmModal = this.nzModal.confirm({
      nzTitle: '确定删除“' + dep.name + '”吗？',
      nzOnOk: () => {
        this.departmentService.deleteDepartment(dep.id).then(res => {
          this.departmentList = this.departmentList.filter(function(item: any) {
            return item.id !== dep.id;
          });
        });
      },
    });
  }

  edit(dep: any) {
    this.modalHelper
      .static(ExtrasDepartmentEditComponent, { param: dep })
      .subscribe(() => {});
  }

  add() {
    this.modalHelper
      .static(ExtrasDepartmentEditComponent, { param: { id: '', name: '' } })
      .subscribe(param => {
        param.createTime = new Date();
        // this.departmentList.push(param);
        // console.log(this.departmentList);
        this.departmentList = [...this.departmentList, param];
      });
  }
}
