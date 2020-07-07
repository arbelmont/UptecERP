import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { take } from 'rxjs/operators';
import { NotaEntrada } from '../models/nota-entrada';
import { NotaService } from '../services/nota.service';
import { NotaEntradaHelper } from '../helper/nota-entrada-helper';
import { Observable } from 'rxjs';
import { EnumType } from 'app/shared/models/enumType';
import { BaseFormComponent } from 'app/shared/base-form/base-form.component';
import { NotaEntradaItemHelper } from '../helper/nota-entrada-item-helper';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nota-cobertura-modal',
  templateUrl: './nota-cobertura-modal.html'
})
export class NotaCoberturaModalComponent extends BaseFormComponent implements OnInit {
  
  @Input() notaEntradaId;

  public notaFornecedor: NotaEntrada;
  public notaCliente: NotaEntrada;
  public hasInconsistenciaFor: boolean = false;
  public hasInconsistenciaCli: boolean = true;
  public qtdeCobertura: number = 0;
  public dropDownClientes$: Observable<EnumType[]>;

  public alertCloseInconsistenciaFor = false;
  public alertCloseInconsistenciaCli = false;

  constructor(public activeModal: NgbActiveModal,
    private service: NotaService,
    private toastrService: ToastrService) {
    super();
  }

  ngOnInit() {
    this.getNota();
    this.dropDownClientes$ = this.service.getNotasClientesConciliar();
  }

  submit(): void {
    const conciliar = {
      "notaEntradaFornecedorId": this.notaFornecedor.id,
      "notaEntradaClienteId": this.notaCliente.id
    }
    this.service.cobrir(conciliar).pipe(take(1)).subscribe(
      result => this.onSubmitComplete(result)
    );
  }
  onSubmitComplete(data: any): void {
    this.activeModal.close('saved');
  }
 
  getNota() {
    this.service.getByIdConsisted(this.notaEntradaId).pipe(take(1))
      .subscribe(n => {
        this.notaFornecedor = n;
        console.log(this.notaFornecedor);
        this.hasInconsistenciaFor = this.notaFornecedor.inconsistencias.length > 0;
        this.alertCloseInconsistenciaFor = false;
      });
  }

  getNotaCobertura(id: string) {
    this.service.getByIdConsisted(id).pipe(take(1))
      .subscribe(n => {
        this.notaCliente = n;
        this.hasInconsistenciaCli = this.notaCliente.inconsistencias.length > 0;
        this.alertCloseInconsistenciaCli = false;
        this.checkItensCobrir();
      });
  }

  notaCoberturaChange(id: string){
    if(id == '0'){
      this.notaCliente = null;
      this.hasInconsistenciaCli = true;
      this.qtdeCobertura = 0;
      this.notaFornecedor.itens.forEach(i => i.cobrir = false);
    }
    else
      this.getNotaCobertura(id);
  }

  checkItensCobrir(){
    this.qtdeCobertura = 0;
    this.notaFornecedor.itens.forEach(i => {
      let itemMatch = this.notaCliente.itens.find(n => n.codigo === i.codigoCliente && 
          n.quantidade == i.quantidade && n.unidade === i.unidade);
      if(itemMatch){
        i.cobrir = true;
        this.qtdeCobertura++;
      }
    });
    if(this.qtdeCobertura > 0)
      this.toastrService.info(`Foram encontrados ${this.qtdeCobertura} item(s) equivalente(s) para cobertura!`);
    else
    this.toastrService.warning("Nenhum item encontrado para cobertura!");
  }

  getDescricaoStatus(status: string): string {
    return NotaEntradaHelper.getDescricaoStatus(status);
  }

  getItemDescricaoStatus(status: string): string {
    return NotaEntradaItemHelper.getDescricaoStatus(status);
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
