using AbstarctFactoryPatternEg.ConcreteClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstarctFactoryPatternEg
{
    class Program
    {
        public static void Main()
        {
            IVehicleFactory regularVehicleFactory = new RegularVehicleFactory();
            
            IBike regularBike = regularVehicleFactory.CreateBike();
            regularBike.GetDetails();
            
            ICar regularCar = regularVehicleFactory.CreateCar();
            regularCar.GetDetails();
            
            IVehicleFactory sportsVehicleFactory = new SportsVehicleFactory();
            
            IBike sportsBike = sportsVehicleFactory.CreateBike();
            sportsBike.GetDetails();
            
            ICar sportsCar = sportsVehicleFactory.CreateCar();
            sportsCar.GetDetails();

            Console.ReadKey();
        }
    }
}
