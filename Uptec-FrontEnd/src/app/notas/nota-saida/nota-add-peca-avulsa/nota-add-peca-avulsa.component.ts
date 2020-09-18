import { TipoDestinatario } from './../../../shared/enums/tipoDestinatario';
import { TipoItemNotaSaida } from './../../../shared/enums/tipoItemNotaSaida';
import { LoteService } from './../../../estoque/lote/services/lote.service';
import { ClienteService } from './../../../cadastros/cliente/services/cliente.service';
import { Component, OnInit } from '@angular/core';
import { Cliente } from 'app/cadastros/cliente/models/cliente';
import { Fornecedor } from 'app/cadastros/fornecedor/models/fornecedor';
import { AuthService } from 'app/security/auth/auth.service';
import { ToastrService } from 'ngx-toastr';
import { ListService } from 'app/shared/services/list.service';
import { BaseListComponet } from 'app/shared/base-list/base-list.component';
import { take } from 'rxjs/operators';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { NotaSaidaAdd, NotaSaidaItemAdd } from '../models/nota-saida';
import { UnidadeMedida } from 'app/shared/enums/unidadeMedida';
import { NotaSaidaService } from '../services/nota-saida.service';
import { AliquotaImpostos } from 'app/shared/models/aliquotaImpostos';
import { NotaSaidaClienteModalComponent } from '../nota-saida-cliente-modal/nota-saida-cliente-modal';
import { FornecedorService } from 'app/cadastros/fornecedor/services/fornecedor.service';
import { NotaSaidaFornecedorModalComponent } from '../nota-saida-fornecedor-modal/nota-saida-fornecedor-modal';
import { Router } from '@angular/router';
import { EnumHelper } from 'app/shared/enums/enumHelper';
import { Lote } from 'app/estoque/lote/models/lote';
import { EnumType } from 'app/shared/models/enumType';
import { TipoNotaSaida } from 'app/shared/enums/tipoNotaSaida';
import swal from 'sweetalert2';

@Component({
  selector: 'app-nota-add-peca-avulsa',
  templateUrl: './nota-add-peca-avulsa.component.html'
})
export class NotaAddPecaAvulsaComponent extends BaseListComponet implements OnInit {

  public tiposDestinatario = EnumHelper.enumSelector(TipoDestinatario);
  public tipoDestinatario = 1;
  public transportadoras: EnumType[];
  public destinatario: any;
  public pesquisaPor: string;
  public pesquisa: string = "";

  public listaClientes: Cliente[];
  public listaFornecedores: Fornecedor[];
  public notaSaidaAdd: NotaSaidaAdd;
  public uf: string;
  public impostos: AliquotaImpostos;
  public lotes: Lote[];
  public notaItens: NotaSaidaItemAdd[] = [];

  constructor(private router: Router,
    private clienteService: ClienteService,
    private fornecedorService: FornecedorService,
    private modal: NgbModal,
    private loteService: LoteService,
    private notaService: NotaSaidaService,
    public listService: ListService,
    private toastrService: ToastrService,
    authService: AuthService) {
    super(listService, toastrService, authService);
    this.notaSaidaAdd = this.novaNota();
  }

  ngOnInit() {
    this.pesquisaPor = "nome";
  }

  searchDestinatario() {
    if (this.tipoDestinatario == TipoDestinatario.Cliente)
      this.getClientes();
    else
      this.getFornecedores();
  }

  confirmSubmit() {
    swal.fire({
      title: `Confirma emissão da Nota ?`,
      text: `Destinatário: ${this.destinatario.nomeFantasia}`,
      type: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sim, emitir!',
      cancelButtonText: 'Não, cancelar!',
      confirmButtonClass: 'btn btn-success btn-raised mr-5',
      cancelButtonClass: 'btn btn-danger btn-raised',
      buttonsStyling: false
    }).then((result) => {
      if (result.value) {
        this.submit();
      }
    })
  }

