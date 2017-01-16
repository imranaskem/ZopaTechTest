using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZopaTechTest
{
    public class LenderComparer : IComparer<Lender>
    {
        public int Compare(Lender x, Lender y)
        {
            return x.CompareTo(y);
        }
    }
}
