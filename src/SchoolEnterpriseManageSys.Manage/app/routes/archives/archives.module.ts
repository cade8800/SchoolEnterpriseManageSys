import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { ArchivesRoutingModule } from './archives-routing.module';
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

const COMPONENTS = [
  ArchivesExplainComponent,
  ArchivesOffCampusBaseComponent,
  ArchivesCampusBaseComponent,
  ArchivesSocialServiceComponent,
  ArchivesOrderTrainingComponent,
  ArchivesCoAuthoredBookOrCourseComponent,
  ArchivesTeachingResearchFundComponent,
  ArchivesAcademicAchievementComponent,
  ArchivesJointlyEstablishedProfessionComponent,
  ArchivesSummaryComponent,
  ArchivesAcademicAchievementEditComponent,
  ArchivesCampusBaseEditComponent,
  ArchivesCoAuthoredBookOrCourseEditComponent,
  ArchivesJointlyEstablishedProfessionEditComponent,
  ArchivesOffCampusBaseEditComponent,
  ArchivesOrderTrainingEditComponent,
  ArchivesSocialServiceEditComponent,
  ArchivesTeachingResearchFundEditComponent,
  ArchivesCampusBaseImportComponent,
  ArchivesAcademicAchievementImportComponent,
  ArchivesCoAuthoredBookOrCourseImportComponent,
  ArchivesTeachingResearchFundImportComponent,
  ArchivesOffCampusBaseImportComponent,
  ArchivesOrderTrainingImportComponent,
  ArchivesSocialServiceImportComponent];
const COMPONENTS_NOROUNT = [];

@NgModule({
  imports: [
    SharedModule,
    ArchivesRoutingModule
  ],
  declarations: [
    ...COMPONENTS,
    ...COMPONENTS_NOROUNT
  ],
  entryComponents: COMPONENTS_NOROUNT
})
export class ArchivesModule { }
