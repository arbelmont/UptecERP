import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

import { ToastrService } from 'ngx-toastr';
import { FornecedorService } from './../services/fornecedor.service';

import { BaseFormComponent } from 'app/shared/base-form/base-form.component';
import { FornecedorEnderecoModalComponent } from '../fornecedor-endereco-modal/fornecedor-endereco-modal.component';
import { FornecedorTelefoneModalComponent } from '../fornecedor-telefone-modal/fornecedor-telefone-modal.component';

import { FormValidatorHelper } from 'app/shared/helpers/form-validator-helper';
import { ScrollHelper } from 'app/shared/helpers/scroll-helper';
import { EnumType } from 'app/shared/models/enumType';
import { Fornecedor, EnderecoFornecedor, TelefoneFornecedor } from '../models/fornecedor';

import swal from 'sweetalert2';
import { SharedService } from 'app/shared/services/shared.service';

@Component({
  selector: 'app-fornecedor-update',
  templateUrl: './fornecedor-update.component.html'
})
export class FornecedorUpdateComponent extends BaseFormComponent implements OnInit {

  public fornecedor : Fornecedor;
  public tipoTelefone$: Observable<EnumType[]>;
  public enderecos$: Observable<EnderecoFornecedor[]>;
  public telefones$: Observable<TelefoneFornecedor[]>;

  constructor(private route: ActivatedRoute,
              private toastr: ToastrService,
              private service: FornecedorService,
              private sharedService: SharedService,
              private modal: NgbModal ) {
    super();
    this.formValidatorHelper = new FormValidatorHelper(Fornecedor.validationMessages());            
  }

  ngOnInit() {
    this.frm = Fornecedor.buildFormUpdate();
    this.tipoTelefone$ = this.sharedService.getTelefoneTipos();
    this.entityId = this.route.snapshot.paramMap.get('id');

    this.service.get(this.entityId).pipe(take(1))
      .subscribe(t => this.fillForm(t));

    this.enderecos$ = this.service.getEnderecos(this.entityId);
    this.telefones$ = this.service.getTelefones(this.entityId);
  }

  submit(): void {
    let t = Object.assign({}, this.fornecedor, this.frm.value);

    this.service.update(t).pipe(take(1)).subscribe(
      result => this.onSubmitComplete(result)
    );
  }
  onSubmitComplete(data: any): void {
    this.errors = [];
    this.toastr.success("","Fornecedor alterado com sucesso!");
    ScrollHelper.scrollToTop();
  }

  private fillForm(fornecedor: Fornecedor) {
    this.fornecedor = fornecedor;
    this.frm.patchValue({
      id: this.fornecedor.id,
      cnpj: this.fornecedor.cnpj,
      inscricaoEstadual: this.fornecedor.inscricaoEstadual,
      razaoSocial: this.fornecedor.razaoSocial,
      nomeFantasia: this.fornecedor.nomeFantasia,
      email: this.fornecedor.email,
      website: this.fornecedor.website,
      observacoes: this.fornecedor.observacoes,
    });
  }

  //Enderecos
  addEndereco(){
    this.openModal(FornecedorEnderecoModalComponent);
  }

  updateEndereco(id: string) {
    this.openModal(FornecedorEnderecoModalComponent).componentInstance.enderecoId = id;
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
    this.openModal(FornecedorTelefoneModalComponent);
  }

  updateTelefone(id: string) {
    this.openModal(FornecedorTelefoneModalComponent).componentInstance.telefoneId = id;
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
    
    modalRef.componentInstance.fornecedorId = this.entityId;
    return modalRef;
  }
}
