using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    class Transaction
    {
        public DateTime Time { get; private set; }
        public int IdentifierCar { get; private set; }
        public decimal Amount { get; private set; }
    }
}
