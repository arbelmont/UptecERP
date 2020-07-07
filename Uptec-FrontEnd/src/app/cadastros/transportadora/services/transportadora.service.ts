import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { throwError, Observable } from 'rxjs';
import { BaseService } from './../../../shared/services/base.service';
import { Transportadora, EnderecoTransportadora, TelefoneTransportadora } from './../models/transportadora';
import { EnumType } from 'app/shared/models/enumType';

@Injectable()
export class TransportadoraService extends BaseService {
    
    constructor(private http: HttpClient) {
        super();
    }

    get(id: string) : any {
        return this.http.get<Transportadora>(`${this.API}/Transportadora/${id}`)
            .pipe(map((res : any) => this.extractData(res)), 
                    catchError(err => throwError(err)));
    }

    getPaged(pageNumber: number, pageSize: number, search: string): any {
        let url = search ? 
            `${this.API}/Transportadora/${pageNumber}/${pageSize}?search=${search}` :
            `${this.API}/Transportadora/${pageNumber}/${pageSize}`;
        return this.http.get<any>(url)
            .pipe(catchError(err => throwError(err)));
    }

    getEntregaTipos(): Observable<EnumType[]> {
        return this.http.get<EnumType[]>(`${this.API}/Transportadora/TipoEntregaPadrao`)
            .pipe(map((res : any) => this.extractData(res)), 
                  catchError(err => throwError(err)));
    }

    add(transportadora: Transportadora) {
        return this.http.post(`${this.API}/Transportadora`, transportadora);
    }

    update(transportadora: Transportadora) {
        return this.http.put(`${this.API}/Transportadora`, transportadora);
    }

    delete(id : string) {
        return this.http.delete(`${this.API}/Transportadora/${id}`);
    }

    //Endereços
    getEndereco(enderecoId: string): Observable<EnderecoTransportadora> {
        return this.http.get<EnderecoTransportadora>(`${this.API}/Transportadora/Endereco/${enderecoId}`)
            .pipe(map((res : any) => this.extractData(res)), 
                catchError(err => throwError(err)));
    }

    getEnderecos(transportadoraId: string): Observable<EnderecoTransportadora[]> {
        return this.http.get<EnderecoTransportadora[]>(`${this.API}/Transportadora/Endereco/Lista/${transportadoraId}`)
            .pipe(map((res : any) => this.extractData(res)), 
                catchError(err => throwError(err)));
    }

    addEndereco(endereco: EnderecoTransportadora) {
        return this.http.post(`${this.API}/Transportadora/Endereco`, endereco);
    }

    updateEndereco(endereco: EnderecoTransportadora) {
        return this.http.put(`${this.API}/Transportadora/Endereco`, endereco);
    }

    deleteEndereco(id : string) {
        return this.http.delete(`${this.API}/Transportadora/Endereco/${id}`);
    }

    //Telefones
    getTelefone(enderecoId: string): Observable<TelefoneTransportadora> {
        return this.http.get<TelefoneTransportadora>(`${this.API}/Transportadora/Telefone/${enderecoId}`)
            .pipe(map((res : any) => this.extractData(res)), 
                catchError(err => throwError(err)));
    }

    getTelefones(transportadoraId: string): Observable<TelefoneTransportadora[]> {
        return this.http.get<TelefoneTransportadora[]>(`${this.API}/Transportadora/Telefone/Lista/${transportadoraId}`)
            .pipe(map((res : any) => this.extractData(res)), 
                catchError(err => throwError(err)));
    }

    addTelefone(endereco: TelefoneTransportadora) {
        return this.http.post(`${this.API}/Transportadora/Telefone`, endereco);
    }

    updateTelefone(endereco: TelefoneTransportadora) {
        return this.http.put(`${this.API}/Transportadora/Telefone`, endereco);
    }

    deleteTelefone(id : string) {
        return this.http.delete(`${this.API}/Transportadora/Telefone/${id}`);
    }
}