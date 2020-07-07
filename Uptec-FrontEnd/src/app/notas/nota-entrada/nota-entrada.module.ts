import { LoteModule } from './../../estoque/lote/lote.module';
import { NotaEntradaUploadModalComponent } from './nota-upload-modal/nota-upload-modal.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'app/shared/shared.module';
import { NotaEntradaRoutingModule } from './nota-entrada.routing.module';
import { CurrencyMaskModule } from 'ng2-currency-mask';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NotaEntradaListComponent } from './nota-entrada-list/nota-entrada-list.component';
import { NotaService } from './services/nota.service';
import { NotaConciliacaoModalComponent } from './nota-conciliacao-modal/nota-conciliacao-modal';
import { NotaCoberturaModalComponent } from './nota-cobertura-modal/nota-cobertura-modal';
import { NotaDetalheModalComponent } from './nota-detalhe-modal/nota-detalhe-modal';
import { NotaTipoEmissorModalComponent } from './nota-tipoEmissor-modal/nota-tipoEmissor-modal';
import { CurrencyMaskConfigHelper } from 'app/shared/helpers/currency-helper';
import { CURRENCY_MASK_CONFIG } from 'ng2-currency-mask/src/currency-mask.config';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    NotaEntradaRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    NgbPaginationModule,
    CurrencyMaskModule
  ],
  declarations: [
    NotaEntradaListComponent,
    NotaEntradaUploadModalComponent,
    NotaConciliacaoModalComponent,
    NotaCoberturaModalComponent,
    NotaDetalheModalComponent,
    NotaTipoEmissorModalComponent
  ],
  entryComponents: [
    NotaEntradaUploadModalComponent,
    NotaConciliacaoModalComponent,
    NotaCoberturaModalComponent,
    NotaDetalheModalComponent,
    NotaTipoEmissorModalComponent
  ],
  providers: [
    NotaService,
    { provide: CURRENCY_MASK_CONFIG, useValue: CurrencyMaskConfigHelper }
  ]
})
export class NotaEntradaModule { }
