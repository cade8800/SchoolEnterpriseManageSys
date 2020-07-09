import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ModalHelper, SettingsService } from '@delon/theme';
import { ProjectService } from '../../../../providers/projectService';
import { NzModalRef, NzModalService, NzMessageService } from 'ng-zorro-antd';
import { XlsxService } from '@delon/abc';

@Component({
  selector: 'archives-teaching-research-fund',
  templateUrl: './teaching-research-fund.component.html',
  providers: [ProjectService],
})
export class ArchivesTeachingResearchFundComponent implements OnInit {
  constructor(
    private projectService: ProjectService,
    private nzModal: NzModalService,
    private modalHelper: ModalHelper,
    public msg: NzMessageService,
    public router: Router,
    public settings: SettingsService,
    private xlsx: XlsxService,
  ) {}

  result: any = { totalCount: 0, projectList: [] };
  input: any = { keyword: '', pageIndex: 1, pageSize: 10, type: 64 }; //
  confirmModal: NzModalRef;
  tableLoading = false;
  expand = false;

  ngOnInit() {
    this.getResult();
  }

  getResult() {
    this.tableLoading = true;
    this.projectService.getProjectList(this.input).then(res => {
      this.tableLoading = false;
      if (!res) return;
      this.result = res.result;
      this.result.projectList.forEach(item => {
        item.expand = false;
      });
    });
  }

  getResultPaging(pageIndex: number) {
    if (!pageIndex) return;
    this.input.pageIndex = pageIndex;
    this.getResult();
  }

  search() {
    this.input.pageIndex = 1;
    this.getResult();
  }

  edit(dep: any) {
    if (dep.id)
      // this.router.navigateByUrl('/archives/TeachingResearchFund/edit/' + dep.id);
      this.router.navigate([
        '/archives/TeachingResearchFund/edit',
        { id: dep.id },
      ]);
  }

  add() {
    this.router.navigateByUrl('/archives/TeachingResearchFund/edit');
  }
  pageSizeChange(pageSize: number) {
    if (!pageSize) return;
    this.input.pageSize = pageSize;
    this.getResult();
  }

  getDate(date: any) {
    date = new Date(date);
    return (
      date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate()
    );
  }

  exportData() {
    const targetList = [
      [
        '序号',
        '系别',
        '基金名称',
        '基金款项',
        '依托单位',
        '建立时间',
        '结束时间',
      ],
    ];
    this.result.projectList.forEach(item => {
      const target = [
        item.number,
        item.departmentName,
        item.projectName,
        item.amount,
        item.enterpriseName,
        this.getDate(item.startTime),
        this.getDate(item.endTime),
      ];
      targetList.push(target);
    });
    this.xlsx.export({
      sheets: [
        {
          data: targetList,
          name: 'Sheet1',
        },
      ],
      filename:
        '教学研基金_****学院校企合作管理系统_' +
        this.getDate(new Date()) +
        '.xlsx',
    });
  }
  delete(id: string) {
    if (!id) return;
    this.projectService.deleteProject(id).then(res => {
      if (res && res.success) {
        this.msg.success('删除成功');
        this.getResult();
      }
    });
  }
}
