import { Injectable } from '@angular/core';
import { BehaviorSubject, Subscription } from 'rxjs';
import { User } from '../models/User';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class SharedDataService {

  //test
  currentUser:User;
  //
  userSubscription:Subscription;
  private currentUserSubject = new BehaviorSubject<User>(new User());
  currentUserObservable = this.currentUserSubject.asObservable();

  
  constructor(private auth:AuthService) { }

  initSubscriptionsCurrentUser(){
    const userId = this.auth.getUserId();
     this.auth.getCurrentUser(userId);
     this.userSubscription = this.auth.userSubject$.subscribe(
       (payload:User) => {
         this.currentUser = payload;
         console.log(this.currentUser);
         this.currentUserSubject.next(this.currentUser);
       }
    )
  }

}
