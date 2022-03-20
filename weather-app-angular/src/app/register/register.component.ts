import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../services/user/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private userService:UserService) { }

  registerForm:FormGroup;

  ngOnInit(): void {
    this.initForm();
  }

  initForm():void {
    this.registerForm = new FormGroup({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      email: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
      confirmPassword: new FormControl('', Validators.required)

    });
  }

  onSubmitRegister() {
    const {firstName, lastName, email, password, confirmPassword} = this.registerForm.value;
    this.userService.registerUser(firstName, lastName, email, password, confirmPassword);
  }

}
