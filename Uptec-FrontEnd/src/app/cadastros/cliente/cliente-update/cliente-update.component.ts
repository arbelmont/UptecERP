import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

import { ToastrService } from 'ngx-toastr';
import { ClienteService } from './../services/cliente.service';

import { BaseFormComponent } from 'app/shared/base-form/base-form.component';
import { ClienteEnderecoModalComponent } from '../cliente-endereco-modal/cliente-endereco-modal.component';
import { ClienteTelefoneModalComponent } from '../cliente-telefone-modal/cliente-telefone-modal.component';

import { FormValidatorHelper } from 'app/shared/helpers/form-validator-helper';
import { ScrollHelper } from 'app/shared/helpers/scroll-helper';
import { EnumType } from 'app/shared/models/enumType';
import { Cliente, EnderecoCliente, TelefoneCliente } from '../models/cliente';

import swal from 'sweetalert2';
import { SharedService } from 'app/shared/services/shared.service';

@Component({
  selector: 'app-cliente-update',
  templateUrl: './cliente-update.component.html'
})
export class ClienteUpdateComponent extends BaseFormComponent implements OnInit {

  public cliente : Cliente;
  public transportadoras$: Observable<EnumType[]>;
  public tipoTelefone$: Observable<EnumType[]>;
  public enderecos$: Observable<EnderecoCliente[]>;
  public telefones$: Observable<TelefoneCliente[]>;

  constructor(private route: ActivatedRoute,
              private toastr: ToastrService,
              private service: ClienteService,
              private sharedService: SharedService,
              private modal: NgbModal ) {
    super();
    this.formValidatorHelper = new FormValidatorHelper(Cliente.validationMessages());            
  }

  ngOnInit() {
    this.frm = Cliente.buildFormUpdate();
    this.transportadoras$ = this.sharedService.getTransportadoras();
    this.tipoTelefone$ = this.sharedService.getTelefoneTipos();
    this.entityId = this.route.snapshot.paramMap.get('id');

    this.service.get(this.entityId).pipe(take(1))
      .subscribe(t => this.fillForm(t));

    this.enderecos$ = this.service.getEnderecos(this.entityId);
    this.telefones$ = this.service.getTelefones(this.entityId);
  }

  submit(): void {
    let t = Object.assign({}, this.cliente, this.frm.value);

    this.service.update(t).pipe(take(1)).subscribe(
      result => this.onSubmitComplete(result)
    );
  }
  onSubmitComplete(data: any): void {
    this.errors = [];
    this.toastr.success("","Cliente alterado com sucesso!");
    ScrollHelper.scrollToTop();
  }

  private fillForm(cliente: Cliente) {
    this.cliente = cliente;
    this.frm.patchValue({
      id: this.cliente.id,
      cnpj: this.cliente.cnpj,
      inscricaoEstadual: this.cliente.inscricaoEstadual,
      razaoSocial: this.cliente.razaoSocial,
      nomeFantasia: this.cliente.nomeFantasia,
      transportadoraId: (this.cliente.transportadoraId == null? '': this.cliente.transportadoraId),
      email: this.cliente.email,
      website: this.cliente.website,
      observacoes: this.cliente.observacoes,
    });
  }

  //Enderecos
  addEndereco(){
    this.openModal(ClienteEnderecoModalComponent);
  }

  updateEndereco(id: string) {
    this.openModal(ClienteEnderecoModalComponent).componentInstance.enderecoId = id;
  }

  confirmDeleteEndereco(id: string) {
    swal.fire({
      title: 'Excluir endereço?',
      text: '',
      type: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sim, excluir!',
      cancelButtonText: 'Não, cancelar!',
      confirmButtonClass: 'btn btn-success btn-raised mr-5',
      cancelButtonClass: 'btn btn-danger btn-raised',
      buttonsStyling: false
    }).then((result) => {
      if(result.value) {
        this.deleteEndereco(id);
      }
    })
  }

  deleteEndereco(id : string) {
    this.service.deleteEndereco(id).pipe(take(1))
      .subscribe(() => {
        this.enderecos$ = this.service.getEnderecos(this.entityId);
        this.toastr.success("Endereço excluído com sucesso!");
      });
  }

  //Telefones
  addTelefone(){
    this.openModal(ClienteTelefoneModalComponent);
  }

  updateTelefone(id: string) {
    this.openModal(ClienteTelefoneModalComponent).componentInstance.telefoneId = id;
  }

  confirmDeleteTelefone(id: string) {
    swal.fire({
      title: 'Excluir contato?',
      text: '',
      type: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sim, excluir!',
      cancelButtonText: 'Não, cancelar!',
      confirmButtonClass: 'btn btn-success btn-raised mr-5',
      cancelButtonClass: 'btn btn-danger btn-raised',
      buttonsStyling: false
    }).then((result) => {
      if(result.value) {
        this.deleteTelefone(id);
      }
    })
  }

  deleteTelefone(id : string) {
    this.service.deleteTelefone(id).pipe(take(1))
      .subscribe(() => {
        this.telefones$ = this.service.getTelefones(this.entityId);
        this.toastr.success("Contato excluído com sucesso!");
      });
  }

  private openModal(component: any) : NgbModalRef {
    const modalRef = this.modal.open(component, { size: 'lg', backdrop: 'static' });
    
    modalRef.result.then(result => {
      if(result == 'enderecoSaved'){
        this.enderecos$ = this.service.getEnderecos(this.entityId);
        this.toastr.success('','Endereço salvo com sucesso!');
      }
      else if(result == 'telefoneSaved'){
        this.telefones$ = this.service.getTelefones(this.entityId);
        this.toastr.success('','Contato salvo com sucesso!');
      }
    },
      reason => {});
    
    modalRef.componentInstance.clienteId = this.entityId;
    return modalRef;
  }
}
