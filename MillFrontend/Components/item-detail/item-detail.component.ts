import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-item-detail',
  templateUrl: './item-detail.component.html',
  styleUrls: ['./item-detail.component.css'],
  standalone: true,
  imports: [CommonModule, HttpClientModule, FormsModule]
})
export class ItemDetailComponent implements OnInit {

  itemId: number = 0;  // Store the item ID
  inventoryItem: any;  // Store the item details

  constructor(private route: ActivatedRoute, private http: HttpClient, private router: Router) { }

  ngOnInit(): void {
    // Retrieve the passed item from the router state
    this.inventoryItem = history.state.item;

    console.log('Item details:', this.inventoryItem);
    if (!this.inventoryItem) {
      // If no item was passed, you can navigate back or handle it
      console.error('No item found in state!');
    }
  }

  

  // Navigate back to dashboard after deleting
  deleteItem(): void {
    console.log('Deleting item:', this.inventoryItem.productId);
    if (confirm('Are you sure you want to delete this item?')) {  // Optional confirmation dialog
      this.http.delete(`http://localhost:5079/api/inventory/delete/${this.inventoryItem.productId}`)
        .subscribe(
          () => {
            console.log('Item deleted successfully');
            this.router.navigate(['/dashboard']);  // Navigate back to dashboard after deleting
          },
          (error) => {
            console.error('Error deleting item:', error);
          }
        );
    }
  }

  // Navigate to the update page (you can create a separate component for updating)
  updateItem(): void {
    // Assume `this.itemId` is available and `this.inventoryItem` contains the updated item data
    this.http.put(`http://localhost:5079/api/inventory/update/${this.inventoryItem.productId}`, this.inventoryItem)
      .subscribe(
        response => {
          console.log('Item updated successfully:', response);
          // Optionally navigate back to dashboard after updating
          this.router.navigate(['/dashboard']);
        },
        error => {
          console.error('Error updating item:', error);
        }
      );
  }
  
}
