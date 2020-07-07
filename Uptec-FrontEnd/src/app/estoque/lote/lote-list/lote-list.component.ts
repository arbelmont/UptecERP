import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { AuthService } from 'app/security/auth/auth.service';
import { ListService } from 'app/shared/services/list.service';
import { ToastrService } from 'ngx-toastr';
import { LoteService } from '../services/lote.service';
import { BaseListComponet } from 'app/shared/base-list/base-list.component';
import { Lote } from '../models/lote';
import swal from 'sweetalert2';
import { LoteHelper } from '../helper/lote-helper';

@Component({
  selector: 'app-lote-list',
  templateUrl: './lote-list.component.html'
})
export class LoteListComponent extends BaseListComponet implements OnInit {
  
  public lotes: Lote[];

  constructor(private loteService: LoteService,
              public listService: ListService,
              private toastrService: ToastrService,
              private authService: AuthService) {
    super(listService, toastrService, authService)
  }

  ngOnInit() {
    this.componentName = "loteList";
    this.restauraList(this.componentName);

    this.getLotes();
  }

  search() {
    this.listService.currentPage = 1;
    this.getLotes();
  }

  confirmDelete(id: string, nome: string) {
    swal.fire({
      title: 'Excluir lote?',
      text: nome,
      type: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sim, excluir!',
      cancelButtonText: 'Não, cancelar!',
      confirmButtonClass: 'btn btn-success btn-raised mr-5',
      cancelButtonClass: 'btn btn-danger btn-raised',
      buttonsStyling: false
    }).then((result) => {
      if(result.value) {
        this.delete(id);
      }
    })
  }

  delete(id : string) {
    /* this.loteService.delete(id).pipe(take(1))
      .subscribe(() => {
        this.getLotes();
        this.toastrService.success("Lote excluído com sucesso!");
      }); */
  }

  private getLotes() {
    this.loteService.getPaged(
      this.listService.currentPage,
      this.listService.itemsPerPage,
      true,
      this.listService.searchText).pipe(take(1))
      .subscribe(t => {
        this.lotes = t.list;
        this.listService.totalItems = t.total;
        this.showPagination = this.listService.totalItems > this.listService.itemsPerPage;
      })
  }

  getStatusClass(status: number): string {
    return LoteHelper.getStatusClass(status);
  }

  getDescricaoStatus(status: string) {
    return LoteHelper.getDescricaoStatus(status);
  }

  pageChanged(event: any) {
    this.listService.currentPage = event;
    this.getLotes();
  }

}
