import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ConsultIndexComponent } from './index/index.component';
import { ConsultListComponent } from './list/list.component';

const routes: Routes = [

  { path: 'index', component: ConsultIndexComponent },
  { path: 'list', component: ConsultListComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ConsultRoutingModule { }
