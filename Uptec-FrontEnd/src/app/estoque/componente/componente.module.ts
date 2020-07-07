import { ComponenteMovimentoDetalheComponent } from './componente-movimento-detalhe/componente-movimento-detalhe';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { CurrencyMaskModule } from "ng2-currency-mask";
import { ComponenteRoutingModule } from './componente.routing.module';
import { SharedModule } from 'app/shared/shared.module';
import { ComponenteService } from './services/componente.service';
import { ComponenteListComponent } from './componente-list/componente-list.component';
import { ComponenteAddComponent } from './componente-add/componente-add.component';
import { ComponenteUpdateComponent } from './componente-update/componente-update.component';
import { CURRENCY_MASK_CONFIG } from 'ng2-currency-mask/src/currency-mask.config';
import { CurrencyMaskConfigHelper } from './../../shared/helpers/currency-helper';
import { ComponenteProgressComponent } from './componente-progress/componente-progress.component';
import { ComponenteMovimentoComponent } from './componente-movimento/componente-movimento.component';
import { ComponenteLancamentoManualComponent } from './componente-lancamento-manual/componente-lancamento-manual.component';

@NgModule({
  imports: [
    CommonModule,
    ComponenteRoutingModule,
    SharedModule,
    ReactiveFormsModule,
    FormsModule,
    NgbPaginationModule,
    CurrencyMaskModule
  ],
  declarations: [
    ComponenteListComponent, 
    ComponenteAddComponent, 
    ComponenteUpdateComponent, 
    ComponenteProgressComponent, 
    ComponenteMovimentoComponent, 
    ComponenteLancamentoManualComponent,
    ComponenteMovimentoDetalheComponent
  ],
  entryComponents: [
    ComponenteLancamentoManualComponent,
    ComponenteMovimentoDetalheComponent
  ],
  providers: [
    ComponenteService,
    { provide: CURRENCY_MASK_CONFIG, useValue: CurrencyMaskConfigHelper }
  ]
})
export class ComponenteModule { }
