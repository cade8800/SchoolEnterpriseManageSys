import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { AssessmentRoutingModule } from './assessment-routing.module';
import { AssessmentIndexComponent } from './index/index.component';
import { AssessmentIndexEditComponent } from './index-edit/index-edit.component';
import { AssessmentListComponent } from './list/list.component';
import { AssessmentEditComponent } from './edit/edit.component';
import { AssessmentDepartmentComponent } from './department/department.component';
import { AssessmentDepartmentIndexComponent } from './department-index/department-index.component';
import { AssessmentDepartmentEditComponent } from './department-edit/department-edit.component';
import { AssessmentExpertEditComponent } from './expert-edit/expert-edit.component';
import { AssessmentDepartmentDetailComponent } from './department-detail/department-detail.component';

const COMPONENTS = [
  AssessmentIndexComponent,
  AssessmentIndexEditComponent,
  AssessmentListComponent,
  AssessmentEditComponent,
  AssessmentDepartmentComponent,
  AssessmentDepartmentIndexComponent,
  AssessmentDepartmentEditComponent,
  AssessmentExpertEditComponent,
  AssessmentDepartmentDetailComponent];
const COMPONENTS_NOROUNT = [];

@NgModule({
  imports: [
    SharedModule,
    AssessmentRoutingModule
  ],
  declarations: [
    ...COMPONENTS,
    ...COMPONENTS_NOROUNT
  ],
  entryComponents: COMPONENTS_NOROUNT
})
export class AssessmentModule { }
