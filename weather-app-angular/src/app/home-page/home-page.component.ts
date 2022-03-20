import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../services/user/user.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {

  constructor(private userService:UserService) { 
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required]),
      password: new FormControl('', Validators.required),
    })
  }

  loginForm:FormGroup;

  ngOnInit(): void {
    // this.initForm();
  }

  // initForm():void {
  //   this.loginForm = new FormGroup({
  //     email: new FormControl('', [Validators.required]),
  //     password: new FormControl('', Validators.required),
  //   });
    
  // }
  
  onSubmitLogin(){
    const {email, password} = this.loginForm.value;
    this.userService.loginUser(email,password);
  }

}
