import { LoteMovimentoListComponent } from './lote-movimento-list/lote-movimento-list.component';
import { LoteService } from './services/lote.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SharedModule } from 'app/shared/shared.module';
import { LoteListComponent } from './lote-list/lote-list.component';
import { LoteRoutingModule } from './lote.routing.module';
import { LoteLancamentoManualComponent } from './lote-lancamento-manual/lote-lancamento-manual.component';
import { CurrencyMaskModule } from 'ng2-currency-mask';
import { LoteMovimentoDetalheComponent } from './lote-movimento-detalhe/lote-movimento-detalhe';
import { CURRENCY_MASK_CONFIG } from 'ng2-currency-mask/src/currency-mask.config';
import { CurrencyMaskConfigHelper } from 'app/shared/helpers/currency-helper';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    LoteRoutingModule,
    SharedModule,
    CurrencyMaskModule
  ],
  declarations: [
    LoteLancamentoManualComponent,
    LoteListComponent,
    LoteMovimentoListComponent,
    LoteMovimentoDetalheComponent
  ],
  entryComponents: [
    LoteLancamentoManualComponent,
    LoteMovimentoDetalheComponent
  ],
  providers: [
    LoteService,
    { provide: CURRENCY_MASK_CONFIG, useValue: CurrencyMaskConfigHelper }
  ]
})
export class LoteModule { }
