import { Component, OnInit } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { NotaEntradaUploadModalComponent } from '../nota-upload-modal/nota-upload-modal.component';
import { ToastrService } from 'ngx-toastr';
import { BaseListComponet } from 'app/shared/base-list/base-list.component';
import { ListService } from 'app/shared/services/list.service';
import { AuthService } from 'app/security/auth/auth.service';
import { Observable } from 'rxjs';
import { EnumType } from 'app/shared/models/enumType';
import { SharedService } from 'app/shared/services/shared.service';
import { formatDate } from '@angular/common';
import { NotaEntrada } from '../models/nota-entrada';
import { NotaService } from '../services/nota.service';
import { take } from 'rxjs/operators';
import { StatusNotaEntrada } from 'app/shared/enums/statusNotaEntrada';
import { NotaConciliacaoModalComponent } from '../nota-conciliacao-modal/nota-conciliacao-modal';
import { NotaEntradaHelper } from '../helper/nota-entrada-helper';
import { NotaCoberturaModalComponent } from '../nota-cobertura-modal/nota-cobertura-modal';
import { NotaDetalheModalComponent } from '../nota-detalhe-modal/nota-detalhe-modal';
import swal from 'sweetalert2';
import { TipoEmissor } from 'app/shared/enums/tipoEmissor';
import { NotaTipoEmissorModalComponent } from '../nota-tipoEmissor-modal/nota-tipoEmissor-modal';

@Component({
  selector: 'app-nota-entrada-list',
  templateUrl: './nota-entrada-list.component.html'
})
export class NotaEntradaListComponent extends BaseListComponet implements OnInit {
  
  public tipoEmissor$: Observable<EnumType[]>;
  public statusNfEntrada: EnumType[];
  public tipoEmissor: number;
  public status: number;
  public startDate: string;
  public endDate: string;
  public notasEntrada: NotaEntrada[];
  public qtdeNotasAcobrir = 0;

  constructor(private modal: NgbModal, 
              public listService: ListService,
              private toastrService: ToastrService,
              private sharedService: SharedService,
              private notaService: NotaService,
              private authService: AuthService) {
    super(listService, toastrService, authService);
  }

  ngOnInit() {
    this.componentName = "notaEntradaList";
    this.restauraList(this.componentName);

    this.tipoEmissor$ = this.sharedService.getTipoEmissor();
    this.tipoEmissor = 0;
    this.sharedService.getStatusNfEntrada().pipe(take(1)).subscribe(s => this.statusNfEntrada = s);
    this.status = 1;
    this.endDate = formatDate(new Date(), 'yyyy-MM-dd', 'pt');
    this.startDate = formatDate(new Date().setDate(new Date().getDate() - 30), 'yyyy-MM-dd', 'pt');

    this.getNotas();
  }

  uploadNota(){
    this.openModalUpload(NotaEntradaUploadModalComponent);
  }

  conciliarNota(notaEntradaId: string){
    this.openModalConciliacao(NotaConciliacaoModalComponent).componentInstance.notaEntradaId = notaEntradaId;
  }

  cobrirNota(notaEntradaId: string){
    this.openModalCobertura(NotaCoberturaModalComponent).componentInstance.notaEntradaId = notaEntradaId;
  }

  detalharNota(notaEntradaId: string){
    this.openModalDetalhes(NotaDetalheModalComponent).componentInstance.notaEntradaId = notaEntradaId;
  }

  definirTipoEmissor(notaEntradaId: string){
    this.openModalTipoEmissor(NotaTipoEmissorModalComponent).componentInstance.notaEntradaId = notaEntradaId;
  }

  getDescricaoStatus(status: string): string{
    return NotaEntradaHelper.getDescricaoStatus(status);
  }

  getDescricaoTipoEmissor(tipo: string){
    return NotaEntradaHelper.getDescricaoTipoEmissor(tipo);
  }

