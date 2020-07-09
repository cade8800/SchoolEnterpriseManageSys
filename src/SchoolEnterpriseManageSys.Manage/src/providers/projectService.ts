import { Injectable } from '@angular/core';
import { HttpService } from './httpService';
import {
  Project_UpdateSocialService,
  Project_InsertTeachingResearchFund,
  Project_UpdateTeachingResearchFund,
  Project_Summary,
  Project_ImportCampusBase,
  Project_ImportAcademicAchievement,
  Project_ImportCoAuthoredBookOrCourse,
  Project_ImportOffCampusBase,
  Project_ImportOrderTraining,
  Project_ImportSocialService,
  Project_ImportTeachingResearchFund,
  Project_SelectSummary,
  Project_Delete,
} from './constants';
import {
  Project_Detail,
  Project_UpdateAcademicAchievement,
  Project_InsertCampusBase,
  Project_UpdateCampusBase,
  Project_InsertCoAuthoredBookOrCourse,
  Project_UpdateCoAuthoredBookOrCourse,
  Project_Type_Get,
  Project_Type_Update,
  Project_Type_Detail,
  Project_InsertAcademicAchievement,
  Project_Get_List,
  Project_InsertJointlyEstablishedProfession,
  Project_UpdateJointlyEstablishedProfession,
  Project_InsertOffCampusBase,
  Project_UpdateOffCampusBase,
  Project_InsertOrderTraining,
  Project_UpdateOrderTraining,
  Project_InsertSocialService,
} from './constants';

@Injectable()
export class ProjectService {
  constructor(public http: HttpService) { }

  getProjectType() {
    return this.http.get(Project_Type_Get, {});
  }
  getProjectTypeDetail(type: number) {
    return this.http.post(Project_Type_Detail + type, {});
  }
  updateProjectType(paramObj: any) {
    return this.http.put(Project_Type_Update, paramObj);
  }

  getProjectList(paramObj: any) {
    return this.http.post(Project_Get_List, paramObj);
  }
  getProjectDetail(projectId: string) {
    return this.http.postBody(Project_Detail, { id: projectId });
  }

  deleteProject(projectId: string) {
    return this.http.delete(Project_Delete + projectId);
  }

  insertAcademicAchievement(paramObj: any) {
    return this.http.postBody(Project_InsertAcademicAchievement, paramObj);
  }
  updateAcademicAchievement(paramObj: any) {
    return this.http.put(Project_UpdateAcademicAchievement, paramObj);
  }
  importAcademicAchievement(paramObj: any) {
    return this.http.postBody(Project_ImportAcademicAchievement, paramObj);
  }

  insertCampusBase(paramObj: any) {
    return this.http.postBody(Project_InsertCampusBase, paramObj);
  }
  updateCampusBase(paramObj: any) {
    return this.http.put(Project_UpdateCampusBase, paramObj);
  }
  importCampusBase(paramObj: any) {
    return this.http.postBody(Project_ImportCampusBase, paramObj);
  }

  insertCoAuthoredBookOrCourse(paramObj: any) {
    return this.http.postBody(Project_InsertCoAuthoredBookOrCourse, paramObj);
  }
  updateCoAuthoredBookOrCourse(paramObj: any) {
    return this.http.put(Project_UpdateCoAuthoredBookOrCourse, paramObj);
  }
  importCoAuthoredBookOrCourse(paramObj: any) {
    return this.http.postBody(Project_ImportCoAuthoredBookOrCourse, paramObj);
  }

  insertJointlyEstablishedProfession(paramObj: any) {
    return this.http.postBody(
      Project_InsertJointlyEstablishedProfession,
      paramObj,
    );
  }
  updateJointlyEstablishedProfession(paramObj: any) {
    return this.http.put(Project_UpdateJointlyEstablishedProfession, paramObj);
  }

  insertOffCampusBase(paramObj: any) {
    return this.http.postBody(Project_InsertOffCampusBase, paramObj);
  }
  updateOffCampusBase(paramObj: any) {
    return this.http.put(Project_UpdateOffCampusBase, paramObj);
  }
  importOffCampusBase(paramObj: any) {
    return this.http.postBody(Project_ImportOffCampusBase, paramObj);
  }

  insertOrderTraining(paramObj: any) {
    return this.http.postBody(Project_InsertOrderTraining, paramObj);
  }
  updateOrderTraining(paramObj: any) {
    return this.http.put(Project_UpdateOrderTraining, paramObj);
  }
  importOrderTraining(paramObj: any) {
    return this.http.postBody(Project_ImportOrderTraining, paramObj);
  }

  insertSocialService(paramObj: any) {
    return this.http.postBody(Project_InsertSocialService, paramObj);
  }
  updateSocialService(paramObj: any) {
    return this.http.put(Project_UpdateSocialService, paramObj);
  }
  importSocialService(paramObj: any) {
    return this.http.postBody(Project_ImportSocialService, paramObj);
  }

  insertTeachingResearchFund(paramObj: any) {
    return this.http.postBody(Project_InsertTeachingResearchFund, paramObj);
  }
  updateTeachingResearchFund(paramObj: any) {
    return this.http.put(Project_UpdateTeachingResearchFund, paramObj);
  }
  importTeachingResearchFund(paramObj: any) {
    return this.http.postBody(Project_ImportTeachingResearchFund, paramObj);
  }

  getSummary() {
    return this.http.get(Project_Summary, {});
  }

  selectSummary(paramObj: any) {
    return this.http.postBody(Project_SelectSummary, paramObj);
  }
}
