/*
 Level-1 Problem 1: Simple Calculator Using Methods
Scenario:
A small retail shop wants a simple calculator application to perform addition and subtraction operations using reusable methods.
Requirements:
1. Create a class named Calculator.
2. Create methods Add(int a, int b) and Subtract(int a, int b).
3. Each method should return the result.
4. In Main(), create an object and call the methods.
5. Display the output.
Technical Constraints:
1. Use method parameters and return types properly.
2. Use appropriate access modifiers.
3. No global variables allowed.
Expectations:
Proper method definition, object creation, and method invocation.
Learning Outcome:
Understand functions, parameters, return types, classes, and objects.
Sample Input: 10 5
Sample Output: Addition = 15, Subtraction = 5
*/
using ConsoleApp39;

namespace ConsoleApp39
{
    class Calculator
    {
      

        public int Add(int a, int b) {  return a + b; }

        public int Subtract(int a, int b) { return a - b; }

    }
}


    internal class Program
    {
        static void Main(string[] args)
        {
            Calculator cal=new Calculator();
        int sum=cal.Add(10, 5);
        int diff=cal.Subtract(10, 5);

        Console.WriteLine($"The Add of two num ={sum}");
        Console.WriteLine($"The Sub of two num ={diff}");
        Console.ReadLine();
    }
    }
