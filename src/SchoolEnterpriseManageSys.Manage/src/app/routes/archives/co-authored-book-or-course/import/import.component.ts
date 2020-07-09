import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { XlsxService, ReuseTabService } from '@delon/abc';
import { ProjectService } from 'providers/projectService';
import { NzMessageService } from 'ng-zorro-antd';
import { Router } from '@angular/router';

@Component({
  selector: 'archives-co-authored-book-or-course-import',
  templateUrl: './import.component.html',
  providers: [ProjectService],
})
export class ArchivesCoAuthoredBookOrCourseImportComponent implements OnInit {
  targetList: any = [];
  coAuthoredType: any = { 教材: 2, 课程: 4 };
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
          coAuthoredType: this.coAuthoredType[item[1]],
          coAuthoredTypeText: item[1],
          projectName: item[2],
          principal: item[3],
          science: item[4],
          enterpriseName: item[5],
          startTime:
            isNaN(item[6]) && !isNaN(Date.parse(item[6]))
              ? new Date(item[6])
              : '',
          isbn: item[7],
        };
        this.targetList = [...this.targetList, target];
      });
      console.log(this.targetList);
    });
  }

  download() {
    const data = [
      [
        '系别',
        '类型',
        '名称',
        '负责老师',
        '适用专业',
        '依托企业',
        '出版日期',
        'ISBN号',
      ],
    ];
    this.xlsx.export({
      sheets: [
        {
          data: data,
          name: 'Sheet1',
        },
      ],
      filename: '共编教材课程_上传模板.xlsx',
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
      .importCoAuthoredBookOrCourse(this.targetList)
      .then(res => {
        this.loading = false;
        if (!res) return;
        this.msgSrv.success(`导入成功`);
        this.reuseTabService.clear();
        this.router.navigateByUrl('/archives/CoAuthoredBookOrCourse');
      });
  }
}
