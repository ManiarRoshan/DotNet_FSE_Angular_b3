/*
Level - 1 Problem 1: Student Grade Evaluator
Scenario
You are developing a console-based application in .NET 8 for a school.
The application should evaluate a student’s marks and assign a grade based on predefined rules.
Requirements
• Accept student name and marks (0-100).
• Use if-else statements to determine grade.
• Display grade as A, B, C, D or Fail.
• Handle invalid input using conditional checks.
Technical Constraints
• Use C# (.NET 8 Console Application).
• Use appropriate data types (string, int).
• Use if-else control flow.
• Do not use advanced concepts like classes or LINQ.
Sample Input
Enter Name: Rahul
Enter Marks: 78
Sample Output
Student: Rahul
Grade: B
Expectations
Program should correctly evaluate grade and handle edge cases like marks below 0 or above 100.
Learning Outcome
Understand variables, data types, input/output handling and if-else control statements in C#.
*/

using System;
namespace prob1
{
    internal class StudentEval
       
    {
        
        static void Main(string[] args)
        {
            string Name;
            int Marks;

            Console.WriteLine("Enter student Name:");
            Name = Console.ReadLine();
            Console.WriteLine("Enter stdent marks");
            Marks = int.Parse(Console.ReadLine());

            if (Marks < 0 || Marks > 100)
            {
                Console.WriteLine("Invalid marks, Enter correct marks ");
                Console.ReadLine();
                return;
                // 4. Shared output (prevents repeating this line 5 times!)
                //Console.WriteLine($"\nStudent: {name}");
            } else if (Marks >= 90) {
                Console.WriteLine($"Student is {Name} and His marks are {Marks}");
                Console.WriteLine("Grade A");
                Console.ReadLine();
            }
            else if (Marks >= 80)
            {
                Console.WriteLine($"Student is {Name} and His marks are {Marks}");
                Console.WriteLine("Grade B");
                Console.ReadLine();
            }
            else if (Marks >= 70)
            {
                Console.WriteLine($"Student is {Name} and His marks are {Marks}");
                Console.WriteLine("Grade C");
                Console.ReadLine();
            }
            else if (Marks >= 60)
            {
                Console.WriteLine($"Student is {Name} and His marks are {Marks}");
                Console.WriteLine("Grade D");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine($"Student is {Name} and His marks are {Marks}");
                Console.WriteLine("Student is FAIL !!!");
                Console.ReadLine();
            }






        }
    }
}
