import { Arquivo, NotaEntrada, NotaEntradaTipoEmissor } from './../models/nota-entrada';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { throwError, Observable } from 'rxjs';
import { BaseService } from './../../../shared/services/base.service';
import { EnumType } from 'app/shared/models/enumType';

@Injectable()
export class NotaService extends BaseService {
    
    constructor(private http: HttpClient) {
        super();
    }

    upload(arquivo: FormData) {
        return this.http.post(`${this.API}/NotaEntrada`, arquivo);
    }

    getPaged(pageNumber: number, pageSize: number, tipoEmissor: number, status: number, 
        startDate: string, endDate: string, search: string): any {
        let url = search ? 
        `${this.API}/NotaEntrada/${pageNumber}/${pageSize}?tipoEmissor=${tipoEmissor}&status=${status}&startDate=${startDate}&endDate=${endDate}&search=${search}` :
        `${this.API}/NotaEntrada/${pageNumber}/${pageSize}?tipoEmissor=${tipoEmissor}&status=${status}&startDate=${startDate}&endDate=${endDate}`;
        return this.http.get<any>(url)
            .pipe(catchError(err => throwError(err)));
    }

    getByIdConsisted(id: string) : any {
        let url = `${this.API}/NotaEntrada/GetFullByIdConsisted/${id}`;
        return this.http.get<NotaEntrada>(url)
            .pipe(map((res : any) => this.extractData(res)), 
                catchError(err => throwError(err)));
    }

    getNotasClientesConciliar(): Observable<EnumType[]> {
        return this.http.get<EnumType[]>(`${this.API}/NotaEntrada/GetNotasClientesConciliar`)
            .pipe(map((res : any) => this.extractData(res)), 
                  catchError(err => throwError(err))
                  );
    }

    definirTipoEmissor(nota: NotaEntradaTipoEmissor) {
        return this.http.put(`${this.API}/NotaEntrada/DefinirTipoEmissor`, nota); 
    }

    conciliar(nota: NotaEntrada) {
        return this.http.put(`${this.API}/NotaEntrada/Conciliar`, nota);
    }

    cobrir(conciliacao: any) {
        return this.http.put(`${this.API}/NotaEntrada/Cobrir`, conciliacao);
    }

    delete(id : string) {
        return this.http.delete(`${this.API}/NotaEntrada/${id}`);
    }
}