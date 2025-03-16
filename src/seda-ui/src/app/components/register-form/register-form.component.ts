import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-form',
  imports: [ReactiveFormsModule],
  templateUrl: './register-form.component.html',
  styleUrl: './register-form.component.scss',
  standalone: true,
})
export class RegisterFormComponent {
  public registerForm = new FormGroup({
    email: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
    repeatPassword: new FormControl('', Validators.required),
    firstName: new FormControl('', Validators.required),
    lastName: new FormControl('', Validators.required),
  });

  constructor(private authService: AuthService, private router: Router) {}

  public register() {
    const request = {
      email: this.registerForm.value.email || '',
      password: this.registerForm.value.password || '',
      repeatedPassword: this.registerForm.value.repeatPassword || '',
      firstName: this.registerForm.value.firstName || '',
      lastName: this.registerForm.value.lastName || '',
    };

    this.authService.callRegister(request).subscribe((res) => {
      if (res) {
        alert('Successful registration, proceed to sign in');
        this.registerForm.reset();
      }
    });
  }
}
