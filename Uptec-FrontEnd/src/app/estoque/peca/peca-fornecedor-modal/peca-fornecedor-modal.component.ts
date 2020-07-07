import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { BaseFormComponent } from 'app/shared/base-form/base-form.component';
import { FormValidatorHelper } from 'app/shared/helpers/form-validator-helper';
import { SharedService } from 'app/shared/services/shared.service';
import { PecaFornecedorCodigo } from '../models/peca';

@Component({
  selector: 'app-peca-fornecedor-modal',
  templateUrl: './peca-fornecedor-modal.component.html'
})
export class PecaFornecedorModalComponent extends BaseFormComponent implements OnInit {
  
  @Input() pecaId;
  @Input() fornecedorId;
  @Input() fornecedores;
  @Input() fornecedorCodigo;

  public pecaFornecedorCodigo: PecaFornecedorCodigo;

  constructor(public activeModal: NgbActiveModal,
              private sharedService: SharedService) {
    super();
    this.formValidatorHelper = new FormValidatorHelper(PecaFornecedorCodigo.validationMessages());            
  }

  ngOnInit() {
    this.frm = PecaFornecedorCodigo.buildForm();
    if(this.fornecedorId)
      this.fillForm();
  }

  submit(): void {
    let f = Object.assign({}, this.pecaFornecedorCodigo, this.frm.value)
    //c.quantidade = +c.quantidade;
    
    if(this.pecaId){
      f.pecaId = this.pecaId;
    }

    this.activeModal.close(f);
  }
  onSubmitComplete(data: any): void {
    
  }

  fillForm(){
    this.frm.patchValue({
      fornecedorId: this.fornecedorId,
      fornecedorCodigo: this.fornecedorCodigo
    });
  }

  close(){
    this.activeModal.close();
  }
}
