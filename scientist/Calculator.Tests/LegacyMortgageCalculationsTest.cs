using Calculator.legacy;
using Calculator.model;
using NUnit.Framework;

namespace Calculator.Tests;

public class LegacyMortgageCalculatorTest
{
    //duration: How long should the mortgage be? (years 5/10/15/30)
    //interest: What is the interestrate? (percentage 1 / 4.1)
    //principal: What is the amount of mortgage? (150000 / 30000 / only numbers)
    [Test]
    public void Test_Some_Things_Of_MorgagePayments()
    {
        Questionnaire questionnaire = new Questionnaire()
        {
            DurationInYears = 1, Interest = 4, Principal = 150000
        };

        var legacyCalculation = new LegacyMortgageCalculator();
        List<MortgagePayment> results = legacyCalculation.Calculate(questionnaire);

        Assert.That(results.Count, Is.EqualTo(12));
        Assert.That(results[0], Is.EqualTo(new MortgagePayment
        {
           Month = 1, StartingBalance = 150000, Interest = 500.00m, 
           MonthlyPayment = 12772.49m, Principal = 12272.49m, EndingBalance = 137727.51m
        }));
        Assert.That(results[11], Is.EqualTo(new MortgagePayment
        {
            Month = 12, StartingBalance = 12730.05m, Interest = 42.43m, 
            MonthlyPayment = 12772.49m, Principal = 12730.05m, EndingBalance = 0.00m
        }));
    }
}