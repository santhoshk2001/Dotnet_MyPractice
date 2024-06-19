using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Day2_Assn1
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the first integer:");
            int num1 = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the second integer:");
            int num2 = Convert.ToInt32(Console.ReadLine());

            if (num1 == num2)
            {
                Console.WriteLine("The two integers are equal.");
            }
            else
            {
                Console.WriteLine("The two integers are not equal.");
            }
            Console.Read();
        }
    }
}
