using Calculator.model;
using GitHub;

namespace Calculator.scientist;

public class ScientistResultPublisher : IResultPublisher
{
    public Task Publish<T, TClean>(Result<T, TClean> result)
    {
        var report = $"Results for experiment '{result.ExperimentName}' on {DateTime.Now}\n";
        report += $"Result: {(result.Matched ? "MATCH" : "MISMATCH")}\n";
        report += $"Control value: {ConvertToMortgageData(result)}\n";
        report +=$"Control duration: {result.Control.Duration}\n";
        foreach (var observation in result.Candidates)
        {
            report +=$"Candidate value: {ConvertToMortgageData(result)}\n";
            report +=$"Candidate duration: {observation.Duration}\n";
        }

        report += "== End experiment ==\n\n";
        return File.AppendAllTextAsync("scientist-result.txt", report);
    }

    private string ConvertToMortgageData<T, TClean>(Result<T, TClean> result)
    {
        var data = result.Control.Value as List<MortgagePayment>;
        return string.Join("\n", data);
    }
}