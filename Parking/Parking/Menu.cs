using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    class Menu
    {
        string[] list = {
            "1 - Current balance",
            "2 - Amount per minute",
            "3 - Count free places",
            "4 - Add car",
            "5 - Remove car",
            "6 - Top up balance car",
            "7 - Display transaction history per minute",
            "8 - Display Transactions.log",
            "0 - Exit"
        };
        Parking parking;
        public void ShowList()
        {
            Console.WriteLine("Write number action, please.");
            foreach (var el in list) Console.WriteLine(el);
        }
        public void Action(int value)
        {
            switch (value)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Current balance");
                    parking.DisplayTotalRevenue();
                    break;
                case 2:
                    Console.WriteLine("Amount per minute");
                    break;
                case 3:
                    Console.WriteLine("Count free places");
                    parking.DisplayNumberOfFreePlaces();
                    break;
                case 4:
                    Console.WriteLine("For add car type in one line with spaces: identifier, car type ( Motorcycle = 1, Bus = 2, Passenger = 3, Truck = 4) and balance:");
                    var values = Console.ReadLine().Split(' ').Select(decimal.Parse).ToArray();
                    int type= (int)values[1];
                    if (Enum.IsDefined(typeof(CarType), type))
                    {
                        parking.AddCar((int)values[0], values[3], (CarType)type);
                    }
                    break;
                case 5:
                    Console.WriteLine("For remove car type the number of this car from 0 to busy places");
                    parking.DisplayNumberOfBusyPlaces();
                    int number=int.Parse(Console.ReadLine());
                    parking.RemoveCar(number);
                    break;
                case 6:
                    Console.WriteLine("For top up balance car type identifier:");
                    
                    break;
                case 7:
                    Console.WriteLine("Display transaction history per minute");
                    break;
                case 8:
                    Console.WriteLine("Display Transactions.log");
                    break;
                case 0:
                    Console.WriteLine("Go back/Exit");
                    break;
            }
        }
    }
}
