import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { CmsRoutingModule } from './cms-routing.module';
import { CmsEmptyComponent } from './empty/empty.component';
import { CmsEditComponent } from './edit/edit.component';
import { CmsListComponent } from './list/list.component';
import { CmsViewComponent } from './view/view.component';
import { CmsCurdComponent } from './curd/curd.component';
import { CmsCurdEditComponent } from './curd/edit/edit.component';
import { CmsCurdViewComponent } from './curd/view/view.component';

const COMPONENTS = [
  CmsEmptyComponent,
  CmsListComponent,
  CmsCurdComponent];
const COMPONENTS_NOROUNT = [
  CmsEditComponent,
  CmsViewComponent,
  CmsCurdEditComponent,
  CmsCurdViewComponent];

@NgModule({
  imports: [
    SharedModule,
    CmsRoutingModule
  ],
  declarations: [
    ...COMPONENTS,
    ...COMPONENTS_NOROUNT
  ],
  entryComponents: COMPONENTS_NOROUNT
})
export class CmsModule { }
