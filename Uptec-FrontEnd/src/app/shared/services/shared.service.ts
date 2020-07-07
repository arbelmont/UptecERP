import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError, map } from "rxjs/operators";
import { BaseService } from "./base.service";
import { Estado } from "../models/estado";
import { EnumType } from "../models/enumType";
import { Cidade } from "../models/cidade";


@Injectable()
export class SharedService extends BaseService {
    constructor(private http: HttpClient){
        super();
    }

    
    getEstados(): Observable<Estado[]> {
        return this.http.get<Estado[]>(`${this.API}/Shared/Estados`)
            .pipe(map((res : any) => this.extractData(res)), 
                  catchError(err => throwError(err))
                  );
    }

    getCidades(uf: string) : Observable<Cidade[]> {
        return this.http.get<Cidade[]>(`${this.API}/Shared/Cidades/${uf}`)
            .pipe(map((res : any) => this.extractData(res)), 
                  catchError(err => throwError(err))
                  );
    }

    getTelefoneTipos(): Observable<EnumType[]> {
        return this.http.get<EnumType[]>(`${this.API}/Shared/TelefoneTipos`)
            .pipe(map((res : any) => this.extractData(res)), 
                  catchError(err => throwError(err))
                  );
    }

    getEnderecoTipos(): Observable<EnumType[]> {
        return this.http.get<EnumType[]>(`${this.API}/Shared/EnderecoTipos`)
            .pipe(map((res : any) => this.extractData(res)), 
                  catchError(err => throwError(err))
                  );
    }

    getUnidadesMedida(): Observable<EnumType[]> {
        return this.http.get<EnumType[]>(`${this.API}/Shared/UnidadesMedida`)
            .pipe(map((res : any) => this.extractData(res)), 
                  catchError(err => throwError(err))
                  );
    }

    getTransportadoras(): Observable<EnumType[]> {
        return this.http.get<EnumType[]>(`${this.API}/Shared/Transportadoras`)
            .pipe(map((res : any) => this.extractData(res)), 
                  catchError(err => throwError(err))
                  );
    }

    getClientes(): Observable<EnumType[]> {
        return this.http.get<EnumType[]>(`${this.API}/Shared/Clientes`)
            .pipe(map((res : any) => this.extractData(res)), 
                  catchError(err => throwError(err))
                  );
    }

    getFornecedores(): Observable<EnumType[]> {
        return this.http.get<EnumType[]>(`${this.API}/Shared/Fornecedores`)
            .pipe(map((res : any) => this.extractData(res)), 
                  catchError(err => throwError(err))
                  );
    }

    getComponentes(): Observable<EnumType[]> {
        return this.http.get<EnumType[]>(`${this.API}/Shared/Componentes`)
            .pipe(map((res : any) => this.extractData(res)), 
                  catchError(err => throwError(err))
                  );
    }

    getTipoEmissor(): Observable<EnumType[]> {
        return this.http.get<EnumType[]>(`${this.API}/Shared/TipoEmissor`)
            .pipe(map((res : any) => this.extractData(res)), 
                  catchError(err => throwError(err))
                  );
    }

    getStatusNfEntrada(): Observable<EnumType[]> {
        return this.http.get<EnumType[]>(`${this.API}/Shared/StatusNfEntrada`)
            .pipe(map((res : any) => this.extractData(res)), 
                  catchError(err => throwError(err))
                  );
    }

    getPecas(): Observable<EnumType[]> {
        return this.http.get<EnumType[]>(`${this.API}/Peca/Dropdown`)
            .pipe(map((res : any) => this.extractData(res)), 
                  catchError(err => throwError(err))
                  );
    }

    /* obterTiposTelefone(): Observable<any> {
        return this.http.get<Response>(`${this.API}/Clinica/Telefone/Tipos`,  this.accountService.getAuthOptions())
        .pipe(catchError(err => throwError(err)));
    } */
}