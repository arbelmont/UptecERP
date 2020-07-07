import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'app/shared/shared.module';
import { OrdemRoutingModule } from './ordem.routing.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { CurrencyMaskModule } from 'ng2-currency-mask';
import { OrdemService } from './services/ordem.service';
import { OrdemListComponent } from './ordem-list/ordem-list.component';
import { OrdemAddComponent } from './ordem-add/ordem-add.component';
import { OrdemExpedicaoModalComponent } from './ordem-expedicao-modal/ordem-expedicao-modal';
import { OrdemDetalheModalComponent } from './ordem-detalhe-modal/ordem-detalhe-modal';
import { OrdemAddPecaModalComponent } from './ordem-add-peca-modal/ordem-add-peca-modal';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    OrdemRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    NgbPaginationModule,
    CurrencyMaskModule
  ],
  declarations: [
    OrdemListComponent,
    OrdemAddComponent,
    OrdemExpedicaoModalComponent,
    OrdemAddPecaModalComponent,
    OrdemDetalheModalComponent
  ],
  entryComponents:[
    OrdemExpedicaoModalComponent,
    OrdemDetalheModalComponent,
    OrdemAddPecaModalComponent
  ],
  providers: [OrdemService]
})
export class OrdemModule { }
