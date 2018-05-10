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
            "Please, press the number of an action.\n",
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
            var str = Console.ReadLine();
            while (str.Contains(',') || !decimal.TryParse(str, out input))
            {
                Console.WriteLine("You need to type digits. Don`t use comma. Example: 25.25");
                str = Console.ReadLine();
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
            var busyPlaces = parking.GetNumberOfBusyPlaces();
            var value = CheckOutInputInt(0, 8);
            Console.Clear();
            switch (value)
            {
                case 1:
                    Console.WriteLine($"Total revenue: {parking.GetTotalRevenue()}");
                    break;
                case 2:
                    Console.WriteLine($"Amount money for the last minute:{Parking.AmountForTheLastMinute()}");
                    break;
                case 3:
                    Console.WriteLine($"Free spaces: {parking.GetNumberOfFreePlaces()}.\nBusy spaces: {busyPlaces}.");
                    break;
                case 4:
                    Console.WriteLine("To add car, please, type: car type and balance.\nType car type (Motorcycle = 1, Bus = 2, Passenger = 3, Truck = 4):");
                    var newCarType = CheckOutInputInt(1, 4);
                    Console.WriteLine("Type balance:");
                    var newCarBalance = CheckOutInputDecimal();
                    parking.AddCar((CarType)newCarType, newCarBalance);
                    Console.WriteLine("The car was added.");
                    break;
                case 5:
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
                                Console.WriteLine("The car was not removed.");
                                break;
                            }
                        }
                        parking.RemoveCar(numberOfCar, out var balance);
                        Console.WriteLine($"Balance was {balance}. The car removed.");
                    }
                    break;
                case 6:
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
                    break;
                case 7:
                    var list = parking.GetTransactionsForTheLastMinute();
                    if (list.Any())
                    {
                        Console.WriteLine("Display transaction for the last minute:");
                        list.ForEach(n => Console.WriteLine($"Data: {n.CreatedOn} Id car:{n.IdCar} Amount: {n.Amount}"));
                    }
                    else
                    {
                        Console.WriteLine("Transaction for the last minute don`t exit.");
                    }
                    break;
                case 8:
                    Console.WriteLine("Display Transactions.log");
                    try
                    {
                        var arrayStr = parking.GetTransactionsFile().Split(' ');
                        for (var i = 0; i < arrayStr.Length - 3; i++)
                        {
                            Console.WriteLine($"Data: {arrayStr[i]} Time: {arrayStr[++i]} Amount for the last minute: {arrayStr[++i]}. The total number of transactions for the last minute: {arrayStr[++i]}.");
                        }
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine("Oops! We are not able to show you the file. Please try again later.");
                    }
                    break;
                default:
                    flag = false;
                    break;
            }

            if (flag)
            {
                EndOfParagraph();
            }
            return flag;
        }
    }
}
