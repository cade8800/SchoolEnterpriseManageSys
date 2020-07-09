import { NzMessageService, NzModalRef } from 'ng-zorro-antd';
import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { DepartmentService } from '../../../../../providers/departmentService';

@Component({
  selector: 'app-extras-department-edit',
  templateUrl: './edit.component.html',
  providers: [DepartmentService],
})
export class ExtrasDepartmentEditComponent implements OnInit {
  param: any;

  constructor(
    private modal: NzModalRef,
    public msgSrv: NzMessageService,
    private departmentService: DepartmentService,
    public http: _HttpClient,
  ) {}

  ngOnInit() {}

  save() {
    if (this.param.id) {
      this.departmentService.updateDepartment(this.param).then(res => {
        if (!res) return;
        this.msgSrv.success('修改系成功');
        this.modal.close(true);
        this.close();
      });
    } else {
      this.departmentService.addDepartment(this.param).then(res => {
        if (!res) return;
        this.param.id = res.result;
        this.msgSrv.success('增加系成功');
        this.modal.close(this.param);
        this.close();
      });
    }
  }

  close() {
    this.modal.destroy();
  }
}
