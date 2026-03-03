import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { RegisterRequest, RegisterResponse } from '../../models/register.model';
@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }
private apiUrl = `${environment.apiUrl}`;


register(data: RegisterRequest) {
    return this.http.post<RegisterResponse>(`${this.apiUrl}/users/register`, data);
  }
}
