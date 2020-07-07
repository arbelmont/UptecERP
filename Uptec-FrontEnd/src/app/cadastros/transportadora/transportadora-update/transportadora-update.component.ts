import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

import { ToastrService } from 'ngx-toastr';
import { TransportadoraService } from './../services/transportadora.service';

import { BaseFormComponent } from 'app/shared/base-form/base-form.component';
import { TransportadoraEnderecoModalComponent } from '../transportadora-endereco-modal/transportadora-endereco-modal.component';
import { TransportadoraTelefoneModalComponent } from '../transportadora-telefone-modal/transportadora-telefone-modal.component';

import { FormValidatorHelper } from 'app/shared/helpers/form-validator-helper';
import { ScrollHelper } from 'app/shared/helpers/scroll-helper';
import { EnumType } from 'app/shared/models/enumType';
import { Transportadora, EnderecoTransportadora, TelefoneTransportadora } from '../models/transportadora';

import swal from 'sweetalert2';
import { SharedService } from 'app/shared/services/shared.service';

@Component({
  selector: 'app-transportadora-update',
  templateUrl: './transportadora-update.component.html'
})
export class TransportadoraUpdateComponent extends BaseFormComponent implements OnInit {

  public transportadora : Transportadora;
  public tipoEntregaPadrao$: Observable<EnumType[]>;
  public tipoTelefone$: Observable<EnumType[]>;
  public enderecos$: Observable<EnderecoTransportadora[]>;
  public telefones$: Observable<TelefoneTransportadora[]>;

  constructor(private route: ActivatedRoute,
              private toastr: ToastrService,
              private service: TransportadoraService,
              private sharedService: SharedService,
              private modal: NgbModal ) {
    super();
    this.formValidatorHelper = new FormValidatorHelper(Transportadora.validationMessages());            
   }

  ngOnInit() {
    this.frm = Transportadora.buildFormUpdate();
    this.tipoEntregaPadrao$ = this.service.getEntregaTipos();
    this.tipoTelefone$ = this.sharedService.getTelefoneTipos();
    this.entityId = this.route.snapshot.paramMap.get('id');

    this.service.get(this.entityId).pipe(take(1))
      .subscribe(t => this.fillForm(t));

    this.enderecos$ = this.service.getEnderecos(this.entityId);
    this.telefones$ = this.service.getTelefones(this.entityId);
  }

  submit(): void {
    let t = Object.assign({}, this.transportadora, this.frm.value);

    this.service.update(t).pipe(take(1)).subscribe(
      result => this.onSubmitComplete(result)
    );
  }
  onSubmitComplete(data: any): void {
    this.errors = [];
    this.toastr.success("","Transportadora alterada com sucesso!");
    ScrollHelper.scrollToTop();
  }

  private fillForm(transportadora: Transportadora) {
    this.transportadora = transportadora;
    this.frm.patchValue({
      id: this.transportadora.id,
      cnpj: this.transportadora.cnpj,
      inscricaoEstadual: this.transportadora.inscricaoEstadual,
      razaoSocial: this.transportadora.razaoSocial,
      nomeFantasia: this.transportadora.nomeFantasia,
      tipoEntregaPadrao: this.transportadora.tipoEntregaPadrao,
      email: this.transportadora.email,
      website: this.transportadora.website,
      observacoes: this.transportadora.observacoes,
    });
  }

  //Enderecos
  addEndereco(){
    this.openModal(TransportadoraEnderecoModalComponent);
  }

  updateEndereco(id: string) {
    this.openModal(TransportadoraEnderecoModalComponent).componentInstance.enderecoId = id;
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
    this.openModal(TransportadoraTelefoneModalComponent);
  }

  updateTelefone(id: string) {
    this.openModal(TransportadoraTelefoneModalComponent).componentInstance.telefoneId = id;
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
    
    modalRef.componentInstance.transportadoraId = this.entityId;
    return modalRef;
  }
}
