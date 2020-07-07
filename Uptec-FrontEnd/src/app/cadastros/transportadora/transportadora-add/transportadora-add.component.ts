import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { TransportadoraService } from './../services/transportadora.service';
import { SharedService } from 'app/shared/services/shared.service';
import { CepService } from 'app/shared/services/cep.service';
import { BaseFormComponent } from 'app/shared/base-form/base-form.component';
import { Transportadora } from '../models/transportadora';
import { FormValidatorHelper } from 'app/shared/helpers/form-validator-helper';
import { ScrollHelper } from 'app/shared/helpers/scroll-helper';
import { Estado } from 'app/shared/models/estado';
import { Cidade } from 'app/shared/models/cidade';
import { EnumType } from 'app/shared/models/enumType';

@Component({
  selector: 'app-transportadora-add',
  templateUrl: './transportadora-add.component.html'
})
export class TransportadoraAddComponent extends BaseFormComponent implements OnInit {

  public transportadora: Transportadora;
  public estados: Observable<Estado[]>;
  public cidades: Cidade[];
  public telefoneTipos: Observable<EnumType[]>;
  public enderecoTipos: Observable<EnumType[]>;
  public tipoEntregaPadrao: Observable<EnumType[]>;

  constructor(private sharedService: SharedService,
              private toastr: ToastrService,
              private service: TransportadoraService,
              private cepService: CepService) {
    super();
    this.formValidatorHelper = new FormValidatorHelper(Transportadora.validationMessages());
  }

  ngOnInit() {
    this.frm = Transportadora.buildFormAdd();
    this.estados = this.sharedService.getEstados();
    this.telefoneTipos = this.sharedService.getTelefoneTipos();
    this.enderecoTipos = this.sharedService.getEnderecoTipos();
    this.tipoEntregaPadrao = this.service.getEntregaTipos();

    this.frm.get('endereco.estado').valueChanges
      .subscribe(uf => this.getCidades(uf));
  }

  submit() {
    let t = Object.assign({}, this.transportadora, this.frm.value)
    t.telefone.numero = t.telefone.numerot;
    t.telefone.observacoes = t.telefone.observacoest;
    t.endereco.tipo = t.endereco.tipoEndereco;

    this.service.add(t).pipe(take(1)).subscribe(
      result => this.onSubmitComplete(result)
    );
  }
  onSubmitComplete(data: any) {
    this.frm = Transportadora.buildFormAdd();
    this.errors = [];
    this.toastr.success("","Transportadora adicionada com sucesso!");
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
