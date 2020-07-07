import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { NotaSaidaListComponent } from "./nota-saida-list/nota-saida-list.component";
import { NotaAddPecaComponent } from "./nota-add-peca/nota-add-peca.component";
import { NotaAddEmbalagemComponent } from "./nota-add-embalagem/nota-add-embalagem.component";
import { NotaAddPecaAvulsaComponent } from "./nota-add-peca-avulsa/nota-add-peca-avulsa.component";

const routes: Routes = [
  { path: '', component: NotaSaidaListComponent },
  { path: 'adicionarPeca', component: NotaAddPecaComponent },
  { path: 'adicionarEmbalagem', component: NotaAddEmbalagemComponent },
  { path: 'adicionarPecaAvulsa', component: NotaAddPecaAvulsaComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class NotaSaidaRoutingModule { }