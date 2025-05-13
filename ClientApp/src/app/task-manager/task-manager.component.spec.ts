import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CommonModule } from '@angular/common';  // Import CommonModule here
import { FormsModule } from '@angular/forms';    // Import FormsModule here

import { TasksComponent } from './task-manager.component';

describe('TaskManagerComponent', () => {
  let component: TasksComponent;
  let fixture: ComponentFixture<TasksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TasksComponent, CommonModule, FormsModule]  // Add CommonModule and FormsModule here
    })
    .compileComponents();

    fixture = TestBed.createComponent(TasksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
