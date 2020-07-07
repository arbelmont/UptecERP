import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { NotaEntrada, NotaEntradaTipoEmissor } from '../models/nota-entrada';
import { NotaService } from '../services/nota.service';
import { EnumHelper } from 'app/shared/enums/enumHelper';
import { TipoEmissorSelecao } from 'app/shared/enums/tipoEmissor';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-nota-tipoEmissor-modal',
  templateUrl: './nota-tipoEmissor-modal.html'
})
export class NotaTipoEmissorModalComponent implements OnInit  {

  @Input() notaEntradaId: string;

  public tiposEmissor = EnumHelper.enumSelector(TipoEmissorSelecao);
  public tipo: TipoEmissorSelecao = 0;

  constructor(public activeModal: NgbActiveModal, private service: NotaService) {
  }

  ngOnInit() {
    
  }

  submit(){
    let nota = new NotaEntradaTipoEmissor();
    nota.id = this.notaEntradaId;
    nota.tipoEmissor = this.tipo;

    console.log(nota);
    this.service.definirTipoEmissor(nota).pipe(take(1))
      .subscribe(result => this.activeModal.close('saved'));
  }

  public closeAlert() {
    return true;
  }

  close() {
    this.activeModal.close();
  }
}
