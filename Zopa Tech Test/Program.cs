using System;
using System.Collections.Generic;

namespace ZopaTechTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int loanRequest;

            if (!int.TryParse(args[1], out loanRequest))
            {
                Console.WriteLine();
                Console.WriteLine("Loan request must only contain numbers, program exiting...");
                Environment.Exit(0);
            }                
            
            Reader reader = new Reader(args[0]);

            List<Lender> lenderList = reader.CreateLenders();

            Quote quote = new Quote(loanRequest, lenderList);

            if (quote.LoanPossible() == false)
            {
                Console.WriteLine();
                Console.WriteLine("It is not possible to provide a quote at this time");
                Console.WriteLine();
                Console.WriteLine("Press any key to exit");
                Console.ReadLine();
                Environment.Exit(0);
            }

            quote.CalculateQuote();

            Output output = new Output(quote.Requested, quote.TotalRepayableRate, quote.MonthlyRepayable);

            output.WriteToScreen();

            Environment.Exit(0);


        }
    }
}
