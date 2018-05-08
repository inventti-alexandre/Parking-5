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
                    break;
                case 2:
                    Console.WriteLine("Amount per minute");
                    break;
                case 3:
                    Console.WriteLine("Count free places");
                    break;
                case 4:
                    Console.WriteLine("Add car");
                    break;
                case 5:
                    Console.WriteLine("Remove car");
                    break;
                case 6:
                    Console.WriteLine("Top up balance car");
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
