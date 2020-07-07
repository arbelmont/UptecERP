import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { throwError, Observable } from 'rxjs';
import { BaseService } from './../../../shared/services/base.service';
import { Componente, ComponenteMovimento } from '../models/componente';

@Injectable()
export class ComponenteService extends BaseService {
    
    constructor(private http: HttpClient) {
        super();
    }

    get(id: string) : any {
        return this.http.get<Componente>(`${this.API}/Componente/${id}`)
            .pipe(map((res : any) => this.extractData(res)), 
                    catchError(err => throwError(err)));
    }

    getPaged(pageNumber: number, pageSize: number, search: string): any {
        let url = search ? 
            `${this.API}/Componente/${pageNumber}/${pageSize}?search=${search}` :
            `${this.API}/Componente/${pageNumber}/${pageSize}`;
        return this.http.get<any>(url)
            .pipe(catchError(err => throwError(err)));
    }

    getPagedMovimento(pageNumber: number, pageSize: number, componenteId: string, startDate: string, endDate: string): any {
        let url = `${this.API}/Componente/Movimentos/${pageNumber}/${pageSize}?componenteId=${componenteId}&startDate=${startDate}&endDate=${endDate}`;
        return this.http.get<any>(url)
            .pipe(catchError(err => throwError(err)));
    }

    add(componente: Componente) {
        return this.http.post(`${this.API}/Componente`, componente);
    }

    update(componente: Componente) {
        return this.http.put(`${this.API}/Componente`, componente);
    }

    delete(id : string) {
        return this.http.delete(`${this.API}/Componente/${id}`);
    }

    getMovimentoById(id: string) : any {
        return this.http.get<ComponenteMovimento>(`${this.API}/Componente/Movimentos/${id}`)
            .pipe(map((res : any) => this.extractData(res)), 
                    catchError(err => throwError(err)));
    }

    addMovimento(movimento: ComponenteMovimento) {
        return this.http.post(`${this.API}/Componente/Movimentos`, movimento);
    }
}