import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { throwError, Observable } from 'rxjs';
import { BaseService } from './../../../shared/services/base.service';
import { Peca } from '../models/peca';

@Injectable({
    providedIn: 'root'
})
export class PecaService extends BaseService {
    
    constructor(private http: HttpClient) {
        super();
    }

    get(id: string) : any {
        return this.http.get<Peca>(`${this.API}/Peca/${id}`)
            .pipe(map((res : any) => this.extractData(res)), 
                    catchError(err => throwError(err)));
    }
    getFull(id: string) : any {
        return this.http.get<Peca>(`${this.API}/Peca/GetFullById/${id}`)
            .pipe(map((res : any) => this.extractData(res)), 
                    catchError(err => throwError(err)));
    }

    getPaged(pageNumber: number, pageSize: number, search: string): any {
        let url = search ? 
            `${this.API}/Peca/${pageNumber}/${pageSize}?search=${search}` :
            `${this.API}/Peca/${pageNumber}/${pageSize}`;
        return this.http.get<any>(url)
            .pipe(catchError(err => throwError(err)));
    }

    getToProducao(search: string): Observable<Peca[]> {
        return this.http.get<Peca[]>(`${this.API}/Peca/GetToProducao?search=${search}`)
            .pipe(map((res : any) => this.extractData(res)), 
                catchError(err => throwError(err)));
    }

    add(peca: Peca) {
        return this.http.post(`${this.API}/Peca`, peca);
    }

    update(peca: Peca) {
        return this.http.put(`${this.API}/Peca`, peca);
    }

    delete(id : string) {
        return this.http.delete(`${this.API}/Peca/${id}`);
    }

    
}