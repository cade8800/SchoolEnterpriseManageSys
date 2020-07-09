import { Injectable } from '@angular/core';
import { Enterprise_Get, Enterprise_GetDetail, Enterprise_Edit } from './constants';
import { HttpService } from './httpService';

@Injectable()
export class EnterpriseService {
  constructor(public http: HttpService) { }

  getEnterprise(paramObj: any) {
    return this.http.postBody(Enterprise_Get, paramObj);
  }

  getEnterpriseDetail(id: string) {
    return this.http.postBody(Enterprise_GetDetail, { id: id });
  }

  editEnterprise(paramObj: any) {
    return this.http.postBody(Enterprise_Edit, paramObj);
  }
}
