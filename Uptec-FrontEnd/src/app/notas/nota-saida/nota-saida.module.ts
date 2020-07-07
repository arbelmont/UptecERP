import { NotaSaidaDetalheModalComponent } from './nota-saida-detalhe-modal/nota-saida-detalhe-modal';
import { NotaSaidaClienteModalComponent } from './nota-saida-cliente-modal/nota-saida-cliente-modal';
import { ClienteModule } from './../../cadastros/cliente/cliente.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'app/shared/shared.module';
import { CurrencyMaskModule } from 'ng2-currency-mask';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NotaSaidaService } from './services/nota-saida.service';
import { NotaSaidaRoutingModule } from './nota-saida.routing.module';
import { NotaSaidaListComponent } from './nota-saida-list/nota-saida-list.component';
import { CURRENCY_MASK_CONFIG } from 'ng2-currency-mask/src/currency-mask.config';
import { CurrencyMaskConfigHelper, CurrencyMaskConfigHelper2 } from 'app/shared/helpers/currency-helper';
import { NotaAddPecaComponent } from './nota-add-peca/nota-add-peca.component';
import { NotaAddEmbalagemComponent } from './nota-add-embalagem/nota-add-embalagem.component';
import { NotaSaidaFornecedorModalComponent } from './nota-saida-fornecedor-modal/nota-saida-fornecedor-modal';
import { FornecedorModule } from 'app/cadastros/fornecedor/fornecedor.module';
import { NotaAddPecaAvulsaComponent } from './nota-add-peca-avulsa/nota-add-peca-avulsa.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    NotaSaidaRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    NgbPaginationModule,
    CurrencyMaskModule,
    ClienteModule,
    FornecedorModule
  ],
  declarations: [
    NotaSaidaListComponent,
    NotaAddPecaComponent,
    NotaAddEmbalagemComponent,
    NotaAddPecaAvulsaComponent,
    NotaSaidaClienteModalComponent,
    NotaSaidaFornecedorModalComponent,
    NotaSaidaDetalheModalComponent
  ],
  entryComponents: [
    NotaSaidaClienteModalComponent,
    NotaSaidaFornecedorModalComponent,
    NotaSaidaDetalheModalComponent
  ],
  providers: [
    NotaSaidaService,
    { provide: CURRENCY_MASK_CONFIG, useValue: CurrencyMaskConfigHelper2 }
  ]
})
export class NotaSaidaModule { }
