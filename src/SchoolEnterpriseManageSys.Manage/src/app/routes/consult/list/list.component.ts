import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ModalHelper } from '@delon/theme';
import { NzModalRef, NzModalService, NzMessageService } from 'ng-zorro-antd';
import { AdvisoryService } from 'providers/advisoryService';


@Component({
  selector: 'consult-list',
  templateUrl: './list.component.html',
  providers: [AdvisoryService],
})
export class ConsultListComponent implements OnInit {
  constructor(
    private advisoryService: AdvisoryService,
    private nzModal: NzModalService,
    private modalHelper: ModalHelper,
    public msg: NzMessageService,
    public router: Router,
  ) { }

  result: any = { totalCount: 0, advisoryList: [] };
  input: any = { pageIndex: 1, pageSize: 10 };

  confirmModal: NzModalRef;
  tableLoading = false;
  expand = false;

  ngOnInit() {
    this.getResult();
  }

  getResult() {
    this.tableLoading = true;
    this.advisoryService.getEnterpriseAdvisory(this.input).then(res => {
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

  edit(advisory: any) {
    if (advisory.initiatorUserId)
      this.router.navigate(['/consult/index', { "id": advisory.initiatorUserId }]);
  }

}
