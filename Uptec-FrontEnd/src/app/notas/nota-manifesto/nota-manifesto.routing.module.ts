import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { NotaManifestoListComponent } from "./nota-manifesto-list/nota-manifesto-list.component";


const routes: Routes = [
  { path: '', component: NotaManifestoListComponent }
  /* { path: '', component: PecaListComponent },
  { path: 'adicionar', component: PecaFormComponent },
  { path: 'editar/:id', component: PecaFormComponent },
  { path: 'lote/:id', component: PecaLoteListComponent } */
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class NotaManifestoRoutingModule { }