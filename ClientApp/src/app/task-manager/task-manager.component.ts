import { Component, OnInit } from '@angular/core';
import { Task, TaskStatusEnum } from '../task.model';
import { TaskService } from '../task.service';
import { FormsModule } from '@angular/forms';
import { TaskStatusPipe } from './task-status.pipe';
import { CommonModule } from '@angular/common';
import { CdkDragDrop, DragDropModule } from '@angular/cdk/drag-drop';


@Component({
  standalone: true,
  selector: 'app-tasks',
  templateUrl: './task-manager.component.html',
  styleUrls: ['./task-manager.component.scss'],
  imports: [FormsModule, TaskStatusPipe, CommonModule, DragDropModule] // 👈 Required here too
})
export class TasksComponent implements OnInit {
  tasks: Task[] = [];
  newTask: Task = {
    id: null,
    name: '',
    description: '',
    status: TaskStatusEnum.Created,
    deadline: null,
    createdOn: null,
    createdByEmail: '',
    isActive: true,
    isDeleted: false,
  };
  
  taskStatuses = Object.values(TaskStatusEnum).filter(
    (value) => typeof value === 'number'
  ) as TaskStatusEnum[];

  constructor(private taskService: TaskService) {}

  onDrop(event: CdkDragDrop<Task[]>, newStatus: number): void {
    const task = event.item.data as Task;
    if (task.status !== newStatus) {
      task.status = newStatus;
      this.updateTask(task);
    }
  }
  
  // Function to handle task status change
  changeStatus(task: Task, newStatus: number) {
    task.status = newStatus;  // Change the task status
    this.updateTask(task);  // Update the task in the backend or locally
  }
  
  getTasksByStatus(status: number) {
    return this.tasks.filter(t => t.status === status);
  }
  
  ngOnInit() {
    this.loadTasks();
  }

  loadTasks() {
    this.taskService.getTasks().subscribe((data) => {
      this.tasks = data;
    });
  }

  createTask() {
    if (!this.newTask.name?.trim()) return;
    this.taskService.addTask(this.newTask).subscribe(() => {
      this.newTask = {
        id: null,
        name: '',
        description: '',
        status: TaskStatusEnum.Created,
        deadline: null,
        createdOn: null,
        createdByEmail: '',
        isActive: true,
        isDeleted: false,
      };
      this.loadTasks();
    });
  }

  updateTask(task: Task) {
    this.taskService.updateTask(task).subscribe();
  }

  deleteTask(id: string) {
    this.taskService.deleteTask(id).subscribe(() => this.loadTasks());
  }

  onMouseEnter(status: TaskStatusEnum) {
    // Optional: Change visual state when mouse enters the column
  }

  onMouseLeave() {
    // Optional: Reset visual state when mouse leaves the column
  }

  onMouseEnterColumn(task: Task, status: TaskStatusEnum) {
    // Optional: Change task status when hovered over the column
    task.status = status;
    this.updateTask(task);
  }

  onMouseLeaveColumn() {
    // Optional: Reset task state if needed
  }
}
