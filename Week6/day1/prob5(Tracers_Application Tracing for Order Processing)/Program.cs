/*
 Prob-5-
Application Tracing for Order Processing
Scenario
An e-commerce application processes customer orders. Sometimes the order processing fails, but developers are unable to identify where the failure occurs. The team decides to implement Tracing to monitor the execution flow and diagnose the issue.
Requirements
•	Create a console application that simulates order processing.
•	The order processing should include the following steps:
o	Validate Order
o	Process Payment
o	Update Inventory
o	Generate Invoice
•	Use Trace class to log messages at each step of the process.
•	Display trace messages showing the execution flow.
Technical Constraints
•	Use System.Diagnostics.Trace.
•	Write trace messages using:
o	Trace.WriteLine()
o	Trace.TraceInformation()
•	Configure a TextWriterTraceListener to store trace logs in a file.
•	Implement the solution using .NET console application.
Expectations
•	The application should log messages for each stage of order processing.
•	Trace logs should help developers identify where failures occur.
•	The trace output should be stored in a log file.
Learning Outcome
Students will learn how to:
•	Implement application tracing using System.Diagnostics.
•	Monitor application flow using Trace listeners.
•	Use trace logs to diagnose runtime issues in real-world applications.

*/

using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
namespace OrderTracingSystem
{
    class Program
    {
        static void ProcessOrder(string id)
        {
            ValidateOrder(id);
            ProcessPayment(id);
            UpdateInventory(id);
            GenerateInvoice(id);
        }

        static void ValidateOrder(string id)
        {
            Trace.TraceInformation($"[{DateTime.Now}] Step 1: Validating Order {id}...");

        }
        static void ProcessPayment(string id)
        {
            Trace.TraceInformation($"[{DateTime.Now}] Step 2: Processing Payment for {id}...");

        }
        static void UpdateInventory(string id)
        {
            Trace.TraceInformation($"[{DateTime.Now}] Step 3: Updating Inventory for {id}...");
        }
        static void GenerateInvoice(string id)
        {
            Trace.TraceInformation($"[{DateTime.Now}] Step 4: Generating Invoice for {id}...");
        }

        static void Main(string[] args)
        {
            TextWriterTraceListener fileListener = new TextWriterTraceListener("OrderLog.txt");
            Trace.Listeners.Add(fileListener);

            Trace.Listeners.Add(new ConsoleTraceListener());

            Trace.AutoFlush = true;

            Trace.WriteLine("--- Starting New Order Session ---");

            try
            {
                ProcessOrder("ORD-999");
            }
            catch(Exception ex)
            {
                Trace.TraceError($"CRITICAL FAILURE: {ex.Message}");
            }

            Console.WriteLine("\nProcessing complete. Check 'OrderLog.txt' for the trace.");
            Trace.WriteLine("--- End of Session ---\n");

            Console.ReadLine();
        }

        

       
    }
}