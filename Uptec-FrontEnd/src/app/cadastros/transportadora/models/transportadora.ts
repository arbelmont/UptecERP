import { Validators, FormBuilder, FormGroup } from '@angular/forms';
export class Transportadora {
    id: string;
    cnpj: string;
    inscricaoEstadual: string;
    razaoSocial: string;
    nomeFantasia: string;
    endereco: EnderecoTransportadora;
    telefone: TelefoneTransportadora;
    email: string;
    website: string;
    tipoEntregaPadrao: number;
    observacoes: string;

    static validationMessages(): any {
        return {
            cnpj: {
                required: 'O Cnpj é requerido',
                maxlength: 'Cnpj inválido'
            },
            inscricaoEstadual: {
                maxlength: 'Inscrição Estadual inválida'
            },
            razaoSocial: {
                required: 'Razão Social é requerida',
                maxlength: 'Razão Social deve ter no máximo 200 caracteres'
            },
            nomeFantasia: {
                required: 'Nome Fantasia é requerido',
                maxlength: 'Nome Fantasia deve ter no máximo 200 caracteres'
            },
            tipoEntregaPadrao: {
                min: 'Selecione o tipo da entrega',
            },
            email: {
                email: 'Email inválido',
                maxlength: 'Email deve ter no máximo 100 caracteres'
            },
            website: {
                maxlength: 'O Site deve ter no máximo 200 caracteres'
            },

            numerot: {
                //required: 'O Número do telefone é requerido',
                maxlength: 'O Número do telefone deve ter no máximo 11 caracteres'
            },
            observacoest: {
                maxlength: 'As observações do telefone devem ter no máximo 1000 caracteres'
            },
            logradouro: {
                //required: 'Logradouro é requerido',
                maxlength: 'Logradouro deve ter no máximo 200 caracteres'
            },
            numero: {
                //required: 'O Número do endereço é requerido',
                maxlength: 'O Número do endereço deve ter no máximo 10 caracteres'
            },
            complemento: {
                maxlength: 'O Complemento do endereço deve ter no máximo 100 caracteres'
            },
            bairro: {
                maxlength: 'O Bairro deve ter no máximo 100 caracteres'
            },
            cep: {
                maxlength: 'Cep inválido'
            },
            cidade: {
                required: 'Selecione uma cidade'
            },
            estado: {
                required: 'Selecione um estado'
            },
            tipoEndereco: {
                min: 'Selecione o tipo do endereço'
            },
            observacoes: {
                maxlength: 'As observações devem ter no máximo 1000 caracteres'
            }
        };
    }

    static buildFormAdd(): any {
        let fb = new FormBuilder();
        return fb.group({
            cnpj: [null, [Validators.required, Validators.maxLength(18)]],
            inscricaoEstadual: [null, [Validators.maxLength(16)]],
            razaoSocial: [null, [Validators.required, Validators.maxLength(200)]],
            nomeFantasia: [null, [Validators.required, Validators.maxLength(200)]],
            tipoEntregaPadrao: [0, Validators.min(1)],
            email: [null, [Validators.email, Validators.maxLength(100)]],
            website: [null, [Validators.maxLength(200)]],
            observacoes: [null, [Validators.maxLength(1000)]],
            endereco: fb.group({
                logradouro: [null, [Validators.maxLength(200)]],
                numero: [null, [Validators.maxLength(10)]],
                complemento: [null, Validators.maxLength(100)],
                cep: [null, Validators.maxLength(8)],
                bairro: [null, Validators.maxLength(100)],
                cidade: ['', Validators.required],
                estado: ['', Validators.required],
                tipoEndereco: [0, Validators.min(1)]
            }),
            telefone: fb.group({
                contato: [null, Validators.maxLength(100)],
                numerot: [null, [Validators.maxLength(11)]],
                tipo: [2],
                whatsapp: [false],
                observacoest: [null, Validators.maxLength(1000)]
            })
        }, { updateOn: 'blur' });
    }

    static buildFormUpdate(): any {
        let fb = new FormBuilder();
        return fb.group({
            cnpj: [null, [Validators.required, Validators.maxLength(18)]],
            inscricaoEstadual: [null, [Validators.maxLength(12)]],
            razaoSocial: [null, [Validators.required, Validators.maxLength(200)]],
            nomeFantasia: [null, [Validators.required, Validators.maxLength(200)]],
            tipoEntregaPadrao: [0, Validators.min(1)],
            email: [null, [Validators.email, Validators.maxLength(100)]],
            website: [null, [Validators.maxLength(200)]],
            observacoes: [null, [Validators.maxLength(1000)]],
        }, { updateOn: 'blur' });
    }
}

export class EnderecoTransportadora {
    id: string;
    transportadoraId: string;
    logradouro: string;
    numero: string;
    complemento: string;
    bairro: string;
    cep: string;
    cidade: string;
    estado: string;
    obrigatorio: boolean;
    tipoEndereco: number;

    static validationMessages(): any {
        return {
            logradouro: {
                //required: 'Logradouro é requerido',
                maxlength: 'Logradouro deve ter no máximo 200 caracteres'
            },
            numero: {
                //required: 'O Número do endereço é requerido',
                maxlength: 'O Número do endereço deve ter no máximo 10 caracteres'
            },
            complemento: {
                maxlength: 'O Complemento do endereço deve ter no máximo 100 caracteres'
            },
            bairro: {
                maxlength: 'O Bairro deve ter no máximo 100 caracteres'
            },
            cep: {
                maxlength: 'Cep inválido'
            },
            cidade: {
                required: 'Selecione uma cidade'
            },
            estado: {
                required: 'Selecione um estado'
            },
            tipoEndereco: {
                min: 'Selecione o tipo do endereço'
            },
            observacoes: {
                maxlength: 'As observações devem ter no máximo 1000 caracteres'
            }
        };
    }

    static buildForm(): any {
        let fb = new FormBuilder();
        return fb.group({
                logradouro: [null, [Validators.maxLength(200)]],
                numero: [null, [Validators.maxLength(10)]],
                complemento: [null, Validators.maxLength(100)],
                cep: [null, Validators.maxLength(8)],
                bairro: [null, Validators.maxLength(100)],
                cidade: ['', Validators.required],
                estado: ['', Validators.required],
                tipoEndereco: [0, Validators.min(1)]
            }, { updateOn: 'blur' })
        };
}

export class TelefoneTransportadora {
    id: string;
    transportadoraId: string;
    numero: string;
    tipo: number;
    whatsapp: boolean;
    observacoes: string;
    contato: string;
    obrigatorio: boolean;

    static validationMessages(): any {
        return {
            numerot: {
                required: 'O Número do telefone é requerido',
                maxlength: 'O Número do telefone deve ter no máximo 11 caracteres'
            },
            tipo: {
                min: 'Selecione o tipo do telefone'
            },
            observacoest: {
                maxlength: 'As observações do telefone devem ter no máximo 1000 caracteres'
            },
            contato: {
                maxlength: 'O Contato deve ter no máximo 100 caracteres'
            }
        };
    }

    static buildForm(): any {
        let fb = new FormBuilder();
        return fb.group({
                numerot: [null, [Validators.maxLength(11), Validators.required]],
                tipo: [0, [Validators.min(1)]],
                whatsapp: [false],
                contato: [null, Validators.maxLength(100)],
                observacoest: [null, Validators.maxLength(1000)]
        }, { updateOn: 'blur' });
    }
}