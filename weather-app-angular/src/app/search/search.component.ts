import { Component, OnInit } from '@angular/core';
import {  Subscription } from 'rxjs';
import { User } from '../models/User';
import { SearchWeatherService } from '../services/search/search-weather.service';
import { SharedDataService } from '../services/shared-data.service';
import { UserService } from '../services/user/user.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  city:string;
  currentUser:User;
  weather:Object;
  //
  weatherSubscription:Subscription;

  constructor(private searchService:SearchWeatherService,
    private sharedData:SharedDataService,
    private userService:UserService) { }

  ngOnInit(): void {
    this.sharedData.currentUserObservable.subscribe(message => this.currentUser = message);
    this.sharedData.initSubscriptionsCurrentUser();
    
    
  }

  initSubscriptionsWeather(){
     this.weatherSubscription = this.searchService.weatherSubject$.subscribe(
       (payload:Object) => {
        console.log("weather: " + this.weather);
        this.weather = payload;
        
      }
    )
  }

  onSubmitSearch() {
    this.searchService.searchWeather(this.city);
    this.initSubscriptionsWeather();
  }

  signOutUser(){
    this.userService.logoutUser();

  }



}
