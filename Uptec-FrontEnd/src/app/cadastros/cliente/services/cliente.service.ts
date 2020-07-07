import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { throwError, Observable } from 'rxjs';
import { BaseService } from './../../../shared/services/base.service';
import { EnumType } from 'app/shared/models/enumType';
import { Cliente, EnderecoCliente, TelefoneCliente } from '../models/cliente';

@Injectable()
export class ClienteService extends BaseService {
    
    constructor(private http: HttpClient) {
        super();
    }

    get(id: string) : any {
        return this.http.get<Cliente>(`${this.API}/Cliente/${id}`)
            .pipe(map((res : any) => this.extractData(res)), 
                    catchError(err => throwError(err)));
    }

    getToNfeSaida(search: string): Observable<Cliente[]> {
        return this.http.get<Cliente[]>(`${this.API}/Cliente?search=${search}`)
            .pipe(map((res : any) => this.extractData(res)), 
                catchError(err => throwError(err)));
    }

    getPaged(pageNumber: number, pageSize: number, search: string): any {
        let url = search ? 
            `${this.API}/Cliente/${pageNumber}/${pageSize}?search=${search}` :
            `${this.API}/Cliente/${pageNumber}/${pageSize}`;
        return this.http.get<any>(url)
            .pipe(catchError(err => throwError(err)));
    }

    getEntregaTipos(): Observable<EnumType[]> {
        return this.http.get<EnumType[]>(`${this.API}/Cliente/TipoEntregaPadrao`)
            .pipe(map((res : any) => this.extractData(res)), 
                  catchError(err => throwError(err)));
    }

    add(cliente: Cliente) {
        return this.http.post(`${this.API}/Cliente`, cliente);
    }

    update(cliente: Cliente) {
        return this.http.put(`${this.API}/Cliente`, cliente);
    }

    delete(id : string) {
        return this.http.delete(`${this.API}/Cliente/${id}`);
    }

    //Endereços
    getEndereco(enderecoId: string): Observable<EnderecoCliente> {
        return this.http.get<EnderecoCliente>(`${this.API}/Cliente/Endereco/${enderecoId}`)
            .pipe(map((res : any) => this.extractData(res)), 
                catchError(err => throwError(err)));
    }

    getEnderecos(clienteId: string): Observable<EnderecoCliente[]> {
        return this.http.get<EnderecoCliente[]>(`${this.API}/Cliente/Endereco/Lista/${clienteId}`)
            .pipe(map((res : any) => this.extractData(res)), 
                catchError(err => throwError(err)));
    }

    addEndereco(endereco: EnderecoCliente) {
        return this.http.post(`${this.API}/Cliente/Endereco`, endereco);
    }

    updateEndereco(endereco: EnderecoCliente) {
        return this.http.put(`${this.API}/Cliente/Endereco`, endereco);
    }

    deleteEndereco(id : string) {
        return this.http.delete(`${this.API}/Cliente/Endereco/${id}`);
    }

    //Telefones
    getTelefone(enderecoId: string): Observable<TelefoneCliente> {
        return this.http.get<TelefoneCliente>(`${this.API}/Cliente/Telefone/${enderecoId}`)
            .pipe(map((res : any) => this.extractData(res)), 
                catchError(err => throwError(err)));
    }

    getTelefones(clienteId: string): Observable<TelefoneCliente[]> {
        return this.http.get<TelefoneCliente[]>(`${this.API}/Cliente/Telefone/Lista/${clienteId}`)
            .pipe(map((res : any) => this.extractData(res)), 
                catchError(err => throwError(err)));
    }

    addTelefone(endereco: TelefoneCliente) {
        return this.http.post(`${this.API}/Cliente/Telefone`, endereco);
    }

    updateTelefone(endereco: TelefoneCliente) {
        return this.http.put(`${this.API}/Cliente/Telefone`, endereco);
    }

    deleteTelefone(id : string) {
        return this.http.delete(`${this.API}/Cliente/Telefone/${id}`);
    }
}