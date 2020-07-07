import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { take } from 'rxjs/operators';
import { ComponenteMovimento } from '../models/componente';
import { ComponenteService } from '../services/componente.service';

@Component({
  selector: 'app-componente-movimento-detalhe',
  templateUrl: './componente-movimento-detalhe.html'
})
export class ComponenteMovimentoDetalheComponent implements OnInit {

  @Input() movimentoId;

  public movimento: ComponenteMovimento;

  constructor(public activeModal: NgbActiveModal,
    private service: ComponenteService) {
  }

  ngOnInit() {
    this.service.getMovimentoById(this.movimentoId).pipe(take(1))
      .subscribe(m => this.movimento = m);
  }

  close() {
    this.activeModal.close();
  }
}
