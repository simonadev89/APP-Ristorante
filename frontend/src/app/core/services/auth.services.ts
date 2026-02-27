import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { LoginRequest, LoginResponse } from '../../features/auth/login.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = `${environment.apiUrl}/auth`;

  constructor(private http: HttpClient) {}

  login(data: LoginRequest) {
    return this.http.post<LoginResponse>(`${this.apiUrl}/login`, data)
      .pipe(
        tap(response => { //tap è un operatore RxJS che permette di eseguire un'azione secondaria senza modificare il flusso dei dati
          localStorage.setItem('token', response.token); // in questo caso salva token JWT e dati utente nel localStorage senza modificarli
          localStorage.setItem('user', JSON.stringify(response.user));
        })
      );
  }

  

  getToken(): string | null { // Recupera il token JWT dal localStorage
    return localStorage.getItem('token');
  }

  isLoggedIn(): boolean { // Verifica se l'utente è autenticato controllando la presenza del token
    return !!this.getToken();
  }
  
  getUser() {
    const userJson = localStorage.getItem('user');
    if (userJson) {
      return JSON.parse(userJson); // { id, email, role }
    }
    return null;
  }
  
  logout() {
    localStorage.removeItem('token'); // Rimuove il token e i dati utente dal localStorage
    localStorage.removeItem('user');
  }
}