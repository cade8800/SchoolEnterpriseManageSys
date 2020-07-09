import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '@shared/shared.module';

import { ExtrasRoutingModule } from './extras-routing.module';

import { HelpCenterComponent } from './helpcenter/helpcenter.component';
import { ExtrasSettingsComponent } from './settings/settings.component';
import { ExtrasPoiComponent } from './poi/poi.component';
import { ExtrasPoiEditComponent } from './poi/edit/edit.component';
import { ExtrasDepartmentComponent } from './department/department.component';
import { ExtrasDepartmentEditComponent } from './department/edit/edit.component';
import { ExtrasUserComponent } from './user/user.component';
import { ExtrasUserInsertComponent } from './user/insert/insert.component';

const COMPONENTS_NOROUNT = [
  ExtrasPoiEditComponent,
  ExtrasDepartmentEditComponent,
  ExtrasUserInsertComponent,
];

@NgModule({
  imports: [SharedModule, ExtrasRoutingModule],
  declarations: [
    HelpCenterComponent,
    ExtrasSettingsComponent,
    ExtrasPoiComponent,
    ExtrasDepartmentComponent,
    ExtrasUserComponent,
    ...COMPONENTS_NOROUNT,
  ],
  entryComponents: COMPONENTS_NOROUNT,
})
export class ExtrasModule {}
