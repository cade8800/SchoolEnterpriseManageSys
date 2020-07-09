import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { NzMessageService } from 'ng-zorro-antd';
import { _HttpClient, SettingsService } from '@delon/theme';
import { FileService } from 'providers/fileService';
import { AssessmentService } from 'providers/assessmentService';

@Component({
  selector: 'assessment-department-detail',
  templateUrl: './department-detail.component.html',
  providers: [AssessmentService, FileService],
})
export class AssessmentDepartmentDetailComponent implements OnInit {
  constructor(
    public _location: Location,
    public msgSrv: NzMessageService,
    public http: _HttpClient,
    private activatedRoute: ActivatedRoute,
    private assessmentService: AssessmentService,
    public settingsService: SettingsService,
  ) {
    this.id = this.activatedRoute.snapshot.paramMap.get('id');
    this.assessmentDepartmentId = this.activatedRoute.snapshot.paramMap.get('dep');
    this.assessmentIndexId = this.activatedRoute.snapshot.paramMap.get('index');
  }

  id: string;
  assessmentDepartmentId: string;
  assessmentIndexId: string;
  projectList = [{
    type: 2,
    typeText: '校外实践基地',
    fileList: [],
    total: 0
  }, {
    type: 4,
    typeText: '校内共建基地',
    fileList: [],
    total: 0
  }, {
    type: 8,
    typeText: '社会服务',
    fileList: [],
    total: 0
  }, {
    type: 16,
    typeText: '订单培养',
    fileList: [],
    total: 0
  }, {
    type: 32,
    typeText: '共编教材/课程',
    fileList: [],
    total: 0
  }, {
    type: 64,
    typeText: '教学研基金',
    fileList: [],
    total: 0
  }, {
    type: 128,
    typeText: '学术成果',
    fileList: [],
    total: 0
  }];

  index: any = { projectList: [] };

  ngOnInit(): void {
    if (this.id) {
      this.getDetail();
    } else {
      this.getIndexDetail();
    }
  }

  getIndexDetail() {
    if (!this.assessmentIndexId) return;
    this.assessmentService.getIndex(this.assessmentIndexId).then(res => {
      this.index = res.result;
    });
  }

  getDetail() {
    if (!this.id) return;
    this.assessmentService.getAssessmentDepartmentIndex(this.id).then(res => {
      if (!res) return;
      if (!res.success) return;
      if (!res.result) return;
      this.index = res.result;

      this.projectList.forEach(t => {
        const target = this.index.projectList.filter(function (item) {
          return item.type === t.type;
        });
        t.total = target.length;
        target.forEach(element => {
          t.fileList = [...element.fileList, ...t.fileList];
        });
      });
      console.log(this.projectList);




    });
  }
}
