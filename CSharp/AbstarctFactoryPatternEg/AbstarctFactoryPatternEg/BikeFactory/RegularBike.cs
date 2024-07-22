using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstarctFactoryPatternEg.BikeFactory
{
    public class RegularBike : IBike
    {
        public void GetDetails()
        {
            Console.WriteLine("*****Retrieving RegularBike Information*****");
        }
    }
}
