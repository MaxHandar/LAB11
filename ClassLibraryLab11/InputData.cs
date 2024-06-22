using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryLab11
{
    public static class InputData
    {
        public static int NumInput(string msg = "", bool notNegative = true)
        {
            bool isConvert;
            int N;
            do
            {
                Console.WriteLine(msg);
                string buf = Console.ReadLine();
                isConvert = int.TryParse(buf, out N);

                if (!isConvert)
                {
                    string msgNotConvert = "Неправильно введено число, попробуй еще раз";
                    Console.WriteLine(msgNotConvert);
                }
            } while (!isConvert);
            return N;
        }
        public static string StringInput(string msg = "")
        {
            Console.WriteLine(msg);
            return Console.ReadLine();
        }
        public static bool BoolInput(string question = "")
        {
            do
            {
                Console.WriteLine(question);
                Console.WriteLine("1. Да");
                Console.WriteLine("2. Нет");
                int answer = NumInput();

                switch (answer)
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
    }
}
