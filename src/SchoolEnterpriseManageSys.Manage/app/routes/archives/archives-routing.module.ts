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

  { path: 'explain', component: ArchivesExplainComponent },
  { path: 'OffCampusBase', component: ArchivesOffCampusBaseComponent },
  { path: 'CampusBase', component: ArchivesCampusBaseComponent },
  { path: 'SocialService', component: ArchivesSocialServiceComponent },
  { path: 'OrderTraining', component: ArchivesOrderTrainingComponent },
  { path: 'CoAuthoredBookOrCourse', component: ArchivesCoAuthoredBookOrCourseComponent },
  { path: 'TeachingResearchFund', component: ArchivesTeachingResearchFundComponent },
  { path: 'AcademicAchievement', component: ArchivesAcademicAchievementComponent },
  { path: 'JointlyEstablishedProfession', component: ArchivesJointlyEstablishedProfessionComponent },
  { path: 'summary', component: ArchivesSummaryComponent },
  { path: 'edit', component: ArchivesAcademicAchievementEditComponent },
  { path: 'edit', component: ArchivesCampusBaseEditComponent },
  { path: 'edit', component: ArchivesCoAuthoredBookOrCourseEditComponent },
  { path: 'edit', component: ArchivesJointlyEstablishedProfessionEditComponent },
  { path: 'edit', component: ArchivesOffCampusBaseEditComponent },
  { path: 'edit', component: ArchivesOrderTrainingEditComponent },
  { path: 'edit', component: ArchivesSocialServiceEditComponent },
  { path: 'edit', component: ArchivesTeachingResearchFundEditComponent },
  { path: 'import', component: ArchivesCampusBaseImportComponent },
  { path: 'import', component: ArchivesAcademicAchievementImportComponent },
  { path: 'import', component: ArchivesCoAuthoredBookOrCourseImportComponent },
  { path: 'import', component: ArchivesTeachingResearchFundImportComponent },
  { path: 'import', component: ArchivesOffCampusBaseImportComponent },
  { path: 'import', component: ArchivesOrderTrainingImportComponent },
  { path: 'import', component: ArchivesSocialServiceImportComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ArchivesRoutingModule { }
