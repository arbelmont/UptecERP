import { OnInit, OnDestroy, ElementRef, ViewChildren } from '@angular/core';
import { FormGroup, FormControlName } from '@angular/forms';
import { Subscription, Observable, fromEvent, merge } from 'rxjs';
import { FormValidatorHelper } from '../helpers/form-validator-helper';

export abstract class BaseFormComponent implements OnInit, OnDestroy {
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  public frm: FormGroup;
  public errors: any[] = [];
  public displayMessage: any = {};
  public formValidatorHelper: FormValidatorHelper;
  public subscriptions: Subscription[] = [];

  public entityId: string = ""

  constructor() { }

  ngOnInit() {
  }

  abstract submit() : void;
  abstract onSubmitComplete(data: any) : void;

  onSubmit() {
    if (!this.frm.valid) {
      this.displayMessage = this.formValidatorHelper.processMessagesOnSubmit(this.frm);
      return;
    }
    this.submit();
  }

  ngAfterViewInit(): void {
    let controlBlurs: Observable<any>[] = this.formInputElements
      .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));

    merge(...controlBlurs).subscribe(value => {
      this.displayMessage = this.formValidatorHelper.processMessages(this.frm);
    });
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((subscription: Subscription) => {
      subscription.unsubscribe();
    });
    this.subscriptions = [];
  }
}
