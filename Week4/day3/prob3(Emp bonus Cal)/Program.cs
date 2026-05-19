/*
 * Level-2 Problem 1: Employee Bonus Calculator
Scenario
Develop a console application that calculates employee bonus based on salary and years of experience.
Requirements
• Accept employee name, salary and years of experience.
• Use if-else and conditional operator.
• Bonus rules:
   - Experience < 2 years: 5% bonus
   - 2-5 years: 10% bonus
   - >5 years: 15% bonus
• Display final salary after bonus.
Technical Constraints
• Use double for salary.
• Use if-else and ternary operator.
• Use proper formatting for currency output.
Sample Input
Enter Name: Aisha
Enter Salary: 50000
Enter Experience: 4
Sample Output
Employee: Aisha
Bonus: 5000
Final Salary: 55000
Expectations
Accurate bonus calculation and correct usage of control statements.
Learning Outcome
Apply conditional logic and arithmetic operations in real-world scenarios.
*/

namespace prob3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string name;
            double salary;
            int experience;
            double bonus;
            double finalSalary;


            Console.WriteLine("Enter name:");
            name=Console.ReadLine();

            Console.WriteLine("Enter salary:");
            salary = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter experience:");
            experience = int.Parse(Console.ReadLine());

            bonus = 0;

            if (experience < 2)
            {
                bonus = salary * 0.05;

            }
            else if (experience <= 5)
            {
                bonus = salary * 0.10;
            }
            else 
            {
                Console.WriteLine("Invalid experience");
            }

            finalSalary = salary + bonus;
            Console.WriteLine($"Employee {name},Total Salary ={finalSalary}");


        }
    }
}
