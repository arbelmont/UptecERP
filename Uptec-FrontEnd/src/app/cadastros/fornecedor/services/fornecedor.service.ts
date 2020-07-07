import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { throwError, Observable } from 'rxjs';
import { BaseService } from './../../../shared/services/base.service';
import { EnumType } from 'app/shared/models/enumType';
import { Fornecedor, EnderecoFornecedor, TelefoneFornecedor } from '../models/fornecedor';

@Injectable()
export class FornecedorService extends BaseService {
    
    constructor(private http: HttpClient) {
        super();
    }

    get(id: string) : any {
        return this.http.get<Fornecedor>(`${this.API}/Fornecedor/${id}`)
            .pipe(map((res : any) => this.extractData(res)), 
                    catchError(err => throwError(err)));
    }

    getPaged(pageNumber: number, pageSize: number, search: string): any {
        let url = search ? 
            `${this.API}/Fornecedor/${pageNumber}/${pageSize}?search=${search}` :
            `${this.API}/Fornecedor/${pageNumber}/${pageSize}`;
        return this.http.get<any>(url)
            .pipe(catchError(err => throwError(err)));
    }

    getToNfeSaida(search: string): Observable<Fornecedor[]> {
        return this.http.get<Fornecedor[]>(`${this.API}/Fornecedor?search=${search}`)
            .pipe(map((res : any) => this.extractData(res)), 
                catchError(err => throwError(err)));
    }

    getEntregaTipos(): Observable<EnumType[]> {
        return this.http.get<EnumType[]>(`${this.API}/Fornecedor/TipoEntregaPadrao`)
            .pipe(map((res : any) => this.extractData(res)), 
                  catchError(err => throwError(err)));
    }

    add(fornecedor: Fornecedor) {
        return this.http.post(`${this.API}/Fornecedor`, fornecedor);
    }

    update(fornecedor: Fornecedor) {
        return this.http.put(`${this.API}/Fornecedor`, fornecedor);
    }

    delete(id : string) {
        return this.http.delete(`${this.API}/Fornecedor/${id}`);
    }

    //Endereços
    getEndereco(enderecoId: string): Observable<EnderecoFornecedor> {
        return this.http.get<EnderecoFornecedor>(`${this.API}/Fornecedor/Endereco/${enderecoId}`)
            .pipe(map((res : any) => this.extractData(res)), 
                catchError(err => throwError(err)));
    }

    getEnderecos(fornecedorId: string): Observable<EnderecoFornecedor[]> {
        return this.http.get<EnderecoFornecedor[]>(`${this.API}/Fornecedor/Endereco/Lista/${fornecedorId}`)
            .pipe(map((res : any) => this.extractData(res)), 
                catchError(err => throwError(err)));
    }

    addEndereco(endereco: EnderecoFornecedor) {
        return this.http.post(`${this.API}/Fornecedor/Endereco`, endereco);
    }

    updateEndereco(endereco: EnderecoFornecedor) {
        return this.http.put(`${this.API}/Fornecedor/Endereco`, endereco);
    }

    deleteEndereco(id : string) {
        return this.http.delete(`${this.API}/Fornecedor/Endereco/${id}`);
    }

    //Telefones
    getTelefone(enderecoId: string): Observable<TelefoneFornecedor> {
        return this.http.get<TelefoneFornecedor>(`${this.API}/Fornecedor/Telefone/${enderecoId}`)
            .pipe(map((res : any) => this.extractData(res)), 
                catchError(err => throwError(err)));
    }

    getTelefones(fornecedorId: string): Observable<TelefoneFornecedor[]> {
        return this.http.get<TelefoneFornecedor[]>(`${this.API}/Fornecedor/Telefone/Lista/${fornecedorId}`)
            .pipe(map((res : any) => this.extractData(res)), 
                catchError(err => throwError(err)));
    }

    addTelefone(endereco: TelefoneFornecedor) {
        return this.http.post(`${this.API}/Fornecedor/Telefone`, endereco);
    }

    updateTelefone(endereco: TelefoneFornecedor) {
        return this.http.put(`${this.API}/Fornecedor/Telefone`, endereco);
    }

    deleteTelefone(id : string) {
        return this.http.delete(`${this.API}/Fornecedor/Telefone/${id}`);
    }
}