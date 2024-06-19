using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Comparing the strings
namespace MyPractices
{
    class Program3
    {
        public static void Main(string[] args)
        {
            string str1 = "C# Programming";
            string str2 = "C# Programming";
            string str3 = "Java";

            Boolean result1 = str1.Equals(str2);
            Console.WriteLine("string str1 and str2 are equal: " + result1);

            Boolean result2 = str1.Equals(str3);
            Console.WriteLine("string str1 and str3 are equal: " + result2);

            Console.ReadLine();
        }
    }
}
