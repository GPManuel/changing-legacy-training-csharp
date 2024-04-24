
using NUnit.Framework;

namespace Trivia.Tests;

public class GameTest
{

    [Test]
    public void sampling_game()
    {
        Game game = new();

        game.Add("Chet");
        game.Add("Pat");
        game.Add("Sue");
        game.Add("Carlos");
        game.Add("Manuel");

        game.Run();

        Assert.Pass();
    }
}

