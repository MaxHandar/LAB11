using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLab11;

namespace LectionTemplates
{
    internal class List<T> where T:IInit, new()
    {
        T[] list;
        int capacity;
        int count;

        public int Capacity
        {
            get => list.Length;
            private set => capacity = value;
        }

        public int Count { get => count; }

        public List()
        {
            Capacity = 0;
            list = new T[Capacity];
        }
        public List(params T[] list)
        {
            Capacity = list.Length;
            count = 0;
            this.list = new T[list.Length];
            for (int i = 0; i < list.Length; i++)
            {
                this.list[i] = list[i];
                count++;
            }
        }

        public List(int length)
        {
            Random rnd = new Random();
            Capacity = length;
            list = new T[length];
            count = length;
            for (int i = 0; i < length; i++)
            {
                T item = new T();
                item.RandomInit(rnd);
                list[i] = item;
            }
        }

        public T this[int index]
        {
            get
            {
                return list[index];
            }

            set
            {
                list[index] = value;
            }
        }

        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException();
            if (Count < Capacity)
            {
                list[count++] = item;
            }
            else
            {
                T[] temp = new T[Capacity * 2];
                for (int i = 0; i < list.Length; i++)
                {
                    temp[i] = list[i];

                }
                temp[count++] = item;

                list = temp;
            }
        }
    }
}
