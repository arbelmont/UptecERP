import { NotaSaidaService } from './../services/nota-saida.service';
import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { NotaSaida } from '../models/nota-saida';
import { NotaSaidaHelper } from '../helper/nota-saida-helper';
import { take } from 'rxjs/operators';
import { TipoDestinatario } from 'app/shared/enums/tipoDestinatario';
import { formatNumber } from '@angular/common';
import { StatusNotaSaida } from 'app/shared/enums/statusNotaSaida';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nota-saida-detalhe-modal',
  templateUrl: './nota-saida-detalhe-modal.html'
})
export class NotaSaidaDetalheModalComponent implements OnInit {

  @Input() notaSaidaId;

  public nota: NotaSaida;
  public hasInconsistencia: boolean = false;

  public alertCloseCobertura = false;
  public alertCloseInconsistencia = false;
  public showDatasConciliacao = false;

  constructor(public activeModal: NgbActiveModal,
    private toastrService: ToastrService,
    private service: NotaSaidaService) {
  }

  ngOnInit() {
    this.getNota();
  }

  getNota() {
    this.service.getWithStatusSefaz(this.notaSaidaId).pipe(take(1))
      .subscribe(n => {
        this.nota = n;
        if (this.nota.tipoDestinatario == TipoDestinatario.Cliente)
          this.nota.destinatario = this.nota.cliente;
        else
          this.nota.destinatario = this.nota.fornecedor;
      });
  }

  Cancelar() {
    this.service.CancelSefaz(this.notaSaidaId).pipe(take(1))
    .subscribe(n => {
      this.nota = n;
      if (this.nota.tipoDestinatario == TipoDestinatario.Cliente)
        this.nota.destinatario = this.nota.cliente;
      else
        this.nota.destinatario = this.nota.fornecedor;
    });
}

  reenviarNota() {
    this.service.reenviar(this.notaSaidaId).pipe(take(1))
      .subscribe(n => {
        this.nota = n;
        if (this.nota.tipoDestinatario == TipoDestinatario.Cliente)
          this.nota.destinatario = this.nota.cliente;
        else
          this.nota.destinatario = this.nota.fornecedor;
        
          this.toastrService.info("Acompanhe o status da nota junto a Sefaz...", "Nota Reenviada!");
      });
  }

  getDescricaoStatus(status: string): string {
    return NotaSaidaHelper.getDescricaoStatus(status);
  }

  getDescricaoTipoDestinatario(tipo: string) {
    return NotaSaidaHelper.getDescricaoTipoDestinatario(tipo);
  }

  getDescricaoUnidadeMedida(tipo: string) {
    return NotaSaidaHelper.getDescricaoUnidadeMedida(tipo);
  }

  getStatusClass(status: number): string {
    return NotaSaidaHelper.getStatusClass(status);
  }

  showDownloads() {
    if (this.nota)
      return this.nota.status == StatusNotaSaida.Processada;

    return false;
  }

  showReenviar() {
    if (this.nota)
      return this.nota.status == StatusNotaSaida.Rejeitada;

    return false;
  }

  showErroApi() {
    if(this.nota == null)
      return false;

    return this.nota.erroApi.length > 0;
  }

  percent(num: number) {
    if (num > 0) return `Alíquiota de ${num}%`;
    return "";
  }

  baseCalculo(num: number) {
    if (num > 0) return `Valor Base Cálculo ${formatNumber(num, 'pt')}`;
    return "";
  }

  LoteSequenciaString(lote: number, sequencia: number) {
    if (sequencia > 0)
      return `${lote}/${sequencia}`;
    return lote;
  }

  downloadPdf() {
    NotaSaidaHelper.downloadPdf(this.nota.numeroNota, this.service);
  }

  downloadXml() {
    NotaSaidaHelper.downloadXml(this.nota.numeroNota, this.service);
  }

  public closeAlert() {
    return true;
  }

  close() {
    this.activeModal.close();
  }
}
