import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './app/guards/auth-guards';
import { HomePageComponent } from './app/home-page/home-page.component';
import { RegisterComponent } from './app/register/register.component';
import { SearchComponent } from './app/search/search.component';

const routes: Routes = [
    {path:'', redirectTo:'home-page' , pathMatch:'full'},
    {path:'home-page', component:HomePageComponent},
    {path:'home-page/register', component:RegisterComponent},
    {path:'search', component:SearchComponent, canActivate:[AuthGuard]},
    
];


@NgModule({
    imports:[RouterModule.forRoot(routes)],
    exports:[RouterModule]
})

export class AppRoutingModule {

}