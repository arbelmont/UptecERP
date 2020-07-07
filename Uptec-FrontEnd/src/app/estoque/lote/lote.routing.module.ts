import { LoteMovimentoListComponent } from './lote-movimento-list/lote-movimento-list.component';
import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { LoteListComponent } from "./lote-list/lote-list.component";

const routes: Routes = [
    { path: '', component: LoteListComponent },
    { path: 'movimento/:id', component: LoteMovimentoListComponent }
    /* { path: '', component: TransportadoraListComponent },
    { path: 'adicionar', component: TransportadoraAddComponent },
    { path: 'editar/:id', component: TransportadoraUpdateComponent } */
  ];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
  })
  export class LoteRoutingModule { }