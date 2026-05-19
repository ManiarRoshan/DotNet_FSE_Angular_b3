namespace ConsoleApp5
{
    public class Employee
    {
        public string Name {get;set; }
        public double BaseSalary {get;set; }

        public Employee(string name, double baseSalary)
        {
            Name = name;
            BaseSalary = baseSalary;
        }

        public virtual double CalculateSalary()
        {
            return BaseSalary;
        }
    }
    public class Manager : Employee
    {
        public Manager(string name, double baseSalary) : base(name, baseSalary) { }


        public override double CalculateSalary()
        {
            return BaseSalary + (BaseSalary * 0.20);
        }
    }

    public class Developer : Employee
    {
        public Developer(string name, double baseSalary) : base(name, baseSalary) { }

        public override double CalculateSalary()
        {
            return BaseSalary + (BaseSalary * 0.10);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            double inputSalary = 50000;
            Employee mgr = new Manager("AR", inputSalary);
            Employee dev = new Developer("MJ", inputSalary);

            Console.WriteLine($"Manager Salary = {mgr.CalculateSalary()}");
            Console.WriteLine($"Developer Salary = {dev.CalculateSalary()}");
        }
    }
}
