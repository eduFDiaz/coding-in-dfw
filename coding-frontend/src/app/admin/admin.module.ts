import { NbThemeModule } from '@nebular/theme';
import { NbSidebarModule, NbLayoutModule, NbButtonModule } from '@nebular/theme';
import { NbSidebarService } from '@nebular/theme';

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './admin.component';
import { RouterModule } from '@angular/router';
import { AdminRoutes } from './admin.routing';

@NgModule({
  providers: [NbSidebarService],
  declarations: [AdminComponent],
  imports: [
    AdminRoutes,
    NbSidebarModule,
    NbLayoutModule,
    NbButtonModule,
    RouterModule,
    CommonModule,
    NbThemeModule.forRoot({ name: 'dark' })
  ]
})
export class AdminModule { }
