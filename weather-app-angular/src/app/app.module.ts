import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from 'src/app-routing.module';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { HomePageComponent } from './home-page/home-page.component';
import { RegisterComponent } from './register/register.component';
import { SearchComponent } from './search/search.component';
import { JwtModule } from '@auth0/angular-jwt';
import { AuthGuard } from './guards/auth-guards';


export function tokenGetter(){
  return localStorage.getItem("jwt");
}


@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    RegisterComponent,
    SearchComponent,
  ],
  imports: [
    BrowserModule, 
    AppRoutingModule,
    FormsModule, 
    ReactiveFormsModule,
    HttpClientModule,

    JwtModule.forRoot({
      config:{
        tokenGetter:tokenGetter,
        allowedDomains:["localhost:18956"],
        disallowedRoutes: []
      }
    })
  ],
  providers: [AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
