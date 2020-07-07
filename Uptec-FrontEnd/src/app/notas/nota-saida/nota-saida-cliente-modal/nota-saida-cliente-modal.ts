import { AuthService } from 'app/security/auth/auth.service';
import { ListService } from 'app/shared/services/list.service';
import { Component, OnInit, Input, AfterViewChecked } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { NotaSaidaService } from '../services/nota-saida.service';
import { Cliente } from 'app/cadastros/cliente/models/cliente';

@Component({
  selector: 'app-nota-saida-cliente-modal',
  templateUrl: './nota-saida-cliente-modal.html'
})
export class NotaSaidaClienteModalComponent implements OnInit  {


  @Input() clientes: Cliente[];

  constructor(public activeModal: NgbActiveModal,
              private service: NotaSaidaService,
              public listService: ListService,
              private toastrService: ToastrService,
              authService: AuthService) {
  
  }

  ngOnInit() {
  }

  close() {
    this.activeModal.close();
  }

  selecionar(cliente: Cliente){
    this.activeModal.close(cliente);
  }
}
