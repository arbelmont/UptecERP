import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { NotaEntradaListComponent } from "./nota-entrada-list/nota-entrada-list.component";

const routes: Routes = [
  { path: '', component: NotaEntradaListComponent }
  /* { path: '', component: PecaListComponent },
  { path: 'adicionar', component: PecaFormComponent },
  { path: 'editar/:id', component: PecaFormComponent },
  { path: 'lote/:id', component: PecaLoteListComponent } */
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class NotaEntradaRoutingModule { }