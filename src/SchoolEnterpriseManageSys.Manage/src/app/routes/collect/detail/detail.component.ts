import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { CollectService } from 'providers/collectService';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'collect-detail',
  templateUrl: './detail.component.html',
  providers: [CollectService]
})
export class CollectDetailComponent implements OnInit {
  collectId = '';
  departmentCollectList: any = [];
  tableLoading = false;

  constructor(private http: _HttpClient,
    private activatedRoute: ActivatedRoute,
    private collectService: CollectService,
    private router: Router, ) { }

  ngOnInit() {
    this.collectId = this.activatedRoute.snapshot.paramMap.get('id');
    this.getDepartmentCollectList();
  }

  getDepartmentCollectList() {
    if (!this.collectId) return;
    this.tableLoading = true;
    this.collectService.getDepartmentCollectList({ collectId: this.collectId }).then(res => {
      this.tableLoading = false;
      if (!res) return;
      this.departmentCollectList = res.result.departmentCollectList;
    });
  }

  look(depCollect: any) {
    if (depCollect.departmentCollectId)
      this.router.navigateByUrl('/collect/department-detail/' + depCollect.departmentCollectId);
  }

}
