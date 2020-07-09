import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { AppointmentRoutingModule } from './appointment-routing.module';
import { AppointmentListComponent } from './list/list.component';
import { AppointmentEditComponent } from './edit/edit.component';

const COMPONENTS = [
  AppointmentListComponent,
  AppointmentEditComponent];
const COMPONENTS_NOROUNT = [];

@NgModule({
  imports: [
    SharedModule,
    AppointmentRoutingModule
  ],
  declarations: [
    ...COMPONENTS,
    ...COMPONENTS_NOROUNT
  ],
  entryComponents: COMPONENTS_NOROUNT
})
export class AppointmentModule { }
