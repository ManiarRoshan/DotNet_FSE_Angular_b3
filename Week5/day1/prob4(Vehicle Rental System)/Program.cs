/*Level-2 Problem 2: Vehicle Rental System
Scenario:
A vehicle rental company wants a system where different vehicle types calculate rental charges differently.
Requirements:
1. Create a base class Vehicle with properties Brand and RentalRatePerDay.
2. Create derived classes Car and Bike.
3. Override CalculateRental(int days) method.
4. Car adds insurance charge of 500 per rental.
5. Bike offers 5% discount on total rental.
Technical Constraints:
• Use encapsulation with proper access modifiers.
• Apply runtime polymorphism.
• Validate number of rental days.
Expectations:
• Use base class reference to call overridden methods.
• Implement clean class hierarchy.
• Display final rental cost.
Learning Outcome:
• Master inheritance and polymorphism.
• Implement real-world OOP scenarios.
• Improve object-oriented design skills.
Sample Input: 
Car RentalRatePerDay = 2000, Days = 3
Sample Output: 
Total Rental = 6500
*/

using System;
using System.Collections.Generic;

namespace VehicleRentalSystem
{
    public class Vehicle
    {
        public string Brand { get; set; }
        private double _rentalRatePerDay;

        public double RentalRatePerDay
        {
            get => _rentalRatePerDay;
            set
            {
                if (value <= 0) 
                    throw new ArgumentException("Rate must be positive.");
                _rentalRatePerDay = value;
            }
        }

        public Vehicle(string brand, double rate)
        {
            Brand = brand;
            RentalRatePerDay = rate;
        }
        public virtual double CalculateRental(int days)
        {
            if (days <= 0) throw new ArgumentException("Days must be at least 1.");
            return days * RentalRatePerDay;
        }
    }
    public class Car : Vehicle
    {
        public Car(string brand, double rate) : base(brand, rate) { }

        public override double CalculateRental(int days)
        {
            return base.CalculateRental(days) + 500;
        }
    }
    public class Bike : Vehicle
    {
        public Bike(string brand, double rate) : base(brand, rate) { }

        public override double CalculateRental(int days)
        {
            double baseTotal = base.CalculateRental(days);
            return baseTotal * 0.95;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Vehicle> rentalFleet = new List<Vehicle>
            {
                new Car("Toyota", 2000),
                new Bike("Yamaha", 1000)
            };

            int rentalDays = 3;

            foreach (var v in rentalFleet)
            {
                Console.WriteLine($"Vehicle: {v.Brand} ({v.GetType().Name})");
                Console.WriteLine($"Total Rental for {rentalDays} days = {v.CalculateRental(rentalDays)}");
                Console.WriteLine("--------------------------------");
            }
        }
    }
}


