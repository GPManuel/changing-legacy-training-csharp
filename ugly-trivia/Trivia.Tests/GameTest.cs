using NUnit.Framework;
using System.Text.Json;

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

        var allAnswers = JsonSerializer.Serialize(game._answersWasWrong);
        Console.WriteLine(allAnswers);
        var allNotifications = JsonSerializer.Serialize(game._notifications);
        Console.WriteLine(allNotifications);
        var allRolss = JsonSerializer.Serialize(game._rollsDie);
        Console.WriteLine(allRolss);

        Assert.Pass();
    }
}

