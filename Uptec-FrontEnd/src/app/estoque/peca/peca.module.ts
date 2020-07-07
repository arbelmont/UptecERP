import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { PecaRoutingModule } from './peca.routing.module';
import { SharedModule } from 'app/shared/shared.module';
import { PecaService } from './services/peca.service';
import { PecaListComponent } from './peca-list/peca-list.component';
import { PecaFormComponent } from './peca-form/peca-form.component';
import { CurrencyMaskModule } from 'ng2-currency-mask';
import { CURRENCY_MASK_CONFIG } from 'ng2-currency-mask/src/currency-mask.config';
import { CurrencyMaskConfigHelper } from 'app/shared/helpers/currency-helper';
import { PecaComponenteModalComponent } from './peca-componente-modal/peca-componente-modal.component';
import { PecaLoteListComponent } from './peca-lote-list/peca-lote-list.component';
import { LoteModule } from '../lote/lote.module';
import { PecaFornecedorModalComponent } from './peca-fornecedor-modal/peca-fornecedor-modal.component';

@NgModule({
  imports: [
    CommonModule,
    PecaRoutingModule,
    SharedModule,
    LoteModule,
    ReactiveFormsModule,
    FormsModule,
    NgbPaginationModule,
    CurrencyMaskModule
  ],
  declarations: [
    PecaListComponent,
    PecaFormComponent,
    PecaComponenteModalComponent,
    PecaFornecedorModalComponent,
    PecaLoteListComponent
  ],
  entryComponents: [
    PecaComponenteModalComponent,
    PecaFornecedorModalComponent
  ],
  providers: [
    PecaService,
    { provide: CURRENCY_MASK_CONFIG, useValue: CurrencyMaskConfigHelper }
  ]
})
export class PecaModule { }
