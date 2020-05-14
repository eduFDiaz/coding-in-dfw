import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductAddComponent } from './product-add/product-add.component';
import { FormsModule } from '@angular/forms'
import { NbAlertModule, NbDialogService, NbTooltipModule, NbIconModule, NbSelectModule, NbTreeGridModule, NbCardModule, NbInputModule, NbButtonModule, NbSpinnerModule } from '@nebular/theme';
import { ProductDeleteComponent } from './product-list/product-delete/product-delete.component';
import { ProductEditComponent } from './product-list/product-edit/product-edit.component';
import { RequirementAddComponent } from './product-add/requirement-add/requirement-add.component'
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
@NgModule({
  declarations: [ProductListComponent, ProductAddComponent, ProductDeleteComponent, ProductEditComponent, RequirementAddComponent],
  imports: [
    CommonModule,
    CKEditorModule,
    NbIconModule,
    NbTooltipModule,
    NbTreeGridModule,
    NbCardModule,
    NbInputModule,
    NbButtonModule,
    NbSpinnerModule,
    NbSelectModule,
    FormsModule,
    NbAlertModule
  ],
  exports: [ProductAddComponent, ProductListComponent],
  entryComponents: [ProductDeleteComponent, ProductEditComponent, RequirementAddComponent]

})
export class ProductsModule { }
