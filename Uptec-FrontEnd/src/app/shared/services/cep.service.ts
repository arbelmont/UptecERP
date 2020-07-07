import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { of, timer } from "rxjs";

@Injectable({
    providedIn: 'root'
  })
  export class CepService {
  
    httOptions = {
        headers : new HttpHeaders({'notIntercept': 'true'})
    };

    constructor(private http: HttpClient) { 
        
    }

    consultaCep(cep: string) {
        cep = cep.replace(/\D/g, '');
        const validacep = /^[0-9]{8}$/;
        if (validacep.test(cep)) {
            return this.http.get(`//viacep.com.br/ws/${cep}/json`, this.httOptions);
        }
        return of({});
    }

  }