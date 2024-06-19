using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Day2_Assn3
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the first number:");
            double num1 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter the second number:");
            double num2 = Convert.ToDouble(Console.ReadLine());

            double addition = num1 + num2;
            double subtraction = num1 - num2;
            double multiplication = num1 * num2;
            double division = num1 / num2;

            Console.WriteLine($"Addition: {num1} + {num2} = {addition}");
            Console.WriteLine($"Subtraction: {num1} - {num2} = {subtraction}");
            Console.WriteLine($"Multiplication: {num1} * {num2} = {multiplication}");

            if (num2 != 0)
                Console.WriteLine($"Division: {num1} / {num2} = {division}");
            else
                Console.WriteLine("Cannot divide by zero.");
            Console.Read();
        }
    }
}
