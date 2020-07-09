import { Injectable } from '@angular/core';
import { HttpService } from './httpService';
import {
  Assessment_GetIndexs,
  Assessment_DeleteIndex,
  Assessment_GetIndex,
  Assessment_EditIndex,
  Assessment_GetAssessments,
  Assessment_EditAssessment,
  Assessment_GetAssessment,
  Assessment_EditAssessmentDepartment,
  Assessment_GetAssessmentDepartmentList,
  Assessment_GetAssessmentDepartmentIndexList,
  Assessment_SelfEvaluation,
  Assessment_ExpertRating,
  Assessment_GetAssessmentDepartmentIndex,
  Assessment_GetAssessmentDepartmentProjects,
} from './constants';

@Injectable()
export class AssessmentService {
  constructor(public http: HttpService) { }

  getIndexs() {
    return this.http.get(Assessment_GetIndexs, {});
  }

  deleteIndex(id: string) {
    return this.http.delete(Assessment_DeleteIndex + id);
  }

  getIndex(id: string) {
    return this.http.postBody(Assessment_GetIndex, { id: id });
  }

  editIndex(paramObj: any) {
    return this.http.postBody(Assessment_EditIndex, paramObj);
  }

  getAssessments(paramObj: any) {
    return this.http.postBody(Assessment_GetAssessments, paramObj);
  }

  editAssessment(paramObj: any) {
    return this.http.postBody(Assessment_EditAssessment, paramObj);
  }

  getAssessment(id: string) {
    return this.http.get(Assessment_GetAssessment, { 'input.id': id });
  }

  editAssessmentDepartment(paramObj: any) {
    return this.http.postBody(Assessment_EditAssessmentDepartment, paramObj);
  }

  getAssessmentDepartmentList(paramObj: any) {
    return this.http.postBody(Assessment_GetAssessmentDepartmentList, paramObj);
  }
  getAssessmentDepartmentIndexList(paramObj: any) {
    return this.http.postBody(
      Assessment_GetAssessmentDepartmentIndexList,
      paramObj,
    );
  }

  selfEvaluation(paramObj: any) {
    return this.http.postBody(Assessment_SelfEvaluation, paramObj);
  }
  expertRating(paramObj: any) {
    return this.http.postBody(Assessment_ExpertRating, paramObj);
  }

  getAssessmentDepartmentIndex(id: string) {
    return this.http.postBody(Assessment_GetAssessmentDepartmentIndex, {
      id: id,
    });
  }

  getAssessmentDepartmentProjects(assessmentDepartmentId: string) {
    return this.http.postBody(Assessment_GetAssessmentDepartmentProjects, {
      assessmentDepartmentId: assessmentDepartmentId,
    });
  }
}
