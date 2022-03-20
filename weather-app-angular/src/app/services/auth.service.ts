import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../models/User';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  userSubject$ = new Subject<User>();
  
  constructor(private jwtHelper:JwtHelperService,
     private http:HttpClient) { }


  readonly baseUrl = environment.apiBaseUrl;


  authenticateUser():boolean{
    const token = localStorage.getItem("jwt");
    if(token && !this.jwtHelper.isTokenExpired(token)){
      return true;
    }
    console.log("not authorized")
    return false;
  }

  getUserId():string|null{
    const token = localStorage.getItem("jwt");
    if(token){
      const decodeToken = this.jwtHelper.decodeToken(token);
      return decodeToken.nameid.toString();

    }
    return null;
    
  }

  getCurrentUser(id:string | null){
    this.http
    .get<User>(`${this.baseUrl}/user/${id}`)
    .subscribe((result) => {
      this.userSubject$.next(result);

    },
    (error:any) => {
      console.log(error);

    })}

}
