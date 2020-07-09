import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { EnterpriseService } from 'providers/enterpriseService';
import { Router } from '@angular/router';

@Component({
  selector: 'enterprise-list',
  templateUrl: './list.component.html',
  providers: [EnterpriseService]
})
export class EnterpriseListComponent implements OnInit {
  result: any = { totalCount: 0, enterprises: [] };
  input: any = { keyword: '', pageIndex: 1, pageSize: 10, isWithFileInfo: true };
  tableLoading = false;
  enterpriseList: any = [];

  constructor(private http: _HttpClient,
    private enterpriseService: EnterpriseService,
    private router: Router) { }

  ngOnInit() {
    this.getResult();
  }

  getResult() {
    this.tableLoading = true;
    this.enterpriseService.getEnterprise(this.input).then(res => {
      this.tableLoading = false;
      if (!res) return;
      this.result = res.result;
      this.result.enterprises.forEach(item => {
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

  edit(enterprise: any) {
    console.log(enterprise);
    if (enterprise.id)
      this.router.navigate(['/enterprise/edit/', { "id": enterprise.id }]);
  }

}
