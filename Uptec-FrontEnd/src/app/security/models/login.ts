export class Login {
    email: string;
    password: string;

    static validationMessages(): any {
        return {
            email: {
                required: 'O Email é requerido',
                email: 'O Email informado é inválido'
            },
            password: {
                required: 'Informe a Senha'
            }
        };
    }
}