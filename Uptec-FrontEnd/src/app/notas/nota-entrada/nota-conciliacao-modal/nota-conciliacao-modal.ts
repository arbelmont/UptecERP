import { NotaEntradaItens } from './../models/nota-entrada';
import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal, NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { take, groupBy } from 'rxjs/operators';
import { NotaEntrada } from '../models/nota-entrada';
import { NotaService } from '../services/nota.service';
import { NotaEntradaHelper } from '../helper/nota-entrada-helper';
import { TipoEstoque } from 'app/shared/enums/tipoEstoque';
import { BaseFormComponent } from 'app/shared/base-form/base-form.component';
import { LoteService } from 'app/estoque/lote/services/lote.service';
import { TipoEmissor } from 'app/shared/enums/tipoEmissor';
import { NotaTipoEmissorModalComponent } from '../nota-tipoEmissor-modal/nota-tipoEmissor-modal';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nota-conciliacao-modal',
  templateUrl: './nota-conciliacao-modal.html'
})
export class NotaConciliacaoModalComponent extends BaseFormComponent implements OnInit {

  @Input() notaEntradaId;

  public nota: NotaEntrada;
  public hasInconsistencia: boolean = false;
  public refreshList: boolean = false;

  public alertCloseCobertura = false;
  public alertCloseInconsistencia = false;
  public showDatasConciliacao = false;
  public nextLote: number;

  constructor(public activeModal: NgbActiveModal,
    private modal: NgbModal,
    private service: NotaService,
    private loteService: LoteService,
    private toast: ToastrService) {
    super();
  }

  ngOnInit() {
    this.getNota();
  }
 
  getNota() {
    this.service.getByIdConsisted(this.notaEntradaId).pipe(take(1))
      .subscribe(n => {
        this.nota = n;
        this.hasInconsistencia = this.nota.inconsistencias.length > 0 ;
        this.showDatasConciliacao = (this.nota.tipoEstoque == TipoEstoque.Peca);
        this.loteService.GetLoteSequenceLastUsed().pipe(take(1)).subscribe(s => {
          this.nextLote = s;
          this.setLote();
        });
      });
  }

  submit(): void {
    if(!this.validaConciliacao())
      return;
      
    this.service.conciliar(this.nota).pipe(take(1)).subscribe(
      result => this.onSubmitComplete(result)
    );
  }

  onSubmitComplete(data: any): void {
    this.activeModal.close('saved');
  }

  getDescricaoStatus(status: string): string {
    return NotaEntradaHelper.getDescricaoStatus(status);
  }

  getDescricaoTipoEmissor(tipo: string) {
    return NotaEntradaHelper.getDescricaoTipoEmissor(tipo);
  }

  getDescricaoUnidadeMedida(tipo: string) {
    return NotaEntradaHelper.getDescricaoUnidadeMedida(tipo);
  }

  getStatusClass(status: number): string {
    return NotaEntradaHelper.getStatusClass(status);
  }

  incrementLote(index: number){
    return this.nextLote + index;
  }

  showDefinirEmissor(tipo: number){
    return tipo == TipoEmissor.Indefinido;
  }

  /* setLote(){
    let itens: NotaEntradaItens[] = [];
    
    this.nota.itens.forEach(el => {
      let match = itens.findIndex(x => x.codigo == el.codigo);
      if(match < 0) {
        el.lote = this.nextLote;
        this.nextLote++;
        itens.push(el);
      }
    });

    this.nota.itens.forEach(el => {
      let lote = itens.find(x => x.codigo === el.codigo).lote;
      el.lote = lote;
    });
    console.log(itens);
    console.log(this.nota.itens);
  } */

  setLote() {
    this.nota.itens.forEach(i => {
      i.lote = this.nextLote;
      this.nextLote++;
    })
  }

  definirTipoEmissor(){
    this.openModalTipoEmissor(NotaTipoEmissorModalComponent).componentInstance.notaEntradaId = this.notaEntradaId;
  }

  private openModalTipoEmissor(component: any) : NgbModalRef {
    const modalRef = this.modal.open(component, { size: 'sm', backdrop: 'static' });
    
    modalRef.result.then(result => {
        if(result == 'saved'){
          this.refreshList = true;
          this.getNota();
        }
    },
      reason => {});

    return modalRef;
  }

  public closeAlert() {
    return true;
  }

  close() {
    if(this.refreshList)
      this.activeModal.close("refresh");
    else
      this.activeModal.close();
  }

  validaConciliacao() : boolean {
    if(this.nota.tipoEstoque == TipoEstoque.MateriaPrima)
      return true;
      
    let retorno = true;

    this.nota.itens.forEach(item => {
      if(item.dataFabricacao == undefined) {
        this.toast.warning(`Data de fabricação inválida para a peça ${item.descricao}`);
        retorno = false;
      }
      if(item.dataValidade == undefined) {
        this.toast.warning(`Data de validade inválida para a peça ${item.descricao}`);
        retorno = false;
      }
    });

    return retorno;
  }
}
