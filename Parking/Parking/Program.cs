using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            bool flag = true;
            while (flag)
            {
                Console.Clear();
                menu.ShowMenu();
                flag = menu.Action();
            }
        }
    }
}
