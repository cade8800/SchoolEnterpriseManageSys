import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { environment } from '@env/environment';
// layout
import { LayoutDefaultComponent } from '../layout/default/default.component';
import { LayoutFullScreenComponent } from '../layout/fullscreen/fullscreen.component';
import { LayoutPassportComponent } from '../layout/passport/passport.component';
// dashboard pages
import { DashboardV1Component } from './dashboard/v1/v1.component';
import { DashboardAnalysisComponent } from './dashboard/analysis/analysis.component';
import { DashboardMonitorComponent } from './dashboard/monitor/monitor.component';
import { DashboardWorkplaceComponent } from './dashboard/workplace/workplace.component';
// passport pages
import { UserLoginComponent } from './passport/login/login.component';
import { UserRegisterComponent } from './passport/register/register.component';
import { UserRegisterResultComponent } from './passport/register-result/register-result.component';
// single pages
import { CallbackComponent } from './callback/callback.component';
import { UserLockComponent } from './passport/lock/lock.component';
import { Exception403Component } from './exception/403.component';
import { Exception404Component } from './exception/404.component';
import { Exception500Component } from './exception/500.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutDefaultComponent,
    children: [
      { path: '', redirectTo: 'dashboard/workplace', pathMatch: 'full' },
      { path: 'dashboard', redirectTo: 'dashboard/v1', pathMatch: 'full' },
      {
        path: 'dashboard/v1',
        component: DashboardV1Component,
        data: { title: '欢迎页' },
      },
      { path: 'dashboard/analysis', component: DashboardAnalysisComponent },
      { path: 'dashboard/monitor', component: DashboardMonitorComponent },
      {
        path: 'dashboard/workplace',
        component: DashboardWorkplaceComponent,
        data: { title: '主页' },
      },
      {
        path: 'widgets',
        loadChildren: './widgets/widgets.module#WidgetsModule',
      },
      { path: 'style', loadChildren: './style/style.module#StyleModule' },
      { path: 'delon', loadChildren: './delon/delon.module#DelonModule' },
      { path: 'extras', loadChildren: './extras/extras.module#ExtrasModule' },
      { path: 'pro', loadChildren: './pro/pro.module#ProModule' },
      { path: 'cms', loadChildren: './cms/cms.module#CmsModule' },
      {
        path: 'archives',
        loadChildren: './archives/archives.module#ArchivesModule',
      },
      {
        path: 'collect',
        loadChildren: './collect/collect.module#CollectModule',
      },
      {
        path: 'enterprise',
        loadChildren: './enterprise/enterprise.module#EnterpriseModule',
      },
      {
        path: 'consult',
        loadChildren: './consult/consult.module#ConsultModule',
      },
      {
        path: 'appointment',
        loadChildren: './appointment/appointment.module#AppointmentModule',
      },
      {
        path: 'assessment',
        loadChildren: './assessment/assessment.module#AssessmentModule',
      },
    ],
  },
  // 全屏布局
  {
    path: 'data-v',
    component: LayoutFullScreenComponent,
    children: [
      { path: '', loadChildren: './data-v/data-v.module#DataVModule' },
    ],
  },
  // passport
  {
    path: 'passport',
    component: LayoutPassportComponent,
    children: [
      {
        path: 'login',
        component: UserLoginComponent,
        data: { title: '登录' },
      },
      {
        path: 'register',
        component: UserRegisterComponent,
        data: { title: '注册' },
      },
      {
        path: 'register-result/:email',
        component: UserRegisterResultComponent,
        data: { title: '注册结果' },
      },
    ],
  },
  // 单页不包裹Layout
  { path: 'callback/:type', component: CallbackComponent },
  {
    path: 'lock',
    component: UserLockComponent,
    data: { title: '锁屏', titleI18n: 'lock' },
  },
  { path: '403', component: Exception403Component },
  { path: '404', component: Exception404Component },
  { path: '500', component: Exception500Component },
  { path: '**', redirectTo: 'dashboard/workplace' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: environment.useHash })],
  exports: [RouterModule],
})
export class RouteRoutingModule { }
