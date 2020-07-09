import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CollectListComponent } from './list/list.component';
import { CollectEditComponent } from './edit/edit.component';
import { CollectDetailComponent } from './detail/detail.component';
import { CollectDepartmentComponent } from './department/department.component';
import { CollectDepartmentDetailComponent } from './department-detail/department-detail.component';

const routes: Routes = [

  { path: 'list', component: CollectListComponent },
  { path: 'edit', component: CollectEditComponent },
  { path: 'detail', component: CollectDetailComponent },
  { path: 'department', component: CollectDepartmentComponent },
  { path: 'DepartmentDetail', component: CollectDepartmentDetailComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CollectRoutingModule { }
