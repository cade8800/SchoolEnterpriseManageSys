import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ModalHelper, SettingsService } from '@delon/theme';
import { NzModalRef, NzModalService, NzMessageService } from 'ng-zorro-antd';
import { AssessmentService } from 'providers/assessmentService';

@Component({
  selector: 'assessment-index',
  templateUrl: './index.component.html',
  providers: [AssessmentService],
})
export class AssessmentIndexComponent implements OnInit {
  constructor(
    private assessmentService: AssessmentService,
    private nzModal: NzModalService,
    private modalHelper: ModalHelper,
    public msg: NzMessageService,
    public router: Router,
    public settings: SettingsService
  ) { }

  result: any = [];
  tableLoading = false;

  ngOnInit() {
    this.getResult();
  }

  getResult() {
    this.tableLoading = true;
    this.assessmentService.getIndexs().then(res => {
      this.tableLoading = false;
      if (!res) return;
      this.result = res['result'];
    });
  }

  delete(assessment: any) {
    if (!assessment) return;
    this.assessmentService.deleteIndex(assessment.id).then(res => {
      if (!res) return;
      this.msg.success('操作成功');
      this.getResult();
    });
  }

  edit(assessment: any) {
    if (assessment.id)
      this.router.navigate(['/assessment/index-edit', { "id": assessment.id }]);
  }

  add() {
    this.router.navigateByUrl('/assessment/index-edit');
  }
}
