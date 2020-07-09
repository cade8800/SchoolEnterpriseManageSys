import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CmsEmptyComponent } from './empty/empty.component';
import { CmsListComponent } from './list/list.component';
import { CmsCurdComponent } from './curd/curd.component';
import { CmsViewComponent } from './view/view.component';
import { CmsEditComponent } from './edit/edit.component';

const routes: Routes = [
  { path: 'empty', component: CmsEmptyComponent },
  { path: 'list', component: CmsListComponent },
  { path: 'curd', component: CmsCurdComponent },
  { path: 'view', component: CmsViewComponent },
  { path: 'edit', component: CmsEditComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CmsRoutingModule {}
