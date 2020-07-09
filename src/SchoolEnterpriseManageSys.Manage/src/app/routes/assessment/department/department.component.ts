import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ModalHelper, SettingsService } from '@delon/theme';
import { NzModalRef, NzModalService, NzMessageService } from 'ng-zorro-antd';
import { AssessmentService } from 'providers/assessmentService';

@Component({
  selector: 'assessment-department',
  templateUrl: './department.component.html',
  providers: [AssessmentService]
})
export class AssessmentDepartmentComponent implements OnInit {
  id: string = '';

  constructor(
    private assessmentService: AssessmentService,
    private nzModal: NzModalService,
    private modalHelper: ModalHelper,
    public msg: NzMessageService,
    public router: Router,
    public settings: SettingsService,
    private activatedRoute: ActivatedRoute
  ) { }

  result: any = { assessmentDepartmentOutputs: [] };

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
    this.assessmentService.getAssessmentDepartmentList({ assessmentId: this.id }).then(res => {
      this.tableLoading = false;
      if (!res) return;
      this.result = res.result;
    });
  }

  detail(departmentAssessment: any) {
    if (departmentAssessment.id)
      this.router.navigate(['/assessment/department-index', { id: departmentAssessment.id }]);
  }


}
