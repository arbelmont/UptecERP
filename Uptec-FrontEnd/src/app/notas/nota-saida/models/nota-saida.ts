import { Fornecedor } from './../../../cadastros/fornecedor/models/fornecedor';
import { Cliente } from 'app/cadastros/cliente/models/cliente';
import { TipoNotaSaida } from 'app/shared/enums/tipoNotaSaida';
export class NotaSaida {
    id: string;
    clienteId: string;
    fornecedorId: string;
    enderecoId: string;
    transportadoraId: string;
    cliente: Cliente;
    fornecedor: Fornecedor;
    itens: NotaSaidaItem[];
    data: Date;
    numeroNota: string;
    tipoDestinatario: number;
    valorIcms: number;
    valorIpi: number;
    valorPis: number;
    valorCofins: number;
    valorTotalProdutos: number;
    valorFrete: number;
    valorSeguro: number;
    valorDesconto: number;
    valorOutrasDespesas: number;
    valorBaseCalculo: number;
    valorTotalNota: number;
    destinatario: any;
    enderecoCompleto: string;
    status: number;
    outrasInformacoes: string;
    erroApi: string;
}




export class NotaSaidaItem {
    id: number;
    codigo: string;
    descricao: string;
    cfop: string;
    ncm: string;
    unidade: number;
    valorUnitario: number;
    aliquotaBaseCalculo: number;
    aliquotaIcms: number;
    aliquotaIpi: number;
    aliquotaIva: number;
    aliquotaPis: number;
    aliquotaCofins: number;
    valorBaseCalculo: number;
    valorIcms: number;
    valorPis: number;
    valorCofins: number;
    valorTotal: number;
    quantidade: number;
    ordemNumero: number;
    loteNumero: number;
    loteSequencia: number;
    notaSaidaId: string;

    static LoteSequenciaString(lote: number, sequencia: number) {
        if(sequencia > 0)
            return `${lote}/${sequencia}`;
        return lote;
    }
}

export class NotaSaidaAdd {
    id: string;
    destinatarioId: string;
    enderecoId: string;
    transportadoraId: string;
    tipoDestinatario: number;
    valorFrete: number;
    valorSeguro: number;
    valorOutrasDespesas: number;
    valorDesconto: number;
    outrasInformacoes: string;
    tipoNota: TipoNotaSaida;
    ordemItens: NotaSaidaItemAdd[];
    loteItens: NotaSaidaItemAdd[];
}


export class NotaSaidaItemAdd {
    id: string;
    codigo: string;
    descricao: string;
    cfop: string;
    unidadeMedida: number;
    precoUnitario: number;
    precoTotal: number;
    quantidade: number;
    ordemNumero: number;
    ordemLoteId: string;
    loteId: string;
    loteNumero: number;
    loteSequencia: number;
    loteSequenciaString: string;
    notaSaidaId: string;
    tipoItem: number;
}

/* export class Destinatario {
    cnpj: string;
    nome: string;
    email: string;
} */

export class Endereco {

}
