﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    class Car
    {
        public int Id { get; private set; }
        public decimal Fine { get; set; }
        private decimal balance;
        public decimal Balance
        {
            get { return balance; }
            set { if (value > 0) balance = value; }
        }
        public CarType CarType { get; private set; }
        public Car(int id, decimal balance, CarType type)
        {
            Id = id;
            Balance = balance;
            CarType = type;
            Fine = 0;
        }

    }
}
