using ClassLibraryLab10;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Linq;
using List;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LectionTemplates
{
    internal class Program
    {
        public static int ChooseType(string question = "")
        {
            do
            {
                Console.WriteLine(question);
                Console.WriteLine("1. Vehicle");
                Console.WriteLine("2. PassengerCar");
                Console.WriteLine("3. Off-Road");
                Console.WriteLine("4. Truck");
                int answer = InputData.NumInput();

                if (answer >= 1 || answer <= 4)
                {
                    return answer;
                }
                else
                {
                    Console.WriteLine("Неправильно задан пункт меню");
                }
            } while (true);
        }
        public static bool ChooseSearch(string question = "Ввести элемент для поиска?", string otherwise = "Взять для поиска 1-й элемент коллекции")
        {
            do
            {
                Console.WriteLine(question);
                Console.WriteLine("1. Да");
                Console.WriteLine($"2. {otherwise}");
                int answer = InputData.NumInput();

                switch(answer)
                {
                    case 1:
                        {
                            return true;
                        }
                    case 2:
                        {
                            return false;
                        }
                    default:
                        {
                            Console.WriteLine("Неправильно задан пункт меню");
                            break;
                        }
                }
            } while (true);
        }
        public static Vehicle VehicleInput(int chosenType)
        {
            switch (chosenType)
            {
                case 1:
                    {
                        Vehicle vehicle = new Vehicle();
                        vehicle.Init();
                        return vehicle;
                    }
                case 2:
                    {
                        PassengerCar vehicle = new PassengerCar();
                        vehicle.Init();
                        return vehicle;
                    }
                case 3:
                    {
                        OffRoad vehicle = new OffRoad();
                        vehicle.Init();
                        return vehicle;
                    }
                case 4:
                    {
                        Truck vehicle = new Truck();
                        vehicle.Init();
                        return vehicle;
                    }
            }
            return null;
        }

        //самый дорогой внедорожник
        static OffRoad MostExpensiveOffRoad(Vehicle[] vehicles)
        {
            double maxPrice = 0;
            Vehicle mostExpensiveOffRoad = null;
            foreach (Vehicle vehicle in vehicles)
            {
                if (vehicle is OffRoad && vehicle.Price > maxPrice)
                {
                    maxPrice = vehicle.Price;
                    mostExpensiveOffRoad = vehicle;
                }
            }
            return mostExpensiveOffRoad as OffRoad;
        }

        //средняя скорость легкового автомобиля
        static double AverageSpeedOfPassengerCar(Vehicle[] vehicles)
        {
            int speedSum = 0;
            int numOfCars = 0;
            foreach (Vehicle vehicle in vehicles)
            {
                if (vehicle is PassengerCar PCar)
                {
                    speedSum += PCar.MaxSpeed;
                    numOfCars++;
                }
            }
            return (double)speedSum / numOfCars;
        }

        //грузовики превышающие заданную грузоподъемность
        static void TrucksExceeedingTonnageLimit(Vehicle[] vehicles, int givenTonnage)
        {
            foreach (Vehicle vehicle in vehicles)
            {
                if (vehicle is Truck truck && truck.Tonnage > givenTonnage)
                {
                    truck.Show();
                    Console.WriteLine("------------------------------------------------------");
                }
            }
        }
        static PassengerCar GetExactElemIn(TestCollection test, string elem)
        {
            switch (elem)
            {
                case "First":
                    return test.First;
                case "Middle":
                    return test.Middle;
                case "Last":
                    return test.Last;
                default:
                    throw new Exception("Нет такого элемента");
            }
        }

        static bool GetTestCollection(Vehicle testElem, int collectionNum, TestCollection test, out long elapsedTicks, bool containsKey)
        {

            Stopwatch sw = Stopwatch.StartNew();
            bool isContains;
            sw.Restart();
            sw.Stop();
            string testElemStr = testElem.ToString();
            switch (collectionNum)
            {
                case 1:
                    sw.Restart();
                    isContains = test.Collection1.Contains(testElem);
                    sw.Stop();
                    elapsedTicks = sw.ElapsedTicks;
                    return isContains;
                case 2:
                    sw.Restart();
                    isContains = test.Collection2.Contains(testElemStr);
                    sw.Stop();
                    elapsedTicks = sw.ElapsedTicks;
                    return isContains;
                case 3:
                    if (containsKey)
                    {
                        testElem = ((PassengerCar)testElem).BaseVehicle();
                        sw.Restart();
                        isContains = test.Collection3.ContainsKey(testElem);
                        sw.Stop();
                        elapsedTicks = sw.ElapsedTicks;
                        return isContains;
                    }
                    else
                    {
                        sw.Restart();
                        isContains = test.Collection3.ContainsValue((PassengerCar)testElem);
                        sw.Stop();
                        elapsedTicks = sw.ElapsedTicks;
                        return isContains;
                    }
                case 4:
                    if (containsKey)
                    {
                        testElemStr = ((PassengerCar)testElem).BaseVehicle().ToString();
                        sw.Restart();
                        isContains = test.Collection4.ContainsKey(testElemStr);
                        sw.Stop();
                        elapsedTicks = sw.ElapsedTicks;
                        return isContains;
                    }
                    else
                    {
                        sw.Restart();
                        isContains = test.Collection4.ContainsValue((PassengerCar)testElem);
                        sw.Stop();
                        elapsedTicks = sw.ElapsedTicks;
                        return isContains;
                    }
                default:
                    throw new Exception("Нет такого номера коллекции");
            }
        }
        static void TestElemIn(int numOfTests, string elem, int collectionNum, bool containsKey = false)
        {
            string isContainingMessage = "(-)";
            bool isContains = true;
            long ticksSum = 0;
            //TestCollection test1 = new TestCollection();
            //Vehicle testElem1 = GetExactElemIn(test1, elem);
            //GetTestCollection(testElem1, collectionNum, test1, out long elapsedTicks1, containsKey);
            for (int i = 0; i < numOfTests; i++)
            {
                TestCollection test = new TestCollection();
                Vehicle testElem = GetExactElemIn(test, elem);
                long elapsedTicks = 0;
                isContains = isContains && GetTestCollection(testElem, collectionNum, test, out elapsedTicks, containsKey);
                ticksSum += elapsedTicks;
            }
            if (isContains)
                isContainingMessage = "(+)";
            
            if (containsKey)
                Console.Write(ticksSum / (float)numOfTests + isContainingMessage + "\t");
            else
                Console.Write(ticksSum / numOfTests + isContainingMessage + "\t");
        }
        static void TestElemNotIn(int numOfTests, int collectionNum, bool containsKey = false)
        {
            string isContainingMessage = "(-)";
            bool isContains = false;
            long ticksSum = 0;
            for (int i = 0; i < numOfTests; i++)
            {
                TestCollection test = new TestCollection();
                Vehicle testElem = test.NotIn;
                long elapsedTicks = 0;
                isContains = isContains | GetTestCollection(testElem, collectionNum, test, out elapsedTicks, containsKey);
                ticksSum += elapsedTicks;
            }
            if (isContains)
                isContainingMessage = "(+)";

            if (containsKey)
                Console.WriteLine(ticksSum / (float)numOfTests + isContainingMessage + "\t");
            else
                Console.WriteLine(ticksSum / numOfTests + isContainingMessage + "\t");
        }
        static void Main(string[] args)
        {
            Random rnd = new Random();
            #region ArrayList
            Console.WriteLine("ArrayList");
            // ArrayList

            ArrayList al = new ArrayList();
            for (int i=0; i<5; i++)
            {
                Vehicle v1 = new Vehicle();
                v1.RandomInit(rnd);
                al.Add(v1);
            }
            for(int i=0; i<5; i++)
            {
                PassengerCar pc = new PassengerCar();
                pc.RandomInit(rnd);
                al.Add(pc);
            }

            //клонирование
            ArrayList alClone = new ArrayList();
            for (int i = 0; i < 10; i++)
            {
                if (al[i] is Vehicle vehicle)
                {
                    if (al[i] is PassengerCar pCar)
                    {
                        alClone.Add(pCar.Clone() as PassengerCar);
                        Console.WriteLine(alClone[i]);
                    }
                    else if (al[i] is OffRoad offRoad)
                    {
                        alClone.Add(offRoad.Clone() as OffRoad);
                        Console.WriteLine(alClone[i]);
                    }
                    else if (al[i] is Truck truck)
                    {
                        alClone.Add(truck.Clone() as Truck);
                        Console.WriteLine(alClone[i]);
                    }
                    else
                    {
                        alClone.Add(vehicle.Clone() as Vehicle);
                        Console.WriteLine(alClone[i]);
                    }
                }
            }
            Console.WriteLine(alClone.Count);

            //вывод клона
            foreach (var vehicle in alClone)
            {
                Console.WriteLine(vehicle.ToString());
            }
            Console.WriteLine();
            Console.WriteLine("вывод отсортированного:");
            //вывод отсортированного
            al.Sort(new SortByYear());
            foreach(object vehicle in al)
            {
                Console.WriteLine(vehicle.ToString());
            }
            Console.WriteLine($"Count = {al.Count}");
            Console.WriteLine($"Capacity = {al.Capacity}");

            //поиск и удаление
            Vehicle v = new Vehicle();
            if (ChooseSearch())
            {
                Console.WriteLine("Введите элемент для поиска");
                v.Init();
            }
            else
            {
                v = al[0] as Vehicle;
            }

            if (al.Contains(v))
            {
                Console.WriteLine("Найден");
            }
            else
            {
                Console.WriteLine("Не найден");
            }
            //удаление
            int pos = al.IndexOf(v);
            if (pos >= 0)
            {
                Console.WriteLine($"Удаляем {al[pos]} на позиции {pos+1}");
                al.RemoveAt(pos);
            }
            //поиск
            if (al.Contains(v))
            {
                Console.WriteLine("Найден");
            }
            else
            {
                Console.WriteLine("Не найден");
            }

            #endregion ArrayList
            Console.WriteLine("--------------------------------------------------------------------------------");

            #region Stack
            Console.WriteLine("Stack");
            // Stack
            Stack<Vehicle> stack = new Stack<Vehicle>();
            for (int i = 0; i < 5; i++)
            {
                Vehicle v1 = new Vehicle();
                v1.RandomInit(rnd);
                stack.Push(v1);
            }
            for (int i = 0; i < 5; i++)
            {
                PassengerCar pc = new PassengerCar();
                pc.RandomInit(rnd);
                stack.Push(pc);
            }
            
            foreach (Vehicle vehicle in stack)
            {
                Console.WriteLine(vehicle.ToString());
            }
            //клонирование
            Stack<Vehicle> stackClone1 = new Stack<Vehicle>();
            Stack<Vehicle> stackClone2 = new Stack<Vehicle>();
            while (stack.Count > 0)
            {
                stackClone1.Push(stack.Peek().Clone() as Vehicle);
                stackClone2.Push(stack.Pop().Clone() as Vehicle);
            }
            stack = stackClone2;
            //while (stack.Count > 0)
            //{
            //    if (stack.Peek() is Vehicle)
            //    {
            //        if (stack.Peek() is PassengerCar pCar)
            //        {
            //            stackClone1.Push(stack.Peek().Clone() as PassengerCar);
            //            stackClone2.Push(stack.Pop().Clone() as PassengerCar);
            //        }
            //        else if (stack.Peek() is OffRoad offRoad)
            //        {
            //            stackClone1.Push(stack.Peek().Clone() as OffRoad);
            //            stackClone2.Push(stack.Pop().Clone() as OffRoad);
            //        }
            //        else if (stack.Peek() is Truck)
            //        {
            //            stackClone1.Push(stack.Peek().Clone() as Truck);
            //            stackClone2.Push(stack.Pop().Clone() as Truck);
            //        }
            //        else
            //        {
            //            stackClone1.Push(stack.Peek().Clone() as Vehicle);
            //            stackClone2.Push(stack.Pop().Clone() as Vehicle);
            //        }
            //    }
            //}
            //stack = stackClone2;
            Vehicle v2 = new Vehicle();
            if (ChooseSearch())
            {
                Console.WriteLine("Введите элемент для поиска");
                v2.Init();
            }
            else
            {
                v2 = stack.Peek() as Vehicle;
            }
            
            if (stackClone1.Contains(v2))
            {
                Console.WriteLine("Найден");
            }
            else
            {
                Console.WriteLine("Не найден");
            }
            
            Stack<Vehicle> stackTemp = new Stack<Vehicle>();
            while(stack.Count > 0)
            {
                Vehicle vehicle = stack.Pop();
                if (!v2.Equals(vehicle as Vehicle))
                {
                    stackTemp.Push(vehicle);
                }
                else
                {
                    Console.WriteLine($"Удаляем {vehicle}");
                }
                //if (stack.Peek() is Vehicle)
                //{
                //    if (stack.Peek() is PassengerCar)
                //    {
                //        PassengerCar pCar = stack.Pop() as PassengerCar;
                //        if (!v2.Equals(pCar as Vehicle))
                //        {
                //            stackTemp.Push(pCar);
                //        }
                //        else
                //        {
                //            Console.WriteLine($"Удаляем {pCar}");
                //        }
                //    }
                //    else if (stack.Peek() is OffRoad)
                //    {
                //        OffRoad offRoad = stack.Pop() as OffRoad;
                //        if (!v2.Equals(offRoad as Vehicle))
                //        {
                //            stackTemp.Push(offRoad);
                //        }
                //        else
                //        {
                //            Console.WriteLine($"Удаляем {offRoad}");
                //        }
                //    }
                //    else if (stack.Peek() is Truck)
                //    {
                //        Truck pCar = stack.Pop() as Truck;
                //        if (!v2.Equals(pCar as Vehicle))
                //        {
                //            stackTemp.Push(pCar);
                //        }
                //        else
                //        {
                //            Console.WriteLine($"Удаляем {pCar}");
                //        }
                //    }
                //    else
                //    {
                //        Vehicle veh = stack.Pop() as Vehicle;
                //        if (!v2.Equals(veh as Vehicle))
                //        {
                //            stackTemp.Push(veh);
                //        }
                //        else
                //        {
                //            Console.WriteLine($"Удаляем {veh}");
                //        }
                //    }
                //}
            }
            stack = new Stack<Vehicle>(stackTemp);
            //после удаления
            foreach (var vehicle in stackTemp)
            {
                Console.WriteLine(vehicle.ToString());
            }

            //вывод клона
            //foreach (Vehicle vehicle in stackClone1)
            //{
            //    Console.WriteLine(vehicle.ToString());
            //}

            Console.WriteLine($"CountDeleted = {stackTemp.Count}");
            Console.WriteLine($"CountClone = {stackClone1.Count}");

            //сортировка
            Vehicle[] sortedList = stack.ToArray();
            Array.Sort(sortedList);

            Stack<Vehicle> sortedStack = new Stack<Vehicle>();
            foreach (var vehicle in sortedList)
            {
                sortedStack.Push(vehicle);
            }

            // Вывод отсортированного стека
            Console.WriteLine("Вывод отсортированного стека");
            foreach (Vehicle vehicle in sortedStack)
            {
                Console.WriteLine(vehicle);
            }

            #endregion Stack
            Console.WriteLine("--------------------------------------------------------------------------------");

            #region Dictionary
            Console.WriteLine("3 заданиие");
            int numOfTests = 100;
            Console.WriteLine($"{numOfTests} теста(-ов) по циклу:");
            Console.WriteLine("\t\t\t\tfirst\tmiddle\tlast\tnot existed");
            
            Console.Write("LinkedList<Pcar>:            \t");
            TestElemIn(numOfTests, "First", 1);
            TestElemIn(numOfTests, "Middle", 1);
            TestElemIn(numOfTests, "Last", 1);
            TestElemNotIn(numOfTests, 1);

            Console.Write("LinkedList<string>:          \t");
            TestElemIn(numOfTests, "First", 2);
            TestElemIn(numOfTests, "Middle", 2);
            TestElemIn(numOfTests, "Last", 2);
            TestElemNotIn(numOfTests, 2);

            Console.Write("Dictionary<Vehicle, PCar>key:\t");
            TestElemIn(numOfTests, "First", 3, true);
            TestElemIn(numOfTests, "Middle", 3, true);
            TestElemIn(numOfTests, "Last", 3, true);
            TestElemNotIn(numOfTests, 3, true);
            Console.Write("Dictionary<Vehicle, PCar>value:\t");
            TestElemIn(numOfTests, "First", 3, false);
            TestElemIn(numOfTests, "Middle", 3, false);
            TestElemIn(numOfTests, "Last", 3, false);
            TestElemNotIn(numOfTests, 3, false);

            Console.Write("Dictionary<string, PCar>key:\t");
            TestElemIn(numOfTests, "First", 4, true);
            TestElemIn(numOfTests, "Middle", 4, true);
            TestElemIn(numOfTests, "Last", 4, true);
            TestElemNotIn(numOfTests, 4, true);
            Console.Write("Dictionary<string, PCar>value:\t");
            TestElemIn(numOfTests, "First", 4, false);
            TestElemIn(numOfTests, "Middle", 4, false);
            TestElemIn(numOfTests, "Last", 4, false);
            TestElemNotIn(numOfTests, 4, false);

            Console.WriteLine("--------------------------------------------------------------------------------");
            ///*
            Console.WriteLine("3 теста без цикла:");
            TestCollection test1 = new TestCollection();
            TestCollection test2 = new TestCollection();
            TestCollection test3 = new TestCollection();
            Stopwatch sw = Stopwatch.StartNew();
            long ticksSum = 0;
            bool is1stContains = false;
            bool is2ndContains = false;
            bool is3rdContains = false;
            string isContainingMessage = "(-)";
            //collection1
            sw.Restart();
            is1stContains = test1.Collection1.Contains(test1.First);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is2ndContains = test2.Collection1.Contains(test2.First);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is3rdContains = test3.Collection1.Contains(test3.First);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            if (is1stContains && is2ndContains && is3rdContains)
                isContainingMessage = "(+)";
            else
                isContainingMessage = "(-)";
            Console.Write("LinkedList<Pcar>:         \t" + ticksSum / 3 + isContainingMessage + "\t");
            ticksSum = 0;

            sw.Restart();
            is1stContains = test1.Collection1.Contains(test1.Middle);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is2ndContains = test2.Collection1.Contains(test2.Middle);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is3rdContains = test3.Collection1.Contains(test3.Middle);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            if (is1stContains && is2ndContains && is3rdContains)
                isContainingMessage = "(+)";
            else
                isContainingMessage = "(-)";
            Console.Write(ticksSum / 3 + isContainingMessage + "\t");
            ticksSum = 0;

            sw.Restart();
            is1stContains = test1.Collection1.Contains(test1.Last);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is2ndContains = test2.Collection1.Contains(test2.Last);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is3rdContains = test3.Collection1.Contains(test3.Last);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            if (is1stContains && is2ndContains && is3rdContains)
                isContainingMessage = "(+)";
            else
                isContainingMessage = "(-)";
            Console.Write(ticksSum / 3 + isContainingMessage + "\t");
            ticksSum = 0;

            sw.Restart();
            is1stContains = test1.Collection1.Contains(test1.NotIn);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is2ndContains = test2.Collection1.Contains(test2.NotIn);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is3rdContains = test2.Collection1.Contains(test3.NotIn);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            if (is1stContains || is2ndContains || is3rdContains)
                isContainingMessage = "(+)";
            else
                isContainingMessage = "(-)";
            Console.Write(ticksSum / 3 + isContainingMessage + "\t");
            ticksSum = 0;
            Console.WriteLine();

            //collection2
            string test1st = test1.First.ToString();
            string test2nd = test2.First.ToString();
            string test3rd = test3.First.ToString();
            sw.Restart();
            is1stContains = test1.Collection2.Contains(test1st);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is2ndContains = test2.Collection2.Contains(test2nd);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is3rdContains = test3.Collection2.Contains(test3rd);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            if (is1stContains && is2ndContains && is3rdContains)
                isContainingMessage = "(+)";
            else
                isContainingMessage = "(-)";
            Console.Write("LinkedList<string>:       \t" + ticksSum / 3 + isContainingMessage + "\t");
            ticksSum = 0;

            test1st = test1.Middle.ToString();
            test2nd = test2.Middle.ToString();
            test3rd = test3.Middle.ToString();
            sw.Restart();
            is1stContains = test1.Collection2.Contains(test1st);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is2ndContains = test2.Collection2.Contains(test2nd);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is3rdContains = test3.Collection2.Contains(test3rd);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            if (is1stContains && is2ndContains && is3rdContains)
                isContainingMessage = "(+)";
            else
                isContainingMessage = "(-)";
            Console.Write(ticksSum / 3 + isContainingMessage + "\t");
            ticksSum = 0;

            test1st = test1.Last.ToString();
            test2nd = test2.Last.ToString();
            test3rd = test3.Last.ToString();
            sw.Restart();
            is1stContains = test1.Collection2.Contains(test1st);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is2ndContains = test2.Collection2.Contains(test2nd);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is3rdContains = test3.Collection2.Contains(test3rd);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            if (is1stContains && is2ndContains && is3rdContains)
                isContainingMessage = "(+)";
            else
                isContainingMessage = "(-)";
            Console.Write(ticksSum / 3 + isContainingMessage + "\t");
            ticksSum = 0;

            test1st = test1.NotIn.ToString();
            test2nd = test2.NotIn.ToString();
            test3rd = test3.NotIn.ToString();
            sw.Restart();
            is1stContains = test1.Collection2.Contains(test1st);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is2ndContains = test2.Collection2.Contains(test2nd);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is3rdContains = test3.Collection2.Contains(test3rd);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            if (is1stContains || is2ndContains || is3rdContains)
                isContainingMessage = "(+)";
            else
                isContainingMessage = "(-)";
            Console.Write(ticksSum / 3 + isContainingMessage + "\t");
            ticksSum = 0;
            Console.WriteLine();

            //collection3 key
            Vehicle Test1st = test1.First.BaseVehicle();
            Vehicle Test2nd = test2.First.BaseVehicle();
            Vehicle Test3rd = test3.First.BaseVehicle();
            sw.Restart();
            is1stContains = test1.Collection3.ContainsKey(Test1st);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is2ndContains = test2.Collection3.ContainsKey(Test2nd);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is3rdContains = test3.Collection3.ContainsKey(Test3rd);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            if (is1stContains && is2ndContains && is3rdContains)
                isContainingMessage = "(+)";
            else
                isContainingMessage = "(-)";
            Console.Write("Dictionary<Vehicle, Pcar>:key\t" + ticksSum / 3 + isContainingMessage + "\t");
            ticksSum = 0;

            Test1st = test1.Middle.BaseVehicle();
            Test2nd = test2.Middle.BaseVehicle();
            Test3rd = test3.Middle.BaseVehicle();
            sw.Restart();
            is1stContains = test1.Collection3.ContainsKey(Test1st);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is2ndContains = test2.Collection3.ContainsKey(Test2nd);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is3rdContains = test3.Collection3.ContainsKey(Test3rd);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            if (is1stContains && is2ndContains && is3rdContains)
                isContainingMessage = "(+)";
            else
                isContainingMessage = "(-)";
            Console.Write(ticksSum / 3 + isContainingMessage + "\t");
            ticksSum = 0;

            Test1st = test1.Last.BaseVehicle();
            Test2nd = test2.Last.BaseVehicle();
            Test3rd = test3.Last.BaseVehicle();
            sw.Restart();
            is1stContains = test1.Collection3.ContainsKey(Test1st);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is2ndContains = test2.Collection3.ContainsKey(Test2nd);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is3rdContains = test3.Collection3.ContainsKey(Test3rd);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            if (is1stContains && is2ndContains && is3rdContains)
                isContainingMessage = "(+)";
            else
                isContainingMessage = "(-)";
            Console.Write(ticksSum / 3 + isContainingMessage + "\t");
            ticksSum = 0;

            Test1st = test1.NotIn.BaseVehicle();
            Test2nd = test2.NotIn.BaseVehicle();
            Test3rd = test3.NotIn.BaseVehicle();
            sw.Restart();
            is1stContains = test1.Collection3.ContainsKey(Test1st);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is2ndContains = test2.Collection3.ContainsKey(Test2nd);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is3rdContains = test3.Collection3.ContainsKey(Test3rd);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            if (is1stContains || is2ndContains || is3rdContains)
                isContainingMessage = "(+)";
            else
                isContainingMessage = "(-)";
            Console.Write(ticksSum / 3 + isContainingMessage + "\t");
            ticksSum = 0;
            Console.WriteLine();

            //collection3 value
            sw.Restart();
            is1stContains = test1.Collection3.ContainsValue(test1.First);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is2ndContains = test2.Collection3.ContainsValue(test2.First);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is3rdContains = test3.Collection3.ContainsValue(test3.First);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            if (is1stContains && is2ndContains && is3rdContains)
                isContainingMessage = "(+)";
            else
                isContainingMessage = "(-)";
            Console.Write("Dictionary<Vehicle, Pcar>:value\t" + ticksSum / 3 + isContainingMessage + "\t");
            ticksSum = 0;

            sw.Restart();
            is1stContains = test1.Collection3.ContainsValue(test1.Middle);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is2ndContains = test2.Collection3.ContainsValue(test2.Middle);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is3rdContains = test3.Collection3.ContainsValue(test3.Middle);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            if (is1stContains && is2ndContains && is3rdContains)
                isContainingMessage = "(+)";
            else
                isContainingMessage = "(-)";
            Console.Write(ticksSum / 3 + isContainingMessage + "\t");
            ticksSum = 0;

            sw.Restart();
            is1stContains = test1.Collection3.ContainsValue(test1.Last);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is2ndContains = test2.Collection3.ContainsValue(test2.Last);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is3rdContains = test3.Collection3.ContainsValue(test3.Last);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            if (is1stContains && is2ndContains && is3rdContains)
                isContainingMessage = "(+)";
            else
                isContainingMessage = "(-)";
            Console.Write(ticksSum / 3 + isContainingMessage + "\t");
            ticksSum = 0;

            sw.Restart();
            is1stContains = test1.Collection3.ContainsValue(test1.NotIn);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is2ndContains = test2.Collection3.ContainsValue(test2.NotIn);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is3rdContains = test3.Collection3.ContainsValue(test3.NotIn);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            if (is1stContains || is2ndContains || is3rdContains)
                isContainingMessage = "(+)";
            else
                isContainingMessage = "(-)";
            Console.Write(ticksSum / 3 + isContainingMessage + "\t");
            ticksSum = 0;
            Console.WriteLine();

            //Console.WriteLine(test1.Collection3.FirstOrDefault().Key);
            //Console.WriteLine(test1.First.BaseVehicle());
            //Console.WriteLine(((Vehicle)test1.Collection3.FirstOrDefault().Key).GetHashCode());
            //Console.WriteLine(((Vehicle)test1.First.BaseVehicle()).GetHashCode());
            //Console.WriteLine(test1.Collection3.FirstOrDefault().Key.ToString().GetHashCode());
            //Console.WriteLine(test1.First.BaseVehicle().ToString().GetHashCode());
            //Console.WriteLine(test1.Collection3.FirstOrDefault().Key);
            //Console.WriteLine(test1.First.BaseVehicle());
            //Console.WriteLine(test1.First.BaseVehicle().Equals(test1.Collection3.FirstOrDefault().Key));

            //collection4 key
            test1st = test1.First.BaseVehicle().ToString();
            test2nd = test2.First.BaseVehicle().ToString();
            test3rd = test3.First.BaseVehicle().ToString();
            sw.Restart();
            is1stContains = test1.Collection4.ContainsKey(test1st);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is2ndContains = test2.Collection4.ContainsKey(test2nd);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is3rdContains = test3.Collection4.ContainsKey(test3rd);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            if (is1stContains && is2ndContains && is3rdContains)
                isContainingMessage = "(+)";
            else
                isContainingMessage = "(-)";
            Console.Write("Dictionary<string, Pcar>:key\t" + ticksSum / 3 + isContainingMessage + "\t");
            ticksSum = 0;

            test1st = test1.Middle.BaseVehicle().ToString();
            test2nd = test2.Middle.BaseVehicle().ToString();
            test3rd = test3.Middle.BaseVehicle().ToString();
            sw.Restart();
            is1stContains = test1.Collection4.ContainsKey(test1st);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is2ndContains = test2.Collection4.ContainsKey(test2nd);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is3rdContains = test3.Collection4.ContainsKey(test3rd);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            if (is1stContains && is2ndContains && is3rdContains)
                isContainingMessage = "(+)";
            else
                isContainingMessage = "(-)";
            Console.Write(ticksSum / 3 + isContainingMessage + "\t");
            ticksSum = 0;

            test1st = test1.Last.BaseVehicle().ToString();
            test2nd = test2.Last.BaseVehicle().ToString();
            test3rd = test3.Last.BaseVehicle().ToString();
            sw.Restart();
            is1stContains = test1.Collection4.ContainsKey(test1st);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is2ndContains = test2.Collection4.ContainsKey(test2nd);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is3rdContains = test3.Collection4.ContainsKey(test3rd);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            if (is1stContains && is2ndContains && is3rdContains)
                isContainingMessage = "(+)";
            else
                isContainingMessage = "(-)";
            Console.Write(ticksSum / 3 + isContainingMessage + "\t");
            ticksSum = 0;

            test1st = test1.NotIn.BaseVehicle().ToString();
            test2nd = test2.NotIn.BaseVehicle().ToString();
            test3rd = test3.NotIn.BaseVehicle().ToString();
            sw.Restart();
            is1stContains = test1.Collection4.ContainsKey(test1st);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is2ndContains = test2.Collection4.ContainsKey(test2nd);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is3rdContains = test3.Collection4.ContainsKey(test3rd);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            if (is1stContains || is2ndContains || is3rdContains)
                isContainingMessage = "(+)";
            else
                isContainingMessage = "(-)";
            Console.Write(ticksSum / 3 + isContainingMessage + "\t");
            ticksSum = 0;
            Console.WriteLine();

            //collection4 value
            sw.Restart();
            is1stContains = test1.Collection4.ContainsValue(test1.First);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is2ndContains = test2.Collection4.ContainsValue(test2.First);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is3rdContains = test3.Collection4.ContainsValue(test3.First);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            if (is1stContains && is2ndContains && is3rdContains)
                isContainingMessage = "(+)";
            else
                isContainingMessage = "(-)";
            Console.Write("Dictionary<string, Pcar>:value\t" + ticksSum / 3 + isContainingMessage + "\t");
            ticksSum = 0;

            sw.Restart();
            is1stContains = test1.Collection4.ContainsValue(test1.Middle);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is2ndContains = test2.Collection4.ContainsValue(test2.Middle);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is3rdContains = test3.Collection4.ContainsValue(test3.Middle);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            if (is1stContains && is2ndContains && is3rdContains)
                isContainingMessage = "(+)";
            else
                isContainingMessage = "(-)";
            Console.Write(ticksSum / 3 + isContainingMessage + "\t");
            ticksSum = 0;

            sw.Restart();
            is1stContains = test1.Collection4.ContainsValue(test1.Last);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is2ndContains = test2.Collection4.ContainsValue(test2.Last);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is3rdContains = test3.Collection4.ContainsValue(test3.Last);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            if (is1stContains && is2ndContains && is3rdContains)
                isContainingMessage = "(+)";
            else
                isContainingMessage = "(-)";
            Console.Write(ticksSum / 3 + isContainingMessage + "\t");
            ticksSum = 0;

            sw.Restart();
            is1stContains = test1.Collection4.ContainsValue(test1.NotIn);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is2ndContains = test2.Collection4.ContainsValue(test2.NotIn);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            sw.Restart();
            is3rdContains = test3.Collection4.ContainsValue(test3.NotIn);
            sw.Stop();
            ticksSum += sw.ElapsedTicks;
            if (is1stContains || is2ndContains || is3rdContains)
                isContainingMessage = "(+)";
            else
                isContainingMessage = "(-)";
            Console.Write(ticksSum / 3 + isContainingMessage + "\t");
            ticksSum = 0;
            Console.WriteLine();
            //*/

            #endregion Dictionary
        }
    }
}