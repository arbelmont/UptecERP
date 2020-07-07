import { AuthService } from './../../security/auth/auth.service';
import { OnInit, OnDestroy } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Subscription } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { ListService } from './../services/list.service';

export abstract class BaseListComponet implements OnInit, OnDestroy {

    public showPagination: boolean;
    protected componentName: string;
    protected subscriptions: Subscription[] = [];
    protected entityId: string;
    protected errors: any[] = [];

    constructor(private lstService: ListService,
        private toastr: ToastrService,
        private auth: AuthService) {

    }

    abstract pageChanged(event: any);

    protected restauraList(componentName: string) {
        if (componentName !== this.lstService.lastComponent) {
            this.lstService.currentPage = 1;
            this.lstService.searchText = "";
        }
    }

    ngOnInit(): void {}

    ngOnDestroy(): void {

        this.lstService.lastComponent = this.componentName;

        this.subscriptions.forEach(s => s.unsubscribe());
        this.subscriptions = [];
    }


}