namespace Pattern_Matching
{
    class program
    {
        static void Main(String [] args)
        {
            Console.WriteLine("Enter the EmployeeName:");
            string EmployeeName=Console.ReadLine();
            Console.WriteLine("Enter the MonthlySalesAmount:");
            double MonthlySalesAmount = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter the CustomerFeedbackRating(1–5):");
            int CustomerFeedbackRating= int.Parse(Console.ReadLine());

            static (double MonthlySalesAmount, int CustomerFeedbackRating) GetEmployeeStats(double msa, int cfr)
            {
                return (msa, cfr);
            }

            var returndata = GetEmployeeStats(MonthlySalesAmount, CustomerFeedbackRating);

            string pattern = returndata switch
            {
                (>= 100000, >= 4) => "High Performer",
                (>= 50000, >= 3) => "Average  Performer",
                _ => "Needs Improvement "

            };
            Console.WriteLine(">--- Performance Report ---<");
            Console.WriteLine($"Employee Name: {EmployeeName}");
            Console.WriteLine($"Sales Amount: {returndata.MonthlySalesAmount}");
            Console.WriteLine($"Rating: {returndata.CustomerFeedbackRating}");
            Console.WriteLine($"Performance: {pattern}");

            Console.ReadLine();
        }
    }
}