using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryLab11
{
    public class DialClock : IInit
    {
        //int seed = (int)DateTime.Now.Ticks; // обновление рандомного ввода
        //Random rnd = new Random();
        int hours;
        int minutes;
        static int count = 0;
        public int Hours
        {
            get => hours;
            set
            {
                if (value < 0) //против оси
                {
                    hours = 12 + value % 12;
                }
                else
                    hours = value % 12;
            }
        }
        public int Minutes
        {
            get => minutes;
            set
            {
                if (value < 0) //против оси
                {
                    minutes = 60 + value % 60;
                    Hours += -1 + value / 60 + minutes / 60;
                    minutes %= 60;
                }
                else
                {
                    Hours += value / 60;
                    minutes = value % 60;
                }
            }
        }
        public DialClock()
        {
            Hours = 0;
            Minutes = 0;
            count++;
        }
        public DialClock(int hours, int minutes)
        {
            Hours = hours;
            Minutes = minutes;
            count++;
        }
        public DialClock(DialClock c)
        {
            Hours = c.hours;
            Minutes = c.minutes;
            count++;
        }
        public static int GetCount => count;
        public double GetAngle() //Нестатический метод
        {
            double minutesAngle = Minutes * 6.0;
            double hoursAngle = Hours * 30.0 + minutesAngle / 12;
            double angle1 = Math.Abs(hoursAngle - minutesAngle);
            double angle2 = Math.Abs(360 - angle1);
            if (angle2 < 180) // поиск минимального угла 
                angle1 = angle2;
            return Math.Round(angle1, 4);
        }
        public static double GetAngle(DialClock c) //Статический метод
        {
            double minutesAngle = c.Minutes * 6.0;
            double hoursAngle = c.Hours * 30.0 + minutesAngle / 12;
            double angle1 = Math.Abs(hoursAngle - minutesAngle);
            double angle2 = Math.Abs(360 - angle1);
            if (angle2 < 180) // поиск минимального угла 
                angle1 = angle2;
            return Math.Round(angle1, 4);
        }
        public override string ToString()
        {
            if (Hours < 10 && Minutes < 10)
                return $"'0{Hours} : 0{Minutes}'.";

            else if (Minutes < 10)
                return $"'{Hours} : 0{Minutes}'.";

            else if (Hours < 10)
                return $"'0{Hours} : {Minutes}'.";

            else
                return $"'{Hours} : {Minutes}'.";
        }
        public virtual void Init()
        {
            Hours = InputData.NumInput("сколько часов?");
            Minutes = InputData.NumInput("сколько минут?");
        }
        public virtual void RandomInit(Random rnd)
        {
            Hours = rnd.Next(11);
            Minutes = rnd.Next(59);
        }
        // 2 часть (операторы)
        public static DialClock operator ++(DialClock c)
        {
            c.Minutes++;
            return c;
        }
        public static DialClock operator --(DialClock c)
        {
            c.Minutes--;
            return c;
        }
        public static explicit operator bool(DialClock c)
        {
            return GetAngle(c) % 2.5 == 0;
        }
        public static implicit operator int(DialClock c)
        {
            return c.Hours * 12 + c.Minutes / 5;
        }
        public static DialClock operator +(DialClock dc, int minutes)
        {
            return new DialClock(dc.Hours, dc.Minutes + minutes);
        }
        public static DialClock operator +(int minutes, DialClock dc)
        {
            return new DialClock(dc.Hours, dc.Minutes + minutes);
        }
        public static DialClock operator -(DialClock dc, int minutes)
        {
            return new DialClock(dc.Hours, dc.Minutes - minutes);
        }
        public static DialClock operator -(int minutes, DialClock dc)
        {
            return new DialClock(dc.Hours, dc.Minutes - minutes);
        }
    }
}
