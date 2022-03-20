import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
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
    .post(`${this.baseUrl}/search`, {city}, {responseType: 'text' as 'json'})
    .subscribe((result) => {
      console.log("weather result:" + result)
      this.weatherSubject$.next(result)

    },
    (error:any) => {
      console.log(error);

    })
  }


}

