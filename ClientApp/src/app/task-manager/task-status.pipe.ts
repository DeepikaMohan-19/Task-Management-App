import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'taskStatus', standalone: true })
export class TaskStatusPipe implements PipeTransform {
  transform(value: number): string {
    switch (value) {
      case 1: return 'Created';
      case 2: return 'Cancelled';
      case 3: return 'Deleted';
      case 4: return 'In Progress';
      case 5: return 'Completed';
      default: return 'Unknown';
    }
  }
}
