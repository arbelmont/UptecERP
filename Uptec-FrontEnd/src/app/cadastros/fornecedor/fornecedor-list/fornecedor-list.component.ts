import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { AuthService } from 'app/security/auth/auth.service';
import { ListService } from 'app/shared/services/list.service';
import { ToastrService } from 'ngx-toastr';
import { FornecedorService } from '../services/fornecedor.service';
import { BaseListComponet } from 'app/shared/base-list/base-list.component';
import { Fornecedor } from '../models/fornecedor';
import swal from 'sweetalert2';

@Component({
  selector: 'app-client-list',
  templateUrl: './fornecedor-list.component.html'
})
export class FornecedorListComponent extends BaseListComponet implements OnInit {
  
  public fornecedors: Fornecedor[];

  constructor(private fornecedorService: FornecedorService,
              public listService: ListService,
              private toastrService: ToastrService,
              private authService: AuthService) {
    super(listService, toastrService, authService)
  }

  ngOnInit() {
    this.componentName = "fornecedorList";
    this.restauraList(this.componentName);

    this.getFornecedors();
  }

  search() {
    this.listService.currentPage = 1;
    this.getFornecedors();
  }

  confirmDelete(id: string, nome: string) {
    swal.fire({
      title: 'Excluir fornecedor?',
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
    this.fornecedorService.delete(id).pipe(take(1))
      .subscribe(() => {
        this.getFornecedors();
        this.toastrService.success("Fornecedor excluído com sucesso!");
      });
  }

  private getFornecedors() {
    this.fornecedorService.getPaged(
      this.listService.currentPage,
      this.listService.itemsPerPage,
      this.listService.searchText).pipe(take(1))
      .subscribe(t => {
        this.fornecedors = t.list;
        this.listService.totalItems = t.total;
        this.showPagination = this.listService.totalItems > this.listService.itemsPerPage;
      })
  }

  pageChanged(event: any) {
    this.listService.currentPage = event;
    this.getFornecedors();
  }

}
