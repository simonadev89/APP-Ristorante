import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/login/login.component';
import { RestaurantListComponentComponent } from './components/restaurant-list/restaurant-list.component';
import { ReservationComponent } from './components/reservation/reservation.component';
import { RegisterComponent } from './components/register/register.component';
import { AuthGuard } from './core/guards/auth.guar';

export const routes: Routes = [
     {path: '', redirectTo: '/restaurants', pathMatch: 'full' },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'restaurants', component: RestaurantListComponentComponent  , canActivate: [AuthGuard] },
    { path: 'reservation/:id', component: ReservationComponent },
    { path: '**', redirectTo: '/restaurants' },
    
        ];
