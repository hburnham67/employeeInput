using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;

namespace Week4Exceptions
{
    // Employee Class to create objects for each loop of input
    public class Employee
    {
        public int EmployeeId { get; internal set; }
        public string Name { get; set; }
        public int Zip { get; set; }
        public double GrossMonthlyPay { get; set; }
        public double GrossAnnualPay { get; set; }
        public double GrossMonthlyTax { get; set; }
        public double GrossAnnualTax { get; set; }
        public string EmployeeType { get; set; }

        /*
        // Employee default constructor
        // public Employee() {}
        // I am using default constructor because we are obtaining the type of employee in the input logic. Because both types of employees
        // DO use the same variables (GrossMonthlyTax, GrossAnnualTax), but their values are determined by type of employee, using a constructor
        // such as the example below would just be more tedious because you cannot have two constructors with the exact same signature. I just put
        // the example below to demonstrate what a constructor would otherwise look like. If there is a better way to do this, I would love to know.
        
        public Employee(int employeeId, string name, int zip, double grossMonthlyPay, double grossAnnualPay, string employeeType,
            double grossMonthlyTax, double grossAnnualTax)
        {
            EmployeeId = employeeId;
            Name = name;
            Zip = zip;
            GrossMonthlyPay = grossMonthlyPay;
            GrossAnnualPay = grossAnnualPay;
            EmployeeType = employeeType;
            GrossMonthlyTax = grossMonthlyTax;
            GrossAnnualTax = grossAnnualTax;
        }
        */

    }

    class Program
    {
        static void Main(string[] args)
        {
            // Method for validating number of digits in an entry - used for Zip in this case
            // Only not including this in grossMonthlyPay valdation to be able to enter a negative number (maybe you're involved
            // in a pyramid scheme and actually pay to work, in which case, a negative number could actually be valid). 
            // Otherwise, this method could be passed in to gross pay section and validate number is greater than  or equal to 0 digits, and less than 9 digits
            int CountDigits(int number)
            {
                number = Math.Abs(number);

                if (number >= 10)
                    return CountDigits(number / 10) + 1;
                return 1;
            }

            var employeeId = 0;
            var employeeList = new List<Employee>();
            // Validation variables
            var validatedNameInput = "";
            var validatedZipInput = 0;
            double validatedGrossMonthlyPayInput = 0;

            // Execute loop 3 times, adding instance of a class after each loop
            // Store each instance of a class in a list (employeeList)
            for (int i = 1; i < 4; i++)
            {
                bool invalidNameInput = true;
                bool invalidZipInput = true;
                bool invalidGrossMonthlyPayInput = true;
                bool invalidEmployeeType = true;

                // Employee is assigned an auto-incremented ID
                employeeId++;
                Console.WriteLine("\nSoftware Developer # " + i + "'s Name: ");

                while (invalidNameInput)
                {
                    var nameInput = Convert.ToString(Console.ReadLine());
                    // Validate name input is not null and only contains characters
                    if (!String.IsNullOrEmpty(nameInput) && Regex.IsMatch(nameInput, @"^[a-zA-Z]+$")) 
                    {
                        validatedNameInput = nameInput;
                        invalidNameInput = false;
                    }
                    else
                    {
                        Console.WriteLine(
                            "Please enter a name:\n Name cannot be blank, and can only contain letters. Try again: ");
                    }
                }

                Console.WriteLine("Zip code:");

                while (invalidZipInput)
                {
                    // Variable for validating input is numeric
                    var isNumeric = int.TryParse(Console.ReadLine(), out var zipInput);
                    // If input is numeric and equals exactly 5 digits, proceed, else output Try Again prompt
                    if(isNumeric && (CountDigits(zipInput) == 5))
                    {
                        validatedZipInput = Convert.ToInt32(zipInput);
                        invalidZipInput = false;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a 5 digit zip code. Try again: ");
                    }
                }

                Console.WriteLine("Gross Monthly Pay:");
                while (invalidGrossMonthlyPayInput)
                {
                    // Variable for validating input is numeric
                    var isNumericDouble = double.TryParse(Console.ReadLine(), out var grossMonthlyPayInput);
                    // If input is numeric, proceed, else output Try Again prompt
                    if (isNumericDouble)
                    {
                        validatedGrossMonthlyPayInput = Convert.ToDouble(grossMonthlyPayInput);
                        invalidGrossMonthlyPayInput = false;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid number. Try again: ");
                    }
                }

                Console.WriteLine("Employee Type (W2 or 1099):");
                    // Determine employee type. Keep looping until valid input (W2 or 1099)
                    // Convert string to upper case in case user inputs lowercase w in "W2"
                    while (invalidEmployeeType)
                    {
                        var employeeTypeInput = Convert.ToString(Console.ReadLine()).ToUpper();
                        if (employeeTypeInput == "W2")
                        {
                            // Employee will have W2 tax type values returned
                            Employee employee = new Employee();

                            employee.EmployeeId = employeeId;
                            employee.Name = validatedNameInput;
                            employee.Zip = validatedZipInput;
                            employee.GrossMonthlyPay = validatedGrossMonthlyPayInput;
                            employee.GrossAnnualPay = (validatedGrossMonthlyPayInput * 12);
                            employee.EmployeeType = employeeTypeInput;
                            employee.GrossMonthlyTax = (validatedGrossMonthlyPayInput * .07);
                            employee.GrossAnnualTax = ((validatedGrossMonthlyPayInput * .07) * 12);
                            employeeList.Add(employee);
                            invalidEmployeeType = false;
                        }
                        else if (employeeTypeInput == "1099")
                        {
                            // Employee will have 1099 tax type values returned which is 0
                            Employee employee = new Employee();

                            employee.EmployeeId = employeeId;
                            employee.Name = validatedNameInput;
                            employee.Zip = validatedZipInput;
                            employee.GrossMonthlyPay = validatedGrossMonthlyPayInput;
                            employee.GrossAnnualPay = (validatedGrossMonthlyPayInput * 12);
                            employee.EmployeeType = employeeTypeInput;
                            employee.GrossMonthlyTax = 0;
                            employee.GrossAnnualTax = 0;
                            employeeList.Add(employee);
                            invalidEmployeeType = false;
                        }
                        else
                        {
                            Console.WriteLine("Please enter W2 or 1099. Try again:");
                        }
                    }

                
            }

            // Display all software developers details, pulling from the list
                foreach (var e in employeeList)
                {
                    Console.WriteLine("\nSoftware Developer ID #: " + e.EmployeeId);
                    Console.WriteLine("Software Developer Name: " + e.Name);
                    Console.WriteLine("Software Developer Zip: " + e.Zip);
                    Console.WriteLine("Software Developer Gross Monthly Pay: " + e.GrossMonthlyPay.ToString("C"));
                    Console.WriteLine("Software Developer Gross Annual Pay: " + e.GrossAnnualPay.ToString("C"));
                    Console.WriteLine("Software Developer Employee Type: " + e.EmployeeType);
                    Console.WriteLine("Software Developer Monthly Tax: " + e.GrossMonthlyTax.ToString("C"));
                    Console.WriteLine("Software Developer Annual Tax: " + e.GrossAnnualTax.ToString("C"));
                }

                //Prompt user to press ENTER to complete program
                Console.WriteLine("Press Enter to complete the program and exit the console");
                //Reads the console after writing so the window pauses for input
                Console.ReadLine();
            }
        
    }
}






