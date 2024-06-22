using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLab10;

namespace List
{
    public class TestCollection
    {
        LinkedList<PassengerCar> collection1 = new LinkedList<PassengerCar>();
        LinkedList<string> collection2 = new LinkedList<string>();
        Dictionary<Vehicle, PassengerCar> collection3 = new Dictionary<Vehicle, PassengerCar>();
        Dictionary<string, PassengerCar> collection4 = new Dictionary<string, PassengerCar>();

        public LinkedList<PassengerCar> Collection1
        {
            get { return collection1; }
            set { collection1 = value; }
        }
        public LinkedList<string> Collection2
        {
            get { return collection2; }
            set { collection2 = value; }
        }
        public Dictionary<Vehicle, PassengerCar> Collection3
        {
            get { return collection3; }
            set { collection3 = value; }
        }
        public Dictionary<string, PassengerCar> Collection4
        {
            get { return collection4; }
            set { collection4 = value; }
        }
        public PassengerCar First { get; set; }
        public PassengerCar Middle { get; set; }
        public PassengerCar Last { get; set; }
        public PassengerCar NotIn { get; set; }
        //public PassengerCar? first, middle, last, notIn;

        Random rnd = new Random();
        int count = 1000;
        public TestCollection()
        {
            for (int i = 0; i < count; i++)
            {
                try
                {
                    PassengerCar pCar = new PassengerCar();
                    pCar.RandomInit(rnd);
                    Collection1.AddLast(pCar);
                    Collection2.AddLast(pCar.ToString());
                    Vehicle key = pCar.BaseVehicle();
                    Collection3.Add(key, pCar);
                    Collection4.Add(key.ToString(), pCar);
                    if (i == 0)
                    {
                        First = (PassengerCar)pCar.Clone();
                    }
                    else if (i == count / 2)
                    {
                        Middle = (PassengerCar)pCar.Clone();
                    }
                    else if (i == count - 1)
                    {
                        Last = (PassengerCar)pCar.Clone();
                    }
                }
                catch (Exception e)
                {
                    i--;
                }
            }
            NotIn = new PassengerCar("s", 9000, "d", 1, 1, 1, 1, 1);
        }
    }
}
