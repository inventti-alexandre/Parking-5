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
        public decimal DisplayTotalRevenue()
        {
            return Balance;
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
                transactions.Add(new Transaction(DateTime.Now, car.Id, price));
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
        public decimal TopUp(int value, decimal money)
        {
            cars[value - 1].Balance += money;
            return cars[value - 1].Balance;
        }
        public int DisplayNumberOfFreePlaces()
        {
            return cars == null ? Settings.ParkingSpace : Settings.ParkingSpace - cars.Count;
        }
        public int DisplayNumberOfBusyPlaces()
        {
            return cars?.Count?? 0;
            //return cars == null ? 0 : cars.Count;
        }
        public decimal AmountPerMinute()
        {
            return transactions.Sum(n => n.Amount);
        }
        public List<Transaction> DisplayTransactionForTheLastMinute()
        {
            return transactions;
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
