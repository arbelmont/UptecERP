import { Component, OnInit, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { EnumType } from 'app/shared/models/enumType';
import { TelefoneFornecedor } from '../models/fornecedor';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { SharedService } from 'app/shared/services/shared.service';
import { FornecedorService } from '../services/fornecedor.service';
import { BaseFormComponent } from 'app/shared/base-form/base-form.component';
import { take } from 'rxjs/operators';
import { FormValidatorHelper } from 'app/shared/helpers/form-validator-helper';

@Component({
  selector: 'app-fornecedor-telefone-modal',
  templateUrl: './fornecedor-telefone-modal.component.html'
})
export class FornecedorTelefoneModalComponent extends BaseFormComponent implements OnInit {

  @Input() fornecedorId;
  @Input() telefoneId;

  public telefoneTipos$: Observable<EnumType[]>;
  public telefone: TelefoneFornecedor;

  constructor(public activeModal: NgbActiveModal,
    private sharedService: SharedService,
    private service: FornecedorService) {
    super();
    this.formValidatorHelper = new FormValidatorHelper(TelefoneFornecedor.validationMessages());
  }

  ngOnInit() {
    this.frm = TelefoneFornecedor.buildForm();
    this.telefoneTipos$ = this.sharedService.getTelefoneTipos();

    if (this.telefoneId) {
      this.service.getTelefone(this.telefoneId).pipe(take(1))
        .subscribe(t => this.fillForm(t));
    }
  }

  submit(): void {
    let t = Object.assign({}, this.telefone, this.frm.value);
    t.numero = t.numerot;
    t.observacoes = t.observacoest;
    t.fornecedorId = this.fornecedorId;

    if(this.telefoneId) {
      t.id = this.telefoneId;
      this.service.updateTelefone(t).pipe(take(1))
      .subscribe(result => this.onSubmitComplete(result));
    }
    else {
      this.service.addTelefone(t).pipe(take(1))
      .subscribe(result => this.onSubmitComplete(result));
    }
  }
  onSubmitComplete(data: any): void {
    this.errors = [];
    this.activeModal.close('telefoneSaved');
  }

  fillForm(telefone: any) {
    this.frm.patchValue({
      id: telefone.id,
      fornecedorId: telefone.fornecedorId,
      numerot: telefone.numero,
      tipo: telefone.tipo,
      whatsapp: telefone.whatsapp,
      observacoest: telefone.observacoes,
      contato: telefone.contato
    });
  }

  close(){
    this.activeModal.close();
  }
}
