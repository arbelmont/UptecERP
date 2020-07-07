import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { LoteMovimento } from '../models/lote';
import { LoteService } from '../services/lote.service';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-lote-movimento-detalhe',
  templateUrl: './lote-movimento-detalhe.html'
})
export class LoteMovimentoDetalheComponent implements OnInit {

  @Input() movimentoId;
  @Input() loteNumero;

  public movimento: LoteMovimento;

  constructor(public activeModal: NgbActiveModal,
    private service: LoteService) {
  }

  ngOnInit() {
     this.service.getMovimentoById(this.movimentoId).pipe(take(1))
      .subscribe(m => this.movimento = m);
  }

  close() {
    this.activeModal.close();
  }
}
