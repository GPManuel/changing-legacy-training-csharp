using NSubstitute;

namespace InteractiveCheckout.Test;

public class CheckoutTest
{
    [Test]
    public void user_accept_newsletter_and_reject_terms()
    {
        // note for tester:
        // Accept Newsletter
        // Do not Accept Terms

        var emailService = Substitute.For<IEmailService>();
        var polkaDotSocks = new Product("Polka-dot Socks");
        var userConfirmations = new List<UserConfirmation>();
        var newsletterAccepted = Substitute.For<UserConfirmation>();
        newsletterAccepted.WasAccepted().Returns(true);
        userConfirmations.Add(newsletterAccepted);
        var termsRejected = Substitute.For<UserConfirmation>();
        termsRejected.WasAccepted().Returns(false);
        userConfirmations.Add(termsRejected);

        var checkout = new CheckoutForTesting(polkaDotSocks, emailService, userConfirmations);

        Assert.Throws<OrderCancelledException>(() => checkout.ConfirmOrder());
    }

    private class CheckoutForTesting : Checkout
    {
        private readonly List<UserConfirmation> _userConfirmations;
        private int _numCalls;

        public CheckoutForTesting(Product product, IEmailService emailService, List<UserConfirmation> userConfirmations) : base(product, emailService)
        {
            _userConfirmations = userConfirmations;
            _numCalls = 0;
        }

        protected override UserConfirmation GetUserConfirmation(string subscriptionMessage)
        {
            var userConfirmation = _userConfirmations[_numCalls];
            _numCalls++;
            return userConfirmation;
        }
    }
}