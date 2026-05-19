/*
 Level-1 Problem 1: Asynchronous File Logger
Scenario:
 An application writes logs to a file whenever an event occurs. Writing logs synchronously can slow down the application. Asynchronous file writing improves performance.
 
Requirements:
 - Create an asynchronous method WriteLogAsync(string message).
 - The method should simulate file writing using Task.Delay().
 - Call this method multiple times to simulate logging different events.
 
Technical Constraints:
 - Use async and await keywords.
 - Use Task.Delay() to simulate file I/O.
 - Use a console application.
 
Expectations:
 - Logs should be written asynchronously.
 - The main thread should remain responsive while logging operations occur.
 
Learning Outcome:
 Students will learn how asynchronous operations improve performance when dealing with I/O operations.
*/

using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
namespace AsyncProg
{
    class Program
    {
        static async Task WriteLogAsync(string message)
        {
            Console.WriteLine($"Start Writing log:{message}...");


            await Task.Delay(2000);
            Console.WriteLine($"Complete logged:{message} at {DateTime.Now.ToString()}");
   
        }

        static async Task Main(string[] args)
        {

            Console.WriteLine("Application Started. Triggering logs...");
            List<Task> logTasks = new List<Task>();

            logTasks.Add(WriteLogAsync("User Logged In"));
            logTasks.Add(WriteLogAsync("Database Connection Opened"));
            logTasks.Add(WriteLogAsync("Payment Processed"));
            await Task.WhenAll(logTasks);

            Console.WriteLine("\nAll logs have been written. Exiting Application.");

            Console.ReadLine();
        }
    }
}