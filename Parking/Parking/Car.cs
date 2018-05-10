using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    class Car
    {
        public Guid Id { get; }
        public decimal Balance { get; set; }
        public CarType CarType { get; }

        public Car(CarType type, decimal balance)
        {
            Id = new Guid(1, 2, 3, new byte[] { 0, 1, 2, 3, 4, 5, 6, 7 });
            CarType = type;
            Balance = balance;
        }

    }
}
