using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace FileHandling
{
    class Program
    {
        static void Main()
        {
            string filePath = "C:\\Users\\santhoshke\\Batch_June24\\CSharp\\FileHandling\\example.txt";

            try
            {
                Console.WriteLine("Enter the text to append:");
                string textToAppend = Console.ReadLine();

                if (File.Exists(filePath))
                {
                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {
                        writer.WriteLine(textToAppend);
                    }
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.WriteLine(textToAppend);
                    }
                }

                Console.WriteLine("Text appended successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.ReadKey();
            }
            Console.Read();
        }
    }
}
