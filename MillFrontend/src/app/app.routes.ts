import { Routes } from '@angular/router';
import { LoginComponent } from '../../Components/login/login.component'; // Corrected path
import { DashboardComponent } from '../../Components/dashboard/dashboard.component';


export const routes: Routes = [
  { path: 'login', component: LoginComponent }, // Define a route for the login page
  { path: '', redirectTo: 'login', pathMatch: 'full' }, // Redirect to login as the default route
  { path: 'dashboard', component: DashboardComponent}
];
