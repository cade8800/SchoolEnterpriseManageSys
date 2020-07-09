import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { CollectService } from 'providers/collectService';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'collect-department-detail',
  templateUrl: './department-detail.component.html',
  providers: [CollectService]
})
export class CollectDepartmentDetailComponent implements OnInit {
  departmentCollectId = '';
  departmentCollectDetail: any = { cooperation: { fileList: [] }, baseList: [{ fileList: [] }] };
  tableData = [{}];

  constructor(private http: _HttpClient, private collectService: CollectService, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.departmentCollectId = this.activatedRoute.snapshot.paramMap.get('id');
    this.getDepartmentCollectDetail();
  }

  getDepartmentCollectDetail() {
    if (!this.departmentCollectId) return;
    this.collectService.getDepartmentCollectDetail({ departmentCollectId: this.departmentCollectId }).then(res => {
      if (!res) return;
      // console.log(res);
      this.departmentCollectDetail = res.result;
    });
  }

}
