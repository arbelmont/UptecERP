import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { SharedService } from 'app/shared/services/shared.service';
import { ToastrService } from 'ngx-toastr';
import { ClienteService } from '../services/cliente.service';
import { CepService } from 'app/shared/services/cep.service';
import { BaseFormComponent } from 'app/shared/base-form/base-form.component';
import { EnumType } from 'app/shared/models/enumType';
import { FormValidatorHelper } from 'app/shared/helpers/form-validator-helper';
import { ScrollHelper } from 'app/shared/helpers/scroll-helper';
import { Cliente } from '../models/cliente';
import { Estado } from 'app/shared/models/estado';
import { Cidade } from 'app/shared/models/cidade';

@Component({
  selector: 'app-cliente-add',
  templateUrl: './cliente-add.component.html'
})
export class ClienteAddComponent extends BaseFormComponent implements OnInit {

  public cliente: Cliente;
  public estados: Observable<Estado[]>;
  public cidades: Cidade[];
  public telefoneTipos: Observable<EnumType[]>;
  public enderecoTipos: Observable<EnumType[]>;
  public transportadoras$: Observable<EnumType[]>;

  constructor(private sharedService: SharedService,
              private toastr: ToastrService,
              private service: ClienteService,
              private cepService: CepService) {
    super();
    this.formValidatorHelper = new FormValidatorHelper(Cliente.validationMessages());
  }

  ngOnInit() {
    this.frm = Cliente.buildFormAdd();
    this.estados = this.sharedService.getEstados();
    this.telefoneTipos = this.sharedService.getTelefoneTipos();
    this.enderecoTipos = this.sharedService.getEnderecoTipos();
    this.transportadoras$ = this.sharedService.getTransportadoras();

    this.frm.get('endereco.estado').valueChanges
      .subscribe(uf => this.getCidades(uf));
  }

  submit() {
    let t = Object.assign({}, this.cliente, this.frm.value)
    t.telefone.numero = t.telefone.numerot;
    t.telefone.observacoes = t.telefone.observacoest;
    t.endereco.tipo = t.endereco.tipoEndereco;
    this.service.add(t).pipe(take(1)).subscribe(
      result => this.onSubmitComplete(result)
    );
  }
  onSubmitComplete(data: any) {
    this.frm = Cliente.buildFormAdd();
    this.errors = [];
    this.toastr.success("","Cliente adicionado com sucesso!");
    ScrollHelper.scrollToTop();
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
    let cep = this.frm.get('endereco.cep').value;
    this.cepService.consultaCep(cep).subscribe(
      dados => this.fillEndereco(dados)
    )
  }

  fillEndereco(dados: any) {
    if(!dados.hasOwnProperty('logradouro'))
      return

    this.frm.patchValue({
      endereco: {
        logradouro: dados.logradouro,
        bairro: dados.bairro,
        cidade: dados.localidade,
        estado: dados.uf
      }
    })
  }

}
