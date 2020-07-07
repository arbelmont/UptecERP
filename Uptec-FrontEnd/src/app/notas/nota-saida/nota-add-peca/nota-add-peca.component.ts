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
import { OrdemLote } from 'app/producao/ordem/models/ordem';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { NotaSaidaAdd, NotaSaidaItemAdd } from '../models/nota-saida';
import { UnidadeMedida } from 'app/shared/enums/unidadeMedida';
import { NotaSaidaService } from '../services/nota-saida.service';
import { AliquotaImpostos } from 'app/shared/models/aliquotaImpostos';
import { NotaSaidaClienteModalComponent } from '../nota-saida-cliente-modal/nota-saida-cliente-modal';
import { Router } from '@angular/router';
import { EnumType } from 'app/shared/models/enumType';
import { TipoNotaSaida } from 'app/shared/enums/tipoNotaSaida';
import { DatePipe } from '@angular/common';
import swal from 'sweetalert2';

@Component({
  selector: 'app-nota-add-peca',
  templateUrl: './nota-add-peca.component.html'
})
export class NotaAddPecaComponent extends BaseListComponet implements OnInit {

  public tipoDestinatario: string;
  public pesquisaPor: string;
  public pesquisa: string = "";

  public cliente: Cliente;
  public transportadoras: EnumType[];
  public listaClientes: Cliente[];
  public notaSaidaAdd: NotaSaidaAdd;
  public uf: string;
  public fornecedor: Fornecedor;
  public impostos: AliquotaImpostos;
  public ordemLotes: OrdemLote[];
  public notaItens: NotaSaidaItemAdd[] = [];

  constructor(private router: Router,
    private clienteService: ClienteService,
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
    this.tipoDestinatario = "c";
    this.pesquisaPor = "nome";
  }

  searchDestinatario() {
    this.getClientes();
  }

