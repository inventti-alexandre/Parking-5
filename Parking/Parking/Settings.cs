using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    class Settings
    {
        public static int Timeout { get; private set; }
        public static Dictionary<CarType, int> prices = new Dictionary<CarType, int>
        {
            [CarType.Motorcycle] = 1,
            [CarType.Bus] = 2,
            [CarType.Passenger] = 4,
            [CarType.Truck] = 5
        };
        public static int ParkingSpace { get; private set; }
        public static int CoefficientFine { get; private set; }
        static Settings()
        {
            Timeout = 3000;
            ParkingSpace = 50;
            CoefficientFine = 3;
        }
    }
}
