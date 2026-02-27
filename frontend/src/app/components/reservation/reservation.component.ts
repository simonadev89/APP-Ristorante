import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Restaurant } from '../../models/Restaurant';
import { CommonModule } from '@angular/common';
import { RestaurantService } from '../../core/services/restaurant-service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ReservationService } from '../../core/services/reservation.service';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.services';
@Component({
  selector: 'app-reservation',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './reservation.component.html',
  styleUrl: './reservation.component.scss'
})
export class ReservationComponent implements OnInit {

restaurant!: Restaurant;
reservationForm!: FormGroup;

constructor(
  private route: ActivatedRoute,
  private restaurantService: RestaurantService,
  private fb: FormBuilder,
  private reservationService: ReservationService,
  private router: Router,
  private authService : AuthService,
) { }

ngOnInit(): void {
  const id = Number(this.route.snapshot.paramMap.get('id')); // prendo id dalla rotta
  this.loadRestaurant(id);



  this.reservationForm = this.fb.group({
  numSeats: [1, [Validators.required, Validators.min(1)]],
  date: ['', Validators.required]
});
}

loadRestaurant(id: number) {
  this.restaurantService.getRestaurant(id).subscribe({
    next: (r: Restaurant) => {
      this.restaurant = r; // salviamo il ristorante per mostrarlo in HTML
    },
    error: err => console.error('Errore caricamento ristorante', err)
  });
}

submitReservation() {
  if (this.reservationForm.invalid) return;

    const currentUser =  this.authService.getUser();
    if (!currentUser) { 
      alert('Devi essere loggato per prenotare');
      return;
    }
    const reservationData = {
    userId: currentUser.id,
    restaurantId: this.restaurant.id,
    numberOfPeople: this.reservationForm.value.numSeats,
    dateTime: this.reservationForm.value.date,
    status: 'PENDING'
  };
  this.reservationService.createReservation(reservationData).subscribe({
    next: () => {
      alert('Prenotazione effettuata con successo!');
      this.router.navigate(['/']);
    },
    error: err => {
      console.error('Errore durante la prenotazione', err);
      alert('Errore durante la prenotazione');
    }
  });
}
}