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

    [Test]
    public void regular_asset_value_decrease_by_1_after_now()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Some Regular Asset").FromDate("2025/06/15").WithValue(55))
            .OnDate("2025/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo("54"));
    }

    [Test]
    public void french_wine_value_increases_by_1_after_now()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("French Wine").FromDate("2025/06/15").WithValue(7))
            .OnDate("2025/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo("8"));
    }

    [Test]
    public void lottery_prediction_increases_by_1_after_now_more_than_or_equal_11_days()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Lottery Prediction").FromDate("2025/06/15").WithValue(33))
            .OnDate("2025/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo("34"));
    }

    [Test]
    public void lottery_prediction_increases_by_2_after_now_less_than_11_and_more_than_or_equals_6_days()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Lottery Prediction").FromDate("2025/01/10").WithValue(77))
            .OnDate("2025/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo("79"));
    }

    [Test]
    public void lottery_prediction_increases_by_3_after_now_less_than_6_days()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Lottery Prediction").FromDate("2025/01/05").WithValue(11))
            .OnDate("2025/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo("14"));
    }


    [Test]
    public void unicorn_value_is_priceless_after_now()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Unicorn").FromDate("2025/06/15").WithValue(47))
            .OnDate("2025/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo("Portfolio is priceless because it got a unicorn!!!!!"));
    }
    /*
     * Boundary conditions:
     *  - Value: (-inf,200) [200,inf) French Wine
     *  - Value: (-inf,800) [800,inf) Lottery Prediction
     *  - Value: (-inf,0] (0,inf) Regular
     *  - Date (days): (-inf, 0) [0, inf) Regular and French Wine   
     *  - Date (days): (-inf, 0) [0, 6) [6, 11) [11, inf)  Lottery Prediction
     */
}
