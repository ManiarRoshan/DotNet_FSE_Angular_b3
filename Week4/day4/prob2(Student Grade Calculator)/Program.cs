/*
 Level-1 Problem 2: Student Grade Calculator
Scenario:
A school wants to calculate the average marks of a student using a class-based approach.
Requirements:
1. Create a class Student.
2. Create method CalculateAverage(int m1, int m2, int m3).
3. Return the average marks.
4. Display grade based on average.
Technical Constraints:
1. Use return type double for average.
2. Avoid hard-coded values.
Expectations:
Clear separation of logic inside methods.
Learning Outcome:
Learn method creation, return values, and basic OOP concepts.
Sample Input: 
80 70 90
Sample Output: 
Average = 80, Grade = A

*/
using ConsoleApp39;
using System.Runtime.Intrinsics.X86;

namespace ConsoleApp39
{
    class Student
    {
      

        public double CalculateAverage(int m1, int m2, int m3) 
        {
            return  (m1 + m2 + m3) / 3;
            
        }
        public string GetGrade(double avg)
        {
            if (avg >= 80)
            {
                return "Grade A";
            }
            else
            {
                return "Fail ";
            }
          
        }

    }
}


    internal class Program
    {
        static void Main(string[] args)
        {
        Console.WriteLine("Enter m1 value=");
        int m1=int.Parse(Console.ReadLine());

        Console.WriteLine("Enter m2 value=");
        int m2 = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter m3 value=");
        int m3 = int.Parse(Console.ReadLine());

        Student Stud = new Student();
        Stud.CalculateAverage(m1, m2, m3);

        double avg = Stud.CalculateAverage(m1, m2, m3);

        string grade = Stud.GetGrade(avg);
        Console.WriteLine($"Average = {avg}, Grade = {grade}");




    }
    }