  confirmSubmit() {
    swal.fire({
      title: `Confirma emissão da Nota ?`,
      text: `Destinatário: ${this.cliente.nomeFantasia}`,
      type: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sim, excluir!',
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

    this.notaSaidaAdd.tipoDestinatario = TipoDestinatario.Cliente;
    this.notaSaidaAdd.ordemItens = [];
    this.notaItens.forEach(i => {
      if (i.ordemNumero != null)
        this.notaSaidaAdd.ordemItens.push(i);
    });

    this.notaService.add(this.notaSaidaAdd).pipe(take(1))
      .subscribe(() => {
        this.router.navigate(['/emissaoNfe']).then(() => {
          this.toastrService.info("Acompanhe o status da nota junto a Sefaz...", "Nota Incluida!");
        })
      });
  }

  getClientes() {
    this.clearData();
    this.clienteService.getToNfeSaida(this.pesquisa)
      .pipe(take(1)).subscribe(c => {
        if (c.length == 1) {
          this.cliente = c[0];
          this.notaSaidaAdd.destinatarioId = this.cliente.id;
          this.notaSaidaAdd.enderecoId = this.cliente.enderecos[0].id;
          this.uf = this.cliente.enderecos[0].estado;
          this.getOrdemLotes();
          this.getImpostos();
          this.dropDownTransportadora();
        }
        else if (c.length > 1) {
          this.listaClientes = c;
          this.openModalClienteEmissor(NotaSaidaClienteModalComponent).componentInstance.clientes = this.listaClientes;
        }
        else {
          this.toastrService.warning("Destinatário da Nfe não localizado", "Atenção!");
        }
      });
  }

  getOrdemLotes() {
    this.notaService.getOrdemLoteToNfeSaida(this.cliente.id).pipe(take(1))
      .subscribe(ol => this.ordemLotes = ol);
  }

  getImpostos() {
    this.notaService.getAliquotaImpostos(this.uf).pipe(take(1))
      .subscribe(i => {
        this.impostos = i;
      });
  }

  addLote(ordemLote: OrdemLote) {
    var some = this.notaItens.some(i => i.ordemLoteId == ordemLote.id)
    if (some)
      return;

    this.loteService.getDadosSaida(ordemLote.loteId).pipe(take(1))
      .subscribe(dados => {

        let itemServico = new NotaSaidaItemAdd();
        itemServico.ordemLoteId = ordemLote.id;
        itemServico.loteNumero = ordemLote.loteNumero;
        itemServico.loteSequencia = ordemLote.loteSequencia;
        itemServico.loteSequenciaString = ordemLote.loteSequenciaString;
        itemServico.ordemNumero = ordemLote.ordem.ordemNumero;
        itemServico.codigo = dados.codigoPecaSaida;
        let datePipe = new DatePipe("pt");
        let validade = datePipe.transform(ordemLote.validade, 'dd/MM/yyyy');
        itemServico.descricao = `${dados.descricaoPeca} LT:${ordemLote.loteSequenciaString} VL:${validade}`;
        itemServico.quantidade = ordemLote.qtdeProduzida;
        itemServico.cfop = dados.cfopSaidaServico;
        itemServico.precoUnitario = dados.precoSaidaServico;
        itemServico.precoTotal = itemServico.precoUnitario * itemServico.quantidade;
        itemServico.unidadeMedida = dados.unidadeMedida;
        itemServico.tipoItem = TipoItemNotaSaida.Servico;
        this.notaItens.push(itemServico);

        let itemRemessa = Object.assign({}, itemServico);
        itemRemessa.descricao = dados.descricaoPeca;
        itemRemessa.loteSequenciaString = "";
        itemRemessa.ordemNumero = null;
        itemRemessa.ordemLoteId = null;
        itemRemessa.codigo = dados.codigoPeca;
        itemRemessa.tipoItem = TipoItemNotaSaida.Remessa;
        itemRemessa.cfop = dados.cfopSaidaRemessa;
        itemRemessa.precoUnitario = dados.precoSaidaRemessa;
        itemRemessa.precoTotal = itemRemessa.precoUnitario * itemRemessa.quantidade;
        this.notaItens.push(itemRemessa);

        this.getOutrasInformacoes();
      });

      
  }

  getOutrasInformacoes() {
    this.notaSaidaAdd.tipoDestinatario = TipoDestinatario.Cliente;
      this.notaSaidaAdd.ordemItens = [];
      this.notaItens.forEach(i => {
        if (i.ordemNumero != null)
          this.notaSaidaAdd.ordemItens.push(i);
      });

      if(this.notaSaidaAdd.ordemItens.length <= 0) {
        this.notaSaidaAdd.outrasInformacoes = "";
        return;
      }

      this.notaService.getOutrasInformacoes(this.notaSaidaAdd).pipe(take(1))
        .subscribe(texto => this.notaSaidaAdd.outrasInformacoes = texto);
  }

  getUnidadeMedida(tipo: string) {
    return UnidadeMedida[tipo];
  }

  removeLote(index: number) {
    this.notaItens.splice(index, 2);
    this.getOutrasInformacoes();
  }

  showDelete(tipoItem: number) {
    return tipoItem == TipoItemNotaSaida.Servico;
  }

  showTabItens(): boolean {
    return this.cliente == null;
  }

  showTabComplemento(): boolean {
    return this.notaItens.length == 0;
  }

  showTabEmissao(): boolean {
    return this.notaItens.length == 0;
  }

  showOrdemLotes() {
    return this.ordemLotes.length > 0;
  }

  getCnpjTransportadora(): string {
    if (this.cliente.transportadoraId == null)
      return this.cliente.cnpj;

    return "";
  }

  getNomeTransportadora(): string {
    if (this.cliente.transportadoraId == null)
      return this.cliente.nomeFantasia;

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

  private dropDownTransportadora(){
    this.transportadoras = [];
    this.notaSaidaAdd.transportadoraId = this.cliente.transportadoraId;

    if(this.notaSaidaAdd.transportadoraId == null)
      this.notaSaidaAdd.transportadoraId = "00000000-0000-0000-0000-000000000000";
    
    let tTransportadora = new EnumType();
    let tCliente = new EnumType();
    tCliente.value = "00000000-0000-0000-0000-000000000000";
    tCliente.name = "Próprio Cliente";

    if(this.cliente.transportadoraId != null){
      tTransportadora.value = this.cliente.transportadoraId;
      tTransportadora.name = this.cliente.transportadora.nomeFantasia;
      this.transportadoras.push(tTransportadora);
    }
    this.transportadoras.push(tCliente);
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
      tipoNota: TipoNotaSaida.Peca,
      ordemItens: [],
      loteItens: []
    };
  }

  private clearData() {
    this.notaSaidaAdd = this.novaNota();
    this.fornecedor = null;
    this.cliente = null;
    this.uf = "";
    this.ordemLotes = [];
    this.notaItens = [];
  }

  private openModalClienteEmissor(component: any): NgbModalRef {
    const modalRef = this.modal.open(component, { size: 'lg', backdrop: 'static' });

    modalRef.result.then(result => {
      if (result) {
        this.cliente = result;
        this.notaSaidaAdd.destinatarioId = this.cliente.id;
        this.notaSaidaAdd.enderecoId = this.cliente.enderecos[0].id;
        this.uf = this.cliente.enderecos[0].estado;
        this.getOrdemLotes();
        this.getImpostos();
        this.dropDownTransportadora();
      }
    },
      reason => { });

    return modalRef;
  }
}
