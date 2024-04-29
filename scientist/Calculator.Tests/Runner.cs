using Calculator.app;
using Calculator.scientist;
using GitHub;
using NUnit.Framework;

namespace Calculator.Tests;

public class Runner
{
    [OneTimeSetUp]
    public void SetUp()
    {
        Scientist.ResultPublisher = new FireAndForgetResultPublisher(new ScientistResultPublisher());
    }

    [Test]
    [TestCase(1, 4, 3000)]
    [TestCase(1, 3, 100000)]
    [TestCase(2, 2, 60000)]
    [TestCase(3, 2, 150000)]
    [TestCase(5, 3, 150000)]
    public void Execute(int durationInYears, double interest, decimal principal)
    {
        var service = new Service();
        var result = service.Execute(
            durationInYears, interest, principal
        );
        Console.WriteLine("== Your Mortgage Calculations ==");
        result.ToList().ForEach(x =>
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Month: " + x.Month);
            Console.WriteLine("Principal Monthly: " + x.Principal);
            Console.WriteLine("Starting Balance: " + x.StartingBalance);
            Console.WriteLine("Ending Balance " + x.EndingBalance);
            Console.WriteLine("Monthly Payment: " + x.MonthlyPayment);
            Console.WriteLine("Monthly Interes: " + x.Interest);
        });
    }
}