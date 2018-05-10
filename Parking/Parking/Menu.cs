using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    class Menu
    {
        string[] menu = {
            "1 - Current balance",
            "2 - Revenue for the last minute",
            "3 - Show number of free and busy places",
            "4 - Add car",
            "5 - Remove car",
            "6 - Top up car balance",
            "7 - Display transaction for the last minute",
            "8 - Display Transactions.log",
            "0 - Exit"
        };

        Parking parking = Parking.GetParking();

        public void ShowMenu()
        {
            Console.WriteLine("Write number action, please.");
            foreach (var el in menu)
            {
                Console.WriteLine(el);
            }
        }

        public void EndOfParagraph()
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("Press any key to go to menu.");
            Console.ReadKey();
        }

        public bool Action()
        {
            bool flag = true;
            int value = int.Parse(Console.ReadLine());

            switch (value)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine($"Total revenue: {parking.DisplayTotalRevenue()}");
                    EndOfParagraph();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine($"Amount money for the last minute:{Parking.AmountForTheLastMinute()}");
                    EndOfParagraph();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine($"Free spaces: {parking.DisplayNumberOfFreePlaces()}");
                    Console.WriteLine($"Busy spaces: {parking.DisplayNumberOfBusyPlaces()}");
                    EndOfParagraph();
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("To add car, please, type in one line with spaces: identifier, balance and car type ( Motorcycle = 1, Bus = 2, Passenger = 3, Truck = 4):");
                    var values4 = Console.ReadLine().Split(' ').Select(decimal.Parse).ToArray();
                    var type = (int)values4[2];
                    if (Enum.IsDefined(typeof(CarType), type))
                    {
                        parking.AddCar((int)values4[0], values4[1], (CarType)type);
                        Console.WriteLine("The car added.");
                        EndOfParagraph();
                    }
                    break;
                case 5:
                    Console.Clear();
                    if (parking.DisplayNumberOfBusyPlaces() == 0)
                    {
                        Console.WriteLine("There is not car in the parking.");
                        EndOfParagraph();
                    }
                    else
                    {
                        Console.WriteLine($"To remove car, please, type the number of this car from 1 to {parking.DisplayNumberOfBusyPlaces()}:");
                        int number = int.Parse(Console.ReadLine());
                        if (parking.HasFine(number))
                        {
                            Console.WriteLine("The car has fine. Would you like to top up balance (press any key) or no (press 0)?");
                            var i = int.Parse(Console.ReadLine());
                            if (i == 0)
                            {
                                Console.WriteLine("The car didn`t remove.");
                                break;
                            }
                        }
                        parking.RemoveCar(number, out var balance);
                        Console.WriteLine($"Balance was {balance}. The car removed.");
                        EndOfParagraph();
                    }
                    break;
                case 6:
                    Console.Clear();
                    if (parking.DisplayNumberOfBusyPlaces() == 0)
                    {
                        Console.WriteLine("There is not car in the parking.");
                        EndOfParagraph();
                    }
                    else
                    {
                        Console.WriteLine($"To top up balance car, please, type in one line with spaces: the number of this car from 1 to {parking.DisplayNumberOfBusyPlaces()} and money:");
                        var values = Console.ReadLine().Split(' ').Select(decimal.Parse).ToArray();
                        Console.WriteLine($"The balance is topped up. Now balance: {parking.TopUp((int)values[0], values[1])}");
                        EndOfParagraph();
                    }
                    break;
                case 7:
                    Console.Clear();
                    Console.WriteLine("Display transaction for the last minute:");
                    var list = parking.DisplayTransactionForTheLastMinute();
                    if (list == null)
                    {
                        Console.WriteLine("Transaction for the last minute don`t exit.");
                    }
                    else
                    {
                        list.ForEach(n => Console.WriteLine($"Data: {n.CreatedOn} Id car:{n.IdCar} Amount: {n.Amount}"));
                    }
                    EndOfParagraph();
                    break;
                case 8:
                    Console.Clear();
                    Console.WriteLine("Display Transactions.log");
                    var arrayStr=parking.DisplayTransactionsFile().Split(' ');
                    for (int i = 0; i < arrayStr.Length-3; i++)
                    {
                        Console.Write($"Data: {arrayStr[i]} Time: {arrayStr[++i]} Amount for the last minute: {arrayStr[++i]}. The total number of transactions for the last minute: {arrayStr[++i]}.");
                    }
                    EndOfParagraph();
                    break;
                case 0:
                    flag = false;
                    break;
            }
            return flag;
        }
    }
}
