using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Get the Length of a string
namespace MyPractices
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("--------Program1--------");
            Practice1.Str_Len();
            Console.WriteLine("------------------------");

            Console.WriteLine("--------Program2--------");
            Practice2.Join_str();
            Console.WriteLine("------------------------");

            Console.WriteLine("--------Program3--------");
            Practice3.Str_Check();
            Console.WriteLine("------------------------");

            Console.ReadLine();
        }
    }

    class Practice1
    {
        public static void Str_Len()
        {
            string str = "C# Programming";
            Console.WriteLine("string: " + str);

            int length = str.Length;
            Console.WriteLine("Length: " + length);

        }
    }

    class Practice2
    {
        public static void Join_str()
        {
            string str1 = "C# ";
            Console.WriteLine("string str1: " + str1);

            string str2 = "Programming";
            Console.WriteLine("string str2: " + str2);

            string joinstr = string.Concat(str1, str2);
            Console.WriteLine("Joined string: " + joinstr);

        }
    }

    class Practice3
    {
        public static void Str_Check()
        {
            string str1 = "C# Programming";
            string str2 = "C# Programming";
            string str3 = "Java";

            Boolean result1 = str1.Equals(str2);
            Console.WriteLine("string str1 and str2 are equal: " + result1);

            Boolean result2 = str1.Equals(str3);
            Console.WriteLine("string str1 and str3 are equal: " + result2);

        }
    }

   
 
}
