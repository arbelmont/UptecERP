import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { take } from 'rxjs/operators';
import { OrdemService } from '../services/ordem.service';
import { LoteService } from 'app/estoque/lote/services/lote.service';
import { Ordem } from '../models/ordem';
import { OrdemHelper } from '../helper/ordem-helper';
import { StatusOrdem } from 'app/shared/enums/statusOrdem';

@Component({
  selector: 'app-ordem-detalhe-modal',
  templateUrl: './ordem-detalhe-modal.html'
})
export class OrdemDetalheModalComponent implements OnInit  {

  @Input() ordemId;

  public ordem: Ordem;

  constructor(public activeModal: NgbActiveModal,
              private service: OrdemService,
              private loteService: LoteService) {
  }

  ngOnInit() {
    this.getOrdem();
  }
 
  getOrdem() {
    this.service.getFullById(this.ordemId).pipe(take(1))
      .subscribe(o => {
        this.ordem = o;
        this.ordem.ordemLotes.sort((l1, l2) => {
          if (l1.loteNumero > l2.loteNumero) return 1;
          if (l1.loteNumero < l2.loteNumero) return -1;
        });
      });
  }

  getDescricaoStatus(status: string): string {
    return OrdemHelper.getDescricaoStatus(status);
  }

  getDescricaoMotivo(motivo: string): string {
    return OrdemHelper.getDescricaoMotivo(motivo);
  }

  getStatusClass(status: number): string {
    return OrdemHelper.getStatusClass(status);
  }

  getLotes() {
    let lotes: string = "";

    if(!this.ordem)
      return;

    this.ordem.ordemLotes.forEach(item => {
      lotes += `${item.loteSequenciaString}, `;
    });

    return lotes.substring(0, lotes.length - 2);
  }

  getSobra(qtdeSolic: number, qtdeProd: number){
    let sobra = "";

    if(!this.ordem)
      return;

    if(this.ordem.status !== StatusOrdem.Producao )
      sobra = (qtdeSolic - qtdeProd).toString();
    
    return sobra;
  }

  public closeAlert() {
    return true;
  }

  close() {
    this.activeModal.close();
  }
}
