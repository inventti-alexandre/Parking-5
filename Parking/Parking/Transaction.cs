using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    class Transaction
    {
        public DateTime CreatedOn { get; private set; }
        public int IdCar { get; private set; }
        public decimal Amount { get; private set; }
        public Transaction(DateTime time, int ident, decimal amount)
        {
            CreatedOn = time;
            IdCar = ident;
            Amount = amount;
        }
    }
}
