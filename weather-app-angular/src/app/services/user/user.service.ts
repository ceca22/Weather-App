import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http:HttpClient, 
    private router:Router) { }

  readonly baseUrl = environment.apiBaseUrl;


  registerUser(firstName:string, lastName:string, 
    email:string, password:string, confirmPassword:string){
    this.http
    .post(`${this.baseUrl}/register`, {firstName, lastName, 
      email, password, confirmPassword}, {responseType:'text'})
    .subscribe((response) => {
      console.log("register" + response);
      this.router.navigate(['']);
      // this.toastr.success(response);
    },
    (error) => {
      console.log(error);
      // this.toastr.error(error.error);
    })
  }


  loginUser(email:string,password:string){
    this.http
    .post(`${this.baseUrl}/login`, {email,password}, {responseType:'text'})
    .subscribe((response) => {
      console.log('from login: '+ response);
      localStorage.setItem("jwt", response);
      this.router.navigate(['search']);
    },
    (error) => {
      console.log(error);
      // this.toastr.error("Incorrect username or password");
    })
  }


  logoutUser(){
    localStorage.removeItem('jwt');
    this.router.navigate(['']);

  }
  
}
