import { Observable } from 'rxjs';

export class BaseService {

    protected readonly API = "https://localhost:5001/api/v1";

    constructor() { }

    protected extractData(response: any) {
        return response.data || {};

    }

    protected handleError(err: any, caught: Observable<any>) {
        console.log(err);
    }
}