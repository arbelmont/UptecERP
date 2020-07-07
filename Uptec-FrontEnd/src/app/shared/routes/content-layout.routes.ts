import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from 'app/security/auth/auth-guard.service';

//Route for content layout without sidebar, navbar and footer for pages like Login, Registration etc...

export const CONTENT_ROUTES: Routes = [
    { path: 'dashboard', loadChildren: './dashboard/dashboard.module#DashboardModule' },
    { path: 'cliente', loadChildren: './cadastros/cliente/cliente.module#ClienteModule', canActivate: [AuthGuard], data: [{ claim: [ 'Master', 'Cadastro' ]}]  },
    { path: 'transportadora', loadChildren: './cadastros/transportadora/transportadora.module#TransportadoraModule' },
    { path: 'fornecedor', loadChildren: './cadastros/fornecedor/fornecedor.module#FornecedorModule' },
    { path: 'peca', loadChildren: './estoque/peca/peca.module#PecaModule' },
    { path: 'componente', loadChildren: './estoque/componente/componente.module#ComponenteModule' },
    { path: 'lote', loadChildren: './estoque/lote/lote.module#LoteModule' },
    { path: 'manifesto', loadChildren: './notas/nota-manifesto/nota-manifesto.module#NotaManifestoModule' },
    { path: 'concilia', loadChildren: './notas/nota-entrada/nota-entrada.module#NotaEntradaModule' },
    { path: 'emissaoNfe', loadChildren: './notas/nota-saida/nota-saida.module#NotaSaidaModule' },
    { path: 'ordem', loadChildren: './producao/ordem/ordem.module#OrdemModule' }
];