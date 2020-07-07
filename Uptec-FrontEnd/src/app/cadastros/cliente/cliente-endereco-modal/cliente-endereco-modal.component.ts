import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { BaseFormComponent } from 'app/shared/base-form/base-form.component';
import { EnderecoCliente } from '../models/cliente';
import { FormValidatorHelper } from 'app/shared/helpers/form-validator-helper';
import { Observable, empty } from 'rxjs';
import { Estado } from 'app/shared/models/estado';
import { SharedService } from 'app/shared/services/shared.service';
import { Cidade } from 'app/shared/models/cidade';
import { take } from 'rxjs/operators';
import { EnumType } from 'app/shared/models/enumType';
import { CepService } from 'app/shared/services/cep.service';
import { ClienteService } from '../services/cliente.service';

@Component({
  selector: 'app-cliente-endereco-modal',
  templateUrl: './cliente-endereco-modal.component.html'
})
export class ClienteEnderecoModalComponent extends BaseFormComponent implements OnInit {
  
  @Input() clienteId;
  @Input() enderecoId;

  public enderecoTipos$: Observable<EnumType[]>;
  public estados$: Observable<Estado[]>;
  public cidades: Cidade[];

  public endereco: EnderecoCliente;

  constructor(public activeModal: NgbActiveModal,
              private sharedService: SharedService,
              private cepService: CepService,
              private service: ClienteService) {
    super();
    this.formValidatorHelper = new FormValidatorHelper(EnderecoCliente.validationMessages());            
  }

  ngOnInit() {
    this.frm = EnderecoCliente.buildForm();

    this.enderecoTipos$ = this.sharedService.getEnderecoTipos();
    this.estados$ = this.sharedService.getEstados();
    this.frm.get('estado').valueChanges
      .subscribe(uf => this.getCidades(uf));
    
    if(this.enderecoId){
      this.service.getEndereco(this.enderecoId).pipe(take(1))
        .subscribe(e => this.fillForm(e));
    }
  }

  submit(): void {
    let e = Object.assign({}, this.endereco, this.frm.value);

    e.clienteId = this.clienteId;
    e.tipo = e.tipoEndereco;

    if(this.enderecoId){
      e.id = this.enderecoId;
      this.service.updateEndereco(e).pipe(take(1))
      .subscribe(result => this.onSubmitComplete(result));
    }
    else {
      this.service.addEndereco(e).pipe(take(1))
      .subscribe(result => this.onSubmitComplete(result));
    } 
  }
  onSubmitComplete(data: any): void {
    this.errors = [];
    this.activeModal.close('enderecoSaved');
  }

  getCidades(uf: string) {
    if(uf == '') {
      this.cidades = [];
      return;
    }

    this.sharedService.getCidades(uf).pipe(take(1))
      .subscribe(dados => this.cidades = dados);
  }

  buscaCep(){
    let cep = this.frm.get('cep').value;
    this.cepService.consultaCep(cep).pipe(take(1))
    .subscribe(
      dados => this.fillCepEndereco(dados),
      error => {empty}
    )
  }

  fillCepEndereco(dados: any) {
    if(!dados.hasOwnProperty('logradouro'))
      return

    this.frm.patchValue({
        logradouro: dados.logradouro,
        bairro: dados.bairro,
        cidade: dados.localidade,
        estado: dados.uf
      });
  }

  fillForm(endereco: any){
    this.frm.patchValue({
      id: endereco.id,
      clienteId: endereco.clienteId,
      cep: endereco.cep,
      logradouro: endereco.logradouro,
      numero: endereco.numero,
      complemento: endereco.complemento,
      bairro: endereco.bairro,
      cidade: endereco.cidade,
      estado: endereco.estado,
      tipoEndereco: endereco.tipo
    });
  }

  close(){
    this.activeModal.close();
  }
}
