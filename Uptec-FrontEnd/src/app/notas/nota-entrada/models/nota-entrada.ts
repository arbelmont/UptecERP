import { FormBuilder, Validators } from "@angular/forms";

export class NotaEntrada {
        id: string;
        numeroNota: string;
        numeroNotaCobertura: string;
        data: Date;
        dataConciliacao: Date;
        valor: number;
        cfop: string;
        cnpjEmissor: string;
        nomeEmissor: string;
        emailEmissor: string;
        tipoEmissor: number;
        tipoEstoque: number;
        status: number;
        arquivoId: string;
        qtdeNotasAcobrir: number;
        itens: NotaEntradaItens[];
        inconsistencias: string[];
        proximoLote: number;
        

    static validationMessages(): any {
        return {};
    }

    static buildForm(): any {
        let fb = new FormBuilder();
        return fb.group({}, { updateOn: 'blur' });
    }
}

export class NotaEntradaItens {
        id: string;
        codigo: string;
        codigoCliente: string;
        descricao: string;
        cfop: string;
        unidade: number;
        precoUnitario: number;
        precoTotal: number;
        quantidade: number;
        dataFabricacao: Date;
        dataValidade: Date;
        localizacao: string;
        qtdeConcilia: number;
        numeroNotaCobertura: string;
        notaEntradaId: string;
        lote: number;
        status: number;
        cobrir: boolean;
}

export class Arquivo {
    dados: any;

    static validationMessages(): any {
        return {
            
        };
    }

    static buildForm(): any {
        let fb = new FormBuilder();
        return fb.group({
            dados: [null]
        }, { updateOn: 'blur' });
    }
}

export class NotaEntradaTipoEmissor {
    id: string;
    tipoEmissor: number;
}
