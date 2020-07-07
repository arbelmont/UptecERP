import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { throwError, Observable } from 'rxjs';
import { BaseService } from './../../../shared/services/base.service';
import { Lote, LoteMovimento, LoteDadosSaida } from '../models/lote';
import { LoteSaldo } from '../models/lote-saldo';

@Injectable({
    providedIn: 'root'
})
export class LoteService extends BaseService {
    
    constructor(private http: HttpClient) {
        super();
    }

    get(id: string) : any {
        return this.http.get<Lote>(`${this.API}/Lote/${id}`)
            .pipe(map((res : any) => this.extractData(res)), 
                    catchError(err => throwError(err)));
    }
    getFull(id: string) : any {
        return this.http.get<Lote>(`${this.API}/Lote/GetFullById/${id}`)
            .pipe(map((res : any) => this.extractData(res)), 
                    catchError(err => throwError(err)));
    }

    getByNumeroNota(numeroNota: string) : any {
        return this.http.get<Lote>(`${this.API}/Lote/GetFullLotesByNota/?numeroNota=${numeroNota}`)
            .pipe(map((res : any) => this.extractData(res)), 
                    catchError(err => throwError(err)));
    }

    getPaged(pageNumber: number, pageSize: number, showLoteFechado: boolean, search: string): any {
        let url = search ? 
            `${this.API}/Lote/${pageNumber}/${pageSize}/${showLoteFechado}?search=${search}` :
            `${this.API}/Lote/${pageNumber}/${pageSize}/${showLoteFechado}`;
        return this.http.get<any>(url)
            .pipe(catchError(err => throwError(err)));
    }

    getPagedByPeca(pageNumber: number, pageSize: number, pecaId: string, showLoteFechado: boolean): any {
        let url = `${this.API}/Lote/${pageNumber}/${pageSize}/${pecaId}/${showLoteFechado}`;
        return this.http.get<any>(url)
            .pipe(catchError(err => throwError(err)));
    }

    getFullPagedByPeca(pageNumber: number, pageSize: number, pecaId: string, showLoteFechado: boolean): any {
        let url = `${this.API}/Lote/GetFullPagedByPeca/${pageNumber}/${pageSize}/${pecaId}/${showLoteFechado}`;
        return this.http.get<any>(url)
            .pipe(catchError(err => throwError(err)));
    }

    getPagedMovimentoByLote(pageNumber: number, pageSize: number, loteId: string): any {
        let url = `${this.API}/Lote/Movimentos/${pageNumber}/${pageSize}/${loteId}`;
        return this.http.get<any>(url)
            .pipe(catchError(err => throwError(err)));
    }

    getMovimentoById(id: string) : any {
        return this.http.get<LoteMovimento>(`${this.API}/Lote/Movimentos/${id}`)
            .pipe(map((res : any) => this.extractData(res)), 
                    catchError(err => throwError(err)));
    }

    getDadosSaida(id: string) : any {
        return this.http.get<LoteDadosSaida>(`${this.API}/Lote/GetLoteDadosSaida/${id}`)
            .pipe(map((res : any) => this.extractData(res)), 
                    catchError(err => throwError(err)));
    }

    GetLoteSequenceLastUsed() : any {
        return this.http.get<number>(`${this.API}/Lote/GetLoteSequenceLastUsed`)
            .pipe(map((res : any) => this.extractData(res)), 
                    catchError(err => throwError(err)));
    }

    getSaldos() : any {
        return this.http.get<LoteSaldo[]>(`${this.API}/Lote/Saldos`)
            .pipe(map((res : any) => this.extractData(res)), 
                    catchError(err => throwError(err)));
    }

    addMovimento(movimento: LoteMovimento) {
        return this.http.post(`${this.API}/Lote/Movimentos`, movimento);
    }

    /* add(lote: Lote) {
        return this.http.post(`${this.API}/Lote`, lote);
    } */

    /* update(lote: Lote) {
        return this.http.put(`${this.API}/Lote`, lote);
    } */

    /* delete(id : string) {
        return this.http.delete(`${this.API}/Lote/${id}`);
    } */

}