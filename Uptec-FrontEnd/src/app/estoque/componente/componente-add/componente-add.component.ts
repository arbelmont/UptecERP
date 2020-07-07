import { CurrencyHelper } from './../../../shared/helpers/currency-helper';
import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { ComponenteService } from './../services/componente.service';
import { SharedService } from 'app/shared/services/shared.service';
import { CepService } from 'app/shared/services/cep.service';
import { BaseFormComponent } from 'app/shared/base-form/base-form.component';
import { Componente } from '../models/componente';
import { FormValidatorHelper } from 'app/shared/helpers/form-validator-helper';
import { ScrollHelper } from 'app/shared/helpers/scroll-helper';
import { EnumType } from 'app/shared/models/enumType';

@Component({
  selector: 'app-componente-add',
  templateUrl: './componente-add.component.html'
})
export class ComponenteAddComponent extends BaseFormComponent implements OnInit {

  public componente: Componente;
  public unidadesMedida$: Observable<EnumType[]>;

  constructor(private sharedService: SharedService,
              private toastr: ToastrService,
              private service: ComponenteService) {
    super();
    this.formValidatorHelper = new FormValidatorHelper(Componente.validationMessages());
  }

  ngOnInit() {
    this.frm = Componente.buildForm();
    this.unidadesMedida$ = this.sharedService.getUnidadesMedida();
  }

  submit() {
    let c = Object.assign({}, this.componente, this.frm.value)
    //c.preco = CurrencyHelper.ToDecimal(c.preco);
    c.quantidade = CurrencyHelper.ToDecimal(c.quantidade);
    c.quantidadeMinima = CurrencyHelper.ToDecimal(c.quantidadeMinima);

    this.service.add(c).pipe(take(1)).subscribe(
      result => this.onSubmitComplete(result)
    );
  }
  onSubmitComplete(data: any) {
    this.frm = Componente.buildForm();
    this.errors = [];
    this.toastr.success("","Componente adicionado com sucesso!");
    ScrollHelper.scrollToTop();
  }
}