  submit() {

    this.notaSaidaAdd.tipoDestinatario = this.tipoDestinatario;
    this.notaSaidaAdd.loteItens = [];
    this.notaItens.forEach(i => {
        this.notaSaidaAdd.loteItens.push(i);
    });

    if(!this.validaLotes(true))
      return;

    this.notaService.add(this.notaSaidaAdd).pipe(take(1))
      .subscribe(() => {
        this.router.navigate(['/emissaoNfe']).then(() => {
          this.toastrService.info("Acompanhe o status da nota junto a Sefaz...", "Nota Incluida!");
        })
      });
  }

  getFornecedores() {
    this.clearData();
    this.fornecedorService.getToNfeSaida(this.pesquisa)
      .pipe(take(1)).subscribe(c => {
        if (c.length == 1) {
          this.setDestinatario(c[0]);
        }
        else if (c.length > 1) {
          this.listaFornecedores = c;
          this.openModalDestinatario(NotaSaidaFornecedorModalComponent)
            .componentInstance.fornecedores = this.listaFornecedores;
        }
        else {
          this.toastrService.warning("Destinatário da Nfe não localizado", "Atenção!");
        }
      });
  }

  getClientes() {
    this.clearData();
    this.clienteService.getToNfeSaida(this.pesquisa)
      .pipe(take(1)).subscribe(c => {
        if (c.length == 1) {
          this.setDestinatario(c[0]);
        }
        else if (c.length > 1) {
          this.listaClientes = c;
          this.openModalDestinatario(NotaSaidaClienteModalComponent)
            .componentInstance.clientes = this.listaClientes;
        }
        else {
          this.toastrService.warning("Destinatário da Nfe não localizado", "Atenção!");
        }
      });
  }

  getLotes() {
    this.notaService.getLotePecaToNfeSaida(this.destinatario.id, this.tipoDestinatario).pipe(take(1))
      .subscribe(l => this.lotes = l);
  }

  getImpostos() {
    this.notaService.getAliquotaImpostos(this.uf).pipe(take(1))
      .subscribe(i => {
        this.impostos = i;
      });
  }

  getOutrasInformacoes() {
    if(!this.validaLotes())
      return;

    this.notaSaidaAdd.tipoDestinatario = this.tipoDestinatario;
    this.notaSaidaAdd.loteItens = [];
    this.notaItens.forEach(i => {
        this.notaSaidaAdd.loteItens.push(i);
    });

    if(this.notaSaidaAdd.loteItens.length <= 0) {
      this.notaSaidaAdd.outrasInformacoes = "";
      return;
    }

    this.notaService.getOutrasInformacoes(this.notaSaidaAdd).pipe(take(1))
        .subscribe(texto => this.notaSaidaAdd.outrasInformacoes = texto);
  }

  addLote(lote: Lote) {
    var some = this.notaItens.some(i => i.loteId == lote.id)
    if (some)
      return;

    this.loteService.getDadosSaida(lote.id).pipe(take(1))
      .subscribe(dados => {
        let itemRemessa = new NotaSaidaItemAdd();
        itemRemessa.loteId = dados.id;
        itemRemessa.loteNumero = lote.loteNumero;
        itemRemessa.loteSequencia = lote.sequencia;
        itemRemessa.loteSequenciaString = lote.loteSequenciaString;
        itemRemessa.ordemNumero = null;
        itemRemessa.ordemLoteId = null;
        itemRemessa.codigo = dados.codigoPeca;
        itemRemessa.descricao = dados.descricaoPeca;
        itemRemessa.quantidade = lote.saldo;
        itemRemessa.cfop = ""; // dados.cfopSaidaRemessa; 
        itemRemessa.tipoItem = TipoItemNotaSaida.Remessa;
        itemRemessa.precoUnitario = dados.precoSaidaRemessa;
        itemRemessa.precoTotal = itemRemessa.precoUnitario * itemRemessa.quantidade;
        itemRemessa.unidadeMedida = dados.unidadeMedida;
        itemRemessa.tipoItem = TipoItemNotaSaida.Remessa;
        this.notaItens.push(itemRemessa);

        this.getOutrasInformacoes();
      });
  }

  getUnidadeMedida(tipo: string) {
    return UnidadeMedida[tipo];
  }

  removeLote(index: number) {
    this.notaItens.splice(index, 1);
    this.getOutrasInformacoes();
  }

