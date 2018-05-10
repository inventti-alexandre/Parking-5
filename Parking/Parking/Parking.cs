using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parking
{
    class Parking
    {
        private static readonly Lazy<Parking> lazy = new Lazy<Parking>(() => new Parking());
        private static List<Car> cars = new List<Car>(50);
        private static List<Transaction> transactions = new List<Transaction>();
        public static decimal Balance { get; private set; }
        private static System.Timers.Timer aTimer;

        private Parking()
        {
            Balance = 0;
            Parking.SetTimer();
        }

        public static Parking GetParking() => lazy.Value;

        public decimal DisplayTotalRevenue() => Balance;

        private static void SetTimer()
        {
            // Create a timer with a given interval.
            aTimer = new System.Timers.Timer(Settings.Timeout);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            //Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss}", e.SignalTime);
            foreach (var car in cars)
            {
                CollectPayment(car);
            }
        }
        public static void CollectPayment(Car car)
        {

            Settings.prices.TryGetValue(car.CarType, out var price);
            if (car.Balance < price)
            {
                price = price * Settings.CoefficientFine;
            }
            car.Balance -= price;
            Balance += price;
            transactions.Add(new Transaction(DateTime.Now, car.Id, price));
        }

        public void AddCar(int ident, decimal balance, CarType type) => cars.Add(new Car(ident, balance, type));

        public bool HasFine(int number) => cars[number - 1].Balance < 0;

        public void RemoveCar(int number, out decimal fine)
        {
            fine = cars[number - 1].Balance;
            if (HasFine(number))
            {
                TopUp(number, Math.Abs(cars[number - 1].Balance));
                CollectPayment(cars[number - 1]);
            }
            cars.Remove(cars[number - 1]);
        }
        public decimal TopUp(int value, decimal money) => cars[value - 1].Balance += money;

        public int DisplayNumberOfFreePlaces() => cars == null ? Settings.ParkingSpace : Settings.ParkingSpace - cars.Count;

        public int DisplayNumberOfBusyPlaces() => cars?.Count ?? 0;

        public decimal AmountPerMinute() => transactions.Sum(n => n.Amount);

        public List<Transaction> DisplayTransactionForTheLastMinute() => transactions;

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
