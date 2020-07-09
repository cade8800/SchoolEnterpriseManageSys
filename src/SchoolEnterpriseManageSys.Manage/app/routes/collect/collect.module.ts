import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { CollectRoutingModule } from './collect-routing.module';
import { CollectListComponent } from './list/list.component';
import { CollectEditComponent } from './edit/edit.component';
import { CollectDetailComponent } from './detail/detail.component';
import { CollectDepartmentComponent } from './department/department.component';
import { CollectDepartmentDetailComponent } from './department-detail/department-detail.component';
import { CollectDepartmentUploadComponent } from './department/upload/upload.component';

const COMPONENTS = [
  CollectListComponent,
  CollectEditComponent,
  CollectDetailComponent,
  CollectDepartmentComponent,
  CollectDepartmentDetailComponent];
const COMPONENTS_NOROUNT = [
  CollectDepartmentUploadComponent];

@NgModule({
  imports: [
    SharedModule,
    CollectRoutingModule
  ],
  declarations: [
    ...COMPONENTS,
    ...COMPONENTS_NOROUNT
  ],
  entryComponents: COMPONENTS_NOROUNT
})
export class CollectModule { }