  showDelete(tipoItem: number) {
    return tipoItem == TipoItemNotaSaida.Servico;
  }

  showTabItens(): boolean {
    return this.destinatario == null;
  }

  showTabComplemento(): boolean {
    return this.notaItens.length == 0;
  }

  showTabEmissao(): boolean {
    return this.notaItens.length == 0;
  }

  showLotes() {
    return this.lotes.length > 0;
  }

  getCnpjTransportadora(): string {
    if (this.destinatario.transportadoraId == null)
      return this.destinatario.cnpj;

    return "";
  }

  getNomeTransportadora(): string {
    if (this.destinatario.transportadoraId == null)
      return this.destinatario.nomeFantasia;

    return "";
  }

  pageChanged(event: any) {
    throw new Error("Method not implemented.");
  }

  onChangeEndereco(id: string, uf: string) {
    this.notaSaidaAdd.enderecoId = id;
    this.uf = uf;
    this.getImpostos();
  }

  private clearData() {
    this.notaSaidaAdd = this.novaNota();
    this.destinatario = null;
    this.uf = "";
    this.lotes = [];
    this.notaItens = [];
  }

  private novaNota(): NotaSaidaAdd {
    return {
      id: "",
      destinatarioId: "",
      enderecoId: "",
      transportadoraId: "",
      tipoDestinatario: 0,
      valorFrete: 0,
      valorSeguro: 0,
      valorOutrasDespesas: 0,
      valorDesconto: 0,
      outrasInformacoes: "",
      tipoNota: TipoNotaSaida.PecaAvulsa,
      ordemItens: [],
      loteItens: []
    };
  }

  private openModalDestinatario(component: any): NgbModalRef {
    const modalRef = this.modal.open(component, { size: 'lg', backdrop: 'static' });

    modalRef.result.then(result => {
      if (result) {
        this.setDestinatario(result);
      }
    },
      () => { });

    return modalRef;
  }

  private setDestinatario(result: any) {
    this.destinatario = result;
    this.notaSaidaAdd.destinatarioId = this.destinatario.id;
    this.notaSaidaAdd.enderecoId = this.destinatario.enderecos[0].id;
    this.uf = this.destinatario.enderecos[0].estado;
    this.getLotes();
    this.getImpostos();
    this.dropDownTransportadora();
  }

  private dropDownTransportadora(){
    this.transportadoras = [];
    this.notaSaidaAdd.transportadoraId = this.destinatario.transportadoraId;

    if(this.notaSaidaAdd.transportadoraId == null)
      this.notaSaidaAdd.transportadoraId = "00000000-0000-0000-0000-000000000000";
    
    let tTransportadora = new EnumType();
    let tDestinatario = new EnumType();
    tDestinatario.value = "00000000-0000-0000-0000-000000000000";
    tDestinatario.name = "Próprio";

    if(this.destinatario.transportadoraId != null){
      tTransportadora.value = this.destinatario.transportadoraId;
      tTransportadora.name = this.destinatario.transportadora.nomeFantasia;
      this.transportadoras.push(tTransportadora);
    }
    this.transportadoras.push(tDestinatario);
  }

  atualizaTotalItem(item: NotaSaidaItemAdd){
    item.precoTotal = (item.precoUnitario * item.quantidade);
    this.getOutrasInformacoes();
  }

  validaLotes(checkCfop = false) : boolean{
    let retorno = true;
    this.notaItens.forEach(i => {
      let lote = this.lotes.find(l => l.id == i.loteId)
      if(i.quantidade > lote.saldo){
        this.toastrService.warning(`Quantidade insuficiente no lote ${lote.loteSequenciaString}`,"");
        retorno = false;
      }
      else if(i.quantidade == 0){
        this.toastrService.warning(`Informe a quantidade para o lote ${lote.loteSequenciaString}`,"");
        retorno = false;
      }
      if(checkCfop) {
        if(i.cfop.trim() == '' || i.cfop.length < 4){
          this.toastrService.warning(`CFOP inválido.`,"");
          retorno = false;
        }
      }
    });
    return retorno;
  }
}
