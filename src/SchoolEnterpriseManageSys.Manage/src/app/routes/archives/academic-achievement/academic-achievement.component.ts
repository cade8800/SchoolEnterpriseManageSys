import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ModalHelper, SettingsService } from '@delon/theme';
import { ProjectService } from '../../../../providers/projectService';
import { NzModalRef, NzModalService, NzMessageService } from 'ng-zorro-antd';
import { XlsxService } from '@delon/abc';

@Component({
  selector: 'archives-academic-achievement',
  templateUrl: './academic-achievement.component.html',
  providers: [ProjectService],
})
export class ArchivesAcademicAchievementComponent implements OnInit {
  constructor(
    private projectService: ProjectService,
    private nzModal: NzModalService,
    private modalHelper: ModalHelper,
    public msg: NzMessageService,
    public router: Router,
    public settings: SettingsService,
    private xlsx: XlsxService
  ) { }

  result: any = { totalCount: 0, projectList: [] };
  input: any = { keyword: '', pageIndex: 1, pageSize: 10, type: 128 };
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
      // this.router.navigateByUrl('/archives/AcademicAchievement/edit/' + dep.id);
      this.router.navigate(['/archives/AcademicAchievement/edit', { "id": dep.id }]);
  }

  add() {
    this.router.navigateByUrl('/archives/AcademicAchievement/edit');
  }



  pageSizeChange(pageSize: number) {
    if (!pageSize) return;
    this.input.pageSize = pageSize;
    this.getResult();
  }

  getDate(date: any) {
    date = new Date(date);
    return (date.getFullYear()) + '-' + (date.getMonth() + 1) + '-' + (date.getDate());
  }

  exportData() {

    let targetList = [['序号', '系部', '作者', '论文名称', '刊物名称', '刊物主办单位', 'ISSN号', 'CN号', '发表时间']];
    this.result.projectList.forEach(item => {
      let target = [

        item.number,
        item.departmentName,
        item.principal,
        item.projectName,
        item.publicationName,
        item.publicationsOrganizer,
        item.issn,
        item.cn,
        this.getDate(item.startTime)
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
      filename: '学术成果_****学院校企合作管理系统_' + this.getDate(new Date()) + '.xlsx'
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
