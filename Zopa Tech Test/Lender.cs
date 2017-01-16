using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZopaTechTest
{
    public class Lender : IComparable<Lender>
    {
        public string Name { get; private set; }
        public int AvailableFunds { get; private set; }
        public double AmountToBeRepaid { get; set; }
        public double InterestRate { get; private set; }

        public Lender(string name, double rate, int available)
        {
            this.Name = name;            
            this.InterestRate = rate;
            this.AvailableFunds = available;
        }

        public int CompareTo(Lender obj)
        {
            if (this.InterestRate == obj.InterestRate) return this.AvailableFunds.CompareTo(obj.AvailableFunds);

            return this.InterestRate.CompareTo(obj.InterestRate);
        }
    }
}
