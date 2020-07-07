import { AuthService } from './../../../security/auth/auth.service';
import { TransportadoraService } from './../services/transportadora.service';
import { Component, OnInit } from '@angular/core';
import { ListService } from './../../../shared/services/list.service';
import { BaseListComponet } from 'app/shared/base-list/base-list.component';
import { ToastrService } from 'ngx-toastr';
import { Transportadora } from '../models/transportadora';
import { take } from 'rxjs/operators';
import swal from 'sweetalert2';


@Component({
  selector: 'app-transportadora-list',
  templateUrl: './transportadora-list.component.html'
})
export class TransportadoraListComponent extends BaseListComponet implements OnInit {

  public transportadoras: Transportadora[];

  constructor(private transportadoraService: TransportadoraService,
              public listService: ListService,
              private toastrService: ToastrService,
              private authService: AuthService) {
    super(listService, toastrService, authService)
  }

  ngOnInit() {
    this.componentName = "transportadoraList";
    this.restauraList(this.componentName);

    this.getTransportadoras();
  }

  search() {
    this.listService.currentPage = 1;
    this.getTransportadoras();
  }

  confirmDelete(id: string, nome: string) {
    swal.fire({
      title: 'Excluir transportadora?',
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
    this.transportadoraService.delete(id).pipe(take(1))
      .subscribe(() => {
        this.getTransportadoras();
        this.toastrService.success("Transportadora excluída com sucesso!");
      });
  }

  private getTransportadoras() {
    this.transportadoraService.getPaged(
      this.listService.currentPage,
      this.listService.itemsPerPage,
      this.listService.searchText).pipe(take(1))
      .subscribe(t => {
        this.transportadoras = t.list;
        this.listService.totalItems = t.total;
        this.showPagination = this.listService.totalItems > this.listService.itemsPerPage;
      })
  }

  pageChanged(event: any) {
    this.listService.currentPage = event;
    this.getTransportadoras();
  }
}
