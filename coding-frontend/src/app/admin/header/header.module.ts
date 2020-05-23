import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header.component';

// tslint:disable-next-line: max-line-length
import { NbBadgeModule, NbLayoutModule, NbActionsModule, NbUserModule, NbIconModule, NbSearchModule, NbMenuModule, NbContextMenuModule, NbContextMenuComponent } from '@nebular/theme';

@NgModule({
  imports: [
    CommonModule,
    NbActionsModule,
    NbUserModule,
    NbIconModule,
    NbSearchModule,
    NbMenuModule,
    NbContextMenuModule,
    NbLayoutModule,
    NbBadgeModule

  ],
  exports: [HeaderComponent],
  declarations: [HeaderComponent]

})
export class HeaderModule { }
