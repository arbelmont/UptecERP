import { ComponenteMovimentoDetalheComponent } from './../componente-movimento-detalhe/componente-movimento-detalhe';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Componente } from '../models/componente';
import { ComponenteService } from '../services/componente.service';
import { ActivatedRoute } from '@angular/router';
import { take } from 'rxjs/operators';
import { BaseListComponet } from 'app/shared/base-list/base-list.component';
import { ListService } from 'app/shared/services/list.service';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'app/security/auth/auth.service';
import { formatDate } from '@angular/common';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ComponenteLancamentoManualComponent } from '../componente-lancamento-manual/componente-lancamento-manual.component';

@Component({
  selector: 'app-componente-movimento',
  templateUrl: './componente-movimento.component.html'
})
export class ComponenteMovimentoComponent extends BaseListComponet implements OnInit {

  public componente: Componente = new Componente();
  public movimentos: any[];
  public entityId: string;
  public startDate: string;
  public endDate: string;

  constructor(private service: ComponenteService,
    private route: ActivatedRoute,
    public listService: ListService,
    private toastrService: ToastrService,
    private authService: AuthService,
    private modal: NgbModal) {
    super(listService, toastrService, authService);
  }

  ngOnInit() {
    this.componentName = "componenteMovimento";
    this.restauraList(this.componentName);

    this.entityId = this.route.snapshot.paramMap.get('id');
    this.endDate = formatDate(new Date(), 'yyyy-MM-dd', 'pt');
    this.startDate = formatDate(new Date().setDate(new Date().getDate() - 30), 'yyyy-MM-dd', 'pt');

    this.service.get(this.entityId).pipe(take(1)).subscribe(c => this.componente = c);
    this.getMovimentos();
  }

  pageChanged(event: any) {
    this.listService.currentPage = event;
    this.getMovimentos();
  }

  getTipoMovimento(tipo: number) : string {
    if(tipo == 1) return "Entrada";
    return "Saída";
  }

  getClass(tipo: number) : string {
    if(tipo == 1) return "text-info";
    return "text-danger";
  }

  getMovimentos() {
    this.service.getPagedMovimento(this.listService.currentPage, 
                                   this.listService.itemsPerPage, 
                                   this.entityId, 
                                   this.startDate, 
                                   this.endDate)
      .pipe(take(1)).subscribe(m => {
        this.movimentos = m.list;
        this.listService.totalItems = m.total;
        this.showPagination = this.listService.totalItems > this.listService.itemsPerPage;
      });
  }

  addMovimento(tipo: string){
    this.openModal(ComponenteLancamentoManualComponent).componentInstance.tipoMovimento = tipo;
  }

  showMovimentoDetatlhe(movimentoId: string){
    this.modal.open(ComponenteMovimentoDetalheComponent).componentInstance.movimentoId = movimentoId;
  }

  private openModal(component: any) : NgbModalRef {
    const modalRef = this.modal.open(component, { backdrop: 'static' });
    
    modalRef.result.then(result => {
      if(result == 'saved'){
        this.getMovimentos();
        this.service.get(this.entityId).pipe(take(1)).subscribe(c => this.componente = c);
        this.toastrService.success('','Lançamento efetuado com sucesso!');
      }
    },
      reason => {});
    
    modalRef.componentInstance.componenteId = this.entityId;
    return modalRef;
  }
}
