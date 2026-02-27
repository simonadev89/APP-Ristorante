import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Restaurant } from '../../models/Restaurant';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {
 private apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getRestaurants(): Observable<Restaurant[]> {
    return this.http.get<Restaurant[]>(`${this.apiUrl}/restaurants`);
  }

  getRestaurant(id: number)  {
    return this.http.get<Restaurant>(`${this.apiUrl}/restaurants/${id}`);
}
}