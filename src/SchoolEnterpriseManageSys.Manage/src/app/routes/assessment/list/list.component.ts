import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ModalHelper, SettingsService } from '@delon/theme';
import { NzModalRef, NzModalService, NzMessageService } from 'ng-zorro-antd';
import { AssessmentService } from 'providers/assessmentService';

@Component({
  selector: 'assessment-list',
  templateUrl: './list.component.html',
  providers: [AssessmentService],
})
export class AssessmentListComponent implements OnInit {
  constructor(
    private assessmentService: AssessmentService,
    private nzModal: NzModalService,
    private modalHelper: ModalHelper,
    public msg: NzMessageService,
    public router: Router,
    public settings: SettingsService,
  ) {}

  result: any = { totalCount: 0, assessmentList: [] };
  input: any = { pageIndex: 1, pageSize: 10 };

  confirmModal: NzModalRef;
  tableLoading = false;
  expand = false;

  ngOnInit() {
    this.getResult();
  }

  getResult() {
    this.tableLoading = true;
    this.assessmentService.getAssessments(this.input).then(res => {
      this.tableLoading = false;
      if (!res) return;
      this.result = res.result;
    });
  }

  getResultPaging(pageIndex: number) {
    if (!pageIndex) return;
    this.input.pageIndex = pageIndex;
    this.getResult();
  }

  department(assessment: any) {
    if (assessment.id)
      this.router.navigate([
        '/assessment/department-index',
        { id: assessment.assessmentDepartmentId },
      ]);
  }

  edit(assessment: any) {
    if (assessment.id)
      this.router.navigate(['/assessment/edit', { id: assessment.id }]);
  }

  detail(assessment: any) {
    if (assessment.id)
      this.router.navigate(['/assessment/department', { id: assessment.id }]);
  }

  add() {
    this.router.navigateByUrl('/assessment/edit');
  }
}
