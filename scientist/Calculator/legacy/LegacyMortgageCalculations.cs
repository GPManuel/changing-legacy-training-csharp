using Calculator.model;

namespace Calculator.legacy;

public class LegacyMortgageCalculator : IMortgageCalculator
{
    public List<MortgagePayment> Calculate(Questionnaire answer)
    {
        Thread.Sleep(500); 
        return Calculations.MortgageCalculations(answer);
    }
}