import { Component, OnInit, Input } from '@angular/core';
import { Peca } from '../models/peca';
import { PecaService } from '../services/peca.service';
import { ActivatedRoute } from '@angular/router';
import { take } from 'rxjs/operators';
import { BaseListComponet } from 'app/shared/base-list/base-list.component';
import { ListService } from 'app/shared/services/list.service';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'app/security/auth/auth.service';
import { LoteService } from 'app/estoque/lote/services/lote.service';
import { Lote, LoteMovimento } from 'app/estoque/lote/models/lote';
import { NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { LoteLancamentoManualComponent } from 'app/estoque/lote/lote-lancamento-manual/lote-lancamento-manual.component';
import { LoteMovimentoDetalheComponent } from 'app/estoque/lote/lote-movimento-detalhe/lote-movimento-detalhe';

@Component({
  selector: 'app-peca-lote-list',
  templateUrl: './peca-lote-list.component.html'
})
export class PecaLoteListComponent  extends BaseListComponet implements OnInit {

  public peca: Peca;
  public lotes: Lote[];
  public loteMovimentos: LoteMovimento[];
  public loteId;
  public loteNumero;
  
  constructor(private pecaService: PecaService,
              private loteService: LoteService,
              private route: ActivatedRoute,
              public listService: ListService,
              private toastrService: ToastrService,
              private modal: NgbModal,
              private authService: AuthService) {
    super(listService, toastrService, authService);
  }

  ngOnInit() {
    this.entityId = this.route.snapshot.paramMap.get('id');
    this.pecaService.get(this.entityId).pipe(take(1)).subscribe(p => {this.peca = p; this.getLotes()});
  }

  detalheLote(){
    console.log("detalhe");
  }

  getMovimentos(loteId: string, loteNumero: number){
    this.loteId = loteId;
    this.loteNumero = loteNumero;
    this.loteService.getPagedMovimentoByLote(
      this.listService.currentPage2,
      this.listService.itemsPerPage2,
      this.loteId).pipe(take(1))
      .subscribe(lm => {
        this.loteMovimentos = lm.list;
        this.listService.totalItems2 = lm.total;
        this.showPagination = this.listService.totalItems2 > this.listService.itemsPerPage2;
      })
  }

  private getLotes() {
    this.loteService.getPagedByPeca(
      this.listService.currentPage,
      this.listService.itemsPerPage, 
      this.peca.id, 
      false).pipe(take(1))
      .subscribe(t => {
        this.lotes = t.list;
        this.listService.totalItems = t.total;
        this.showPagination = this.listService.totalItems > this.listService.itemsPerPage;
      })
  }

  getClass(tipo: number) : string {
    if(tipo == 1) return "text-info";
    return "text-danger";
  }

  getClassLoteSelected(loteId: string){
    if(loteId === this.loteId) return "text-bold-700";
    return "";
  }

  getTipoMovimento(tipo: number) : string {
    if(tipo == 1) return "Entrada";
    return "Saída";
  }

  addMovimento(tipo: string){
    this.openModal(LoteLancamentoManualComponent).componentInstance.tipoMovimento = tipo;
  }

  showMovimentoDetatlhe(movimentoId: string){
    const modalRef = this.modal.open(LoteMovimentoDetalheComponent);
    modalRef.componentInstance.movimentoId = movimentoId;
    modalRef.componentInstance.loteNumero = this.loteNumero;
  }

  pageChanged(event: any) {
    this.listService.currentPage2 = event;
    this.getLotes();
  }
  pageChanged2(event: any) {
    this.listService.currentPage2 = event;
    this.getMovimentos(this.loteId, this.loteNumero);
  }

  private openModal(component: any) : NgbModalRef {
    const modalRef = this.modal.open(component, { backdrop: 'static' });
    
    modalRef.result.then(result => {
      if(result == 'saved'){
        this.getLotes();
        this.getMovimentos(this.loteId, this.loteNumero);
        this.toastrService.success('','Lançamento efetuado com sucesso!');
      }
    },
      reason => {});
    
    modalRef.componentInstance.loteId = this.loteId;
    modalRef.componentInstance.dropdownLotes = this.lotes;
    return modalRef;
  }
}
