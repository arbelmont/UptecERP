import { FormBuilder, Validators } from "@angular/forms";
import { formatDate } from "@angular/common";
import { Peca } from "app/estoque/peca/models/peca";

export class Lote {
    id: string;
    loteNumero: number;
    data: Date;
    pecaId: string;
    tipoPeca: number;
    quantidade: number;
    saldo: number;
    precoEntrada: number;
    notaFiscal: string;
    notaFiscalCobertura: string;
    dataFabricacao: Date;
    dataValidade: Date;
    localizacao: string;
    qtdeConcilia: number;
    peca: Peca;
    status: number;
    sequencia: number;
    loteSequenciaString: string;
}

export class LoteMovimento {
    id: string;
    data: Date;
    loteId: string;
    loteSequencia: number;
    quantidade: number;
    precoUnitario: number;
    precoTotal: number;
    notaFiscal: string;
    tipoMovimento: number;
    historico: string;

    static validationMessages(): any {
        return {
            loteId: {
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
            }
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

export class LoteDadosSaida {
    id: string;
    precoSaidaServico: number;
    precoSaidaRemessa: number;
    cfopSaidaServico: string;
    cfopSaidaRemessa: string;
    unidadeMedida: number;
    codigoPeca: string;
    codigoPecaSaida: string;
}