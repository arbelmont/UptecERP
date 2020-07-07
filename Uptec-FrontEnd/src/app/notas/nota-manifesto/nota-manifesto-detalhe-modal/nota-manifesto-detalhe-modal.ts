import { TipoNfe } from 'app/shared/enums/tipoNfe';
import { SituacaoNfe } from './../../../shared/enums/situacaoNfe';
import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-nota-manifesto-detalhe-modal',
  templateUrl: './nota-manifesto-detalhe-modal.html'
})
export class NotaManifestoDetalheModalComponent implements OnInit {

  @Input() manifesto;

  constructor(public activeModal: NgbActiveModal) {
  }

  ngOnInit() {
  }

  notaCompleta(value: boolean) {
    return value ? "Sim" : "Não";
  }

  getDescricaoSituacao(situacao: string): string {
    return SituacaoNfe[situacao];
  }

  getDescricaoTipoNfe(tipo: string): string {
    return TipoNfe[tipo];
  }

  close() {
    this.activeModal.close();
  }
}
