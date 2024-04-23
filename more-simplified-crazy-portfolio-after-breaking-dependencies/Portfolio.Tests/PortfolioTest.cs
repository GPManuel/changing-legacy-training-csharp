using NUnit.Framework;
using static Portfolio.Tests.helpers.AssetsFileLinesBuilder;
using static Portfolio.Tests.helpers.TestingPortfolioBuilder;

namespace Portfolio.Tests;

public class PortfolioTest
{
    [Test]
    public void regular_asset_value_decreases_by_2_before_now()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Some Regular Asset").FromDate("2024/01/15").WithValue(100))
            .OnDate("2025/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo("98"));
    }

    [Test]
    public void french_wine_value_increases_by_2_before_now()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("French Wine").FromDate("2024/01/15").WithValue(100))
            .OnDate("2025/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo("102"));
    }

    [Test]
    public void lotery_prediction_value_drops_to_0_before_now()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Lottery Prediction").FromDate("2024/01/15").WithValue(100))
            .OnDate("2025/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo("0"));
    }

    [Test]
    public void unicorn_value_is_priceless_before_now()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Unicorn").FromDate("2024/01/15").WithValue(100))
            .OnDate("2025/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo("Portfolio is priceless because it got a unicorn!!!!!"));
    }

}