  showTipoEmissor(tipoEmissor: number){
    return tipoEmissor == TipoEmissor.Indefinido;
  }

  showConciliar(status: number, tipoEmissor: number){
    return status == StatusNotaEntrada.Conciliar && tipoEmissor != TipoEmissor.Indefinido;
  }

  showExcluir(status: number){
    return status == StatusNotaEntrada.Conciliar || status == StatusNotaEntrada.Inconsistente;
  }

  showCobrir(status: number){
    return status == StatusNotaEntrada["A Cobrir"];
  }

  getStatusClass(status: number): string {
    return NotaEntradaHelper.getStatusClass(status);
  }

  search() {
    this.listService.currentPage = 1;
    this.getNotas();
  }

  private getNotas() {
    this.notaService.getPaged(
      this.listService.currentPage,
      this.listService.itemsPerPage,
      this.tipoEmissor,
      this.status,
      this.startDate,
      this.endDate,
      this.listService.searchText).pipe(take(1))
      .subscribe(t => {
        this.notasEntrada = t.list;
        this.listService.totalItems = t.total;
        this.showPagination = this.listService.totalItems > this.listService.itemsPerPage;
        this.qtdeNotasAcobrir = this.notasEntrada.filter(n => n.status == StatusNotaEntrada["A Cobrir"]).length;
      })
  }

  private openModalUpload(component: any) : NgbModalRef {
    const modalRef = this.modal.open(component, { backdrop: 'static' });
    
    modalRef.result.then(result => {
      if(result == 'saved'){
        this.getNotas();
        this.toastrService.success('','Upload efetuado com sucesso!');
      }
    },
      reason => {});

    return modalRef;
  }

  private openModalDetalhes(component: any) : NgbModalRef {
    const modalRef = this.modal.open(component, { size: 'lg', backdrop: 'static' });
    
    modalRef.result.then(result => {
      result => {}
    },
      reason => {});

    return modalRef;
  }

  private openModalConciliacao(component: any) : NgbModalRef {
    const modalRef = this.modal.open(component, { size: 'lg', backdrop: 'static' });
    
    modalRef.result.then(result => {
      if(result == 'saved'){
        this.getNotas();
        this.toastrService.success('','Nota conciliada com sucesso!');
      }
      else if(result == 'refresh'){
        this.getNotas();
      }
    },
      reason => {});

    return modalRef;
  }

  private openModalCobertura(component: any) : NgbModalRef {
    const modalRef = this.modal.open(component, { size: 'lg', backdrop: 'static' });
    
    modalRef.result.then(result => {
      if(result == 'saved'){
        this.getNotas();
        this.toastrService.success('','Cobertura de nota efetuada com sucesso!');
      }
    },
      reason => {});

    return modalRef;
  }

  private openModalTipoEmissor(component: any) : NgbModalRef {
    const modalRef = this.modal.open(component, { size: 'sm', backdrop: 'static' });
    
    modalRef.result.then(result => {
        if(result == 'saved'){
          this.getNotas();
          this.toastrService.success('','Tipo Emissor definido com sucesso!');
        }
    },
      reason => {});

    return modalRef;
  }

  confirmDelete(id: string, nome: string) {
    swal.fire({
      title: 'Excluir Nota?',
      text: nome,
      type: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sim, excluir!',
      cancelButtonText: 'Não, cancelar!',
      confirmButtonClass: 'btn btn-success btn-raised mr-5',
      cancelButtonClass: 'btn btn-danger btn-raised',
      buttonsStyling: false
    }).then((result) => {
      if(result.value) {
        this.delete(id);
      }
    })
  }

  delete(id : string) {
    this.notaService.delete(id).pipe(take(1))
      .subscribe(() => {
        this.getNotas();
        this.toastrService.success("Nota excluída com sucesso!");
      });
  }

  pageChanged(event: any) {
    this.listService.currentPage = event;
    this.getNotas();
  }
}
