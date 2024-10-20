import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule  } from '@angular/common/http';
import { Router } from '@angular/router';  // Import Router for navigation


@Component({
  selector: 'app-login',
  standalone: true, // Mark the component as standalone
  imports: [ReactiveFormsModule, CommonModule, HttpClientModule], // Import necessary modules directly
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const { username, password } = this.loginForm.value;
      console.log('sending request', username, password);

      // Send a request to the server
      this.http.post('http://localhost:5079/api/user/validate', { username, password })
      .subscribe({
        next: (response: any) => {
          console.log('Login successful', response.message); // Handle successful response
          this.router.navigate(['/dashboard']); // Redirect on success
        },
        error: (error) => {
          console.error('Login failed', error); // Handle error response
        },
        complete: () => {
          console.log('Request completed'); // Optional: Handle completion of the request
        }
      });


    } else {
      console.log('Invalid form');
    }
  }
}

