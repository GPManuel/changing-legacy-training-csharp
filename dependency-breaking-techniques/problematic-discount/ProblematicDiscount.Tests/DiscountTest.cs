using NSubstitute;
using NUnit.Framework;

namespace ProblematicDiscount.Tests;

public class DiscountTest
{
    [Test]
    public void Discount_On_Crazy_Sales_Day()
    {
        var marketingCampaign = Substitute.For<Campaign>();
        var discount = new Discount(marketingCampaign);
        marketingCampaign.IsCrazySalesDay().Returns(true);

        var total = discount.DiscountFor(new Money(100.0m));

        Assert.That(total, Is.EqualTo(new Money(85.0m)));
    }
}