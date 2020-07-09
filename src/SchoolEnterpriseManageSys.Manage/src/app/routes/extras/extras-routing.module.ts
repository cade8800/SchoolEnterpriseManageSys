import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HelpCenterComponent } from './helpcenter/helpcenter.component';
import { ExtrasSettingsComponent } from './settings/settings.component';
import { ExtrasPoiComponent } from './poi/poi.component';
import { ExtrasDepartmentComponent } from './department/department.component';
import { ExtrasUserComponent } from './user/user.component';

const routes: Routes = [
  { path: 'helpcenter', component: HelpCenterComponent },
  {
    path: 'settings',
    component: ExtrasSettingsComponent,
    data: { title: '个人中心' },
  },
  { path: 'poi', component: ExtrasPoiComponent },
  {
    path: 'department',
    component: ExtrasDepartmentComponent,
    data: { title: '系管理' },
  },
  { path: 'user', component: ExtrasUserComponent, data: { title: '用户管理' } },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ExtrasRoutingModule {}
