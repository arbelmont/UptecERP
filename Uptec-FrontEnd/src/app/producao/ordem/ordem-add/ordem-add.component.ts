import { OrdemAddPecaModalComponent } from './../ordem-add-peca-modal/ordem-add-peca-modal';
import { Peca } from 'app/estoque/peca/models/peca';
import { SharedService } from 'app/shared/services/shared.service';
import { OrdemLote, Ordem } from './../models/ordem';
import { Component, OnInit } from '@angular/core';
import { ListService } from 'app/shared/services/list.service';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'app/security/auth/auth.service';
import { BaseListComponet } from 'app/shared/base-list/base-list.component';
import { LoteService } from 'app/estoque/lote/services/lote.service';
import { Lote } from 'app/estoque/lote/models/lote';
import { take } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { EnumType } from 'app/shared/models/enumType';
import { OrdemService } from '../services/ordem.service';
import { NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PecaService } from 'app/estoque/peca/services/peca.service';

@Component({
  selector: 'app-ordem-add',
  templateUrl: './ordem-add.component.html'
})
export class OrdemAddComponent extends BaseListComponet implements OnInit {

  public lotes: Lote[];
  public itens: OrdemLote[];
  public ordem: Ordem;
  //public pecas$: Observable<EnumType[]>;
  public listaPecas: Peca[];
  public saldoProduzir: number = 0;
  public totalLotes: number = 0;
  //public pecaId: string = "0";
  public peca: Peca;
  public pesquisa: string = "";

  constructor(public listService: ListService,
    private service: OrdemService,
    private toastrService: ToastrService,
    private authService: AuthService,
    private sharedService: SharedService,
    private modal: NgbModal,
    private loteService: LoteService,
    private pecaService: PecaService) {
    super(listService, toastrService, authService)
  }

  ngOnInit() {
    this.componentName = "ordemAdd";
    this.restauraList(this.componentName);

    //this.pecas$ = this.sharedService.getPecas();
    this.lotes = [];
    this.ordem = new Ordem();
    this.itens = [];
  }

  submit() {
    this.ordem.ordemLotes = [];
    this.itens.forEach(i => this.ordem.ordemLotes.push(i))
    //console.log(this.ordem);

    this.service.add(this.ordem).pipe(take(1)).subscribe(
      result => this.onSubmitComplete(result)
    );

  }

  searchPeca() {
    this.limpar();
    this.pecaService.getToProducao(this.pesquisa)
      .pipe(take(1)).subscribe(p => {
        if (p.length == 1) {
          this.setPeca(p[0]);
        }
        else if (p.length > 1) {
          this.listaPecas = p;
          this.openModalPeca(OrdemAddPecaModalComponent)
            .componentInstance.pecas = this.listaPecas;
        }
        else {
          this.toastrService.warning("Peça não localizada", "Atenção!");
        }
      });
  }

  private setPeca(result: Peca) {
    console.log(result);
    this.limpar();
    this.peca = result;
    this.getLotes(this.peca.id);
  }

  onSubmitComplete(data: any) {

    this.limpar();
    this.pesquisa = "";
    this.toastrService.success("", "Ordem de produção emitida com sucesso!");
  }

  /* onChangePeca(){
    this.ordem.qtdeTotal = 0;
    this.lotes = [];
    this.itens = [];

    if(this.pecaId != "0"){
      this.getLotes(this.pecaId);
      return;
    }
  } */

  onChangeQuantidade() {
    this.itens = [];
  }

  aplicar() {
    if (!this.validAplicacao())
      return;

    this.itens = [];
    this.saldoProduzir = this.ordem.qtdeTotal;

    this.lotes.forEach(l => this.gerarOdermLote(l));
  }

  limpar() {
    this.peca = null;
    this.listaPecas = [];
    this.ordem.qtdeTotal = 0;
    this.lotes = [];
    this.itens = [];
  }

  updateTotalLotes() {
    let total = 0;
    this.lotes.forEach(lote => {
      total += lote.saldo;
    });

    this.totalLotes = total;
    this.ordem.qtdeTotal = total;
  }

  getItensTotal(): number {
    let total = 0;
    this.itens.forEach(item => {
      total += item.qtde;
    });
    return total;
  }

  private getLotes(pecaId: string) {
    this.loteService.getFullPagedByPeca(
      this.listService.currentPage,
      this.listService.itemsPerPage,
      pecaId,
      false).pipe(take(1))
      .subscribe(t => {
        if (t.total == 0) {
          this.toastrService.warning("Nenhum lote encontrado");
          return;
        }
        this.lotes = t.list;
        this.lotes.sort((l1, l2) => {
          if (l1.data > l2.data) return 1;
          if (l1.data < l2.data) return -1;
        });
        this.listService.totalItems = t.total;
        this.showPagination = this.listService.totalItems > this.listService.itemsPerPage;
        this.updateTotalLotes();
        this.toastrService.success(`${t.total} lote(s) encontrado(s).`);
      })
  }

  pageChanged(event: any) {
    throw new Error("Method not implemented.");
  }

  private gerarOdermLote(lote: Lote) {

    if (this.saldoProduzir <= 0)
      return;

    let item: OrdemLote = new OrdemLote();
    item.loteId = lote.id;
    item.loteNumero = lote.loteNumero;
    item.loteSequencia = lote.sequencia;
    item.data = lote.data;
    item.codigo = lote.peca.codigo;
    item.descricao = lote.peca.descricao;
    item.saldo = lote.saldo;
    item.loteSequenciaString = lote.loteSequenciaString;

    if (this.saldoProduzir >= item.saldo) {
      item.qtde = item.saldo;
      this.saldoProduzir -= lote.saldo;
    }
    else {
      item.qtde = this.saldoProduzir;
      this.saldoProduzir = 0;
    }
    this.itens.push(item);
  }

  private validAplicacao(): boolean {
    if (this.lotes.length <= 0) {
      this.toastrService.warning("", "Nenhum lote disponível!");
      return false;
    }

    if (this.ordem.qtdeTotal <= 0)
      return false;

    if (this.totalLotes < this.ordem.qtdeTotal) {
      this.toastrService.warning("", "Quantidade insuficiente para a produção desejada!");
      return false;
    }

    return true;
  }

  private openModalPeca(component: any): NgbModalRef {
    const modalRef = this.modal.open(component, { size: 'lg', backdrop: 'static' });

    modalRef.result.then(result => {
      if (result) {
        this.setPeca(result);
      }
    },
      reason => { });

    return modalRef;
  }
}
