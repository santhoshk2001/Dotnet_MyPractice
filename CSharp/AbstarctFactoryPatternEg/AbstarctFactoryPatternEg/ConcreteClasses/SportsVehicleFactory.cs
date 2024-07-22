using AbstarctFactoryPatternEg.BikeFactory;
using AbstarctFactoryPatternEg.CarFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstarctFactoryPatternEg.ConcreteClasses
{
    public class SportsVehicleFactory : IVehicleFactory
    {
        public IBike CreateBike()
        {
            return new SportsBike();
        }
        public ICar CreateCar()
        {
            return new SportsCar();
        }
    }
}
