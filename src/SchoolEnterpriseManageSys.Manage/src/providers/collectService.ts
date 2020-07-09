import { Injectable } from '@angular/core';
import { HttpService } from './httpService';
import {
  Collect_Insert,
  Collect_Update,
  Collect_Get,
  Collect_Get_Detail,
  Collect_GetDepartmentCollectDetail,
  Collect_InsertDepartmentCollect,
  Collect_UpdateDepartmentCollectCooperation,
  Collect_GetDepartmentCollectList,
  Collect_DeleteDepartmentCollectBase,
  Collect_UpdateDepartmentCollectBase,
  Collect_InsertDepartmentCollectBase,
  Collect_GetDepartmentCollecdtItemFileList,
} from './constants';

@Injectable()
export class CollectService {
  constructor(public http: HttpService) { }

  insertCollect(paramObj: any) {
    return this.http.postBody(Collect_Insert, paramObj);
  }
  updateCollect(paramObj: any) {
    return this.http.put(Collect_Update, paramObj);
  }
  getCollect(paramObj: any) {
    return this.http.postBody(Collect_Get, paramObj);
  }
  getCollectDetail(id: string) {
    return this.http.get(Collect_Get_Detail, { 'input.id': id });
  }

  getDepartmentCollectDetail(paramObj: any) {
    return this.http.postBody(Collect_GetDepartmentCollectDetail, paramObj);
  }

  insertDepartmentCollect(paramObj: any) {
    return this.http.postBody(Collect_InsertDepartmentCollect, paramObj);
  }

  updateDepartmentCollectCooperation(paramObj: any) {
    return this.http.put(Collect_UpdateDepartmentCollectCooperation, paramObj);
  }

  getDepartmentCollectList(paramObj: any) {
    return this.http.postBody(Collect_GetDepartmentCollectList, paramObj);
  }

  deleteDepartmentCollectBase(id: string) {
    return this.http.delete(Collect_DeleteDepartmentCollectBase + id);
  }

  updateDepartmentCollectBase(paramObj: any) {
    return this.http.put(Collect_UpdateDepartmentCollectBase, paramObj);
  }

  insertDepartmentCollectBase(paramObj: any) {
    return this.http.postBody(Collect_InsertDepartmentCollectBase, paramObj);
  }

  getDepartmentCollecdtItemFileList(id: string) {
    return this.http.get(Collect_GetDepartmentCollecdtItemFileList, { 'id': id });
  }
}
