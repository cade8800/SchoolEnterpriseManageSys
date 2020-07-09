import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ModalHelper, SettingsService } from '@delon/theme';
import { NzModalRef, NzModalService, NzMessageService } from 'ng-zorro-antd';
import { CollectService } from 'providers/collectService';

@Component({
  selector: 'collect-list',
  templateUrl: './list.component.html',
  providers: [CollectService],
})
export class CollectListComponent implements OnInit {
  constructor(
    private collectService: CollectService,
    private nzModal: NzModalService,
    private modalHelper: ModalHelper,
    public msg: NzMessageService,
    public router: Router,
    public settings: SettingsService,
  ) {}

  result: any = { totalCount: 0, collectList: [] };
  input: any = { pageIndex: 1, pageSize: 10 };

  confirmModal: NzModalRef;
  tableLoading = false;
  expand = false;

  ngOnInit() {
    this.getResult();
  }

  getResult() {
    this.tableLoading = true;
    this.collectService.getCollect(this.input).then(res => {
      this.tableLoading = false;
      if (!res) return;
      this.result = res.result;
      this.result.collectList.forEach(item => {
        item.expand = false;
      });
    });
  }

  getResultPaging(pageIndex: number) {
    if (!pageIndex) return;
    this.input.pageIndex = pageIndex;
    this.getResult();
  }

  depCollect(collect: any) {
    if (collect.id)
      this.router.navigateByUrl('/collect/department/' + collect.id);
  }

  edit(collect: any) {
    if (collect.id) this.router.navigateByUrl('/collect/edit/' + collect.id);
  }

  detail(collect: any) {
    if (collect.id) this.router.navigateByUrl('/collect/detail/' + collect.id);
  }

  add() {
    this.router.navigateByUrl('/collect/edit/');
  }

  depDetail(collect: any) {
    if (collect.collectDepartmentId)
      this.router.navigateByUrl(
        '/collect/department-detail/' + collect.collectDepartmentId,
      );
  }
}
