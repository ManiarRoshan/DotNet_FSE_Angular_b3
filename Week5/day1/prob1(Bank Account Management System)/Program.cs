/*
 Level-1 Problem 1: Bank Account Management System
Scenario:
A bank wants to develop a simple console-based application to manage customer bank accounts.
The system should protect account balance information and allow controlled access using properties.
Requirements:
1. Create a BankAccount class with private fields for account number and balance.
2. Use properties to allow controlled access to account number and balance.
3. Implement Deposit and Withdraw methods with proper validation.
4. Prevent withdrawal if balance is insufficient.
Technical Constraints:
• Use private fields with public properties.
• Apply encapsulation and data hiding.
• No direct access to balance field from outside the class.
Expectations:
• Demonstrate correct use of access modifiers.
• Validate negative deposit or withdrawal amounts.
• Display updated balance after each transaction.
Learning Outcome:
• Understand encapsulation using properties.
• Apply data hiding effectively.
• Implement validation logic inside class methods.
Sample Input: 
Deposit = 5000, Withdraw = 2000
Sample Output: 
Current Balance = 3000
*/
using System;

namespace ConsoleApp5
{
    public class BankAccount
    {
        private string _accountNumber;
        private decimal _balance;

        public string AccountNumber
        {
            get { return _accountNumber; }
            set { _accountNumber = value; }
        }

        public decimal Balance 
        {
            get { return _balance; }
        }

        public BankAccount(string accNo, decimal initialBalance)
        {
            _accountNumber = accNo;
            _balance = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Error: Deposit amount must be positive.");
                return;
            }
            _balance += amount;
            Console.WriteLine($"Deposited: {amount}. New Balance: {_balance}");
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Error: Withdrawal amount must be positive.");
            }
            else if (amount > _balance)
            {
                Console.WriteLine("Error: Insufficient balance.");
            }
            else
            {
                _balance -= amount;
                Console.WriteLine($"Withdrew: {amount}. New Balance: {_balance}");
            }
        }
    } 

    internal class Program
    {
        static void Main(string[] args)
        {
            
            BankAccount myAcc = new BankAccount("12345", 0m);

            myAcc.Deposit(5000);
            myAcc.Withdraw(2000);

            Console.WriteLine($"Current Balance = {myAcc.Balance}");
        }
    }
}
