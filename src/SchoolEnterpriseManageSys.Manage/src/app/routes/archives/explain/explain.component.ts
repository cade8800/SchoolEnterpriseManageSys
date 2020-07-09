import { Component, OnInit, ViewChild } from '@angular/core';
import { ProjectService } from '../../../../providers/projectService';
import { NzMessageService } from 'ng-zorro-antd';

@Component({
  selector: 'archives-explain',
  templateUrl: './explain.component.html',
  providers: [ProjectService],
})
export class ArchivesExplainComponent implements OnInit {
  typeList = [];

  constructor(
    private projectService: ProjectService,
    public msgSrv: NzMessageService,
  ) { }

  ngOnInit() {
    this.projectService.getProjectType().then(res => {
      if (!res) return;
      if (!res['success']) return;
      this.typeList = res['result'].types.filter(function (item) {
        return item.projectTypeName !== '共建专业';
      });
      // console.log(this.typeList);
    });
  }

  update(type: any) {
    this.projectService.updateProjectType(type).then(res => {
      if (!res) return;
      this.msgSrv.success('修改成功');
    });
  }
}
