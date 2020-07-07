import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ComponenteService } from './../services/componente.service';
import { SharedService } from 'app/shared/services/shared.service';
import { CepService } from 'app/shared/services/cep.service';
import { BaseFormComponent } from 'app/shared/base-form/base-form.component';
import { Componente } from '../models/componente';
import { FormValidatorHelper } from 'app/shared/helpers/form-validator-helper';
import { CurrencyHelper } from './../../../shared/helpers/currency-helper';
import { ScrollHelper } from 'app/shared/helpers/scroll-helper';
import { EnumType } from 'app/shared/models/enumType';


@Component({
  selector: 'app-componente-update',
  templateUrl: './componente-update.component.html'
})
export class ComponenteUpdateComponent extends BaseFormComponent implements OnInit {

  public componente: Componente;
  public unidadesMedida$: Observable<EnumType[]>;

  constructor(private route: ActivatedRoute,
              private sharedService: SharedService,
              private toastr: ToastrService,
              private service: ComponenteService) {
    super();
    this.formValidatorHelper = new FormValidatorHelper(Componente.validationMessages());
  }

  ngOnInit() {
    this.frm = Componente.buildForm();
    this.unidadesMedida$ = this.sharedService.getUnidadesMedida();
    this.entityId = this.route.snapshot.paramMap.get('id');
    this.service.get(this.entityId).pipe(take(1))
      .subscribe(c => this.fillForm(c));
  }

  submit() {
    let c = Object.assign({}, this.componente, this.frm.value)
    c.quantidade = this.componente.quantidade; //readonly
    c.preco = this.componente.preco; //readonly
    c.quantidadeMinima = CurrencyHelper.ToDecimal(c.quantidadeMinima);
    

    console.log(c);
    this.service.update(c).pipe(take(1)).subscribe(
      result => this.onSubmitComplete(result)
    );
  }
  onSubmitComplete(data: any) {
    this.errors = [];
    this.toastr.success("","Componente alterado com sucesso!");
    ScrollHelper.scrollToTop();
  }

  private fillForm(componente: Componente) {
    this.componente = componente;
    this.frm.patchValue({
      id: this.componente.id,
      codigo: this.componente.codigo,
      descricao: this.componente.descricao,
      unidade: this.componente.unidade,
      preco: this.componente.preco,
      ncm: this.componente.ncm,
      quantidade: this.componente.quantidade,
      quantidadeMinima: this.componente.quantidadeMinima
    });
  }
}
