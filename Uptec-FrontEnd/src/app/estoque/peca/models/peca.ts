import { FormBuilder, Validators } from "@angular/forms";
import { Cliente } from "app/cadastros/cliente/models/cliente";

export class Peca {
    id: string;
    codigo: string;
    codigoSaida: string;
    descricao: string;
    unidade: number;
    tipo: number;
    preco: number;
    precoSaida: number;
    ncm: string;
    revisao: string;
    clienteId: string;
    componentes: PecaComponente[];
    codigosFornecedor: PecaFornecedorCodigo[];
    cliente: Cliente;

    static validationMessages(): any {
        return {
            codigo: {
                required: 'O Código é requerido',
                maxlength: 'O Código deve ter no máximo 30 caracteres'
            },
            codigoSaida: {
                required: 'O Código de saída é requerido',
                maxlength: 'O Código de saída deve ter no máximo 30 caracteres'
            },
            descricao: {
                required: 'A Descrição é requerida',
                maxlength: 'A Descrição deve ter no máximo 50 caracteres'
            },
            unidade: {
                min: 'Selecione a unidade de medida'
            },
            tipo: {
                min: 'Selecione o tipo da peça'
            },
            preco: {
                required: 'O Preço é requerido'
            },
            precoSaida: {
                required: 'O Preço de saída é requerido'
            },
            ncm: {
                required: 'O Codigo Ncm é requerido',
                maxlength: 'O Código Ncm deve ter no máximo 12 caracteres'
            },
            revisao: {
                maxlength: 'A Revisão deve ter no máximo 40 caracteres'
            },
            clienteId: {
                required: 'Selecione o Cliente',
                min: 'Selecione o cliente'
            }
        };
    }

    static buildForm(): any {
        let fb = new FormBuilder();
        return fb.group({
                codigo: [null, [Validators.maxLength(30), Validators.required]],
                codigoSaida: [null, [Validators.maxLength(30), Validators.required]],
                descricao: [null, [Validators.maxLength(50), Validators.required]],
                unidade: [0, [Validators.min(1)]],
                tipo: [0, [Validators.min(1)]],
                preco: [null, [Validators.required]],
                precoSaida: [null, [Validators.required]],
                ncm: [null, [Validators.required, Validators.maxLength(12)]],
                revisao: [null, [Validators.maxLength(40)]],
                clienteId: ['', [Validators.required ]],
        }, { updateOn: 'blur' });
    }
}

export class PecaComponente {
    componenteId: string;
    quantidade: number;
    pecaId: string;
    descricao: string;

    static validationMessages(): any {
        return {
            componenteId: {
                required: 'Selecione o Componente',
                min: 'Selecione o Componente'
            },
            quantidade: {
                required: 'A Quantidade é requerida',
                min: 'Quantidade inválida'
            }
            
        };
    }

    static buildForm(): any {
        let fb = new FormBuilder();
        return fb.group({
                componenteId: ['', [Validators.required ]],
                quantidade: [null, [Validators.required, Validators.min(-1)]]
        }, { updateOn: 'blur' });
    }
}

/* export class PecaComponenteFull {
    componenteId: string;
    quantidade: number;
    pecaId: string;
    descricao: string;
} */

export class PecaFornecedorCodigo {
    fornecedorId: string;
    fornecedorCodigo: string;
    pecaId: string;
    descricao: string;

    static validationMessages(): any {
        return {
            fornecedorId: {
                required: 'Selecione o Fornecedor',
                min: 'Selecione o Fornecedor'
            },
            fornecedorCodigo: {
                required: 'Informe o código da peça para o fornecedor',
                maxlength: 'O Código da Peça máximo 30 caracteres'
            }
            
        };
    }

    static buildForm(): any {
        let fb = new FormBuilder();
        return fb.group({
                fornecedorId: ['', [Validators.required ]],
                fornecedorCodigo: [null, [Validators.required, Validators.maxLength(30)]]
        }, { updateOn: 'blur' });
    }
}