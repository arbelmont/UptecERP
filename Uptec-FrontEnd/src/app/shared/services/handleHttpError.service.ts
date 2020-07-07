import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { AuthService } from './../../security/auth/auth.service';
import { ToastrService } from 'ngx-toastr';

@Injectable({
    providedIn: 'root'
})
export class HandleHttpErrorService {

    private listErrors: any[] = [];

    constructor(private auth: AuthService,
        private toastr: ToastrService) { }

    handleHttpError(error: HttpErrorResponse): void {
        if (error.status === 401) {
            this.auth.logout();
        }
        else if (error.status === 403) {
            this.toastr.error("Acesso Negado.", "Atenção!", { enableHtml: true })
        }
        else if (error.status === 400) {
            this.showDomainErrors(error)
        }
        else if (error.status === 500) {
            this.toastr.error("Erro interno do servidor. Contate a equipe de suporte.","Erro!", { enableHtml: true })
        } 
        else if (error.status === 0) {
            this.toastr.error("Servidor indisponível.","Erro!", { enableHtml: true })
        }
        else {
            this.toastr.error("Erro inesperado.","Erro!", { enableHtml: true })
        }
    }

    private showDomainErrors(error: any) {
        this.listErrors = error.error.errors;
        let errorsHtml: string = "";

        for (let err of this.listErrors) {
            errorsHtml += `<p>${err}</p>`;
        }

        this.toastr.error(errorsHtml, "Atenção!", { enableHtml: true });
    }
}