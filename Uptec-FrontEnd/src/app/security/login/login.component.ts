import { Component, ViewChild, OnInit, OnDestroy } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from './../auth/auth.service';
import { Subject } from 'rxjs';
import { debounceTime, take } from 'rxjs/operators';
import { Login } from '../models/login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
    
  @ViewChild('f', {static: false}) loginForm: NgForm;

  private $failLogin = new Subject<string>();
  failMessage: string;

  constructor(private router: Router,
    private authService: AuthService) { }

    ngOnInit(): void {
      this.$failLogin.subscribe((message) => this.failMessage = message);
      this.$failLogin.pipe(
        debounceTime(5000)
      ).subscribe(() => this.failMessage = null);
    }

  onSubmit() {
    let u = new Login;
        u.email = this.loginForm.controls['inputEmail'].value;
        u.password = this.loginForm.controls['inputPass'].value;
    this.authService.signinUser(u).pipe(take(1)).subscribe(
      result => this.onSubmitComplete(result),
      error => this.onSubmitError(error));
  }

  onSubmitComplete(data: any){
      this.authService.loginSuccess(data);
      this.router.navigateByUrl(this.authService.returnUrl || '/');
  }

  onSubmitError(data: any) {
    this.$failLogin.next(data.error.errors);
  }

  // On Forgot password link click
  onForgotPassword() {
    //this.router.navigate(['forgotpassword'], { relativeTo: this.route.parent });
  }
  // On registration link click
  onRegister() {
    //this.router.navigate(['register'], { relativeTo: this.route.parent });
  }

  ngOnDestroy(): void {
    this.$failLogin.unsubscribe();
  }

}
