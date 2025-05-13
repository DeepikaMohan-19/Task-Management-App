export interface Task {
  id: string | null;              // The "id" can now be a string or null
  name: string | null;            // "name" can now be a string or null
  createdOn: Date | null;       // "createdOn" can now be a string or null
  createdByEmail: string | null;  // "createdByEmail" can now be a string or null
  isActive: boolean | null;       // "isActive" can now be a boolean or null
  isDeleted: boolean | null;      // "isDeleted" can now be a boolean or null
  description: string | null;     // "description" can now be a string or null
  deadline: Date | null;        // "deadline" can now be a string or null
  status: TaskStatusEnum | null;          // "status" can now be a number or null
}

// Enum definition in TypeScript
export enum TaskStatusEnum {
  Created = 1,
  Cancelled = 2,
  Deleted = 3,
  InProgress = 4,
  Completed = 5,
}
