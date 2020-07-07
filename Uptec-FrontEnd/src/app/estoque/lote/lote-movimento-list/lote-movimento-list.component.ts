import { LoteMovimentoDetalheComponent } from './../lote-movimento-detalhe/lote-movimento-detalhe';
import { Component, OnInit, Input } from '@angular/core';
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
import { Peca } from 'app/estoque/peca/models/peca';
import { PecaService } from 'app/estoque/peca/services/peca.service';

@Component({
  selector: 'app-lote-movimento-list',
  templateUrl: './lote-movimento-list.component.html'
})
export class LoteMovimentoListComponent  extends BaseListComponet implements OnInit {

  public peca: Peca;
  public lote: Lote;
  public movimentos: LoteMovimento[];
  public loteId;
  public lotes: Lote[] = [];
  
  constructor(private loteService: LoteService,
              private route: ActivatedRoute,
              public listService: ListService,
              private toastrService: ToastrService,
              private modal: NgbModal,
              private authService: AuthService) {
    super(listService, toastrService, authService);
  }

  ngOnInit() {
    this.entityId = this.route.snapshot.paramMap.get('id');
    this.getLote();
  }

  detalheLote(){
    console.log("detalhe");
  }

  getLote(){
    this.loteService.getFull(this.entityId).pipe(take(1))
      .subscribe(l => {this.lote = l; this.lotes.push(l); this.getMovimentos();})
  }

  getMovimentos(){
    this.loteService.getPagedMovimentoByLote(
      this.listService.currentPage,
      this.listService.itemsPerPage,
      this.entityId).pipe(take(1))
      .subscribe(lm => {
        this.movimentos = lm.list;
        this.listService.totalItems = lm.total;
        this.showPagination = this.listService.totalItems > this.listService.itemsPerPage;
      })
  }

  getClass(tipo: number) : string {
    if(tipo == 1) return "text-info";
    return "text-danger";
  }

  getClassLoteSelected(loteId: string){
    if(loteId === this.entityId) return "text-bold-700";
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
    //this.openModal(LoteMovimentoDetalheComponent).componentInstance.movimentoId = movimentoId;
    const modalRef = this.modal.open(LoteMovimentoDetalheComponent);
    modalRef.componentInstance.movimentoId = movimentoId;
    modalRef.componentInstance.loteNumero = this.lote.loteNumero;
  }

  pageChanged(event: any) {
    this.listService.currentPage = event;
    this.getMovimentos();
  }

  private openModal(component: any) : NgbModalRef {
    const modalRef = this.modal.open(component, { backdrop: 'static' });
    
    modalRef.result.then(result => {
      if(result == 'saved'){
        this.getLote();
        this.toastrService.success('','Lançamento efetuado com sucesso!');
      }
    },
      reason => {});
    
    modalRef.componentInstance.loteId = this.entityId;
    modalRef.componentInstance.dropdownLotes = this.lotes;
    return modalRef;
  }
}
