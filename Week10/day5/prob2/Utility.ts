import {Student} from "./studentModel"

export function formatName(name: string): string {
  return name.charAt(0).toUpperCase() + name.slice(1).toLowerCase();
}

export function calculateAverage(students: Student[]): number 
{
     if (students.length === 0) return 0;

    const total = students.reduce((sum, s) => sum + s.marks, 0);
    return total / students.length;
}