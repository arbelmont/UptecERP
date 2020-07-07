import { TipoPeca } from './../../../shared/enums/tipoPeca';
import { PecaComponenteModalComponent } from '../peca-componente-modal/peca-componente-modal.component';
import { CurrencyHelper } from '../../../shared/helpers/currency-helper';
import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { PecaService } from '../services/peca.service';
import { SharedService } from 'app/shared/services/shared.service';
import { BaseFormComponent } from 'app/shared/base-form/base-form.component';
import { Peca } from '../models/peca';
import { FormValidatorHelper } from 'app/shared/helpers/form-validator-helper';
import { ScrollHelper } from 'app/shared/helpers/scroll-helper';
import { EnumType } from 'app/shared/models/enumType';
import { ActivatedRoute } from '@angular/router';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import swal from 'sweetalert2';
import { EnumHelper } from 'app/shared/enums/enumHelper';
import { UnidadeMedida } from 'app/shared/enums/unidadeMedida';
import { PecaFornecedorModalComponent } from '../peca-fornecedor-modal/peca-fornecedor-modal.component';

@Component({
  selector: 'app-peca-form',
  templateUrl: './peca-form.component.html'
})
export class PecaFormComponent extends BaseFormComponent implements OnInit {

  public peca: Peca;
  public componentes: EnumType[];
  public fornecedores: EnumType[];
  public unidadesMedida: EnumType[];
  public tipos: EnumType[];
  public clientes$: Observable<EnumType[]>;

  constructor(private sharedService: SharedService,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private service: PecaService,
    private modal: NgbModal) {
    super();
    this.formValidatorHelper = new FormValidatorHelper(Peca.validationMessages());
  }

  ngOnInit() {
    this.peca = new Peca;
    this.peca.componentes = [];
    this.peca.codigosFornecedor = [];
    this.entityId = this.route.snapshot.paramMap.get('id');
    this.frm = Peca.buildForm();
    this.unidadesMedida = EnumHelper.enumSelector(UnidadeMedida);
    this.tipos = EnumHelper.enumSelector(TipoPeca);
    this.clientes$ = this.sharedService.getClientes();
    this.sharedService.getFornecedores().pipe(take(1))
      .subscribe(f => this.fornecedores = f);
    this.sharedService.getComponentes().pipe(take(1))
      .subscribe(c => {
        this.componentes = c;
        this.getPeca();
      });
  }

  submit() {
    let p = Object.assign({}, this.peca, this.frm.value)
    p.componentes = this.peca.componentes;
    p.codigosFornecedor = this.peca.codigosFornecedor;

    if (this.entityId) {
      this.service.update(p).pipe(take(1)).subscribe(
        result => this.onSubmitComplete(result));
    }
    else {
      this.service.add(p).pipe(take(1)).subscribe(
        result => this.onSubmitComplete(result));
    }
  }

  onSubmitComplete(data: any) {
    this.errors = [];
    if (this.entityId) {
      /* this.peca = new Peca;
      this.peca.componentes = [];
      this.getPeca(); */
      this.toastr.success("", "Peca alterada com sucesso!");
    }
    else {
      this.peca = new Peca;
      this.peca.componentes = [];
      this.peca.codigosFornecedor = [];
      this.frm = Peca.buildForm();
      this.toastr.success("", "Peca adicionada com sucesso!");
      ScrollHelper.scrollToTop();
    }
  }

  private fillForm(peca: Peca) {
    this.peca = peca;
    this.peca.componentes.forEach(cp => {
      cp.descricao = this.componentes.find(c => c.value === cp.componenteId).name;
    });
    this.peca.codigosFornecedor.forEach(cf => {
      cf.descricao = this.fornecedores.find(c => c.value === cf.fornecedorId).name;
    })

    this.frm.patchValue({
      id: this.peca.id,
      codigo: this.peca.codigo,
      codigoSaida: this.peca.codigoSaida,
      descricao: this.peca.descricao,
      unidade: this.peca.unidade,
      tipo: this.peca.tipo,
      preco: this.peca.preco,
      precoSaida: this.peca.precoSaida,
      ncm: this.peca.ncm,
      revisao: this.peca.revisao,
      clienteId: this.peca.clienteId
    });

    if (this.peca.codigosFornecedor == null)
      this.peca.codigosFornecedor = [];

    if (this.peca.componentes == null)
      this.peca.componentes = [];
  }

