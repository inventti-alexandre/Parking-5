using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    class Parking
    {
        private static readonly Lazy<Parking> lazy = new Lazy<Parking>(() => new Parking());
        private List<Car> cars = new List<Car>(50);
        private List<Transaction> transactions = new List<Transaction>();
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
        public void AddCar(int ident, decimal balance, CarType type)
        {
            cars.Add(new Car(ident, balance, type));
        }
        public void RemoveCar(int number)
        {
            if (cars[number - 1].Fine > 0)
            {
                TopUp(number, cars[number - 1].Fine);
                CollectPayment(cars[number - 1]);
            }
            cars.Remove(cars[number - 1]);
        }
        public void TopUp(int value, decimal money)
        {
            cars[value - 1].Balance += money;
            Console.WriteLine($"The balance is topped up. Now balance:{cars[value - 1].Balance}");
        }
        public void DisplayNumberOfFreePlaces()
        {
            if (cars == null) Console.WriteLine($"Free spaces: {Settings.ParkingSpace}");
            else Console.WriteLine($"Free spaces: {Settings.ParkingSpace - cars.Count}");
        }
        public int DisplayNumberOfBusyPlaces()
        {
            if (cars == null) return 0;
            else return cars.Count;
        }
        public void AmountPerMinute()
        {
            Console.WriteLine(transactions.Sum(n => n.Amount));
        }
        public void DisplayTransactionHistoryPerMinute()
        {
            transactions.ForEach(n => Console.WriteLine(n));
            //foreach (var el in transactions) Console.WriteLine(el);
        }
        public void WriteToTransactionsFile()
        {
            using (FileStream fstream = new FileStream(@"C:\Users\Eugene\Documents\GitHub\Parking\Parking\Transactions.log", FileMode.OpenOrCreate))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes("" + DateTime.Now + transactions.Count); // преобразуем строку в байты
                fstream.Write(array, 0, array.Length); // запись массива байтов в файл
            }
        }
        public void DisplayTransactionsFile()
        {
            using (FileStream fstream = File.OpenRead(@"C:\Users\Eugene\Documents\GitHub\Parking\Parking\Transactions.log"))
            {
                byte[] array = new byte[fstream.Length]; // преобразуем строку в байты                
                fstream.Read(array, 0, array.Length); // считываем данные
                string textFromFile = System.Text.Encoding.Default.GetString(array); // декодируем байты в строку
                Console.WriteLine($"Text from file: {textFromFile}");
            }
        }
    }
}
