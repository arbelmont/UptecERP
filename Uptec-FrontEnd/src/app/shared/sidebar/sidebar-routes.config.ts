import { RouteInfo } from './sidebar.metadata';

//Sidebar menu Routes and data
export const ROUTES: RouteInfo[] = [
    {
        path: '/dashboard', title: 'Dashboard ', icon: 'ft-home', class: '', badge: '', badgeClass: '', isExternalLink: false, submenu: [], role:[]
    },
    {
        path: '#', title: 'Cadastros', icon: 'ft-layers', class: 'has-sub', badge: '', badgeClass: '', isExternalLink: false, role:['Master','Cadastro'],
        submenu: [
            { path: '/cliente', title: 'Clientes', icon: '', class: 'third-level', badge: '', badgeClass: '', isExternalLink: false, submenu: [], role:[] },
            { path: '/fornecedor', title: 'Fornecedores', icon: '', class: 'third-level', badge: '', badgeClass: '', isExternalLink: false, submenu: [], role:[] },
            { path: '/transportadora', title: 'Transportadoras', icon: '', class: 'third-level', badge: '', badgeClass: '', isExternalLink: false, submenu: [], role:[] }
        ]
    },
    {
        path: '#', title: 'Estoque', icon: 'ft-box', class: 'has-sub', badge: '', badgeClass: '', isExternalLink: false, role:['Master','Estoque'],
        submenu: [
            { path: '/peca', title: 'Peças', icon: '', class: 'third-level', badge: '', badgeClass: '', isExternalLink: false, submenu: [], role:[] },
            { path: '/lote', title: 'Lotes', icon: '', class: 'third-level', badge: '', badgeClass: '', isExternalLink: false, submenu: [], role:[] },
            { path: '/componente', title: 'Matéria Prima', icon: '', class: 'third-level', badge: '', badgeClass: '', isExternalLink: false, submenu: [], role:[] }
        ]
    },
    {
        path: '#', title: 'Notas Fiscais', icon: 'ft-inbox', class: 'has-sub', badge: '', badgeClass: '', isExternalLink: false, role:['Master','Fiscal'],
        submenu: [
            { path: '/manifesto', title: 'Manifestação', icon: '', class: 'third-level', badge: '', badgeClass: '', isExternalLink: false, submenu: [], role:[] },
            { path: '/concilia', title: 'Conciliação', icon: '', class: 'third-level', badge: '', badgeClass: '', isExternalLink: false, submenu: [], role:[] },
            { path: '/emissaoNfe', title: 'Emissão de Notas', icon: '', class: 'third-level', badge: '', badgeClass: '', isExternalLink: false, submenu: [], role:[] }
        ]
    },
    {
        path: '#', title: 'Produção', icon: 'ft-trending-up', class: 'has-sub', badge: '', badgeClass: '', isExternalLink: false, role:['Master','Producao'],
        submenu: [
            { path: '/ordem', title: 'Ordens Produção', icon: '', class: 'third-level', badge: '', badgeClass: '', isExternalLink: false, submenu: [], role:[] }
            
        ]
    },
    {
        path: '#', title: 'Qualidade', icon: 'ft-tag', class: '', badge: '', badgeClass: '', isExternalLink: false, submenu: [], role:['Master','Qualidade']
    },
    {
        path: '#', title: 'Financeiro', icon: 'ft-percent', class: 'has-sub', badge: '', badgeClass: '', isExternalLink: false, role:['Master','Financeiro'],
        submenu: [
            { path: '#', title: 'Contas a Pagar', icon: '', class: 'third-level', badge: '', badgeClass: '', isExternalLink: false, submenu: [], role:[] },
            { path: '#', title: 'Contas a Receber', icon: '', class: 'third-level', badge: '', badgeClass: '', isExternalLink: false, submenu: [], role:[] }
        ]
    },
    {
        path: '#', title: 'Segurança', icon: 'ft-lock', class: 'has-sub', badge: '', badgeClass: '', isExternalLink: false, role:['Master','Seguranca'],
        submenu: [
            { path: '#', title: 'Usuários', icon: '', class: 'third-level', badge: '', badgeClass: '', isExternalLink: false, submenu: [], role:[] },
            { path: '#', title: 'Perfis', icon: '', class: 'third-level', badge: '', badgeClass: '', isExternalLink: false, submenu: [], role:[] },
        ]
    }
];