  getPeca() {
    if (this.entityId) {
      this.service.getFull(this.entityId).pipe(take(1))
        .subscribe(p => { this.fillForm(p) });
    }
  }

  addComponente() {
    this.openModalComponente(PecaComponenteModalComponent, '', 0);
  }

  showComponentes(){
    return this.frm.get("tipo").value == TipoPeca.Peca;
  }

  addFornecedor() {
    this.openModalFornecedor(PecaFornecedorModalComponent, '', '');
  }

  updateComponente(componenteId: string, quantidade: number) {
    this.openModalComponente(PecaComponenteModalComponent, componenteId, quantidade).componentInstance.pecaId = this.entityId;
  }

  updateFornecedor(fornecedorId: string, fornecedorCodigo: string) {
    this.openModalFornecedor(PecaFornecedorModalComponent, fornecedorId, fornecedorCodigo).componentInstance.pecaId = this.entityId;
  }

  deleteComponente(componenteId: string) {
    let index = this.peca.componentes.findIndex(c => c.componenteId == componenteId);
    if (index >= 0)
      this.peca.componentes.splice(index, 1);
  }

  deleteFornecedor(fornecedorId: string) {
    let index = this.peca.codigosFornecedor.findIndex(c => c.fornecedorId == fornecedorId);
    if (index >= 0)
      this.peca.codigosFornecedor.splice(index, 1);
  }

  confirmDeletecomponente(componenteId: string, descricao: string) {
    swal.fire({
      title: 'Excluir componente?',
      text: descricao,
      type: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sim, excluir!',
      cancelButtonText: 'Não, cancelar!',
      confirmButtonClass: 'btn btn-success btn-raised mr-5',
      cancelButtonClass: 'btn btn-danger btn-raised',
      buttonsStyling: false
    }).then((result) => {
      if (result.value) {
        this.deleteComponente(componenteId);
      }
    })
  }

  confirmDeleteFornecedor(fornecedorId: string, descricao: string) {
    swal.fire({
      title: 'Excluir fornecedor?',
      text: descricao,
      type: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sim, excluir!',
      cancelButtonText: 'Não, cancelar!',
      confirmButtonClass: 'btn btn-success btn-raised mr-5',
      cancelButtonClass: 'btn btn-danger btn-raised',
      buttonsStyling: false
    }).then((result) => {
      if (result.value) {
        this.deleteFornecedor(fornecedorId);
      }
    })
  }

  private openModalComponente(component: any, componenteId: string, quantidade: number): NgbModalRef {
    const modalRef = this.modal.open(component, { size: 'sm', backdrop: 'static' });

    modalRef.result.then(result => {
      result.descricao = this.componentes.find(c => c.value === result.componenteId).name
      let componenteExistente = this.peca.componentes.findIndex(c => c.componenteId == result.componenteId);
      if (componenteExistente >= 0)
        this.peca.componentes.splice(componenteExistente, 1, result);
      else
        this.peca.componentes.push(result);
    },
      reason => { });

    if (componenteId) {
      modalRef.componentInstance.componenteId = componenteId;
      modalRef.componentInstance.quantidade = quantidade;
    }

    modalRef.componentInstance.componentes = this.componentes;
    return modalRef;
  }

  private openModalFornecedor(component: any, fornecedorId: string, fornecedorCodigo: string): NgbModalRef {
    const modalRef = this.modal.open(component, { size: 'sm', backdrop: 'static' });

    modalRef.result.then(result => {
      result.descricao = this.fornecedores.find(c => c.value === result.fornecedorId).name;
      let fornecedorExistente = this.peca.codigosFornecedor.findIndex(f => f.fornecedorId == result.fornecedorId);
      if (fornecedorExistente >= 0)
        this.peca.codigosFornecedor.splice(fornecedorExistente, 1, result);
      else
        this.peca.codigosFornecedor.push(result);
    },
      reason => { });

    if (fornecedorId) {
      modalRef.componentInstance.fornecedorId = fornecedorId;
      modalRef.componentInstance.fornecedorCodigo = fornecedorCodigo;
    }

    modalRef.componentInstance.fornecedores = this.fornecedores;
    return modalRef;
  }
}
