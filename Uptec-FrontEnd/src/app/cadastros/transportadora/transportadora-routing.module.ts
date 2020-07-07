import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { TransportadoraListComponent } from "./transportadora-list/transportadora-list.component";
import { TransportadoraAddComponent } from "./transportadora-add/transportadora-add.component";
import { TransportadoraUpdateComponent } from "./transportadora-update/transportadora-update.component";

const routes: Routes = [
    { path: '', component: TransportadoraListComponent },
    { path: 'adicionar', component: TransportadoraAddComponent },
    { path: 'editar/:id', component: TransportadoraUpdateComponent }
  ];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
  })
  export class TransportadoraRoutingModule { }