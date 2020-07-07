import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { BaseFormComponent } from 'app/shared/base-form/base-form.component';
import { LoteMovimento } from '../models/lote';
import { FormValidatorHelper } from 'app/shared/helpers/form-validator-helper';
import { LoteService } from '../services/lote.service';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-lote-lancamento-manual',
  templateUrl: './lote-lancamento-manual.component.html'
})
export class LoteLancamentoManualComponent extends BaseFormComponent implements OnInit {

  @Input() loteId;
  @Input() tipoMovimento;
  @Input() dropdownLotes;

  public loteMovimento: LoteMovimento;

  constructor(public activeModal: NgbActiveModal,
    private service: LoteService) {
    super();
    this.formValidatorHelper = new FormValidatorHelper(LoteMovimento.validationMessages());
  }

  ngOnInit() {
    this.frm = LoteMovimento.buildForm();
    if (this.loteId) {
      this.frm.patchValue({
        loteId: this.loteId
      });
    }
  }

  submit(): void {
    let m = Object.assign({}, this.loteMovimento, this.frm.value);

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
