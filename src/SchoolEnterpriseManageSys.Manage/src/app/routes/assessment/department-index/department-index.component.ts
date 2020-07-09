import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ModalHelper, SettingsService } from '@delon/theme';
import { NzModalRef, NzModalService, NzMessageService } from 'ng-zorro-antd';
import { AssessmentService } from 'providers/assessmentService';

@Component({
  selector: 'assessment-department-index',
  templateUrl: './department-index.component.html',
  providers: [AssessmentService],
})
export class AssessmentDepartmentIndexComponent implements OnInit {
  id: string = '';
  expertRatingScoreTotal: number = 0;
  selfEvaluationScoreTotal: number = 0;
  standardScoreTotal: number = 0;

  constructor(
    private assessmentService: AssessmentService,
    private nzModal: NzModalService,
    private modalHelper: ModalHelper,
    public msg: NzMessageService,
    public router: Router,
    public settings: SettingsService,
    private activatedRoute: ActivatedRoute,
  ) { }

  result: any = { assessmentDepartmentIndexOutputs: [] };

  confirmModal: NzModalRef;
  tableLoading = false;
  expand = false;

  ngOnInit() {
    this.id = this.activatedRoute.snapshot.paramMap.get('id');
    this.getResult();
  }

  getResult() {
    if (!this.id) return;
    this.tableLoading = true;
    this.assessmentService
      .getAssessmentDepartmentIndexList({ assessmentDepartmentId: this.id })
      .then(res => {
        this.tableLoading = false;
        if (!res) return;
        this.result = res.result;
        if (this.result.assessmentDepartmentIndexOutputs) {
          this.result.assessmentDepartmentIndexOutputs.forEach(item => {
            this.expertRatingScoreTotal += item.expertRatingScore;
            this.selfEvaluationScoreTotal += item.selfEvaluationScore;
            this.standardScoreTotal += item.standardScore;
          });
        }
      });
  }

  department(assessmentIndex: any) {
    this.router.navigate([
      '/assessment/department-edit',
      {
        id: assessmentIndex.id || '',
        dep: assessmentIndex.assessmentDepartmentId,
        index: assessmentIndex.assessmentIndexId,
      },
    ]);
  }
  expert(assessmentIndex: any) {
    this.router.navigate([
      '/assessment/expert-edit',
      {
        id: assessmentIndex.id || '',
        dep: assessmentIndex.assessmentDepartmentId,
        index: assessmentIndex.assessmentIndexId,
      },
    ]);
  }

  detail(assessmentIndex: any) {
    this.router.navigate([
      '/assessment/department-detail',
      {
        id: assessmentIndex.id || '',
        dep: assessmentIndex.assessmentDepartmentId,
        index: assessmentIndex.assessmentIndexId,
      },
    ]);
  }
}
