namespace Calculator.model;

public interface IMortgageCalculator
{
    List<MortgagePayment> Calculate(Questionnaire answer);
}

