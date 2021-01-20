using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6
{
    //Create a class named Customer that implements IComparable interface.
    class Customer : IComparable
    {
        //Create 3 Customer class fields: Customer number, customer name, and amount due. Create automatic accessors for each field.
        public int CustomerNum { get; set; }
        public string Name { get; set; }
        public double AmountDue { get; set; }

        //Create an Customer class constructor that takes parameters for all of the class fields and assigns the passed values through the accessors.
        public Customer(int customerNum, string name, double amountDue)
        {
            CustomerNum = customerNum;
            Name = name;
            AmountDue = amountDue;
        }
        //Create a default, no-argument Customer class constructor that will take no parameters and will cause default values of (9, "ZZZ", 0) 
        //to be sent to the 3-argument constructor.
        public Customer()
        {
            CustomerNum = 9;
            Name = "ZZZ";
            AmountDue = 0;
        }
        //Implement CompareTo to compare object customer numbers for >, <, == to implement sorting for the array of objects.
        public int CompareTo(object obj)
        {
            Customer otherCustomer = (Customer)(obj);
            return this.CustomerNum.CompareTo(otherCustomer.CustomerNum);
        }
        // Create an (override) Equals() method that determines two Customers are equal if they have the same Customer number.
        public override bool Equals(object obj)
        {
            Customer customer = (Customer)(obj);
            if(this.CustomerNum == customer.CustomerNum)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Create an (override) GetHashCode() method that returns the Customer number.
        public override int GetHashCode()
        {
            return this.CustomerNum;
        }
        //Create an (override) ToString() method that returns a string containing the general Customer information
        //(eg:  CreditCustomer 1 russell AmountDue is $4,311.00 Interest rate is 0.01).  Display the dollar amounts in currency format.
        public override string ToString()
        {
            return "CreditCustomer " + CustomerNum + " " + Name + AmountDue.GetType().Name + " is " 
                   + string.Format("{0:C2}", AmountDue, 2);
        }
    }
    //Create a CreditCustomer class that derives from Customer and implements IComparable interface.
    class CreditCustomer : Customer, IComparable
    {
        //Create a class variable named Rate using an automatic accessor.
        public double Rate { get; set; }
        //Create an CreditCustomer class constructor that takes parameters for the Customer class fields customer number, name, amount, 
        //and rate percent that sets the Rate CreditCustomer variable to the rate percentage. 
        //Pass the id number, name and amount back to the base Customer class constructor.
        public CreditCustomer(int customerNum, string name, double amountDue, double rate)
        {
            CustomerNum = customerNum;
            Name = name;
            AmountDue = amountDue;
            Rate = rate;
        }
        //Create a default, no-argument CreditCustomer class constructor that will take no parameters and will cause default values of 
        //(0, "", 0, 0) to be sent to the 4-argument CreditCustomer constructor.
        public CreditCustomer()
        {
            CustomerNum = 0;
            Name = "";
            AmountDue = 0;
            Rate = 0;
        }
        //Create an (override) ToString() method that returns a string containing the general Customer information 
        //(eg:  CreditCustomer 1 russell AmountDue is $4,311.00 Interest rate is 0.01 Monthly payment is $179.63).  
        //Display the dollar amounts in currency format.
        public override string ToString()
        {
            return this.GetType().Name + " " + CustomerNum + " " + Name + " AmountDue is "
                   + string.Format("{0:C2}", AmountDue, 2) + " Interest rate is " + Rate;
        }
    }
    class Assignment6_Lui
    {
        static void Main(string[] args)
        {
            //Create an array of five CreditCustomer objects.
            CreditCustomer[] listOfCreditCustomers = new CreditCustomer[5];
            //Prompt the user for values for each of the five Customer object; 
            for (int i = 0; i < 5; i++)
            {
                CreditCustomer creditCustomer = new CreditCustomer();
                bool isDuplicate = false;
                Console.Write("Enter customer number ");
                creditCustomer.CustomerNum = int.Parse(Console.ReadLine());
                listOfCreditCustomers[i] = creditCustomer;
                //do NOT allow duplicate Customer numbers and force the user to reenter the Customer when a duplicate Customer number is entered.
                do
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (listOfCreditCustomers[j].Equals(listOfCreditCustomers[i]) == true)
                        {
                            isDuplicate = true;
                            Console.WriteLine("Sorry, the customer number {0} is a duplicate.", creditCustomer.CustomerNum);
                            Console.Write("Please reenter ");
                            creditCustomer.CustomerNum = int.Parse(Console.ReadLine());
                            listOfCreditCustomers[i] = creditCustomer;
                        }
                        else
                        {
                            isDuplicate = false;
                        }
                    }
                } while (isDuplicate == true);
                
                Console.Write("Enter name ");
                creditCustomer.Name = Console.ReadLine();
                Console.Write("Enter amount due ");
                creditCustomer.AmountDue = double.Parse(Console.ReadLine());
                Console.Write("Enter interest rate ");
                creditCustomer.Rate = double.Parse(Console.ReadLine());
            }
            //CreditCustomer objects should be sorted by Customer number before they are displayed.
            Array.Sort(listOfCreditCustomers);
            //When the five valid Customers have been entered, display them all, display a total amount due for all Customers, 
            //display the same information again  with the monthly payment for each customer.
            Display(listOfCreditCustomers);

        }
        //Create a static GetPaymentAmounts method that will have the current Credit customer object as a parameter and returns a double value type.  
        public static double GetPayMentAmounts(double amountDue)
        {
            //Each CreditCustomer monthly payment will be 1/24 of the balance (amount due).  
            //The computed monthly individual customer payment will be returned for each CreditCustomer object in the object array.
            return amountDue / 24.0;
        }
        public static void Display(CreditCustomer[] creditCustomers)
        {
            Console.WriteLine();
            Console.WriteLine("Summary:");
            Console.WriteLine();
            double totalDue = 0;
            //When the five valid Customers have been entered, display them all, display a total amount due for all Customers, 
            foreach (var creditCustomer in creditCustomers)
            {
                Console.WriteLine(creditCustomer.ToString());
                totalDue += creditCustomer.AmountDue;
            }

            Console.WriteLine();
            Console.WriteLine("AmountDue for all Customers is {0:C2}", totalDue, 2);
            Console.WriteLine();
            //display the same information again  with the monthly payment for each customer.
            Console.WriteLine("Payment Information:");
            Console.WriteLine();
            foreach (var creditCustomer in creditCustomers)
            {
                Console.WriteLine(creditCustomer.ToString());
                Console.WriteLine("Monthly payment is {0:C2}", GetPayMentAmounts(creditCustomer.AmountDue));
            }
        }
    }
}
