namespace ProblematicDiscount;

public class Discount
{
    private readonly Campaign _campaign;

    public Discount(Campaign campaign)
    {
        _campaign = campaign;
    }

    public Money DiscountFor(Money netPrice)
    {
        if (_campaign.IsCrazySalesDay()) return netPrice.ReduceBy(15);
        if (netPrice.MoreThan(Money.OneThousand)) return netPrice.ReduceBy(10);
        if (netPrice.MoreThan(Money.OneHundred) && _campaign.IsActive()) return netPrice.ReduceBy(5);
        return netPrice;
    }
}