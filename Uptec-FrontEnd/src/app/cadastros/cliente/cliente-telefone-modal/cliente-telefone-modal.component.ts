import { Component, OnInit, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { EnumType } from 'app/shared/models/enumType';
import { TelefoneCliente } from '../models/cliente';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { SharedService } from 'app/shared/services/shared.service';
import { ClienteService } from '../services/cliente.service';
import { BaseFormComponent } from 'app/shared/base-form/base-form.component';
import { take } from 'rxjs/operators';
import { FormValidatorHelper } from 'app/shared/helpers/form-validator-helper';

@Component({
  selector: 'app-cliente-telefone-modal',
  templateUrl: './cliente-telefone-modal.component.html'
})
export class ClienteTelefoneModalComponent extends BaseFormComponent implements OnInit {

  @Input() clienteId;
  @Input() telefoneId;

  public telefoneTipos$: Observable<EnumType[]>;
  public telefone: TelefoneCliente;

  constructor(public activeModal: NgbActiveModal,
    private sharedService: SharedService,
    private service: ClienteService) {
    super();
    this.formValidatorHelper = new FormValidatorHelper(TelefoneCliente.validationMessages());
  }

  ngOnInit() {
    this.frm = TelefoneCliente.buildForm();
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
    t.clienteId = this.clienteId;

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
      clienteId: telefone.clienteId,
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
