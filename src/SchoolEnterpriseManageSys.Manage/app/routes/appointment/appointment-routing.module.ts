import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppointmentListComponent } from './list/list.component';
import { AppointmentEditComponent } from './edit/edit.component';

const routes: Routes = [

  { path: 'list', component: AppointmentListComponent },
  { path: 'edit', component: AppointmentEditComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AppointmentRoutingModule { }
