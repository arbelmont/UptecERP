import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { AuthService } from 'app/security/auth/auth.service';
import { ListService } from 'app/shared/services/list.service';
import { ToastrService } from 'ngx-toastr';
import { PecaService } from '../services/peca.service';
import { BaseListComponet } from 'app/shared/base-list/base-list.component';
import { Peca } from '../models/peca';
import swal from 'sweetalert2';

@Component({
  selector: 'app-peca-list',
  templateUrl: './peca-list.component.html'
})
export class PecaListComponent extends BaseListComponet implements OnInit {
  
  public pecas: Peca[];

  constructor(private pecaService: PecaService,
              public listService: ListService,
              private toastrService: ToastrService,
              private authService: AuthService) {
    super(listService, toastrService, authService)
  }

  ngOnInit() {
    this.componentName = "pecaList";
    this.restauraList(this.componentName);

    this.getPecas();
  }

  search() {
    this.listService.currentPage = 1;
    this.getPecas();
  }

  confirmDelete(id: string, nome: string) {
    swal.fire({
      title: 'Excluir peca?',
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
    this.pecaService.delete(id).pipe(take(1))
      .subscribe(() => {
        this.getPecas();
        this.toastrService.success("Peca excluída com sucesso!");
      });
  }

  private getPecas() {
    this.pecaService.getPaged(
      this.listService.currentPage,
      this.listService.itemsPerPage,
      this.listService.searchText).pipe(take(1))
      .subscribe(t => {
        this.pecas = t.list;
        this.listService.totalItems = t.total;
        this.showPagination = this.listService.totalItems > this.listService.itemsPerPage;
      })
  }

  pageChanged(event: any) {
    this.listService.currentPage = event;
    this.getPecas();
  }

}
