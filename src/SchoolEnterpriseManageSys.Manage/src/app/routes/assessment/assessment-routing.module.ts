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
  {
    path: 'index',
    component: AssessmentIndexComponent,
    data: { title: '考核指标' },
  },
  {
    path: 'index-edit',
    component: AssessmentIndexEditComponent,
    data: { title: '编辑 - 考核指标' },
  },
  { path: 'list', component: AssessmentListComponent },
  {
    path: 'edit',
    component: AssessmentEditComponent,
    data: { title: '编辑 - 考核' },
  },
  {
    path: 'department',
    component: AssessmentDepartmentComponent,
    data: { title: '考核 - 系部列表' },
  },
  {
    path: 'department-index',
    component: AssessmentDepartmentIndexComponent,
    data: { title: '考核 - 系 - 指标' },
  },
  {
    path: 'department-edit',
    component: AssessmentDepartmentEditComponent,
    data: { title: '考核 - 系部自评' },
  },
  {
    path: 'expert-edit',
    component: AssessmentExpertEditComponent,
    data: { title: '考核 - 专家评分' },
  },
  {
    path: 'department-detail',
    component: AssessmentDepartmentDetailComponent,
    data: { title: '考核 - 系 - 指标 - 详细' },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AssessmentRoutingModule {}
