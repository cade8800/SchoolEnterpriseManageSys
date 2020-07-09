import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AssessmentIndexComponent } from './index/index.component';
import { AssessmentIndexEditComponent } from './index-edit/index-edit.component';
import { AssessmentListComponent } from './list/list.component';
import { AssessmentEditComponent } from './edit/edit.component';
import { AssessmentDepartmentComponent } from './department/department.component';
import { AssessmentDepartmentIndexComponent } from './department-index/department-index.component';
import { AssessmentDepartmentEditComponent } from './department-edit/department-edit.component';
import { AssessmentExpertEditComponent } from './expert-edit/expert-edit.component';
import { AssessmentDepartmentDetailComponent } from './department-detail/department-detail.component';

const routes: Routes = [

  { path: 'index', component: AssessmentIndexComponent },
  { path: 'IndexEdit', component: AssessmentIndexEditComponent },
  { path: 'list', component: AssessmentListComponent },
  { path: 'edit', component: AssessmentEditComponent },
  { path: 'department', component: AssessmentDepartmentComponent },
  { path: 'DepartmentIndex', component: AssessmentDepartmentIndexComponent },
  { path: 'DepartmentEdit', component: AssessmentDepartmentEditComponent },
  { path: 'ExpertEdit', component: AssessmentExpertEditComponent },
  { path: 'DepartmentDetail', component: AssessmentDepartmentDetailComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AssessmentRoutingModule { }
