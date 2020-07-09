import { Injectable } from '@angular/core';
import {
  Department_Delete,
  Department_Get,
  Department_Update,
  Department_Add,
} from './constants';
import { HttpService } from './httpService';

@Injectable()
export class DepartmentService {
  constructor(public http: HttpService) {}

  getDepartment() {
    return this.http.get(Department_Get, {});
  }
  updateDepartment(paramObj: any) {
    return this.http.put(Department_Update, paramObj);
  }
  deleteDepartment(id: string) {
    return this.http.delete(Department_Delete + id);
  }
  addDepartment(paramObj: any) {
    return this.http.postBody(Department_Add, paramObj);
  }
}
