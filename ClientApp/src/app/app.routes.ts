import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';  // Correct import path
import { TasksComponent } from './task-manager/task-manager.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './auth.guard';  // Correct import path


export const routes: Routes = [
  { path: 'login', component: LoginComponent },  // Correct route for login page
  { path: '', redirectTo: '/login', pathMatch: 'full' },  // Default route
  { path: 'task-manager', component: TasksComponent, canActivate: [AuthGuard] },  // Wildcard route to handle unknown paths
  { path: 'register', component: RegisterComponent },  // Wildcard route to handle unknown paths
];
