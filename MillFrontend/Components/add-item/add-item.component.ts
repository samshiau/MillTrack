import { Component } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-add-item',
  templateUrl: './add-item.component.html',
  styleUrls: ['./add-item.component.css'],
  standalone: true,
  imports: [FormsModule,HttpClientModule]
})
export class AddItemComponent {

  inventoryItem = {
    name: '',
    sku: '',
    description: '',
    category: '',
    location: '',
    unitPrice: 0,
    amountInStock: 0,
    isActive: true
  };

  constructor(private http: HttpClient, private router: Router) { }

  onSubmit(): void {
    this.http.post('http://localhost:5079/api/inventory/add', this.inventoryItem)
      .subscribe(
        response => {
          console.log('Inventory item added:', response);
          this.router.navigate(['/dashboard']);  // Navigate back to dashboard after successful add
        },
        error => {
          console.error('Error adding inventory item:', error);
        }
      );
  }
}
