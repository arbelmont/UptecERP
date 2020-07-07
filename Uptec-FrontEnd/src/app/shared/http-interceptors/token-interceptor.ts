
import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from './../../security/auth/auth.service';
import { HandleHttpErrorService } from './../services/handleHttpError.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
    
    constructor(private auth: AuthService,
                private handle: HandleHttpErrorService){}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        let cloneReq: HttpRequest<any>;

        if(!req.headers.has('notIntercept')) {
            cloneReq = req.clone({
                headers: req.headers.set('Authorization', `Bearer ${this.auth.getToken()}`)
            })
        }
        else {
            cloneReq = req.clone({
                headers: new HttpHeaders()
            })
        }
        
        return next.handle(cloneReq).pipe(
            catchError((error: HttpErrorResponse) => {
                this.handle.handleHttpError(error);
                return throwError(error);
            })
        );
    }
}