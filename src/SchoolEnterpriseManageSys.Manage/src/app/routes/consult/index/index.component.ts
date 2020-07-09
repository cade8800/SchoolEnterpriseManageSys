import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { AdvisoryService } from 'providers/advisoryService';
import { ActivatedRoute } from '@angular/router';
import { NzMessageService } from 'ng-zorro-antd';

@Component({
  selector: 'consult-index',
  templateUrl: './index.component.html',
  providers: [AdvisoryService]
})
export class ConsultIndexComponent implements OnInit {
  input: any = {
    content: "",
    recipientUserId: ""
  };
  advisoryList: any = [];


  data = [
    {
      title: 'Ant Design Title 1'
    },
    {
      title: 'Ant Design Title 2'
    },
    {
      title: 'Ant Design Title 3'
    },
    {
      title: 'Ant Design Title 4'
    }
  ];

  constructor(
    private http: _HttpClient,
    private advisoryService: AdvisoryService,
    private activatedRoute: ActivatedRoute,
    public msgSrv: NzMessageService,
  ) {
    this.input.recipientUserId = this.activatedRoute.params["value"].id;
  }

  ngOnInit() {
    this.getAdvisory();
  }


  getAdvisory() {
    this.advisoryService.getAdvisory(this.input.recipientUserId).then(res => {
      if (!res) return;
      this.advisoryList = res.result.advisoryList;
    });
  }

  send() {
    if (!this.input.content) {
      this.msgSrv.error('请输入咨询内容');
      return;
    }
    this.advisoryService.insertAdvisory(this.input).then(res => {
      if (!res) return;
      this.msgSrv.success(`提交成功`);
      this.input.content = '';
      this.getAdvisory();
    });

  }

}
