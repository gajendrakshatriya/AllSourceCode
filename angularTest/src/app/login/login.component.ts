import { Component } from '@angular/core';
import {MatSelectModule} from '@angular/material/select';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatCardModule} from '@angular/material/card';
import { FormControl, Validators, FormsModule, ReactiveFormsModule,FormBuilder } from '@angular/forms';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [MatSelectModule,MatInputModule,MatFormFieldModule,MatCardModule
    , FormsModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  usernameFormControl = new FormControl('', [Validators.required]);
  passwordFormControl = new FormControl('', [Validators.required]);
  loginForm = this.formBuilder.group({
    username: '',
    password: ''
  });

  /**
   *
   */
  constructor(private formBuilder: FormBuilder) {}

  onSubmit(): void {
    // Process checkout data here
    if(this.usernameFormControl.value == 'uzer' && this.passwordFormControl.value=='pass')
      alert('You are logged in now...');
    else
      alert('Invalid Credentials');
    this.loginForm.reset();
  }
}
