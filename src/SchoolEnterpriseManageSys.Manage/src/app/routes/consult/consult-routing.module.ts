import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ConsultIndexComponent } from './index/index.component';
import { ConsultListComponent } from './list/list.component';

const routes: Routes = [

  { path: 'index', component: ConsultIndexComponent, data: { title: '在线咨询' } },
  { path: 'list', component: ConsultListComponent, data: { title: '企业咨询' } }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ConsultRoutingModule { }
