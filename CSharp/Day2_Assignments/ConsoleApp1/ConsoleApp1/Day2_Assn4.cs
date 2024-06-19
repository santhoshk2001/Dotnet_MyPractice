using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Day2_Assn4
    {
        static void Main()
        {
            int number1, number2, temp;

            Console.WriteLine("Enter the first number: ");
            number1 = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the second number: ");
            number2 = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\nBefore swapping:" + number1 + " " + number2);
            Console.WriteLine("First number: " + number1);
            Console.WriteLine("Second number: " + number2);
            
            temp = number1;
            number1 = number2;
            number2 = temp;

            Console.WriteLine("\nAfter swapping:"+number1+" "+number2);
            Console.WriteLine("First number: " + number1);
            Console.WriteLine("Second number: " + number2);

            Console.ReadLine();
        }

    }
}
