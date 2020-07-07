import { AuthService } from 'app/security/auth/auth.service';
import { ListService } from 'app/shared/services/list.service';
import { Component, OnInit, Input, AfterViewChecked } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { NotaSaidaService } from '../services/nota-saida.service';
import { Fornecedor } from 'app/cadastros/fornecedor/models/fornecedor';

@Component({
  selector: 'app-nota-saida-fornecedor-modal',
  templateUrl: './nota-saida-fornecedor-modal.html'
})
export class NotaSaidaFornecedorModalComponent implements OnInit  {


  @Input() fornecedores: Fornecedor[];

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

  selecionar(fornecedor: Fornecedor){
    this.activeModal.close(fornecedor);
  }
}
