import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from 'app/shared/shared.module';
import { FornecedorService } from './services/fornecedor.service';
import { FornecedorListComponent } from './fornecedor-list/fornecedor-list.component';
import { FornecedorRoutingModule } from './fornecedor.routing.module';
import { FornecedorAddComponent } from './fornecedor-add/fornecedor-add.component';
import { FornecedorEnderecoModalComponent } from './fornecedor-endereco-modal/fornecedor-endereco-modal.component';
import { FornecedorTelefoneModalComponent } from './fornecedor-telefone-modal/fornecedor-telefone-modal.component';
import { FornecedorUpdateComponent } from './fornecedor-update/fornecedor-update.component';

@NgModule({
  imports: [
    CommonModule,
    FornecedorRoutingModule,
    SharedModule,
    ReactiveFormsModule,
    FormsModule,
    NgbPaginationModule
  ],
  declarations: [
      FornecedorListComponent,
      FornecedorAddComponent,
      FornecedorEnderecoModalComponent,
      FornecedorTelefoneModalComponent,
      FornecedorUpdateComponent
    ],

  entryComponents: [
    FornecedorEnderecoModalComponent,
    FornecedorTelefoneModalComponent
  ],
  providers: [FornecedorService]
})
export class FornecedorModule { }