import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SearchWeatherService {

  weatherSubject$ = new Subject<Object>();

  constructor(private http:HttpClient) { }

  readonly baseUrl = environment.apiBaseUrl;

  

  searchWeather(city:string){
    this.http
    .get(`${this.baseUrl}/search/${city}`)
    .subscribe((result) => {
      console.log("weather result:" + result)
      this.weatherSubject$.next(JSON.stringify(result));

    },
    (error:any) => {
      console.log(error);

    })
  }


}

