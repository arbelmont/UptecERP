import { FormBuilder, Validators } from "@angular/forms";
import { formatDate } from "@angular/common";

export class Componente {
    id: string;
    codigo: string;
    descricao: string;
    unidade: number;
    preco: string;
    ncm: string;
    quantidade: string;
    quantidadeMinima: string;

    static validationMessages(): any {
        return {
            codigo: {
                required: 'O Código é requerido',
                maxlength: 'O Código deve ter no máximo 30 caracteres'
            },
            descricao: {
                required: 'A Descrição é requerida',
                maxlength: 'A Descrição deve ter no máximo 50 caracteres'
            },
            unidade: {
                min: 'Selecione a unidade de medida'
            },
            preco: {
                required: 'O Preço é requerido'
            },
            ncm: {
                required: 'O Código Ncm é requerido',
                maxlength: 'O Código Ncm deve ter no máximo 12 caracteres'
            }
        };
    }

    static buildForm(): any {
        let fb = new FormBuilder();
        return fb.group({
                codigo: [null, [Validators.maxLength(30), Validators.required]],
                descricao: [null, [Validators.maxLength(50), Validators.required]],
                unidade: [0, [Validators.min(1)]],
                preco: [null, [Validators.required]],
                ncm: [null, [ Validators.required, Validators.maxLength(12)]],
                quantidade: ['0'],
                quantidadeMinima: ['0'],
        }, { updateOn: 'blur' });
    }
}

export class ComponenteMovimento {
    id: string;
    componenteId: string;
    quantidade: number;
    data: Date;
    tipoMovimento: number;
    precoUnitario: number;
    notaFiscal: string;
    historico: string;

    static validationMessages(): any {
        return {
            quantidade: {
                min: 'Informe a Quantidade',
            },
            unidade: {
                min: 'Selecione a unidade de medida'
            },
            precoUnitario: {
                required: 'Informe o Preço Unitário',
                min: 'Informe o Preço Unitário'
            },
            notaFiscal: {
                maxlength: 'A Nota Fiscal deve ter no máximo 50 caracteres'
            },
            historico: {
                required: 'O Histórico do lançamento é requerido',
                maxlength: 'O Histórico deve ter no máximo 200 caracteres'
            }
        }
    };

    static buildForm(): any {
        let fb = new FormBuilder();
        return fb.group({
                quantidade: [null, [Validators.min(0.1)]],
                data: [formatDate(new Date(), 'yyyy-MM-dd', 'pt')],
                precoUnitario: [null, [Validators.required, Validators.min(0.1)]],
                notaFiscal: [null, [ Validators.maxLength(50)]],
                historico: [null, [Validators.required, Validators.maxLength(200)]],
        }, { updateOn: 'blur' });
    }
}