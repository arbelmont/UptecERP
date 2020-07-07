import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { BaseFormComponent } from 'app/shared/base-form/base-form.component';
import { FormValidatorHelper } from 'app/shared/helpers/form-validator-helper';
import { SharedService } from 'app/shared/services/shared.service';
import { PecaComponente } from '../models/peca';

@Component({
  selector: 'app-peca-componente-modal',
  templateUrl: './peca-componente-modal.component.html'
})
export class PecaComponenteModalComponent extends BaseFormComponent implements OnInit {
  
  @Input() pecaId;
  @Input() componenteId;
  @Input() componentes;
  @Input() quantidade;

  public pecaComponente: PecaComponente;

  constructor(public activeModal: NgbActiveModal,
              private sharedService: SharedService) {
    super();
    this.formValidatorHelper = new FormValidatorHelper(PecaComponente.validationMessages());            
  }

  ngOnInit() {
    this.frm = PecaComponente.buildForm();

    if(this.componenteId)
      this.fillForm();
  }

  submit(): void {
    let c = Object.assign({}, this.pecaComponente, this.frm.value)
    c.quantidade = +c.quantidade;
    
    if(this.pecaId){
      c.pecaId = this.pecaId;
    }

    this.activeModal.close(c);
  }

  onSubmitComplete(data: any): void {
    
  }

  fillForm(){
    this.frm.patchValue({
      componenteId: this.componenteId,
      quantidade: this.quantidade
    });
  }

  close(){
    this.activeModal.close();
  }
}
