import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { ClientListComponent } from "./client-list/client-list.component";
import { ClienteAddComponent } from "./cliente-add/cliente-add.component";
import { ClienteUpdateComponent } from "./cliente-update/cliente-update.component";

const routes: Routes = [
    { path: '', component: ClientListComponent },
    { path: 'adicionar', component: ClienteAddComponent },
    { path: 'editar/:id', component: ClienteUpdateComponent }
    /* { path: '', component: TransportadoraListComponent },
    { path: 'adicionar', component: TransportadoraAddComponent },
    { path: 'editar/:id', component: TransportadoraUpdateComponent } */
  ];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
  })
  export class ClienteRoutingModule { }