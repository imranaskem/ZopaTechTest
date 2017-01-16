using System;

namespace ZopaTechTest
{
    public class Output
    {
        public string Request { get; private set; }
        public string Rate { get; private set; }
        public string MonthlyRepayment { get; private set; }
        public string TotalRepayment { get; private set; }

        public Output(int request, double rate, double monthlyPayment)
        {
            this.Request = "Requested amount: £" + request.ToString();
            
            this.Rate = "Rate: " + string.Format("{0:P}", rate); 

            this.MonthlyRepayment = "Monthly Repayment: £" + monthlyPayment.ToString();

            this.TotalRepayment = "Total Repayment: £" + (monthlyPayment * 36).ToString();
        }

        public void WriteToScreen()
        {
            Console.WriteLine();
            Console.WriteLine(this.Request);
            Console.WriteLine(this.Rate);
            Console.WriteLine(this.MonthlyRepayment);
            Console.WriteLine(this.TotalRepayment);
            Console.WriteLine();
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}
