using System;
using System.Collections.Generic;
using System.Linq;

namespace ZopaTechTest
{
    public class Quote
    {
        public int TotalAvailable { get; private set; }
        public int Requested { get; private set; }
        public double MonthlyRepayable { get; private set; }
        public double TotalRepayableRate { get; private set; }
        public int PeriodMonths { get; private set; }
        public List<Lender> Lenders { get; private set; }
        public List<Lender> AllocatedMoney { get; private set; }

        public Quote(int request, List<Lender> lenderlist)
        {
            this.Requested = request;
            this.Lenders = lenderlist;
            this.AllocatedMoney = new List<Lender>();
            this.PeriodMonths = 36;

            foreach (Lender lender in lenderlist)
            {
                this.TotalAvailable += lender.AvailableFunds;
            }
        }

        public bool LoanPossible()
        {
            if (this.Requested > this.TotalAvailable) return false;
            else return true;
        }

        public void CalculateQuote()
        {
            WhoIsLending();
            RateCalc();
            MonthlyRepayableCalc();
        }

        private void WhoIsLending()
        {          
            int request = this.Requested;            

            var sortedLenders = this.Lenders
                .OrderBy(l => l.InterestRate)
                .ThenByDescending(l => l.AvailableFunds);            

            foreach (Lender lender in sortedLenders)
            {
                if (request >= lender.AvailableFunds)
                {
                    request -= lender.AvailableFunds;
                    this.AllocatedMoney.Add(new Lender(lender.Name, lender.InterestRate, lender.AvailableFunds));
                }

                if (request <= 0) break;

                if (request < lender.AvailableFunds)
                {
                    this.AllocatedMoney.Add(new Lender(lender.Name, lender.InterestRate, request));
                    request = 0;
                }
            }            
        }

        private void RateCalc()
        {
            foreach (Lender lender in this.AllocatedMoney)
            {
                double request = this.Requested;
                double available = lender.AvailableFunds;

                double weightedAverage = available / request;

                this.TotalRepayableRate += lender.InterestRate * weightedAverage;                
            }

            this.TotalRepayableRate = Math.Round(this.TotalRepayableRate, 4);
        }

        private void MonthlyRepayableCalc()
        {
            double yearlyRate = this.TotalRepayableRate + 1;

            double power = 1d / 12d;

            double monthlyRate = Math.Pow(yearlyRate, power) - 1;

            double topHalfEquation = this.Requested * monthlyRate;

            double bottomHalfEquation = 1 - Math.Pow(1 + monthlyRate, -this.PeriodMonths);

            this.MonthlyRepayable = Math.Round(topHalfEquation / bottomHalfEquation, 2);
        }
    }
}
