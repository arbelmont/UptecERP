import { DataHelper } from 'app/shared/helpers/data-helper';
import { OrdemLote } from './../models/ordem';
import { ToastrService } from 'ngx-toastr';
import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { BaseFormComponent } from 'app/shared/base-form/base-form.component';
import { Ordem } from '../models/ordem';
import { OrdemService } from '../services/ordem.service';
import { take } from 'rxjs/operators';
import { OrdemHelper } from '../helper/ordem-helper';
import { OrdemMotivoExpedicao } from 'app/shared/enums/ordemMotivoExpedicao';
import { EnumHelper } from 'app/shared/enums/enumHelper';

@Component({
  selector: 'app-ordem-expedicao-modal',
  templateUrl: './ordem-expedicao-modal.html'
})
export class OrdemExpedicaoModalComponent extends BaseFormComponent implements OnInit {

  @Input() ordemId;

  public ordem: Ordem;
  public motivosExpedicao = EnumHelper.enumSelector(OrdemMotivoExpedicao);

  constructor(public activeModal: NgbActiveModal,
    private service: OrdemService,
    private toastr: ToastrService) {
    super();
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
        this.ordem.ordemLotes.forEach(o => {
          o.motivoExpedicao = OrdemMotivoExpedicao.Total;
          o.validade = new Date();
          o.validade.setDate(o.validade.getDate()+30);
        });
      });
  }

  submit(): void {
    if (!this.validar())
      return;

    let o: any = {
      id: this.ordem.id,
      ordemLotes: this.ordem.ordemLotes
    };

    this.service.finalizar(o).pipe(take(1)).subscribe(
      result => this.onSubmitComplete(result)
    );
  }

  onSubmitComplete(data: any): void {
    this.activeModal.close('saved');
  }

  getStatusClass(status: number): string {
    return OrdemHelper.getStatusClass(status);
  }

  getDescricaoStatus(status: string) {
    return OrdemHelper.getDescricaoStatus(status);
  }

  getCodigo(ordemId: string) {
    return this.ordem.ordemLotes[0].lote.peca.codigo;
  }

  getDescricao(ordemId: string) {
    return this.ordem.ordemLotes[0].lote.peca.descricao;
  }

  getLotes(ordemId: string) {
    let lotes: string = "";

    this.ordem.ordemLotes.forEach(item => {
      lotes += `${item.loteSequenciaString.toString()}, `;
    });

    return lotes.substring(0, lotes.length - 2);
  }

  showQuantidade(motivo: number): boolean {
    return motivo != OrdemMotivoExpedicao.Total;
  }

  closeAlert() {
    return true;
  }

  close() {
    this.activeModal.close();
  }

  private validar(): boolean {
    let resultValidation = "";

    this.ordem.ordemLotes.forEach(item => {
      if(item.motivoExpedicao == OrdemMotivoExpedicao.Total)
        item.qtdeProduzida = item.qtde;
        
      if (item.qtdeProduzida <= 0)
        resultValidation += `Quantidade Produzida não informada para o Lote: ${item.loteNumero}. `;

      if(new Date(item.validade) <= new Date())
        resultValidation += `Data de Validade do Lote: ${item.loteNumero} é inválida.`;
    });

    if (resultValidation != "") {
      this.toastr.warning(resultValidation, "Atenção!!");
      return false;
    }
    return true;
  }
}
