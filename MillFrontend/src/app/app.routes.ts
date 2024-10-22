import { RouterModule, Routes } from '@angular/router';import { LoginComponent } from '../../Components/login/login.component'; // Corrected path
import { DashboardComponent } from '../../Components/dashboard/dashboard.component';
import { AddItemComponent } from '../../Components/add-item/add-item.component';
import { NgModule } from '@angular/core';

export const routes: Routes = [
  { path: 'login', component: LoginComponent }, // Define a route for the login page
  { path: '', redirectTo: 'login', pathMatch: 'full' }, // Redirect to login as the default route
  { path: 'dashboard', component: DashboardComponent},
  { path: 'add-item', component: AddItemComponent}
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }