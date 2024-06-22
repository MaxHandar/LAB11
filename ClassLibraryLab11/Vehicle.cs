using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryLab11
{
    public class IDNumber
    {
        int number;

        public int Number
        {
            get => number;
            set
            {
                if (value < 0)
                    number = 0;
                else
                    number = value;
            }
        }
        public IDNumber(int number)
        {
            this.Number = number;
        }
        public override string ToString()
        {
            return Number.ToString();
        }
        public override bool Equals(object obj)
        {
            if (obj is IDNumber n)
            {
                return this.Number == n.Number;
            }
            else
            {
                return false;
            }
        }
    }
    public class Vehicle : IInit, IComparable<Vehicle>, IEquatable<Vehicle>, ICloneable
    {
        protected string brand;
        protected int year;
        protected string color;
        protected double price;
        protected double groundClearance;

        string[] Brands = { "Ауди", "Пежо", "Лада" };
        string[] Colors = { "black", "white", "green", "yellow" };

        public IDNumber id;
        public string Brand
        {
            get => brand;
            set => brand = value;
        }
        public int Year
        {
            get => year;
            set
            {
                if (value < 1800)
                    year = 1800;
                else
                    year = value;
            }
        }
        public string Color
        {
            get => color;
            set => color = value;
        }
        public double Price
        {
            get => price;
            set
            {
                if (value < 0)
                    price = 0;
                else
                    price = value;
            }
        }
        public double GroundClearance
        {
            get => groundClearance;
            set
            {
                if (value < 0)
                    groundClearance = 0;
                else
                    groundClearance = value;
            }
        }
        public Vehicle()
        {
            Brand = "Lada";
            Year = 2000;
            Color = "Cherry";
            Price = 500;
            GroundClearance = 170;
            id = new IDNumber(1);
        }
        public Vehicle(string brand, int year, string color, double price, double groundClearance, int id)
        {
            Brand = brand;
            Year = year;
            Color = color;
            Price = price;
            GroundClearance = groundClearance;
            this.id = new IDNumber(id);
        }
        public override string ToString()
        {
            return $"бренд={Brand}, год={Year}, цвет={Color}, цена={Price}, просвет={GroundClearance}, id={id.Number}";
        }
        public void Show()
        {
            Console.WriteLine($"Vehicle Бренд - {Brand}");
            Console.WriteLine($"Vehicle Год - {Year}");
            Console.WriteLine($"Vehicle Цвет - {Color}");
            Console.WriteLine($"Vehicle Стоимость - {Price}");
            Console.WriteLine($"Vehicle Дорожный просвет - {GroundClearance}");
            Console.WriteLine($"Vehicle ID - {id}");
        }
        public virtual void ShowVirt()
        {
            Console.WriteLine($"Vehicle Бренд - {Brand}");
            Console.WriteLine($"Vehicle Год - {Year}");
            Console.WriteLine($"Vehicle Цвет - {Color}");
            Console.WriteLine($"Vehicle Стоимость - {Price}");
            Console.WriteLine($"Vehicle Дорожный просвет - {GroundClearance}");
            Console.WriteLine($"Vehicle ID - {id}");
        }
        public void Init()
        {
            Brand = InputData.StringInput("Введите бренд");
            Year = InputData.NumInput("Введите год");
            Color = InputData.StringInput("Введите цвет");
            Price = InputData.NumInput("Введите цену");
            GroundClearance = InputData.NumInput("Введите дорожный просвет");
            id.Number = InputData.NumInput("Введите ID");
        }
        public void RandomInit(Random rnd)
        {
            Brand = Brands[rnd.Next(Brands.Length)];
            Year = rnd.Next(1950, 2024);
            Color = Colors[rnd.Next(Colors.Length)];
            Price = rnd.Next(1000);
            GroundClearance = rnd.Next(1000);
            id.Number = rnd.Next(1, 1000);
        }
        //public bool Equals(Vehicle car1, Vehicle car2)
        //{
        //    return car1.Price == car2.Price &&
        //        car1.GroundClearance == car2.GroundClearance &&
        //        car1.Color == car2.Color &&
        //        car1.Year == car2.Year &&
        //        car1.Brand == car2.Brand;
        //}
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Vehicle vehicle)
                return this.Price == vehicle.Price &&
                    this.GroundClearance == vehicle.GroundClearance &&
                    this.Color == vehicle.Color &&
                    this.Year == vehicle.Year &&
                    this.Brand == vehicle.Brand &&
                    this.id.Number == vehicle.id.Number;
            else
                return false;
        }

        //public int CompareTo(object obj)
        //{
        //    if (obj != null && !(obj is Vehicle)) return -1;
        //    Vehicle v = obj as Vehicle;
        //    return String.Compare(this.Brand, v.Brand);
        //}

        public object Clone() //глубокое копирование
        {
            return new Vehicle(Brand, Year, Color, Price, GroundClearance, id.Number);
        }
        public override int GetHashCode()
        {
            //return base.GetHashCode();
            int hash = 100;
            hash = hash * 20 + Brand.GetHashCode();
            hash = hash * 20 + Year.GetHashCode();
            return hash;
        }
        public object ShallowCopy() //поверхностное копирование
        {
            return this.MemberwiseClone();
        }

        public virtual int CompareTo(Vehicle other)
        {
            return String.Compare(this.Brand, other.Brand);
        }
        public virtual bool Equals(Vehicle other)
        {
            return this.Price == other.Price &&
                    this.GroundClearance == other.GroundClearance &&
                    this.Color == other.Color &&
                    this.Year == other.Year &&
                    this.Brand == other.Brand &&
                    this.id.Number == other.id.Number;
        }

    }
}
