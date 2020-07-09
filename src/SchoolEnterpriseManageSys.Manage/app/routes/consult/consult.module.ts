import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { ConsultRoutingModule } from './consult-routing.module';
import { ConsultIndexComponent } from './index/index.component';
import { ConsultListComponent } from './list/list.component';

const COMPONENTS = [
  ConsultIndexComponent,
  ConsultListComponent];
const COMPONENTS_NOROUNT = [];

@NgModule({
  imports: [
    SharedModule,
    ConsultRoutingModule
  ],
  declarations: [
    ...COMPONENTS,
    ...COMPONENTS_NOROUNT
  ],
  entryComponents: COMPONENTS_NOROUNT
})
export class ConsultModule { }
