import { NotaService } from '../services/nota.service';
import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { BaseFormComponent } from 'app/shared/base-form/base-form.component';
import { FormValidatorHelper } from 'app/shared/helpers/form-validator-helper';
import { SharedService } from 'app/shared/services/shared.service';
import { Arquivo } from '../models/nota-entrada';
import { take } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { EnumType } from 'app/shared/models/enumType';

@Component({
  selector: 'app-nota-upload-modal',
  templateUrl: './nota-upload-modal.component.html'
})
export class NotaEntradaUploadModalComponent extends BaseFormComponent implements OnInit {
  
  public arquivo: Arquivo;
  public formData: FormData;
  public uploadMessage: string;
  
  constructor(public activeModal: NgbActiveModal,
              private sharedService: SharedService,
              private service: NotaService) {
    super();
    this.formValidatorHelper = new FormValidatorHelper(Arquivo.validationMessages());            
  }

  ngOnInit() {
    this.frm = Arquivo.buildForm();
  }

  onFileChange(event){
    this.uploadMessage = null;
    if(event.target.files && event.target.files.length > 0){
      let file = event.target.files[0];
      this.frm.patchValue({
        dados: file
      })
    };
  }

  submit(): void {
    this.validUpload();
    if(this.uploadMessage)
      return;

    this.formData = new FormData();
    this.formData.append('dados', this.frm.get('dados').value);

    this.service.upload(this.formData).pipe(take(1))
    .subscribe(result => this.onSubmitComplete(result));
    
  }

  onSubmitComplete(data: any): void {
    console.log(data);
    this.uploadMessage = null;
    this.activeModal.close('saved');
  }
  
  close(){
    this.activeModal.close();
  }

  private validUpload(){
    this.uploadMessage = '';
    let file = this.frm.get('dados').value;

    if(!file){
      this.uploadMessage = 'Nenhum arquivo selecionado'
      return;
    }

    if(file.size > 1048576){
      this.uploadMessage+= "O arquivo xml deve conter no máximo 1 Mbyte<br>"
    };
    if(file.type != "text/xml")
      this.uploadMessage+= "Formato do arquivo inválido <br>"
  }
}
