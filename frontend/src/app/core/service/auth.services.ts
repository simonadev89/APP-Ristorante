import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { LoginRequest, LoginResponse } from '../../models/login.model';
import { RegisterRequest, RegisterResponse } from '../../models/register.model';

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
          localStorage.setItem('token', response.data.token); // in questo caso salva token JWT e dati utente nel localStorage senza modificarli
          localStorage.setItem('user', JSON.stringify(response.data.user));
        })
      );
  }

  

  getToken(): string | null { // Recupera il token JWT dal localStorage
    return localStorage.getItem('token');
  }

  isLoggedIn(): boolean { // Verifica se l'utente è autenticato controllando la presenza del token
    const token = this.getToken();
    if (!token) return false;
    try {
    // Base64URL → Base64
    let payload = token.split('.')[1];
    if (!payload) return false;
    
    payload = payload.replace(/-/g, '+').replace(/_/g, '/');
    payload = payload.padEnd(payload.length + (4 - payload.length % 4) % 4, '=');

    const decoded = JSON.parse(atob(payload));
    return (decoded.exp * 1000) > Date.now();
  } catch (err) {
    console.error('Token non valido', err);
    this.logout();
    return false;
  }
  }

   getRole(): string | null {
    const user = this.getUser();
    return user ? user.role : null;
  }

  getUser() {
      const userJson = localStorage.getItem('user');
  if (!userJson || userJson === 'null') {  // ✅ Controllo esplicito
    return null;
  }
  try {
    return JSON.parse(userJson);
  } catch (error) {
    console.error('Errore parsing user:', error);
    this.logout();
    return null;
  }}
  
  logout() {
    localStorage.removeItem('token'); // Rimuove il token e i dati utente dal localStorage
    localStorage.removeItem('user');
  }



  
}