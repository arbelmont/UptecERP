import { AuthService } from 'app/security/auth/auth.service';
import { ListService } from 'app/shared/services/list.service';
import { Component, OnInit, Input, AfterViewChecked } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { Peca } from 'app/estoque/peca/models/peca';

@Component({
  selector: 'app-ordem-add-peca-modal',
  templateUrl: './ordem-add-peca-modal.html'
})
export class OrdemAddPecaModalComponent implements OnInit  {

  @Input() pecas: Peca[];

  constructor(public activeModal: NgbActiveModal,
              public listService: ListService,
              private toastrService: ToastrService,
              authService: AuthService) {
  
  }

  ngOnInit() {
  }

  close() {
    this.activeModal.close();
  }

  selecionar(peca: Peca){
    this.activeModal.close(peca);
  }
}
