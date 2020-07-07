import { Directive, Input, ElementRef, HostListener } from "@angular/core";
import { NgForm } from "@angular/forms";

@Directive({ selector: '[scrollFirstInvalid]' })
export class ScrollFirstInvalid {
    @Input() formGroup: NgForm;

    constructor(private el: ElementRef) { }

    @HostListener('submit', ['$event'])
    onSubmit(event) {
        event.preventDefault();

        if (!this.formGroup.valid) {
            ScrollFirstInvalid.markFormGroupTouched(this.formGroup);
            const formControl = ScrollFirstInvalid.findElement(this.el);
            ScrollFirstInvalid.scrollToElement(formControl);
        }
    }

    static findElement(element: ElementRef) : any {
        const controlInvalid = element.nativeElement.querySelector('form .ng-invalid');
        const formGroupInvalid = controlInvalid.querySelector('form .ng-invalid');
        //const subFormGroupInvalid = formGroupInvalid.querySelectorAll('form .ng-invalid');

        if(formGroupInvalid)
            return formGroupInvalid;
        
        return controlInvalid;
    }

    
    static scrollToElement(element) {
        if (element) {
            const distance = window.pageYOffset - Math.abs(element.getBoundingClientRect().y);

            window.scroll({
                behavior: 'smooth',
                left: 0,
                top: element.getBoundingClientRect().top + window.scrollY - 150
            });

            setTimeout(() => {
                element.focus();
                //element.blur(); // Trigger error messages
                //element.focus();
            }, distance);
        }
    }

    static markFormGroupTouched(formGroup) {
        (<any>Object).values(formGroup.controls).forEach(control => {
            control.markAsTouched();

            if (control.controls) {
                ScrollFirstInvalid.markFormGroupTouched(control);
            }
        });
    }
}