import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalHelper } from '@delon/theme';
import { UserService } from '../../../../providers/userService';

import { NzModalRef, NzModalService, NzMessageService } from 'ng-zorro-antd';
import { ExtrasUserInsertComponent } from './insert/insert.component';

@Component({
  selector: 'app-extras-user',
  templateUrl: './user.component.html',
  providers: [UserService],
})
export class ExtrasUserComponent implements OnInit {
  constructor(
    private userService: UserService,
    private nzModal: NzModalService,
    private modalHelper: ModalHelper,
    public msg: NzMessageService,
  ) {}

  userResult: any = { totalCount: 0, userList: [] };
  input: any = { pageIndex: 1, pageSize: 10 };
  confirmModal: NzModalRef;
  tableLoading = false;

  ngOnInit() {
    this.getUsers();
  }

  getUsers() {
    this.tableLoading = true;
    this.userService.getUsers(this.input).then(res => {
      this.tableLoading = false;
      if (!res) return;
      this.userResult = res.result;
    });
  }

  getUserPaging(pageIndex: number) {
    if (!pageIndex) return;
    this.input.pageIndex = pageIndex;
    this.getUsers();
  }

  updateUserState(user: any) {
    if (!user) return;
    if (!user.id) return;
    const title = user.isDelete
      ? '确定启用用户：' + user.userName + '吗？'
      : '确定禁用用户：' + user.userName + '吗？';
    this.confirmModal = this.nzModal.confirm({
      nzTitle: title,
      nzOnOk: () => {
        this.userService.updateUserState(user.id).then(res => {
          if (!res) return;
          this.userResult.userList.forEach(item => {
            if (item.id === user.id) item.isDelete = !item.isDelete;
          });
          this.msg.success('操作成功');
        });
      },
    });
  }

  resetPassword(userId: string) {
    if (!userId) return;
    this.confirmModal = this.nzModal.confirm({
      nzTitle: '确定需要重置密码吗？',
      nzOnOk: () => {
        this.userService.resetPassword(userId).then(res => {
          if (!res) return;
          this.msg.success('操作成功');
        });
      },
    });
  }

  insertUser(roleType: number) {
    this.modalHelper
      .static(ExtrasUserInsertComponent, { param: roleType })
      .subscribe(param => {
        if (!param) return;
        this.input.pageIndex = 1;
        this.getUsers();
      });
  }

  edit(dep: any) {
    // this.modalHelper
    //   .static(ExtrasdepartmentEditComponent, { param: dep })
    //   .subscribe(() => {});
  }

  add() {
    // this.modalHelper
    //   .static(ExtrasdepartmentEditComponent, { param: { id: '', name: '' } })
    //   .subscribe(param => {
    //     param.createTime = new Date();
    //     // this.departmentList.push(param);
    //     // console.log(this.departmentList);
    //     this.departmentList = [...this.departmentList, param];
    //   });
  }
}
