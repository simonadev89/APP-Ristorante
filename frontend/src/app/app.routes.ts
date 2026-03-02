import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/login/login.component';
import { RestaurantListComponent } from './components/restaurant-list/restaurant-list.component';
import { ReservationComponent } from './components/reservation/reservation.component';
import { RegisterComponent } from './components/register/register.component';
import { AuthGuard } from './core/guards/auth.guard';

export const routes: Routes = [
     {path: '', redirectTo: '/restaurants', pathMatch: 'full' },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    
    { 
        path: 'restaurants', 
        component:RestaurantListComponent, 
        canActivate: [AuthGuard],
        
    },

    // { 
    //     path: 'admin', 
    //     component: AdminDashboardComponent,
    //     canActivate: [AuthGuard],
    //     data: { roles: ['Manager'] } // solo manager
    // },

    { path: 'reservation/:id', component: ReservationComponent, canActivate: [AuthGuard] },
    
        ];
    