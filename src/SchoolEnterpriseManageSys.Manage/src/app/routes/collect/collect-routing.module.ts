import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CollectListComponent } from './list/list.component';
import { CollectEditComponent } from './edit/edit.component';
import { CollectDetailComponent } from './detail/detail.component';
import { CollectDepartmentComponent } from './department/department.component';
import { CollectDepartmentDetailComponent } from './department-detail/department-detail.component';

const routes: Routes = [
  {
    path: 'list',
    component: CollectListComponent,
    data: { title: '数据采集' },
  },
  {
    path: 'edit/:id',
    component: CollectEditComponent,
    data: { title: '编辑 - 数据采集' },
  },
  {
    path: 'detail/:id',
    component: CollectDetailComponent,
    data: { title: '数据采集详情' },
  },
  {
    path: 'department/:id',
    component: CollectDepartmentComponent,
    data: { title: '系数据采集' },
  },
  {
    path: 'department-detail/:id', component: CollectDepartmentDetailComponent,
    data: { title: '系数据采集详情' },
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CollectRoutingModule { }
