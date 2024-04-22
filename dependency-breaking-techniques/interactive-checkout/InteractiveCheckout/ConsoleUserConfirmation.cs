namespace InteractiveCheckout;

public class ConsoleUserConfirmation : UserConfirmation
{
    private readonly bool _accepted;

    public ConsoleUserConfirmation(string message)
    {
        Console.WriteLine($"{message} Choose Option (Y yes) (N no):");
        var result = Console.ReadLine();
        _accepted = result != null && result.ToLower() == "y";
    }

    public bool WasAccepted()
    {
        return _accepted;
    }
}