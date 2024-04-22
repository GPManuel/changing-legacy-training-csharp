namespace InteractiveCheckout;

public class Checkout
{
    private readonly IEmailService _emailService;

    private readonly UserConfirmation _newsLetterSubscribed;
    private readonly Product _product;

    private readonly UserConfirmation _termsAndConditionsAccepted;

    public Checkout(Product product, IEmailService emailService)
    {
        _product = product;
        _emailService = emailService;
        _newsLetterSubscribed = GetUserConfirmation("Subscribe to our product " + product + " newsletter?");
        _termsAndConditionsAccepted = GetUserConfirmation(
            "Accept our terms and conditions?\n" + //
            "(Mandatory to place order for " + product + ")");
    }

    protected virtual UserConfirmation GetUserConfirmation(string subscriptionMessage)
    {
        return new ConsoleUserConfirmation(subscriptionMessage);
    }

    public virtual void ConfirmOrder()
    {
        if (!_termsAndConditionsAccepted.WasAccepted()) throw new OrderCancelledException(_product);
        if (_newsLetterSubscribed.WasAccepted()) _emailService.SubscribeUserFor(_product);
    }
}