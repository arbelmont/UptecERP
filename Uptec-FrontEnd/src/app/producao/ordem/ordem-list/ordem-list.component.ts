import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { AuthService } from 'app/security/auth/auth.service';
import { ListService } from 'app/shared/services/list.service';
import { ToastrService } from 'ngx-toastr';
import { OrdemService } from '../services/ordem.service';
import { BaseListComponet } from 'app/shared/base-list/base-list.component';
import { Ordem } from '../models/ordem';
import swal from 'sweetalert2';
import { OrdemHelper } from '../helper/ordem-helper';
import { StatusOrdem } from 'app/shared/enums/statusOrdem';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { OrdemExpedicaoModalComponent } from '../ordem-expedicao-modal/ordem-expedicao-modal';
import { EnumHelper } from 'app/shared/enums/enumHelper';
import { OrdemDetalheModalComponent } from '../ordem-detalhe-modal/ordem-detalhe-modal';

@Component({
  selector: 'app-ordem-list',
  templateUrl: './ordem-list.component.html'
})
export class OrdemListComponent extends BaseListComponet implements OnInit {
  
  public ordems: Ordem[];
  public status = EnumHelper.enumSelector(StatusOrdem);

  public pesquisaStatus = 0;
  public pesquisaPor = "peca";

  constructor(private modal: NgbModal,
              private ordemService: OrdemService,
              public listService: ListService,
              private toastrService: ToastrService,
              authService: AuthService) {
    super(listService, toastrService, authService);
  }

  ngOnInit() {
    this.componentName = "ordemList";
    this.restauraList(this.componentName);
    this.getOrdems();
  }

  search() {
    this.listService.currentPage = 1;
    if(!this.validaSearch())
    {
      this.toastrService.warning("",`${this.pesquisaPor} inválido.`);
      return;
    }
      
    this.getOrdems();
  }

  expedicao(ordemId: string){
    this.openModalConciliacao(OrdemExpedicaoModalComponent).componentInstance.ordemId = ordemId;
  }

  detalhes(ordemId: string){
    this.openModalDetalhe(OrdemDetalheModalComponent).componentInstance.ordemId = ordemId;
  }

  confirmDelete(id: string, nome: string) {
    swal.fire({
      title: 'Excluir ordem?',
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

  delete(id: string) {
    this.ordemService.delete(id).pipe(take(1))
      .subscribe(() => {
        this.getOrdems();
        this.toastrService.success("Ordem cancelada com sucesso!");
      });
  }

  private getOrdems() {
    this.ordemService.getPaged(
      this.listService.currentPage,
      this.listService.itemsPerPage,
      this.pesquisaStatus,
      this.pesquisaPor,
      this.listService.searchText).pipe(take(1))
      .subscribe(t => {
        this.ordems = t.list;
        this.listService.totalItems = t.total;
        this.showPagination = this.listService.totalItems > this.listService.itemsPerPage;
      })
  }

  getStatusClass(status: number): string {
    return OrdemHelper.getStatusClass(status);
  }

  getDescricaoStatus(status: string) {
    return OrdemHelper.getDescricaoStatus(status);
  }

  getLotes(ordemId: string) {
    let lotes: string = "";
    let ordem = this.ordems.find(o => o.id == ordemId);
    ordem.ordemLotes.sort((l1, l2) => {
      if (l1.loteNumero > l2.loteNumero) return 1;
      if (l1.loteNumero < l2.loteNumero) return -1;
    });
    ordem.ordemLotes.forEach(item => {
      lotes += `${item.loteSequenciaString.toString()}, `;
    });

    return lotes.substring(0, lotes.length - 2);
  }

  showExpedicao(status: number): boolean {
    return status == StatusOrdem.Producao;
  }

  showDelete(status: number): boolean {
    return status == StatusOrdem.Producao;
  }

  /* showEmitirNf(status: number): boolean {
    return status == StatusOrdem.Expedicao;
  } */

  pageChanged(event: any) {
    this.listService.currentPage = event;
    this.getOrdems();
  }

  private validaSearch(): boolean{
    if(this.pesquisaPor == "lote" || this.pesquisaPor == "ordem"){
      return !isNaN(Number(this.listService.searchText));
    }
    return true;
  }

  private openModalConciliacao(component: any) : NgbModalRef {
    const modalRef = this.modal.open(component, { size: 'lg', backdrop: 'static' });
    
    modalRef.result.then(result => {
      if(result == 'saved'){
        this.listService.searchText = "";
        this.getOrdems();
        this.toastrService.success('','Ordem de produção finalizada com sucesso!');
      }
    },
      reason => {});

    return modalRef;
  }

  private openModalDetalhe(component: any) : NgbModalRef {
    const modalRef = this.modal.open(component, { size: 'lg', backdrop: 'static' });
    
    modalRef.result.then(result => { },
      reason => {});

    return modalRef;
  }

}
