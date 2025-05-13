import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common'; // Import CommonModule for *ngIf

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: true,
  imports: [FormsModule, CommonModule],
})
export class LoginComponent {
  email = '';
  password = '';
  errorMessage = '';
  isError = false; // Flag to show the error popup

  constructor(private authService: AuthService, private router: Router) {}

  login(): void {
  // Clear any previous error
  this.isError = false;
  this.errorMessage = '';
  
  this.authService.login(this.email, this.password).subscribe({
    next: (response) => {
      console.log('Login successful:', response);
      // Redirect to dashboard after successful login
      this.router.navigate(['/task-manager']);
    },
    error: (error) => {
      console.error('Login failed:', error);
      // Handle login error
      this.errorMessage = 'Invalid login credentials';
      this.isError = true; // Show the error popup
    },
    complete: () => {
      console.log('Login request completed');
    }
  });
}
   register(): void {
    
        // Redirect to dashboard after successful login
        this.router.navigate(['/register']);
      
  }

  closeErrorPopup() {
    this.isError = false; // Close the error popup
  }
}
