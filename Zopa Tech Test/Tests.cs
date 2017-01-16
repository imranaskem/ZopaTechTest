using System.Collections.Generic;
using NUnit.Framework;

namespace ZopaTechTest
{
    [TestFixture]
    class Tests
    {             

        [Test]
        public static void TestPossibleTrue()
        {           
            Quote q = new Quote(1000, new List<Lender> { new Lender("Imran", 0.25, 1500) });
            Assert.That(q.LoanPossible(), Is.True);
            
        }

        [Test]
        public static void TestPossibleFalse()
        {
            Quote q = new Quote(2000, new List<Lender> { new Lender("Imran", 0.25, 1500) });
            Assert.That(q.LoanPossible(), Is.False);

        }               

        [Test]
        public static void TestAvailable()
        {
            Quote q = new Quote(1000, new List<Lender> {
                new Lender("Imran", 0.25, 1500),
                new Lender("Sandy", 0.5, 500) });            
            Assert.That(2000, Is.EqualTo(q.TotalAvailable));
        }

        [Test]
        public static void WhoLendingTest()
        {
            Lender Jane = new Lender("Jane", 0.069, 480);
            Lender Fred = new Lender("Fred", 0.071, 520);

        Quote q = new Quote(1000, new List<Lender> {
                new Lender("Bob", 0.075, 640),
                Jane,
                Fred,
                new Lender("Mary", 0.104, 170),
                new Lender("John", 0.081, 320),
                new Lender("Dave", 0.074, 140),
                new Lender("Angela", 0.071, 60) });            

            q.CalculateQuote();

            Assert.That(q.AllocatedMoney[0].Name, Is.EqualTo("Jane"));
            Assert.That(q.AllocatedMoney[0].InterestRate, Is.EqualTo(0.069));
            Assert.That(q.AllocatedMoney[0].AvailableFunds, Is.EqualTo(480));

            Assert.That(q.AllocatedMoney[1].Name, Is.EqualTo("Fred"));
            Assert.That(q.AllocatedMoney[1].InterestRate, Is.EqualTo(0.071));
            Assert.That(q.AllocatedMoney[1].AvailableFunds, Is.EqualTo(520));
        }

        [Test]
        public static void ComparingLendersTest()
        {
            Lender Jane = new Lender("Jane", 0.069, 480);
            Lender Fred = new Lender("Fred", 0.071, 520);

            Quote q = new Quote(1000, new List<Lender> {
                new Lender("Bob", 0.075, 640),
                Jane,
                Fred,
                new Lender("Mary", 0.104, 170),
                new Lender("John", 0.081, 320),
                new Lender("Dave", 0.074, 140),
                new Lender("Angela", 0.071, 60) });            

            q.CalculateQuote();

            Assert.That(q.AllocatedMoney[0], Is.EqualTo(Jane).Using(new LenderComparer()));
            Assert.That(q.AllocatedMoney[1], Is.EqualTo(Fred).Using(new LenderComparer()));
        }

        [Test]
        public static void SortLendersTest()
        {
            Lender Jane = new Lender("Jane", 0.069, 480);
            Lender Fred = new Lender("Fred", 0.071, 520);
            Lender Bob = new Lender("Bob", 0.075, 640);
            Lender Mary = new Lender("Mary", 0.104, 170);
            Lender John = new Lender("John", 0.081, 320);
            Lender Dave = new Lender("Dave", 0.074, 140);
            Lender Angela = new Lender("Angela", 0.071, 60);

            Quote q = new Quote(1000, new List<Lender>
            {
                Mary,
                Bob,
                Jane,
                Fred,                
                John,
                Dave,
                Angela });

            List<Lender> sortedList = new List<Lender>
            {
                Jane,
                Angela,
                Fred,
                Dave,
                Bob,
                John,
                Mary
            };

            q.Lenders.Sort();

            Assert.That(q.Lenders, Is.EqualTo(sortedList).Using(new LenderComparer()));            
        }

        [Test]
        public static void RateCalcTest()
        {
            double expected = 0.0700;

            Quote q = new Quote(1000, new List<Lender> {
                new Lender("Bob", 0.075, 640),
                new Lender("Jane", 0.069, 480),
                new Lender("Fred", 0.071, 520),
                new Lender("Mary", 0.104, 170),
                new Lender("John", 0.081, 320),
                new Lender("Dave", 0.074, 140),
                new Lender("Angela", 0.071, 60) });

            q.CalculateQuote();

            Assert.That(expected, Is.EqualTo(q.TotalRepayableRate));
        }

        [Test]
        public static void MonthlyRepaymentCalcTest()
        {
            double expected = 30.78;

            Quote q = new Quote(1000, new List<Lender> {
                new Lender("Bob", 0.075, 640),
                new Lender("Jane", 0.069, 480),
                new Lender("Fred", 0.071, 520),
                new Lender("Mary", 0.104, 170),
                new Lender("John", 0.081, 320),
                new Lender("Dave", 0.074, 140),
                new Lender("Angela", 0.071, 60) });

            q.CalculateQuote();

            Assert.That(expected, Is.EqualTo(q.MonthlyRepayable));
        }

        [Test]
        public static void OutputTest()
        {
            Output o = new Output(1000, 0.0700, 30.78);

            Assert.That("Requested amount: £1000", Is.EqualTo(o.Request));
            Assert.That("Rate: 7.00%", Is.EqualTo(o.Rate));
            Assert.That("Monthly Repayment: £30.78", Is.EqualTo(o.MonthlyRepayment));
            Assert.That("Total Repayment: £1108.08", Is.EqualTo(o.TotalRepayment));
        }
    }
}
