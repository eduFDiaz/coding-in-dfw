import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header.component';

// tslint:disable-next-line: max-line-length
import { NbLayoutModule, NbActionsModule, NbUserModule, NbIconModule, NbSearchModule, NbMenuModule, NbContextMenuModule, NbContextMenuComponent } from '@nebular/theme';

@NgModule({
  imports: [
    CommonModule,
    NbActionsModule,
    NbUserModule,
    NbIconModule,
    NbSearchModule,
    NbMenuModule,
    NbContextMenuModule,
    NbLayoutModule

  ],
  exports: [HeaderComponent],
  declarations: [HeaderComponent]

})
export class HeaderModule { }
