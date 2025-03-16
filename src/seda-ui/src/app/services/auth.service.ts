import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';
import { LoginRequest } from '../models/login-request.model';
import { LoginResponse } from '../models/login-response.model';
import { RegsiterRequest } from '../models/register-request.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly urlPath = `${environment.apiUrl}/User`;
  public user = signal<User | undefined>(undefined);

  constructor(private http: HttpClient) {}

  public callLogin(credentials: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.urlPath}/login`, credentials);
  }

  public callRegister(request: RegsiterRequest) {
    return this.http.post(`${this.urlPath}/register`, request);
  }

  public storeAccessToken(token: string) {
    localStorage.setItem('jwt', token);
  }
}
