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
            "Please, write number action.",
            "1 - Current balance",
            "2 - Revenue for the last minute",
            "3 - Show number of free and busy places",
            "4 - Add car",
            "5 - Remove car",
            "6 - Top up car balance",
            "7 - Display transaction for the last minute",
            "8 - Display Transactions.log",
            "Any key - Exit"
        };

        Parking parking = Parking.GetParking();

        public void ShowMenu()
        {
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

        public decimal CheckOutInputDecimal()
        {
            decimal input = 0;
            while (!decimal.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("You need to type digits.");
            }
            return input;
        }

        public int CheckOutInputInt(int firstCondition, int secondCondition)
        {
            int input;
            while (!(int.TryParse(Console.ReadLine(), out input) && (input >= firstCondition && input <= secondCondition)))
            {
                Console.WriteLine($"You need to type number from {firstCondition} to {secondCondition}.");
            }
            return input;
        }

        public bool Action()
        {
            var flag = true;
            var value = Console.ReadKey();
            var busyPlaces = parking.DisplayNumberOfBusyPlaces();
            switch (value.Key)
            {
                case ConsoleKey.NumPad1:
                    Console.Clear();
                    Console.WriteLine($"Total revenue: {parking.DisplayTotalRevenue()}");
                    EndOfParagraph();
                    break;
                case ConsoleKey.NumPad2:
                    Console.Clear();
                    Console.WriteLine($"Amount money for the last minute:{Parking.AmountForTheLastMinute()}");
                    EndOfParagraph();
                    break;
                case ConsoleKey.NumPad3:
                    Console.Clear();
                    Console.WriteLine($"Free spaces: {parking.DisplayNumberOfFreePlaces()}.\nBusy spaces: {busyPlaces}");
                    EndOfParagraph();
                    break;
                case ConsoleKey.NumPad4:
                    Console.Clear();
                    Console.WriteLine("To add car, please, type: car type and balance.\nType car type (Motorcycle = 1, Bus = 2, Passenger = 3, Truck = 4):");
                    var newCarType = CheckOutInputInt(1, 4);
                    Console.WriteLine("Type balance:");
                    var newCarBalance = CheckOutInputDecimal();
                    parking.AddCar((CarType)newCarType, newCarBalance);
                    Console.WriteLine("The car added.");
                    EndOfParagraph();
                    break;
                case ConsoleKey.NumPad5:
                    Console.Clear();
                    if (busyPlaces == 0)
                    {
                        Console.WriteLine("There is not car in the parking.");
                    }
                    else
                    {
                        Console.WriteLine($"To remove car, please, type the number of this car from 1 to {busyPlaces}:");
                        var numberOfCar = CheckOutInputInt(1, busyPlaces);
                        if (parking.HasFine(numberOfCar))
                        {
                            Console.WriteLine("The car has fine. Would you like to top up balance (press any key) or no (press 0)?");
                            var i = int.Parse(Console.ReadLine());
                            if (i == 0)
                            {
                                Console.WriteLine("The car didn`t remove.");
                                EndOfParagraph();
                                break;
                            }
                        }
                        parking.RemoveCar(numberOfCar, out var balance);
                        Console.WriteLine($"Balance was {balance}. The car removed.");
                    }
                    EndOfParagraph();
                    break;
                case ConsoleKey.NumPad6:
                    Console.Clear();
                    if (busyPlaces == 0)
                    {
                        Console.WriteLine("There is not car in the parking.");
                    }
                    else
                    {
                        Console.WriteLine($"To top up car balance, please, type the number of this car from 1 to {busyPlaces}:");
                        var numberOfCar = CheckOutInputInt(1, busyPlaces);
                        Console.WriteLine("Type money:");
                        var money = CheckOutInputDecimal();
                        Console.WriteLine($"The balance is topped up. Now balance: {parking.TopUp(numberOfCar, money)}");
                    }
                    EndOfParagraph();
                    break;
                case ConsoleKey.NumPad7:
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
                case ConsoleKey.NumPad8:
                    Console.Clear();
                    Console.WriteLine("Display Transactions.log");
                    var arrayStr = parking.DisplayTransactionsFile().Split(' ');
                    for (var i = 0; i < arrayStr.Length - 3; i++)
                    {
                        Console.Write($"Data: {arrayStr[i]} Time: {arrayStr[++i]} Amount for the last minute: {arrayStr[++i]}. The total number of transactions for the last minute: {arrayStr[++i]}.");
                    }
                    EndOfParagraph();
                    break;
                default:
                    flag = false;
                    break;
            }
            return flag;
        }
    }
}
