import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { PecaListComponent } from "./peca-list/peca-list.component";
import { PecaFormComponent } from "./peca-form/peca-form.component";
import { PecaLoteListComponent } from "./peca-lote-list/peca-lote-list.component";

const routes: Routes = [
    { path: '', component: PecaListComponent },
    { path: 'adicionar', component: PecaFormComponent },
    { path: 'editar/:id', component: PecaFormComponent },
    { path: 'lote/:id', component: PecaLoteListComponent }
  ];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
  })
  export class PecaRoutingModule { }