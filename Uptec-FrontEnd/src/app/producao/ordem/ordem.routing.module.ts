import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { OrdemListComponent } from "./ordem-list/ordem-list.component";
import { OrdemAddComponent } from "./ordem-add/ordem-add.component";

const routes: Routes = [
  { path: '', component: OrdemListComponent },
  { path: 'adicionar', component: OrdemAddComponent }
  /* { path: '', component: PecaListComponent },
  { path: 'adicionar', component: PecaFormComponent },
  { path: 'editar/:id', component: PecaFormComponent },
  { path: 'lote/:id', component: PecaLoteListComponent } */
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class OrdemRoutingModule { }