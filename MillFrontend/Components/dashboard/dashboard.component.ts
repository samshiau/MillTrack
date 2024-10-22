import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpClientModule  } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';  // Import Router for navigation


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  standalone: true,
  imports: [CommonModule, HttpClientModule],
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  inventoryItems: any[] = [];  // Array to hold the items fetched from API

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit(): void {
    // Call the API to retrieve all items when the component is initialized
    this.getInventoryItems();
  }

  navigateToAddItem(): void {
    this.router.navigate(['/add-item']);  // Navigate to add item page
  }

  getInventoryItems(): void {
    // API call to retrieve all inventory items
    this.http.get<any[]>('http://localhost:5079/api/inventory/all')  // Update URL to your API endpoint
      .subscribe(
        (response) => {
          this.inventoryItems = response;  // Store the items in the array
          console.log('Inventory items retrieved successfully:', response);
        },
        (error) => {
          console.error('Error retrieving inventory items:', error);
        }
      );
  }
}
