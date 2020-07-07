import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { ComponenteListComponent } from "./componente-list/componente-list.component";
import { ComponenteAddComponent } from "./componente-add/componente-add.component";
import { ComponenteUpdateComponent } from "./componente-update/componente-update.component";
import { ComponenteMovimentoComponent } from "./componente-movimento/componente-movimento.component";

const routes: Routes = [
    { path: '', component: ComponenteListComponent },
    { path: 'adicionar', component: ComponenteAddComponent },
    { path: 'editar/:id', component: ComponenteUpdateComponent },
    { path: 'movimento/:id', component: ComponenteMovimentoComponent }
    /* { path: '', component: TransportadoraListComponent },
    { path: 'adicionar', component: TransportadoraAddComponent },
    { path: 'editar/:id', component: TransportadoraUpdateComponent } */
  ];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
  })
  export class ComponenteRoutingModule { }