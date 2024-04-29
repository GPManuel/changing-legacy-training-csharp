using Calculator.model;

namespace Calculator.experiment;

public class NewMortgageCalculator : IMortgageCalculator
{
    public List<MortgagePayment> Calculate(Questionnaire answer)
    {
        List<MortgagePayment> result = new();

        var durationInMonths = CalculateTotalMonths(answer.DurationInYears);
        var principalMonthly = answer.Principal;
        for (int i = 0; i < durationInMonths; i++)
        {
            decimal monthlyInterest = MonthlyInterest(principalMonthly, answer);
            decimal monthlyPrincipalPayment = MonthlyPrincipalPayment(principalMonthly, answer);
            decimal startingBalance = principalMonthly;
            principalMonthly += PrincipalMonthly(principalMonthly, answer);
            result.Add(new MortgagePayment
            {
                Month = i + 1,
                StartingBalance = Round(startingBalance),
                Interest = Round(monthlyInterest),
                MonthlyPayment = Round(MonthlyPayment(answer)),
                Principal = Round(monthlyPrincipalPayment),
                EndingBalance = Round(principalMonthly)
            });
        }

        return result;
    }

    private static decimal PrincipalMonthly(decimal principalMonthly, Questionnaire answer)
    {
        var monthlyInterest = MonthlyInterest(principalMonthly, answer);
        var monthlyPayment = MonthlyPayment(answer);
        return monthlyInterest - monthlyPayment;
    }

    private static decimal MonthlyPrincipalPayment(decimal principalMonthly, Questionnaire answer)
    {
        var monthlyInterest = MonthlyInterest(principalMonthly, answer);
        var monthlyPayment = MonthlyPayment(answer);
        return monthlyPayment - monthlyInterest;
    }

    private static decimal MonthlyPayment(Questionnaire answer)
    {
        var duration = CalculateTotalMonths(answer.DurationInYears);
        double monthlyInterestRate = MonthlyInterestRate(answer.Interest);
        decimal mortgageMagic = (decimal)(1 - Math.Pow(1 + monthlyInterestRate, -duration));

        return answer.Principal * ((decimal)monthlyInterestRate) / mortgageMagic;
    }

    private static decimal Round(decimal value)
    {
        return Math.Round(value, 2);
    }

    private static decimal MonthlyInterest(decimal principalMonthly, Questionnaire answer)
    {
        return principalMonthly * (decimal)MonthlyInterestRate(answer.Interest);
    }


    private static double MonthlyInterestRate(double interest)
    {
        return interest / 100 / 12;
    }

    private static int CalculateTotalMonths(int years)
    {
        return years * 12;
    }
}