import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { TransportadoraRoutingModule } from './transportadora-routing.module';
import { TransportadoraListComponent } from './transportadora-list/transportadora-list.component';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from 'app/shared/shared.module';
import { TransportadoraService } from './services/transportadora.service';
import { TransportadoraAddComponent } from './transportadora-add/transportadora-add.component';
import { TransportadoraUpdateComponent } from './transportadora-update/transportadora-update.component';
import { TransportadoraEnderecoModalComponent } from './transportadora-endereco-modal/transportadora-endereco-modal.component';
import { TransportadoraTelefoneModalComponent } from './transportadora-telefone-modal/transportadora-telefone-modal.component';

@NgModule({
  imports: [
    CommonModule,
    TransportadoraRoutingModule,
    SharedModule,
    ReactiveFormsModule,
    FormsModule,
    NgbPaginationModule
  ],
  declarations: [
    TransportadoraListComponent, 
    TransportadoraAddComponent, 
    TransportadoraUpdateComponent, 
    TransportadoraEnderecoModalComponent, 
    TransportadoraTelefoneModalComponent
  ],
  entryComponents: [
    TransportadoraEnderecoModalComponent,
    TransportadoraTelefoneModalComponent
  ],
  providers: [TransportadoraService]
})
export class TransportadoraModule { }
