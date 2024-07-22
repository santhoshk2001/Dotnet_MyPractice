using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstarctFactoryPatternEg.CarFactory
{
    public class RegularCar : ICar
    {
        public void GetDetails()
        {
            Console.WriteLine("*****Retrieving RegularCar Information*****");
        }
    }
}
