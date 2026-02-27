import { Component, OnInit } from '@angular/core';
import { RestaurantService } from '../../core/services/restaurant-service';
import { Restaurant } from '../../models/Restaurant';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-restaurant-list',
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './restaurant-list.component.html',
  styleUrl: './restaurant-list.component.scss'
})
export class RestaurantListComponentComponent implements OnInit {

  restaurants: Restaurant[] = []; // Lista completa dei ristoranti dal backend
  filteredRestaurants: Restaurant[] = []; // lista filtrata da mostrare in UI
  filterForm!: FormGroup; // FormGroup che contiene i campi city e minSeats
  



  constructor(
    private restaurantService: RestaurantService, // Iniezione del servizio per ottenere i ristoranti
    private fb: FormBuilder, // Iniezione del FormBuilder per creare il form di filtro
    private router: Router  // Iniezione del Router per navigare alla pagina di prenotazione
  ) { }

  ngOnInit(): void {
    this.loadRestaurants(); // Carica i ristoranti al caricamento del componente
    this.initFilterForm(); // Inizializza il form di filtro e imposta la logica di filtraggio
  }

  loadRestaurants(): void {
    this.restaurantService.getRestaurants().subscribe({ 
      next: (data) => {this.restaurants = data, 
      this.filteredRestaurants = [...this.restaurants]; 
      }, 
      error: (err) => console.error('Errore caricamento ristoranti', err) 
    });
  }

initFilterForm(): void {
    this.filterForm = this.fb.group({
      city: [''],
      minSeats: [null]
    });

this.filterForm.valueChanges.subscribe(val => {
      this.filteredRestaurants = this.restaurants.filter(r => {
        const cityMatch = val.city ? r.city.toLowerCase().includes(val.city.toLowerCase()) : true;
        const seatsMatch = val.minSeats ? r.totalSeats >= val.minSeats : true;
        return cityMatch && seatsMatch;
      });
    });
  }


  goToReservation(id: number) {
  // esempio controllo semplice login
  const token = localStorage.getItem('token');
  if (!token) {
    alert('Devi fare login per prenotare!');
    return;
  }

  this.router.navigate(['/reservation', id]);
}
}
