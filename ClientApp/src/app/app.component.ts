import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthService } from './auth.service';
import { TasksComponent } from './task-manager/task-manager.component'; // 👈 Add this
import { TaskStatusPipe } from './task-manager/task-status.pipe';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  imports: [
    RouterModule,
    TasksComponent,
    TaskStatusPipe 
  ],
  providers: [AuthService]
})
export class AppComponent {
  title = 'Angular App';
}
