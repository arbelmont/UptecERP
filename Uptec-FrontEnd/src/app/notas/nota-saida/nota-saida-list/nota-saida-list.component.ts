import { TipoDestinatario } from './../../../shared/enums/tipoDestinatario';
import { Component, OnInit } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { BaseListComponet } from 'app/shared/base-list/base-list.component';
import { ListService } from 'app/shared/services/list.service';
import { AuthService } from 'app/security/auth/auth.service';
import { SharedService } from 'app/shared/services/shared.service';
import { formatDate } from '@angular/common';
import { NotaSaida } from '../models/nota-saida';
import { NotaSaidaService } from '../services/nota-saida.service';
import { take } from 'rxjs/operators';
import swal from 'sweetalert2';
import { EnumHelper } from 'app/shared/enums/enumHelper';
import { NotaSaidaHelper } from '../helper/nota-saida-helper';
import { NotaSaidaDetalheModalComponent } from '../nota-saida-detalhe-modal/nota-saida-detalhe-modal';
import { StatusNotaSaida } from 'app/shared/enums/statusNotaSaida';

@Component({
  selector: 'app-nota-saida-list',
  templateUrl: './nota-saida-list.component.html',
  styleUrls: ['./nota-saida-list.component.scss']
})
export class NotaSaidaListComponent extends BaseListComponet implements OnInit {

  public status: number;
  public startDate: string;
  public endDate: string;
  public notasSaida: NotaSaida[];
  public statusNfSaida = EnumHelper.enumSelector(StatusNotaSaida);
  public tiposDestinatario = EnumHelper.enumSelector(TipoDestinatario);
  public tipoDestinatario = 0;

  constructor(private modal: NgbModal,
    public listService: ListService,
    private toastrService: ToastrService,
    private sharedService: SharedService,
    private notaService: NotaSaidaService,
    private authService: AuthService) {
    super(listService, toastrService, authService);
  }

  ngOnInit() {
    this.componentName = "notaSaidaList";
    this.restauraList(this.componentName);

    this.status = 0;
    this.endDate = formatDate(new Date(), 'yyyy-MM-dd', 'pt');
    this.startDate = formatDate(new Date().setDate(new Date().getDate() - 30), 'yyyy-MM-dd', 'pt');

    this.getNotas();
  }

  detalharNota(notaSaidaId: string, status: number) {
    this.openModalDetalhes(NotaSaidaDetalheModalComponent, status).componentInstance.notaSaidaId = notaSaidaId;
  }

  getDescricaoStatus(status: string): string {
    return NotaSaidaHelper.getDescricaoStatus(status);
  }

  getStatusClass(status: number): string {
    return NotaSaidaHelper.getStatusClass(status);
  }

  search() {
    this.listService.currentPage = 1;
    this.getNotas();
  }

  private getNotas() {
    this.notaService.getPaged(
      this.listService.currentPage,
      this.listService.itemsPerPage,
      this.tipoDestinatario,
      this.status,
      this.startDate,
      this.endDate,
      this.listService.searchText).pipe(take(1))
      .subscribe(t => {
        this.notasSaida = t.list;
        this.listService.totalItems = t.total;
        this.showPagination = this.listService.totalItems > this.listService.itemsPerPage;
      })

  }

  getDescricaoTipoDestinatario(tipo: string) {
    return NotaSaidaHelper.getDescricaoTipoDestinatario(tipo);
  }

  getCnpjDestinatario(nota: NotaSaida) {
    if (nota.tipoDestinatario == TipoDestinatario.Cliente)
      return nota.cliente.cnpj;
    return nota.fornecedor.cnpj;
  }

  getNomeDestinatario(nota: NotaSaida) {
    if (nota.tipoDestinatario == TipoDestinatario.Cliente)
      return nota.cliente.nomeFantasia;
    return nota.fornecedor.nomeFantasia;
  }

  showDownloads(status: number){
    return status == StatusNotaSaida.Processada;
  }

  showAtualizar(status: number){
    return status == StatusNotaSaida.Processando;
  }

  downloadPdf(numeroNota: string){
    NotaSaidaHelper.downloadPdf(numeroNota, this.notaService);
  }

  downloadXml(numeroNota: string){
    NotaSaidaHelper.downloadXml(numeroNota, this.notaService);
  }

  atualizarStatus(id: string, status: number) {
    this.notaService.getWithStatusSefaz(id).pipe(take(1))
      .subscribe((n: NotaSaida) => {
        if(n.status != status) {
          this.getNotas();
          this.toastrService.info("", `Nota ${n.numeroNota} atualizada.`)
        }
      });
  }

  private openModalDetalhes(component: any, status: number): NgbModalRef {
    const modalRef = this.modal.open(component, { size: 'lg', backdrop: 'static' });

    modalRef.result.then(result => {
      if(status == StatusNotaSaida.Processando || status == StatusNotaSaida.Rejeitada) {
        this.getNotas();
      }
        
    },
      reason => { });

    return modalRef;
  }

  confirmDelete(id: string, nome: string) {
    swal.fire({
      title: 'Excluir Nota?',
      text: nome,
      type: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sim, excluir!',
      cancelButtonText: 'Não, cancelar!',
      confirmButtonClass: 'btn btn-success btn-raised mr-5',
      cancelButtonClass: 'btn btn-danger btn-raised',
      buttonsStyling: false
    }).then((result) => {
      if (result.value) {
        this.delete(id);
      }
    })
  }

  delete(id: string) {
    this.notaService.delete(id).pipe(take(1))
      .subscribe(() => {
        this.getNotas();
        this.toastrService.success("Nota excluída com sucesso!");
      });
  }

  pageChanged(event: any) {
    this.listService.currentPage = event;
    this.getNotas();
  }
}
