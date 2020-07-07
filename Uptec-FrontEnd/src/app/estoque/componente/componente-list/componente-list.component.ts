import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { AuthService } from 'app/security/auth/auth.service';
import { ListService } from 'app/shared/services/list.service';
import { ToastrService } from 'ngx-toastr';
import { ComponenteService } from '../services/componente.service';
import { BaseListComponet } from 'app/shared/base-list/base-list.component';
import { Componente } from '../models/componente';
import swal from 'sweetalert2';

@Component({
  selector: 'app-componente-list',
  templateUrl: './componente-list.component.html'
})
export class ComponenteListComponent extends BaseListComponet implements OnInit {
  
  public componentes: Componente[];

  constructor(private componenteService: ComponenteService,
              public listService: ListService,
              private toastrService: ToastrService,
              private authService: AuthService) {
    super(listService, toastrService, authService)
  }

  ngOnInit() {
    this.componentName = "componenteList";
    this.restauraList(this.componentName);

    this.getComponentes();
  }

  search() {
    this.listService.currentPage = 1;
    this.getComponentes();
  }

  confirmDelete(id: string, nome: string) {
    swal.fire({
      title: 'Excluir componente?',
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
    this.componenteService.delete(id).pipe(take(1))
      .subscribe(() => {
        this.getComponentes();
        this.toastrService.success("Componente excluído com sucesso!");
      });
  }

  private getComponentes() {
    this.componenteService.getPaged(
      this.listService.currentPage,
      this.listService.itemsPerPage,
      this.listService.searchText).pipe(take(1))
      .subscribe(t => {
        this.componentes = t.list;
        this.listService.totalItems = t.total;
        this.showPagination = this.listService.totalItems > this.listService.itemsPerPage;
      })
  }

  pageChanged(event: any) {
    this.listService.currentPage = event;
    this.getComponentes();
  }

}
