import { NotaManifestoDetalheModalComponent } from './../nota-manifesto-detalhe-modal/nota-manifesto-detalhe-modal';
import { ManifestacaoStatusSelecao } from './../../../shared/enums/manifestacaoStatusSelecao';
import { ManifestacaoStatus } from './../../../shared/enums/manifestacaoStatus';
import { NotaManifesto, UsuarioManifesto } from './../models/nota-manifesto';
import { Component, OnInit } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { BaseListComponet } from 'app/shared/base-list/base-list.component';
import { ListService } from 'app/shared/services/list.service';
import { AuthService } from 'app/security/auth/auth.service';
import { SharedService } from 'app/shared/services/shared.service';
import swal from 'sweetalert2';
import { NotaManifestoService } from '../services/nota-manifesto.service';
import { EnumHelper } from 'app/shared/enums/enumHelper';
import { NotaManifestoHelper } from '../helper/nota-manifesto-helper';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-nota-manifesto-list',
  templateUrl: './nota-manifesto-list.component.html'
})
export class NotaManifestoListComponent extends BaseListComponet implements OnInit {

  public manifestos: NotaManifesto[];
  public manifestacaoStatus = EnumHelper.enumSelector(ManifestacaoStatusSelecao);

  constructor(private modal: NgbModal,
    public listService: ListService,
    private toastrService: ToastrService,
    private service: NotaManifestoService,
    authService: AuthService) {
    super(listService, toastrService, authService);
  }

  ngOnInit() {
    this.componentName = "notaManifestoList";
    this.restauraList(this.componentName);

    this.service.GetUnmanifestedFromIntegration()
      .pipe(take(1)).subscribe(
        m => { this.getManifestos(); }
      );
  }

  detalharNota(manifesto: NotaManifesto) {
    this.openModalDetalhes(NotaManifestoDetalheModalComponent).componentInstance.manifesto = manifesto;
  }

  getDescricaoStatus(status: string): string {
    return NotaManifestoHelper.getDescricaoStatus(status);
  }

  search() {
    this.listService.currentPage = 1;
    this.getManifestos();
  }

  private getManifestos() {
    this.service.getPaged(
      this.listService.currentPage,
      this.listService.itemsPerPage).pipe(take(1))
      .subscribe(t => {
        this.manifestos = t.list;
        this.manifestos.forEach(el => {
          if (el.manifestacaoDestinatario == null)
            el.manifestacaoDestinatario = 0;
          el.manifestacaoUsuario = 0;
        });
        this.listService.totalItems = t.total;
        this.showPagination = this.listService.totalItems > this.listService.itemsPerPage;
      })
  }

  cancelar() {
    this.getManifestos();
  }

  confirmManifesto() {
    let selecao = this.manifestos.filter(x => x.manifestacaoDestinatario == ManifestacaoStatus.Manifestar && x.manifestacaoUsuario > 0);

    if (selecao.length == 0) {
      this.toastrService.warning("Nenhuma nota selecionada para manifestação.", "Atenção!");
      return;
    }

    let errors = "";
    selecao.forEach(el => {
      if (el.manifestacaoUsuario == ManifestacaoStatusSelecao.NaoRealizada) {
        if (el.justificativaManifestacao == null || el.justificativaManifestacao.length <= 0 || el.justificativaManifestacao.length > 50) {
          errors += `Justificativa inválida ou não informada para a nota ${el.chaveNfe}. `
        };
      };
    });

    if (errors.length > 0) {
      this.toastrService.warning(errors, "Atenção!");
      return;
    }

    swal.fire({
      title: 'Confirma a manifestação?',
      text: `${selecao.length} nota(s) selecionada(s).`,
      type: 'info',
      showCancelButton: true,
      confirmButtonText: 'Sim, confirmo!',
      cancelButtonText: 'Não, cancelar!',
      confirmButtonClass: 'btn btn-success btn-raised mr-5',
      cancelButtonClass: 'btn btn-danger btn-raised',
      buttonsStyling: false
    }).then((result) => {
      if (result.value) {
        this.manifestar(selecao);
      }
    })
  }

  manifestar(manifestos: any) {

    let manifestosBackEnd: UsuarioManifesto[] = [];

    manifestos.forEach(el => {
      manifestosBackEnd.push({
        id: el.id,
        chaveNfe: el.chaveNfe,
        manifestacaoDestinatario: Number(el.manifestacaoUsuario),
        justificativa: el.justificativaManifestacao
      });
    });

    //console.log(manifestosBackEnd);

    this.service.manifestar(manifestosBackEnd).pipe(take(1))
      .subscribe(result => {
        this.getManifestos();
        this.toastrService.success("", "Manifestação enviada à Sefaz!");
      });
  }

  showJustificativa(status: number): boolean {
    return status == ManifestacaoStatusSelecao.NaoRealizada
  }

  cienciaToAll() {
    this.manifestos.forEach(el => {
      el.manifestacaoUsuario = ManifestacaoStatusSelecao.Ciencia;
    });
  }

  desconhecimentoToAll() {
    this.manifestos.forEach(el => {
      el.manifestacaoUsuario = ManifestacaoStatusSelecao.Desconhecimento;
    });
  }

  private openModalDetalhes(component: any): NgbModalRef {
    const modalRef = this.modal.open(component, { size: 'lg', backdrop: 'static' });

    modalRef.result.then(result => {
      result => { }
    },
      reason => { });

    return modalRef;
  }

  pageChanged(event: any) {
    this.listService.currentPage = event;
    this.getManifestos();
  }
}
