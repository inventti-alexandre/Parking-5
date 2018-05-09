using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    class Parking
    {
        private static readonly Lazy<Parking> lazy = new Lazy<Parking>(() => new Parking());
        private List<Car> cars;
        private List<Transaction> transactions;
        public decimal Balance { get; private set; }
        private Parking()
        {
            Balance = 0;
        }
        public static Parking GetParking()
        {
            return lazy.Value;
        }
        public void DisplayTotalRevenue()
        {
            Console.WriteLine($"Total revenue: {Balance}");
        }
        public void CollectPayment(Car car)
        {
            Settings.prices.TryGetValue(car.CarType, out int price);
            if (car.Balance < price)
                car.Fine = price * Settings.CoefficientFine;
            else
            {
                car.Balance -= price;
                Balance += price;
                transactions.Add(new Transaction(DateTime.Now, car.Identifier, price));
            }

        }
        public void AddCar(int ident, decimal balance, int type)
        {
            cars.Add(new Car(ident, balance, type));
        }
        public void RemoveCar(int number)
        {
            if (cars[number].Fine > 0)
            {
                cars[number].TopUp(cars[number].Fine);
                CollectPayment(cars[number]);
            }
            cars.Remove(cars[number]);
        }
        public void DisplayNumberOfFreePlaces()
        {
            Console.WriteLine($"Free spaces: {Settings.ParkingSpace - cars.Count}");
        }
        public void DisplayNumberOfBusyPlaces()  
        {
            Console.WriteLine($"Busy spaces: {cars.Count}");
        }
    }
}
