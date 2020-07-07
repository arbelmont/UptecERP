import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { FornecedorListComponent } from "./fornecedor-list/fornecedor-list.component";
import { FornecedorAddComponent } from "./fornecedor-add/fornecedor-add.component";
import { FornecedorUpdateComponent } from "./fornecedor-update/fornecedor-update.component";

const routes: Routes = [
    { path: '', component: FornecedorListComponent },
    { path: 'adicionar', component: FornecedorAddComponent },
    { path: 'editar/:id', component: FornecedorUpdateComponent } 
  ];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
  })
  export class FornecedorRoutingModule { }