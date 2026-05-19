
/*Level - 1 Problem 2: Simple Calculator Using Switch
Scenario
Create a simple calculator application that performs basic arithmetic operations.
Requirements
• Accept two numbers from user.
• Accept operator (+, -, *, /).
• Use switch statement to perform operation.
• Display result.
Technical Constraints
• Use int or double data types.
• Use switch-case statement.
• Handle division by zero.
Sample Input
Enter First Number: 10
Enter Second Number: 5
Enter Operator: *
Sample Output
Result: 50
Expectations
Correct operator selection and proper validation of inputs.
Learning Outcome
Understand switch statements, arithmetic operators and control flow in C#.
*/

using System;
namespace prob2
{
    internal class program
    {

        static void Main(string[] args)
        {
            int num1;
            int num2;
            char op;
            Console.Write("Enter First Number: ");
            num1 = int.Parse(Console.ReadLine());
            Console.Write("Enter First Number: ");
            num2 = int.Parse(Console.ReadLine());
            

            Console.WriteLine("Select Operator --> + - * / ");

            op = char.Parse(Console.ReadLine());

            int z = 0;

            switch (op)
            {
                case '+':
                    z = num1 + num2;
                    break;
                case '-':
                    z = num1 - num2;
                    break;
                case '*':
                    z = num1 * num2;
                    break;
                case '/':
                    z = num1 / num2;
                    break;
                default:
                    Console.WriteLine("Invalid Operation");
                    Console.ReadLine();
                    return;

            }
            Console.WriteLine("Final Result : " + z);

            Console.ReadLine();




        }
    }
}