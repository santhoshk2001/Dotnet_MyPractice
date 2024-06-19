using System;

//Join two strings in c#
namespace MyPractices
{
    class Program2
    {
        static void Main()
        {
            string str1 = "C# ";
            Console.WriteLine("string str1: " + str1);

            string str2 = "Programming";
            Console.WriteLine("string str2: " + str2);

            string joinstr = string.Concat(str1, str2);
            Console.WriteLine("Joined string: " + joinstr);

            Console.ReadLine();
        }

    }
}
