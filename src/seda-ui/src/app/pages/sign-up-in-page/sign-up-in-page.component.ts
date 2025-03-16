import { Component, signal } from '@angular/core';
import { LoginFormComponent } from '../../components/login-form/login-form.component';
import { RegisterFormComponent } from '../../components/register-form/register-form.component';

type SignupinSwitchValues = 'signup' | 'signin';

@Component({
  selector: 'app-sign-up-in-page',
  imports: [LoginFormComponent, RegisterFormComponent],
  templateUrl: './sign-up-in-page.component.html',
  styleUrl: './sign-up-in-page.component.scss',
})
export class SignUpInPageComponent {
  public switch = signal<SignupinSwitchValues>('signup');

  constructor() {}

  public changeFormSwitch(value: SignupinSwitchValues) {
    this.switch.set(value);
  }
}
