import { TipoDestinatario } from 'app/shared/enums/tipoDestinatario';
import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { throwError, Observable } from 'rxjs';
import { BaseService } from './../../../shared/services/base.service';
import { AliquotaImpostos } from 'app/shared/models/aliquotaImpostos';
import { NotaSaida } from '../models/nota-saida';
import { ResponseContentType } from '@angular/http';
import { OrdemLote } from 'app/producao/ordem/models/ordem';
import { Lote } from 'app/estoque/lote/models/lote';
import { AnyFn } from '@ngrx/store/src/selector';

@Injectable()
export class NotaSaidaService extends BaseService {

    constructor(private http: HttpClient) {
        super();
    }

    getFullById(id: string): any {
        return this.http.get<NotaSaida>(`${this.API}/NotaSaida/GetFullById/${id}`)
            .pipe(map((res: any) => this.extractData(res)),
                catchError(err => throwError(err)));
    }

    getWithStatusSefaz(id: string): any {
        return this.http.get<NotaSaida>(`${this.API}/NotaSaida/GeWithStatusSefaz/${id}`)
            .pipe(map((res: any) => this.extractData(res)),
                catchError(err => throwError(err)));
    }

    getPaged(pageNumber: number, pageSize: number, tipoDestinatario: number, status: number,
        startDate: string, endDate: string, search: string): any {
        let url = search ?
            `${this.API}/NotaSaida/${pageNumber}/${pageSize}?tipoDestinatario=${tipoDestinatario}&status=${status}&startDate=${startDate}&endDate=${endDate}&search=${search}` :
            `${this.API}/NotaSaida/${pageNumber}/${pageSize}?tipoDestinatario=${tipoDestinatario}&status=${status}&startDate=${startDate}&endDate=${endDate}`;
        return this.http.get<any>(url)
            .pipe(catchError(err => throwError(err)));
    }

    add(nota: any) {
        return this.http.post(`${this.API}/NotaSaida`, nota);
    }

    delete(id: string) {
        return this.http.delete(`${this.API}/NotaSaida/${id}`);
    }

    CancelSefaz(id: string): any {
        return this.http.delete<NotaSaida>(`${this.API}/NotaSaida/CancelSefaz/${id}`)
            .pipe(map((res: any) => this.extractData(res)),
                catchError(err => throwError(err)));
    }

    reenviar(id: string): any {
        return this.http.get<NotaSaida>(`${this.API}/NotaSaida/Reenviar/${id}`)
            .pipe(map((res: any) => this.extractData(res)),
                catchError(err => throwError(err)));
    }

    getOutrasInformacoes(nota: any): any {
        return this.http.post(`${this.API}/NotaSaida/GerarOutrasInformacoes`, nota)
            .pipe(map((res: any) => this.extractData(res)),
            catchError(err => throwError(err)));
    }

    getAliquotaImpostos(uf: string): any {
        return this.http.get<AliquotaImpostos>(`${this.API}/NotaSaida/AliquotaImpostos?uf=${uf}`)
            .pipe(map((res: any) => this.extractData(res)),
                catchError(err => throwError(err)));
    }

    downloadPdf(numeroNota: string): Observable<Blob> {
        return this.http.get(`${this.API}/NotaSaida/DownloadPdf?numeroNota=${numeroNota}`, { responseType: "blob" })
            .pipe(
                catchError(err => throwError(err)));
    }

    downloadXml(numeroNota: string): Observable<Blob> {
        return this.http.get(`${this.API}/NotaSaida/DownloadXml?numeroNota=${numeroNota}`, { responseType: "blob" })
            .pipe(
                catchError(err => throwError(err)));
    }

    getOrdemLoteToNfeSaida(clienteId: string) : any {
        return this.http.get<OrdemLote[]>(`${this.API}/Ordem/OrdemLotes/${clienteId}`)
            .pipe(map((res : any) => this.extractData(res)), 
                    catchError(err => throwError(err)));
    }

    getLoteEmbalagemToNfeSaida(destinatarioId: string, tipoDestinatario: number) : any {
        return this.http.get<Lote[]>(`${this.API}/Lote/GetLotesEmbalagemNfeSaida/${destinatarioId}/${tipoDestinatario}`)
            .pipe(map((res : any) => this.extractData(res)), 
                    catchError(err => throwError(err)));
    }

    getLotePecaToNfeSaida(destinatarioId: string, tipoDestinatario: number) : any {
        return this.http.get<Lote[]>(`${this.API}/Lote/GetLotesPecaNfeSaida/${destinatarioId}/${tipoDestinatario}`)
            .pipe(map((res : any) => this.extractData(res)), 
                    catchError(err => throwError(err)));
    }
}