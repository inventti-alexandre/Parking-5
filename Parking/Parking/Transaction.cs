using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    class Transaction
    {
        public DateTime CreatedOn { get; }
        public Guid IdCar { get; }
        public decimal Amount { get; }

        public Transaction(DateTime time, Guid id, decimal amount)
        {
            CreatedOn = time;
            IdCar = id;
            Amount = amount;
        }
    }
}
