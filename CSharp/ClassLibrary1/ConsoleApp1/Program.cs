using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary;
namespace ConsoleApp1
{
class
Program
{
    public static
    void
    Main
    (string[] args)
    {
        MyClass myClass =
new
MyClass(); Console.WriteLine(myClass.GetMessage());
        Console.ReadLine();
    }
}
}