import { LinhaProducao } from './../models/linha-producao';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { BaseService } from './../../../shared/services/base.service';
import { Ordem, OrdemLote } from '../models/ordem';

@Injectable({
    providedIn: 'root'
})
export class OrdemService extends BaseService {
    
    constructor(private http: HttpClient) {
        super();
    }

    get(id: string) : any {
        return this.http.get<Ordem>(`${this.API}/Ordem/${id}`)
            .pipe(map((res : any) => this.extractData(res)), 
                    catchError(err => throwError(err)));
    }

    getFullById(id: string) : any {
        return this.http.get<Ordem>(`${this.API}/Ordem/GetFullById/${id}`)
            .pipe(map((res : any) => this.extractData(res)), 
                    catchError(err => throwError(err)));
    }

    getPaged(pageNumber: number, pageSize: number, status: number, campo: string, search: string): any {
        let url = `${this.API}/Ordem/${pageNumber}/${pageSize}/?status=${status}&campo=${campo}&search=${search}`;

        return this.http.get<any>(url)
            .pipe(catchError(err => throwError(err)));
    }

    getOrdemLoteToNfeSaida(clienteId: string) : any {
        return this.http.get<OrdemLote[]>(`${this.API}/Ordem/OrdemLotes/${clienteId}`)
            .pipe(map((res : any) => this.extractData(res)), 
                    catchError(err => throwError(err)));
    }

    getLinhaProducao() : any {
        return this.http.get<LinhaProducao[]>(`${this.API}/Ordem/LinhaProducao`)
            .pipe(map((res : any) => this.extractData(res)), 
                    catchError(err => throwError(err)));
    }

    add(ordem: Ordem) {
        return this.http.post(`${this.API}/Ordem`, ordem);
    }

    finalizar(ordem: any) {
        return this.http.put(`${this.API}/Ordem`, ordem);
    }

    delete(id : string) {
        return this.http.delete(`${this.API}/Ordem/${id}`);
    }

    /* getByNumeroNota(numeroNota: string) : any {
        return this.http.get<Ordem>(`${this.API}/Ordem/GetFullOrdemsByNota/?numeroNota=${numeroNota}`)
            .pipe(map((res : any) => this.extractData(res)), 
                    catchError(err => throwError(err)));
    }

    getPaged(pageNumber: number, pageSize: number, showOrdemFechado: boolean, search: string): any {
        let url = search ? 
            `${this.API}/Ordem/${pageNumber}/${pageSize}/${showOrdemFechado}?search=${search}` :
            `${this.API}/Ordem/${pageNumber}/${pageSize}/${showOrdemFechado}`;
        return this.http.get<any>(url)
            .pipe(catchError(err => throwError(err)));
    }

    getPagedByPeca(pageNumber: number, pageSize: number, pecaId: string, showOrdemFechado: boolean): any {
        let url = `${this.API}/Ordem/${pageNumber}/${pageSize}/${pecaId}/${showOrdemFechado}`;
        return this.http.get<any>(url)
            .pipe(catchError(err => throwError(err)));
    }

    getPagedMovimentoByOrdem(pageNumber: number, pageSize: number, ordemId: string): any {
        let url = `${this.API}/Ordem/Movimentos/${pageNumber}/${pageSize}/${ordemId}`;
        return this.http.get<any>(url)
            .pipe(catchError(err => throwError(err)));
    }

    getMovimentoById(id: string) : any {
        return this.http.get<OrdemMovimento>(`${this.API}/Ordem/Movimentos/${id}`)
            .pipe(map((res : any) => this.extractData(res)), 
                    catchError(err => throwError(err)));
    }

    GetOrdemSequenceLastUsed() : any {
        return this.http.get<number>(`${this.API}/Ordem/GetOrdemSequenceLastUsed`)
            .pipe(map((res : any) => this.extractData(res)), 
                    catchError(err => throwError(err)));
    }

    addMovimento(movimento: OrdemMovimento) {
        return this.http.post(`${this.API}/Ordem/Movimentos`, movimento);
    } */


}