import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { XlsxService, ReuseTabService } from '@delon/abc';
import { ProjectService } from 'providers/projectService';
import { NzMessageService } from 'ng-zorro-antd';
import { Router } from '@angular/router';

@Component({
  selector: 'archives-social-service-import',
  templateUrl: './import.component.html',
  providers: [ProjectService],
})
export class ArchivesSocialServiceImportComponent implements OnInit {
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
          principal: item[1],
          enterpriseName: item[2],
          startTime:
            isNaN(item[3]) && !isNaN(Date.parse(item[3]))
              ? new Date(item[3])
              : '',
          amount: isNaN(item[4]) ? 0 : item[4],
        };
        this.targetList = [...this.targetList, target];
      });
      console.log(this.targetList);
    });
  }

  download() {
    const data = [['系别', '负责人', '服务单位', '签订时间', '服务费用']];
    this.xlsx.export({
      sheets: [
        {
          data: data,
          name: 'Sheet1',
        },
      ],
      filename: '社会服务_上传模板.xlsx',
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
    this.projectService.importSocialService(this.targetList).then(res => {
      this.loading = false;
      if (!res) return;
      this.msgSrv.success(`导入成功`);
      this.reuseTabService.clear();
      this.router.navigateByUrl('/archives/SocialService');
    });
  }
}
