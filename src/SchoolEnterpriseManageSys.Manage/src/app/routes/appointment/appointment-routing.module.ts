import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppointmentListComponent } from './list/list.component';
import { AppointmentEditComponent } from './edit/edit.component';

const routes: Routes = [

  { path: 'list', component: AppointmentListComponent, data: { title: '企业预约' } },
  { path: 'edit', component: AppointmentEditComponent, data: { title: '编辑 - 企业预约' } }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AppointmentRoutingModule { }
