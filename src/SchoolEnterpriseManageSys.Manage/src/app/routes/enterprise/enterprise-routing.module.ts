import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EnterpriseListComponent } from './list/list.component';
import { EnterpriseDetailComponent } from './detail/detail.component';
import { EnterpriseEditComponent } from './edit/edit.component';

const routes: Routes = [

  { path: 'list', component: EnterpriseListComponent, data: { title: '企业查询' } },
  { path: 'detail', component: EnterpriseDetailComponent, data: { title: '企业详情' } },
  { path: 'edit', component: EnterpriseEditComponent, data: { title: '企业信息' } }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EnterpriseRoutingModule { }
