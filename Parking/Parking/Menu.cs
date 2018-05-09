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

        Parking parking = Parking.GetParking();

        public void ShowList()
        {
            Console.WriteLine("Write number action, please.");
            foreach (var el in list) Console.WriteLine(el);
        }
        public bool Action()
        {
            bool flag = true;
            int value = int.Parse(Console.ReadLine());
            switch (value)
            {
                case 1:
                    Console.Clear();
                    //Console.WriteLine("If you change your mind type 0 (Exit) or 1(Go back)");
                    parking.DisplayTotalRevenue(); //Current balance
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Amount money per minute:");

                    break;
                case 3:
                    Console.Clear();
                    parking.DisplayNumberOfFreePlaces(); //Count free places
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("For add car, please, type in one line with spaces: identifier, balance and car type ( Motorcycle = 1, Bus = 2, Passenger = 3, Truck = 4):");
                    var values4 = Console.ReadLine().Split(' ').Select(decimal.Parse).ToArray();
                    int type = (int)values4[2];
                    if (Enum.IsDefined(typeof(CarType), type))
                    {
                        parking.AddCar((int)values4[0], values4[1], (CarType)type);
                        Console.WriteLine("The car added.");
                    }
                    break;
                case 5:
                    Console.Clear();
                    if (parking.DisplayNumberOfBusyPlaces() == 0) Console.WriteLine("There is not car in the parking.");
                    else
                    {
                        Console.WriteLine($"For remove car, please, type the number of this car from 1 to {parking.DisplayNumberOfBusyPlaces()}:");
                        int number = int.Parse(Console.ReadLine());
                        parking.RemoveCar(number);
                        Console.WriteLine("The car removed.");
                    }
                    break;
                case 6:
                    Console.Clear();
                    if (parking.DisplayNumberOfBusyPlaces() == 0) Console.WriteLine("There is not car in the parking.");
                    else
                    {
                        Console.WriteLine($"For top up balance car, please, type in one line with spaces: the number of this car from 1 to {parking.DisplayNumberOfBusyPlaces()} and money:");
                        var values = Console.ReadLine().Split(' ').Select(decimal.Parse).ToArray();
                        parking.TopUp((int)values[0], values[1]);
                    }
                    break;
                case 7:
                    Console.Clear();
                    Console.WriteLine("Display transaction history per minute:");
                    parking.DisplayTransactionHistoryPerMinute();
                    break;
                case 8:
                    Console.Clear();
                    Console.WriteLine("Display Transactions.log");
                    break;
                case 0:
                    flag=false;
                    break;
            }
            return flag;
        }
    }
}
