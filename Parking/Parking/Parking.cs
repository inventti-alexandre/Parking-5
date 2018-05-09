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
        public void AddCar(int ident, decimal balance, CarType type)
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

        public void DisplayTransactionHistoryPerMinute()
        {
            foreach (var el in transactions)
                Console.WriteLine(el);
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
                Console.WriteLine($"Текст из файла: {textFromFile}");
            }
        }
    }
}
