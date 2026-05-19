/*
 Level-2 Problem 4: Asynchronous Order Processing System
Scenario:
 An e-commerce system processes customer orders. Each order requires multiple steps such as payment verification, inventory check, and order confirmation. These steps involve delays and should be handled asynchronously.
 
Requirements:
 - Create asynchronous methods:
   - VerifyPaymentAsync()
   - CheckInventoryAsync()
   - ConfirmOrderAsync()
 - Simulate processing delays using Task.Delay().
 - Execute steps asynchronously while maintaining the logical order of operations.
Technical Constraints:
 - Use async and await.
 - Use Task-based asynchronous methods.
 - Implement using a console application.
 
Expectations:
 - Each processing step should run asynchronously.
 - The program should display messages indicating the progress of order processing.
 
Learning Outcome:
 Students will understand how to design real-world workflows using asynchronous methods with async/await.

*/

using System;
using System.IO;
using System.Threading.Tasks;
namespace RetailDebugger
{
    class Program
    {
        static async Task VerifyPaymentAsync()
        {
            Console.WriteLine("[LOG]: Verifying Payment... (Please wait)");
            await Task.Delay(2000);
            Console.WriteLine("[LOG]: Payment Verified Successfully.");
        }

        static async Task CheckInventoryAsync()
        {
            Console.WriteLine("[LOG]: Checking Warehouse Inventory...");
            await Task.Delay(1500);
            Console.WriteLine("[LOG]: Items are available in stock.");
        }

        static async Task ConfirmOrderAsync()
        {
            Console.WriteLine("[LOG]: Finalizing Order and Sending Email...");
            await Task.Delay(1000);
            Console.WriteLine("[LOG]: Order Confirmation Sent to Customer.");
        }

        static async Task ProcessOrderAsync(string orderNumber)
        {
            Console.WriteLine($"=== PROCESSING ORDER: {orderNumber} ===");


            await VerifyPaymentAsync();
            await CheckInventoryAsync();
            await ConfirmOrderAsync();

            Console.WriteLine($"=== ORDER {orderNumber} COMPLETED ===\n");
        }




        static async Task Main(string[] args)
        {

            await ProcessOrderAsync("ORD-12345");

            Console.WriteLine(" Ready for next order....");
            Console.ReadLine();
        }
    }

}

    