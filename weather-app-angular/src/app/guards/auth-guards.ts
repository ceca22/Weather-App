import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private router:Router, 
     private authService:AuthService) {

  }

  
  canActivate():boolean {
    if(this.authService.authenticateUser()){
      
     return true;
    }
    console.log("not");
    this.router.navigate([""]);
    return false;
    
  }
    
  



}