import { Injectable } from '@angular/core';
import { HttpService } from './httpService';
import { Advisory_Get, Advisory_Insert, Advisory_Get_Enterprise } from './constants';

@Injectable()
export class AdvisoryService {
    constructor(public http: HttpService) { }

    getAdvisory(enterpriseUserId: string) {
        return this.http.postBody(Advisory_Get, { enterpriseUserId: enterpriseUserId });
    }
    insertAdvisory(paramObj: any) {
        return this.http.postBody(Advisory_Insert, paramObj);
    }
    getEnterpriseAdvisory(paramObj: any) {
        return this.http.postBody(Advisory_Get_Enterprise, paramObj);
    }
}