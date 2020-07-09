import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ArchivesExplainComponent } from './explain/explain.component';
import { ArchivesOffCampusBaseComponent } from './off-campus-base/off-campus-base.component';
import { ArchivesCampusBaseComponent } from './campus-base/campus-base.component';
import { ArchivesSocialServiceComponent } from './social-service/social-service.component';
import { ArchivesOrderTrainingComponent } from './order-training/order-training.component';
import { ArchivesCoAuthoredBookOrCourseComponent } from './co-authored-book-or-course/co-authored-book-or-course.component';
import { ArchivesTeachingResearchFundComponent } from './teaching-research-fund/teaching-research-fund.component';
import { ArchivesAcademicAchievementComponent } from './academic-achievement/academic-achievement.component';
import { ArchivesJointlyEstablishedProfessionComponent } from './jointly-established-profession/jointly-established-profession.component';
import { ArchivesSummaryComponent } from './summary/summary.component';
import { ArchivesAcademicAchievementEditComponent } from './academic-achievement/edit/edit.component';
import { ArchivesCampusBaseEditComponent } from './campus-base/edit/edit.component';
import { ArchivesCoAuthoredBookOrCourseEditComponent } from './co-authored-book-or-course/edit/edit.component';
import { ArchivesJointlyEstablishedProfessionEditComponent } from './jointly-established-profession/edit/edit.component';
import { ArchivesOffCampusBaseEditComponent } from './off-campus-base/edit/edit.component';
import { ArchivesOrderTrainingEditComponent } from './order-training/edit/edit.component';
import { ArchivesSocialServiceEditComponent } from './social-service/edit/edit.component';
import { ArchivesTeachingResearchFundEditComponent } from './teaching-research-fund/edit/edit.component';
import { ArchivesCampusBaseImportComponent } from './campus-base/import/import.component';
import { ArchivesAcademicAchievementImportComponent } from './academic-achievement/import/import.component';
import { ArchivesCoAuthoredBookOrCourseImportComponent } from './co-authored-book-or-course/import/import.component';
import { ArchivesTeachingResearchFundImportComponent } from './teaching-research-fund/import/import.component';
import { ArchivesOffCampusBaseImportComponent } from './off-campus-base/import/import.component';
import { ArchivesOrderTrainingImportComponent } from './order-training/import/import.component';
import { ArchivesSocialServiceImportComponent } from './social-service/import/import.component';

const routes: Routes = [
  {
    path: 'explain',
    component: ArchivesExplainComponent,
    data: { title: '档案说明' },
  },
  {
    path: 'OffCampusBase',
    component: ArchivesOffCampusBaseComponent,
    data: { title: '校外实践基地' },
  },
  {
    path: 'OffCampusBase/import',
    component: ArchivesOffCampusBaseImportComponent,
    data: { title: '导入 - 校外实践基地' },
  },
  {
    path: 'OffCampusBase/edit',
    component: ArchivesOffCampusBaseEditComponent,
    data: { title: '编辑 - 校外实践基地' },
  },
  {
    path: 'CampusBase',
    component: ArchivesCampusBaseComponent,
    data: { title: '校内共建基地' },
  },
  {
    path: 'CampusBase/edit',
    component: ArchivesCampusBaseEditComponent,
    data: { title: '编辑 - 校内共建基地' },
  },
  {
    path: 'CampusBase/import',
    component: ArchivesCampusBaseImportComponent,
    data: { title: '导入 - 校内共建基地' },
  },
  {
    path: 'SocialService',
    component: ArchivesSocialServiceComponent,
    data: { title: '社会服务' },
  },
  {
    path: 'SocialService/edit',
    component: ArchivesSocialServiceEditComponent,
    data: { title: '编辑 - 社会服务' },
  },
  {
    path: 'SocialService/import',
    component: ArchivesSocialServiceImportComponent,
    data: { title: '导入 - 社会服务' },
  },
  {
    path: 'OrderTraining',
    component: ArchivesOrderTrainingComponent,
    data: { title: '订单培养' },
  },
  {
    path: 'OrderTraining/edit',
    component: ArchivesOrderTrainingEditComponent,
    data: { title: '编辑 - 订单培养' },
  },
  {
    path: 'OrderTraining/import',
    component: ArchivesOrderTrainingImportComponent,
    data: { title: '导入 - 订单培养' },
  },
  {
    path: 'CoAuthoredBookOrCourse',
    component: ArchivesCoAuthoredBookOrCourseComponent,
    data: { title: '共编教材/课程' },
  },
  {
    path: 'CoAuthoredBookOrCourse/edit',
    component: ArchivesCoAuthoredBookOrCourseEditComponent,
    data: { title: '编辑 - 共编教材/课程' },
  },
  {
    path: 'CoAuthoredBookOrCourse/import',
    component: ArchivesCoAuthoredBookOrCourseImportComponent,
    data: { title: '导入 - 共编教材/课程' },
  },
  {
    path: 'TeachingResearchFund',
    component: ArchivesTeachingResearchFundComponent,
    data: { title: '教学研基金' },
  },
  {
    path: 'TeachingResearchFund/edit',
    component: ArchivesTeachingResearchFundEditComponent,
    data: { title: '编辑 - 教学研基金' },
  },
  {
    path: 'TeachingResearchFund/import',
    component: ArchivesTeachingResearchFundImportComponent,
    data: { title: '导入 - 教学研基金' },
  },
  {
    path: 'AcademicAchievement',
    component: ArchivesAcademicAchievementComponent,
    data: { title: '学术成果' },
  },
  {
    path: 'AcademicAchievement/edit',
    component: ArchivesAcademicAchievementEditComponent,
    data: { title: '编辑 - 学术成果' },
  },
  {
    path: 'AcademicAchievement/import',
    component: ArchivesAcademicAchievementImportComponent,
    data: { title: '导入 - 学术成果' },
  },
  {
    path: 'JointlyEstablishedProfession',
    component: ArchivesJointlyEstablishedProfessionComponent,
    data: { title: '文件管理' },
  },
  {
    path: 'JointlyEstablishedProfession/edit',
    component: ArchivesJointlyEstablishedProfessionEditComponent,
    data: { title: '文件管理' },
  },
  {
    path: 'summary',
    component: ArchivesSummaryComponent,
    data: { title: '汇总统计' },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ArchivesRoutingModule {}
