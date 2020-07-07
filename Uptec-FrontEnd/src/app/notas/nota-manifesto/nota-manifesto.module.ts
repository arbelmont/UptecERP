import { NotaManifestoDetalheModalComponent } from './nota-manifesto-detalhe-modal/nota-manifesto-detalhe-modal';
import { NotaManifestoListComponent } from './nota-manifesto-list/nota-manifesto-list.component';
import { NotaManifestoRoutingModule } from './nota-manifesto.routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'app/shared/shared.module';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { CurrencyMaskModule } from 'ng2-currency-mask';
import { CURRENCY_MASK_CONFIG } from 'ng2-currency-mask/src/currency-mask.config';
import { CurrencyMaskConfigHelper } from 'app/shared/helpers/currency-helper';
import { NotaManifestoService } from './services/nota-manifesto.service';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    NotaManifestoRoutingModule,
    NgbPaginationModule,
    CurrencyMaskModule
  ],
  declarations: [
    NotaManifestoListComponent,
    NotaManifestoDetalheModalComponent
  ],
  providers: [
    NotaManifestoService,
    { provide: CURRENCY_MASK_CONFIG, useValue: CurrencyMaskConfigHelper }
  ],
  entryComponents: [NotaManifestoDetalheModalComponent]
})
export class NotaManifestoModule { }
