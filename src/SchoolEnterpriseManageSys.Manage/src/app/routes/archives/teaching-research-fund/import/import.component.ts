import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { XlsxService, ReuseTabService } from '@delon/abc';
import { ProjectService } from 'providers/projectService';
import { NzMessageService } from 'ng-zorro-antd';
import { Router } from '@angular/router';

@Component({
  selector: 'archives-teaching-research-fund-import',
  templateUrl: './import.component.html',
  providers: [ProjectService],
})
export class ArchivesTeachingResearchFundImportComponent implements OnInit {
  targetList: any = [];
  reportLevel: any = { 校: 2, 省: 4, 部: 8 };
  loading = false;

  constructor(
    private projectService: ProjectService,
    public msgSrv: NzMessageService,
    private reuseTabService: ReuseTabService,
    private router: Router,
    private xlsx: XlsxService,
  ) {}

  ngOnInit() {}

  change(e: Event) {
    const file = (e.target as HTMLInputElement).files[0];
    if (!file) return;
    this.targetList = [];
    this.xlsx.import(file).then(res => {
      let data = res['Sheet1'];
      if (!data) return;
      data = data.slice(1);

      console.log(data);
      data.forEach(item => {
        const target = {
          departmentName: item[0],
          projectName: item[1],
          amount: isNaN(item[2]) ? 0 : item[2],
          enterpriseName: item[3],
          startTime:
            isNaN(item[4]) && !isNaN(Date.parse(item[4]))
              ? new Date(item[4])
              : '',
          endTime:
            isNaN(item[5]) && !isNaN(Date.parse(item[5]))
              ? new Date(item[5])
              : '',
        };
        this.targetList = [...this.targetList, target];
      });
      console.log(this.targetList);
    });
  }

  download() {
    const data = [
      ['系别', '基金名称', '基金款项', '依托单位', '建立时间', '结束时间'],
    ];
    this.xlsx.export({
      sheets: [
        {
          data: data,
          name: 'Sheet1',
        },
      ],
      filename: '教学研基金_上传模板.xlsx',
    });
  }

  del(item: any) {
    this.targetList = this.targetList.filter(function(i) {
      return i !== item;
    });
  }
  upload() {
    console.log(this.targetList);
    if (!this.targetList || this.targetList.length < 1) return;
    this.loading = true;
    this.projectService
      .importTeachingResearchFund(this.targetList)
      .then(res => {
        this.loading = false;
        if (!res) return;
        this.msgSrv.success(`导入成功`);
        this.reuseTabService.clear();
        this.router.navigateByUrl('/archives/TeachingResearchFund');
      });
  }
}
