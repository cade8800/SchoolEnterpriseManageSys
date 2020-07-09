import { Component, OnInit, ViewChild } from '@angular/core';
import { NzModalRef, NzMessageService, UploadFile } from 'ng-zorro-antd';
import { _HttpClient } from '@delon/theme';
import { File_Upload } from 'providers/constants';
import { CollectService } from 'providers/collectService';
import { FileService } from 'providers/fileService';

@Component({
  selector: 'collect-department-upload',
  templateUrl: './upload.component.html',
  providers: [CollectService, FileService]
})
export class CollectDepartmentUploadComponent implements OnInit {
  uploadUrl = File_Upload;
  fileList: any = [];
  id: string;

  constructor(
    private modal: NzModalRef,
    public msgSrv: NzMessageService,
    public http: _HttpClient,
    private collectService: CollectService,
    private fileService: FileService,
  ) { }

  ngOnInit(): void {
    // console.log(this.id);
    this.getFileList();
  }

  getFileList() {
    if (!this.id) return;
    this.collectService.getDepartmentCollecdtItemFileList(this.id).then(res => {
      // console.log(res);
      if (!res) return;
      this.fileList = res['result'] || [];
    });
  }

  fileUpload(res) {
    if (!this.id) return;
    if (
      res.type === 'success' &&
      res.file.response.result &&
      res.file.response.result.length > 0
    ) {
      res.file.response.result.forEach(item => {

        this.fileList.forEach(f => {
          if (!f.fileId) {
            f.url = item.fileUrl;
            f.thumbUrl = '';
            f.uid = item.id;
            f.fileId = item.id;
          }
        });
        this.fileService.insertCollectFile({ collectionItemId: this.id, fileId: item.id, });
      });
    }
  }

  fileRemove = (file: UploadFile) => {
    if (this.id && file.fileId) {
      this.fileService.deleteCollectFile({
        collectionItemId: this.id,
        fileId: file.fileId,
      });
    }
    return true;
  }

}
