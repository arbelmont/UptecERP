import { Component, OnInit, Input, AfterViewChecked } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { take } from 'rxjs/operators';
import { NotaEntrada } from '../models/nota-entrada';
import { NotaService } from '../services/nota.service';
import { NotaEntradaHelper } from '../helper/nota-entrada-helper';
import { TipoEstoque } from 'app/shared/enums/tipoEstoque';
import { Observable } from 'rxjs';
import { Lote } from 'app/estoque/lote/models/lote';
import { LoteService } from 'app/estoque/lote/services/lote.service';
import { NotaEntradaItemHelper } from '../helper/nota-entrada-item-helper';

@Component({
  selector: 'app-nota-detalhe-modal',
  templateUrl: './nota-detalhe-modal.html'
})
export class NotaDetalheModalComponent implements OnInit  {

  @Input() notaEntradaId;

  public nota: NotaEntrada;
  public hasInconsistencia: boolean = false;
  public lotes$: Observable<Lote[]>;

  public alertCloseCobertura = false;
  public alertCloseInconsistencia = false;
  public showDatasConciliacao = false;

  constructor(public activeModal: NgbActiveModal,
              private service: NotaService,
              private loteService: LoteService) {
  }

  ngOnInit() {
    this.getNota();
  }
 
  getNota() {
    this.service.getByIdConsisted(this.notaEntradaId).pipe(take(1))
      .subscribe(n => {
        this.nota = n;
        this.hasInconsistencia = this.nota.inconsistencias.length > 0;
        this.showDatasConciliacao = (this.nota.tipoEstoque == TipoEstoque.Peca);
        this.getNotasCobertura()
        this.lotes$ = this.loteService.getByNumeroNota(this.nota.numeroNota);
      });
  }

  getNotasCobertura(){
    let nfes: string = "";

    this.nota.itens.forEach(item => {
      nfes += item.numeroNotaCobertura.toString() + ", ";
    });

    this.nota.numeroNotaCobertura = nfes.substring(0, nfes.length - 2);
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

  getItemDescricaoStatus(status: string): string {
    return NotaEntradaItemHelper.getDescricaoStatus(status);
  }

  getItemStatusClass(status: number): string {
    return NotaEntradaItemHelper.getStatusClass(status);
  }

  public closeAlert() {
    return true;
  }

  close() {
    this.activeModal.close();
  }
}
