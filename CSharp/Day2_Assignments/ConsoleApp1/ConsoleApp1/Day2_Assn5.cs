using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Day2_Assn5
    {
        static void Main()
        {
            Console.WriteLine("Enter a digit: ");
            int number = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("{0} {0} {0} {0}", number);
            Console.WriteLine("{0}{0}{0}{0}", number);

            Console.WriteLine("{0} {0} {0} {0}", number);
            Console.WriteLine("{0}{0}{0}{0}", number);

            Console.ReadLine();
        }
    }
}
