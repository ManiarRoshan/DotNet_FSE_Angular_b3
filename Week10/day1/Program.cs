using ContactMgmtConsoleApp.Models;
using ContactMgmtConsoleApp.Services;

namespace ContactMgmtConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // CHANGE: Using 'ContactService' directly instead of 'IContactService'
            // This allows the JIT compiler to inline methods and avoid virtual lookups.
            ContactService services = new ContactService();

            try
            {
                // 1. Add a new contact
                var newContact = new Contact
                {
                    Id = 1,
                    Name = "John Doe",
                    Email = "john@example.com",
                    Phone = "555-0199"
                };
                services.AddContact(newContact);
                Console.WriteLine("Contact added successfully.");

                // 2. Retrieve and display
                Console.WriteLine("\n--- Contact List ---");

                // Ensure the method name matches your service implementation (e.g., GetAll)
                foreach (var c in services.GetAll())
                {
                    Console.WriteLine($"ID: {c.Id} | Name: {c.Name} | Email: {c.Email}");
                }
            }
            catch (Exception ex)
            {
                // Meaningful error message for the user
                Console.WriteLine($"Quality Check Failed: {ex.Message}");
            }
        }
    }
}
