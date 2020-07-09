import { Component, OnInit } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd';
import { _HttpClient } from '@delon/theme';
import { ProjectService } from 'providers/projectService';
import { DepartmentService } from 'providers/departmentService';
import G2 from '@antv/g2';

@Component({
  selector: 'archives-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.less'],
  providers: [ProjectService, DepartmentService],
})
export class ArchivesSummaryComponent implements OnInit {
  loading = true;
  summary: any = { rankingList: [], timelineList: [] };
  departmentList = [];

  dateRange = [];
  departmentId = '';
  input = {
    departmentId: this.departmentId || '',
    startTime: '',
    endTime: '',
  };

  selectSummaryChart;

  constructor(
    private http: _HttpClient,
    public msg: NzMessageService,
    private projectService: ProjectService,
    private departmentService: DepartmentService,
  ) { }

  ngOnInit() {
    this.initSelectSummaryChart();
    this.getSummary();
    this.getDepList();
    this.getSelectSummery();
  }

  getSummary() {
    this.projectService.getSummary().then(res => {
      this.loading = false;
      if (!res) return;
      this.summary = res['result'];
    });
  }

  getDepList() {
    this.departmentService.getDepartment().then(res => {
      if (!res) return;
      this.departmentList = res['result'].departmentList;
    });
  }

  getSelectSummery() {
    this.projectService.selectSummary(this.input).then(res => {
      if (!res) return;
      this.selectSummaryChart.source(res['result']);
      this.selectSummaryChart.interval().position('x*y').color('x');
      this.selectSummaryChart.render();
    });
  }

  departmentChange(ev) {
    this.input.departmentId = ev || '';
    this.getSelectSummery();
  }

  datePickerChange(ev) {
    this.input.startTime = ev[0] || '';
    this.input.endTime = ev[1] || '';
    this.getSelectSummery();
  }

  initSelectSummaryChart() {
    this.selectSummaryChart = new G2.Chart({
      container: 'selectSummaryChart',
      forceFit: true
    });
  }

}
