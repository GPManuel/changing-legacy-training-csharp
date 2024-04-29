namespace Calculator.model;

public record Questionnaire
{
    public int DurationInYears { get; set; }
    public double Interest { get; set; }
    public decimal Principal { get; set; }
}