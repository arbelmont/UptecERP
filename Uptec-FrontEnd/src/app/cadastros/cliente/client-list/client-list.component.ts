import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { AuthService } from 'app/security/auth/auth.service';
import { ListService } from 'app/shared/services/list.service';
import { ToastrService } from 'ngx-toastr';
import { ClienteService } from '../services/cliente.service';
import { BaseListComponet } from 'app/shared/base-list/base-list.component';
import { Cliente } from '../models/cliente';
import swal from 'sweetalert2';

@Component({
  selector: 'app-client-list',
  templateUrl: './client-list.component.html'
})
export class ClientListComponent extends BaseListComponet implements OnInit {
  
  public clientes: Cliente[];

  constructor(private clienteService: ClienteService,
              public listService: ListService,
              private toastrService: ToastrService,
              private authService: AuthService) {
    super(listService, toastrService, authService)
  }

  ngOnInit() {
    this.componentName = "clienteList";
    this.restauraList(this.componentName);

    this.getClientes();
  }

  search() {
    this.listService.currentPage = 1;
    this.getClientes();
  }

  confirmDelete(id: string, nome: string) {
    swal.fire({
      title: 'Excluir cliente?',
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
    this.clienteService.delete(id).pipe(take(1))
      .subscribe(() => {
        this.getClientes();
        this.toastrService.success("Cliente excluído com sucesso!");
      });
  }

  private getClientes() {
    this.clienteService.getPaged(
      this.listService.currentPage,
      this.listService.itemsPerPage,
      this.listService.searchText).pipe(take(1))
      .subscribe(t => {
        this.clientes = t.list;
        this.listService.totalItems = t.total;
        this.showPagination = this.listService.totalItems > this.listService.itemsPerPage;
      })
  }

  pageChanged(event: any) {
    this.listService.currentPage = event;
    this.getClientes();
  }

}
