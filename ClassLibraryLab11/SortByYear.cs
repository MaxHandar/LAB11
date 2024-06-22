using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryLab11
{
    public class SortByYear : IComparer
    {
        public int Compare(object x, object y)
        {
            Vehicle v1 = x as Vehicle;
            Vehicle v2 = y as Vehicle;
            if (v1.Year < v2.Year) return -1;
            else if (v2.Year == v1.Year) return 0;
            else return 1;
        }
    }
}
