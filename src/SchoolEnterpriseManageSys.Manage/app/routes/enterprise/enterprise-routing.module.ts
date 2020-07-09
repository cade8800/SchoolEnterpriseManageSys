import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EnterpriseListComponent } from './list/list.component';
import { EnterpriseDetailComponent } from './detail/detail.component';
import { EnterpriseEditComponent } from './edit/edit.component';

const routes: Routes = [

  { path: 'list', component: EnterpriseListComponent },
  { path: 'detail', component: EnterpriseDetailComponent },
  { path: 'edit', component: EnterpriseEditComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EnterpriseRoutingModule { }
