import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ModalHelper, SettingsService } from '@delon/theme';
import { NzModalRef, NzModalService, NzMessageService } from 'ng-zorro-antd';
import { AppointmentService } from 'providers/appointmentService';
import { DepartmentService } from 'providers/departmentService';

@Component({
  selector: 'appointment-list',
  templateUrl: './list.component.html',
  providers: [AppointmentService, DepartmentService]
})
export class AppointmentListComponent implements OnInit {

  constructor(
    private appointmentService: AppointmentService,
    private nzModal: NzModalService,
    private modalHelper: ModalHelper,
    public msg: NzMessageService,
    public router: Router,
    public settings: SettingsService,
    private departmentService: DepartmentService
  ) { }

  result: any = { totalCount: 0, appointmentList: [] };
  input: any = { keyword: '', pageIndex: 1, pageSize: 10 };
  confirmModal: NzModalRef;
  tableLoading = false;
  departmentList: any = [];

  ngOnInit() {
    this.getResult();
    this.getDepList();
  }

  getResult() {
    this.tableLoading = true;
    this.appointmentService.getEnterpriseAppointment(this.input).then(res => {
      this.tableLoading = false;
      if (!res) return;
      this.result = res.result;
      // console.log(this.result);

    });
  }

  getDepList() {
    if (this.settings.user.role != 'administrator') return;
    this.departmentService.getDepartment().then(res => {
      if (!res) return;
      this.departmentList = res['result'].departmentList;
    });
  }

  getResultPaging(pageIndex: number) {
    if (!pageIndex) return;
    this.input.pageIndex = pageIndex;
    this.getResult();
  }

  confirm(appointment: any) {
    if (!appointment.id) return;
    this.appointmentService.confirmAppointment(appointment.id).then(res => {
      if (!res) return;
      this.msg.success('确认成功');
      this.getResult();
    });
  }

  edit(appointment: any) {
    if (appointment.id)
      this.router.navigate(['/appointment/edit', { "id": appointment.id }]);
  }

  add() {
    this.router.navigateByUrl('/appointment/edit');
  }

  consult(appointment: any) {
    if (this.settings.user.role == 'enterprise') {
      this.router.navigateByUrl('/consult/index');
    } else {
      this.router.navigate(['/consult/index', { "id": appointment.enterpriseId }]);
    }
  }

  delete(appointment: any) {
    if (!appointment.id) return;
    this.appointmentService.deleteAppointment(appointment.id).then(res => {
      if (!res) return;
      this.msg.success('取消成功');
      this.getResult();
    });
  }

  send(appointmentId: string, departmentId: string) {
    if (!appointmentId || !departmentId) return;
    this.appointmentService.sendToDepartment({ appointmentId: appointmentId, departmentId: departmentId }).then(res => {
      if (!res) return;
      this.msg.success('转发至系部成功');
      this.getResult();
    });

  }

}
