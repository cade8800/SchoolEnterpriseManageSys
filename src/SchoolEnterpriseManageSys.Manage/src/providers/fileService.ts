import { Injectable } from '@angular/core';
import {
  File_Delete,
  File_Delete_Project,
  File_Insert_Project,
  File_Insert_Collect,
  File_Delete_Collect,
  File_Delete_Enterprise,
  File_Insert_Enterprise,
  File_Delete_Assessment,
  File_Insert_Assessment,
} from './constants';
import { HttpService } from './httpService';

@Injectable()
export class FileService {
  constructor(public http: HttpService) { }

  deleteFile(id: string) {
    return this.http.delete(File_Delete + id);
  }



  deleteProjectFile(paramObj: any) {
    return this.http.delete(File_Delete_Project + '?input.projectId=' + paramObj.projectId + '&input.fileId=' + paramObj.fileId);
  }
  insertProjectFile(paramObj: any) {
    return this.http.postBody(File_Insert_Project, paramObj);
  }



  deleteCollectFile(paramObj: any) {
    return this.http.delete(File_Delete_Collect + '?input.collectionItemId=' + paramObj.collectionItemId + '&input.fileId=' + paramObj.fileId);
  }
  insertCollectFile(paramObj: any) {
    return this.http.postBody(File_Insert_Collect, paramObj);
  }



  deleteEnterpriseFile(paramObj: any) {
    return this.http.delete(File_Delete_Enterprise + '?input.enterpriseId=' + paramObj.enterpriseId + '&input.fileId=' + paramObj.fileId);
  }
  insertEnterpriseFile(paramObj: any) {
    return this.http.postBody(File_Insert_Enterprise, paramObj);
  }


  deleteAssessmentFile(paramObj: any) {
    return this.http.delete(File_Delete_Assessment + '?input.departmentIndexId=' + paramObj.departmentIndexId + '&input.fileId=' + paramObj.fileId);
  }
  insertAssessmentFile(paramObj: any) {
    return this.http.postBody(File_Insert_Assessment, paramObj);
  }

}
