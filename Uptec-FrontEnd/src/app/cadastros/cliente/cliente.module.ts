import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ClienteRoutingModule } from './cliente-routing.module';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from 'app/shared/shared.module';
import { ClienteService } from './services/cliente.service';
import { ClientListComponent } from './client-list/client-list.component';
import { ClienteAddComponent } from './cliente-add/cliente-add.component';
import { ClienteEnderecoModalComponent } from './cliente-endereco-modal/cliente-endereco-modal.component';
import { ClienteTelefoneModalComponent } from './cliente-telefone-modal/cliente-telefone-modal.component';
import { ClienteUpdateComponent } from './cliente-update/cliente-update.component';

@NgModule({
  imports: [
    CommonModule,
    ClienteRoutingModule,
    SharedModule,
    ReactiveFormsModule,
    FormsModule,
    NgbPaginationModule
  ],
  declarations: [
    ClientListComponent,
    ClienteAddComponent,
    ClienteEnderecoModalComponent,
    ClienteTelefoneModalComponent,
    ClienteUpdateComponent
  ],
  entryComponents: [
    ClienteEnderecoModalComponent,
    ClienteTelefoneModalComponent
  ],
  providers: [ClienteService]
})
export class ClienteModule { }
