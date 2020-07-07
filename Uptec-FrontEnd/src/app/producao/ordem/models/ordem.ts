import { FormBuilder, Validators } from "@angular/forms";
import { formatDate } from "@angular/common";
import { Cliente } from "app/cadastros/cliente/models/cliente";
import { Fornecedor } from "app/cadastros/fornecedor/models/fornecedor";
import { Lote } from "app/estoque/lote/models/lote";

export class Ordem {
    id: string;
    ordemNumero: number;
    dataEmissao: Date;
    dataProducao: Date;
    qtdeTotal: number;
    qtdeTotalProduzida: number;
    status: number;
    clienteId: string;
    fornecedorId: string;
    codigoPeca: string;
    descricaoPeca: string;
    ordemLotes: OrdemLote[];
    cliente: Cliente[];
    fornecedor: Fornecedor[];
}

export class OrdemLote {
    id: string;
    loteNumero: number;
    loteSequencia: number;
    data: Date;
    codigo: string;
    descricao: string;
    saldo: number;
    qtde: number;
    qtdeProduzida: number;
    validade: Date;
    motivoExpedicao: number;
    loteId: string;
    ordemId: string;
    lote: Lote;
    loteSequenciaString: string;
    notaFiscalSaida: string;
    ordem: Ordem;

    static validationMessages(): any {
        return {
            /* loteId: {
                required: 'Selecione o Lote'
            },
            quantidade: {
                min: 'Quantidade inválida',
                required: 'Informe a Quantidade'
            },
            historico: {
                required: 'O Histórico do lançamento é requerido',
                maxlength: 'O Histórico deve ter no máximo 200 caracteres'
            },
            notaFiscal: {
                maxlength: 'A NotaFiscal deve ter no máximo 50 caracteres'
            } */
        }
    }

    static buildForm(): any {
        let fb = new FormBuilder();
        return fb.group({
                loteId: ['', [Validators.required]],
                quantidade: [null, [Validators.required, Validators.min(0.1)]],
                precoUnitario: [null],
                notaFiscal: [null, [Validators.maxLength(50)]],
                data: [formatDate(new Date(), 'yyyy-MM-dd', 'pt')],
                historico: [null, [Validators.required, Validators.maxLength(200)]],
        }, { updateOn: 'blur' });
    }
}