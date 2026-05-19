/*
 Level-1 Problem 3:
Scenario
A financial application needs to process multiple reports simultaneously to reduce waiting time. Instead of executing tasks sequentially, the system should run them concurrently using C# Tasks so that reports are generated faster.
Requirements
1.	Create three methods:
a.	GenerateSalesReport()
b.	GenerateInventoryReport()
c.	GenerateCustomerReport()
2.	Each method should simulate processing time using Thread.Sleep() or Task.Delay().
3.	Execute all three operations concurrently using Task.
4.	Display a message when each report starts and when it finishes.
5.	Display a final message once all reports are completed.
Technical Constraints
•	Use Task class from System.Threading.Tasks.
•	Use Task.Run() to execute methods.
•	Use Task.WaitAll() or await Task.WhenAll() to wait for completion.
•	The program must run in a Console Application.
Expectations
The program should start multiple report-generation tasks simultaneously and display completion messages for each report along with a final message once all tasks are completed.
Learning Outcome
Students will learn:
•	How to create and run Tasks in C#
•	How to execute multiple operations concurrently
•	How to wait for multiple tasks to complete

*/

using System;
using System.IO;
using System.Threading.Tasks;
namespace RetailDebugger
{
    class Program
    {
       
        static async Task GenerateSalesReport()
        {
            Console.WriteLine("[START] Generating Sales Report...");
            await Task.Delay(5000);
            Console.WriteLine("[FINISH] Sales Report Completed.");
        }

        
        static async Task GenerateInventoryReport()
        {
            Console.WriteLine("[START] Generating Inventory Report...");
            await Task.Delay(3000);
            Console.WriteLine("[FINISH] Inventory Report Completed.");
        }

        
        static async Task GenerateCustomerReport()
        {
            Console.WriteLine("[START] Generating Customer Report...");
            await Task.Delay(1000);
            Console.WriteLine("[FINISH] Customer Report Completed.");
        }
        static async Task Main(string[] args)
        {
            
            var salesTask = GenerateSalesReport();
            var inventoryTask = GenerateInventoryReport();
            var customerTask = GenerateCustomerReport();


            await Task.WhenAll(salesTask, inventoryTask, customerTask);

            Console.WriteLine("All reports are completed successfully....");
            Console.ReadLine(); 
        }
    }
}

    