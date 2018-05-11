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
            Id = Guid.NewGuid(); 
            CarType = type;
            Balance = balance;
        }

    }
}
