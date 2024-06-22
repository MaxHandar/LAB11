using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryLab11
{
    public class PassengerCar : Vehicle
    {
        protected int numOfSeats;
        protected int maxSpeed;

        public int NumOfSeats
        {
            get => numOfSeats;
            set
            {
                if (value < 1)
                    numOfSeats = 1;
                else
                    numOfSeats = value;
            }
        }
        public int MaxSpeed
        {
            get => maxSpeed;
            set
            {
                if (value < 0)
                    maxSpeed = 0;
                else
                    maxSpeed = value;
            }
        }
        public PassengerCar() : base()
        {
            NumOfSeats = 5;
            MaxSpeed = 100;
        }
        public PassengerCar(string brand,
                            int year,
                            string color,
                            double price,
                            double groundClearance,
                            int id,
                            int numOfSeats,
                            int maxSpeed) : base(brand,
                                                year,
                                                color,
                                                price,
                                                groundClearance,
                                                id)
        {
            NumOfSeats = numOfSeats;
            MaxSpeed = maxSpeed;
        }
        public new void Show()
        {
            base.Show();
            Console.WriteLine($"PassengerCar Кол-во сидений = {NumOfSeats}");
            Console.WriteLine($"PassengerCar Макс. скорость = {MaxSpeed}");
        }
        public override void ShowVirt()
        {
            Console.WriteLine($"PassengerCar Бренд - {Brand}");
            Console.WriteLine($"PassengerCar Год - {Year}");
            Console.WriteLine($"PassengerCar Цвет - {Color}");
            Console.WriteLine($"PassengerCar Стоимость - {Price}");
            Console.WriteLine($"PassengerCar Дорожный просвет - {GroundClearance}");
            Console.WriteLine($"PassengerCar Кол-во сидений = {NumOfSeats}");
            Console.WriteLine($"PassengerCar Макс. скорость = {MaxSpeed}");
            Console.WriteLine($"PassengerCar ID = {id}");
        }
        public new void Init()
        {
            base.Init();
            NumOfSeats = InputData.NumInput("Количество сидений?");
            MaxSpeed = InputData.NumInput("Макс. скорость?");
        }
        public override string ToString()
        {
            return base.ToString() + $", сидений={NumOfSeats}, макс.скорость={MaxSpeed}";
        }
        public new void RandomInit(Random rnd)
        {
            base.RandomInit(rnd);
            NumOfSeats = rnd.Next(1, 5);
            MaxSpeed = rnd.Next(300);
        }
        //public bool Equals(PassengerCar car1, PassengerCar car2)
        //{
        //    return base.Equals(car1, car2) && 
        //        car1.NumOfSeats == car2.NumOfSeats &&
        //        car1.MaxSpeed == car2.MaxSpeed;
        //}
        public override bool Equals(object obj)
        {
            PassengerCar PCar = obj as PassengerCar;
            if (PCar != null)
                return this.Price == PCar.Price &&
                    this.GroundClearance == PCar.GroundClearance &&
                    this.Color == PCar.Color &&
                    this.Year == PCar.Year &&
                    this.Brand == PCar.Brand &&
                    this.NumOfSeats == PCar.NumOfSeats &&
                    this.MaxSpeed == PCar.MaxSpeed &&
                    this.id.Number == PCar.id.Number;
            else
                return false;
        }
    }
}
