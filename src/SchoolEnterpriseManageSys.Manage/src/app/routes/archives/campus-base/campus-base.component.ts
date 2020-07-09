import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ModalHelper, SettingsService } from '@delon/theme';
import { ProjectService } from '../../../../providers/projectService';
import { NzModalRef, NzModalService, NzMessageService } from 'ng-zorro-antd';
import { XlsxService } from '@delon/abc';

@Component({
  selector: 'archives-campus-base',
  templateUrl: './campus-base.component.html',
  providers: [ProjectService],
})
export class ArchivesCampusBaseComponent implements OnInit {
  constructor(
    private projectService: ProjectService,
    private nzModal: NzModalService,
    private modalHelper: ModalHelper,
    public msg: NzMessageService,
    public router: Router,
    public settings: SettingsService,
    private xlsx: XlsxService,
  ) { }

  result: any = { totalCount: 0, projectList: [] };
  input: any = { keyword: '', pageIndex: 1, pageSize: 10, type: 4 }; //
  confirmModal: NzModalRef;
  tableLoading = false;

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
      this.router.navigate(['/archives/CampusBase/edit', { id: dep.id }]);
  }

  add() {
    this.router.navigateByUrl('/archives/CampusBase/edit');
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
        '依托专业',
        '单位名称',
        '联系人',
        '建立时间',
        '结束时间',
        '申报情况',
      ],
    ];
    this.result.projectList.forEach(item => {
      const target = [
        item.number,
        item.departmentName,
        item.science,
        item.enterpriseName,
        item.principal,
        this.getDate(item.startTime),
        this.getDate(item.endTime) +
        (item.overdueShow ? '[' + item.overdueShow + ']' : ''),
        item.reportLevelText,
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
        '校内共建基地_****学院校企合作管理系统_' +
        this.getDate(new Date()) +
        '.xlsx',
    });
  }

  beAssociatedEdit(project: any) {
    const projectTypeUrlList = {
      8: '/archives/SocialService/edit',
      16: '/archives/OrderTraining/edit',
      32: '/archives/CoAuthoredBookOrCourse/edit',
      64: '/archives/TeachingResearchFund/edit',
      128: '/archives/AcademicAchievement/edit',
    };
    const url = projectTypeUrlList[project.type.toString()];
    if (!project.id || !url) return;
    this.router.navigate([url, { id: project.id }]);
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
