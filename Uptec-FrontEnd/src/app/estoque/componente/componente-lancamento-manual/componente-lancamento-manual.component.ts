import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { BaseFormComponent } from 'app/shared/base-form/base-form.component';
import { ComponenteMovimento } from '../models/componente';
import { FormValidatorHelper } from 'app/shared/helpers/form-validator-helper';
import { ComponenteService } from '../services/componente.service';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-componente-lancamento-manual',
  templateUrl: './componente-lancamento-manual.component.html'
})
export class ComponenteLancamentoManualComponent extends BaseFormComponent implements OnInit {

  @Input() componenteId;
  @Input() tipoMovimento;

  public componenteMovimento: ComponenteMovimento;

  constructor(public activeModal: NgbActiveModal,
    private service: ComponenteService) {
    super();
    this.formValidatorHelper = new FormValidatorHelper(ComponenteMovimento.validationMessages());
  }

  ngOnInit() {
    this.frm = ComponenteMovimento.buildForm();
  }

  submit(): void {
    let m = Object.assign({}, this.componenteMovimento, this.frm.value);

    m.componenteId = this.componenteId;

    if (this.tipoMovimento == 'E')
      m.tipoMovimento = 1;
    else
      m.tipoMovimento = 2;

    this.service.addMovimento(m).pipe(take(1))
      .subscribe(result => this.onSubmitComplete(result));
  }
  onSubmitComplete(data: any): void {
    this.errors = [];
    this.activeModal.close('saved');
  }

  close() {
    this.activeModal.close();
  }
}
