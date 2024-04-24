using EmptyFiles;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Text.Json;

namespace Trivia.Tests;

public class GameTest
{

    [Test]
    public void sampling_game_with_3_players()
    {
        var asnwers = IsWrongAnswersFor3PlayersGame();
        var rolls = RollsFor3PlayersGame();
        var game = new GameForTesting(asnwers, rolls);

        game.Add("Chet");
        game.Add("Pat");
        game.Add("Manuel");

        game.Run();

        var expectedNotifications = ExpectedNotificationsFor3Players();
        Assert.That(game.Notifications, Is.EqualTo(expectedNotifications));
    }

    [Test]
    public void sampling_game_with_5_players()
    {
        var asnwers = IsWrongAnswersFor5PlayersGame();
        var rolls = RollsFor5PlayersGame();
        var game = new GameForTesting(asnwers, rolls);

        game.Add("Chet");
        game.Add("Pat");
        game.Add("Sue");
        game.Add("Carlos");
        game.Add("Manuel");

        game.Run();

        var expectedNotifications = ExpectedNotificationsFor5Players();
        Assert.That(game.Notifications, Is.EqualTo(expectedNotifications));
    }

    private List<string> ExpectedNotificationsFor5Players()
    {
        return new List<string>()
        {
            "Chet was added","They are player number 1","Pat was added","They are player number 2","Sue was added","They are player number 3","Carlos was added","They are player number 4","Manuel was added","They are player number 5","Chet is the current player","They have rolled a 5","Chet's new location is 5","The category is Science","Science Question 0",
            "Answer was corrent!!!!","Chet now has 1 Gold Coins.","Pat is the current player","They have rolled a 5","Pat's new location is 5","The category is Science","Science Question 1","Answer was corrent!!!!","Pat now has 1 Gold Coins.","Sue is the current player","They have rolled a 5","Sue's new location is 5","The category is Science","Science Question 2",
            "Question was incorrectly answered","Sue was sent to the penalty box","Carlos is the current player","They have rolled a 1","Carlos's new location is 1","The category is Science","Science Question 3","Answer was corrent!!!!","Carlos now has 1 Gold Coins.","Manuel is the current player","They have rolled a 4","Manuel's new location is 4","The category is Pop",
            "Pop Question 0","Question was incorrectly answered","Manuel was sent to the penalty box","Chet is the current player","They have rolled a 2","Chet's new location is 7","The category is Rock","Rock Question 0","Answer was corrent!!!!","Chet now has 2 Gold Coins.","Pat is the current player","They have rolled a 2","Pat's new location is 7","The category is Rock",
            "Rock Question 1","Answer was corrent!!!!","Pat now has 2 Gold Coins.","Sue is the current player","They have rolled a 3","Sue is getting out of the penalty box","Sue's new location is 8","The category is Pop","Pop Question 1","Question was incorrectly answered","Sue was sent to the penalty box","Carlos is the current player","They have rolled a 1",
            "Carlos's new location is 2","The category is Sports","Sports Question 0","Answer was corrent!!!!","Carlos now has 2 Gold Coins.","Manuel is the current player","They have rolled a 3","Manuel is getting out of the penalty box","Manuel's new location is 7","The category is Rock","Rock Question 2","Answer was correct!!!!","Manuel now has 1 Gold Coins.",
            "Chet is the current player","They have rolled a 2","Chet's new location is 9","The category is Science","Science Question 4","Answer was corrent!!!!","Chet now has 3 Gold Coins.","Pat is the current player","They have rolled a 5","Pat's new location is 0","The category is Pop","Pop Question 2","Answer was corrent!!!!","Pat now has 3 Gold Coins.",
            "Sue is the current player","They have rolled a 4","Sue is not getting out of the penalty box","Carlos is the current player","They have rolled a 1","Carlos's new location is 3","The category is Rock","Rock Question 3","Answer was corrent!!!!","Carlos now has 3 Gold Coins.","Manuel is the current player","They have rolled a 5","Manuel is getting out of the penalty box",
            "Manuel's new location is 0","The category is Pop","Pop Question 3","Answer was correct!!!!","Manuel now has 2 Gold Coins.","Chet is the current player","They have rolled a 3","Chet's new location is 0","The category is Pop","Pop Question 4","Answer was corrent!!!!","Chet now has 4 Gold Coins.","Pat is the current player","They have rolled a 5",
            "Pat's new location is 5","The category is Science","Science Question 5","Answer was corrent!!!!","Pat now has 4 Gold Coins.","Sue is the current player","They have rolled a 4","Sue is not getting out of the penalty box","Carlos is the current player","They have rolled a 4","Carlos's new location is 7","The category is Rock","Rock Question 4","Answer was corrent!!!!",
            "Carlos now has 4 Gold Coins.","Manuel is the current player","They have rolled a 2","Manuel is not getting out of the penalty box","Chet is the current player","They have rolled a 3","Chet's new location is 3","The category is Rock","Rock Question 5","Answer was corrent!!!!","Chet now has 5 Gold Coins.","Pat is the current player","They have rolled a 1",
            "Pat's new location is 6","The category is Sports","Sports Question 1","Answer was corrent!!!!","Pat now has 5 Gold Coins.","Sue is the current player","They have rolled a 5","Sue is getting out of the penalty box","Sue's new location is 1","The category is Science","Science Question 6","Answer was correct!!!!","Sue now has 1 Gold Coins.",
            "Carlos is the current player","They have rolled a 4","Carlos's new location is 11","The category is Rock","Rock Question 6","Answer was corrent!!!!","Carlos now has 5 Gold Coins.","Manuel is the current player","They have rolled a 2","Manuel is not getting out of the penalty box","Chet is the current player","They have rolled a 4","Chet's new location is 7","The category is Rock",
            "Rock Question 7","Answer was corrent!!!!","Chet now has 6 Gold Coins."
        };
    }

    private List<bool> IsWrongAnswersFor5PlayersGame()
    {
        return new List<bool> { false, false, true, false, true, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
    }
    private List<int> RollsFor5PlayersGame()
    {
        return new List<int> { 5, 5, 5, 1, 4, 2, 2, 3, 1, 3, 2, 5, 4, 1, 5, 3, 5, 4, 4, 2, 3, 1, 5, 4, 2, 4 };
    }
    private static List<string> ExpectedNotificationsFor3Players()
    {
        return new List<string>(){
                "Chet was added", "They are player number 1", "Pat was added", "They are player number 2", "Manuel was added", "They are player number 3", "Chet is the current player", "They have rolled a 3", "Chet's new location is 3", "The category is Rock", "Rock Question 0", "Answer was corrent!!!!", "Chet now has 1 Gold Coins.", "Pat is the current player", "They have rolled a 1"
                , "Pat's new location is 1", "The category is Science", "Science Question 0", "Answer was corrent!!!!", "Pat now has 1 Gold Coins.", "Manuel is the current player", "They have rolled a 1", "Manuel's new location is 1", "The category is Science", "Science Question 1", "Answer was corrent!!!!", "Manuel now has 1 Gold Coins.", "Chet is the current player",
                "They have rolled a 3","Chet's new location is 6","The category is Sports","Sports Question 0","Answer was corrent!!!!","Chet now has 2 Gold Coins.","Pat is the current player","They have rolled a 4","Pat's new location is 5","The category is Science","Science Question 2","Answer was corrent!!!!","Pat now has 2 Gold Coins.","Manuel is the current player",
            "They have rolled a 1","Manuel's new location is 2","The category is Sports","Sports Question 1","Answer was corrent!!!!","Manuel now has 2 Gold Coins.","Chet is the current player","They have rolled a 5","Chet's new location is 11","The category is Rock","Rock Question 1","Question was incorrectly answered","Chet was sent to the penalty box","Pat is the current player",
            "They have rolled a 5","Pat's new location is 10","The category is Sports","Sports Question 2","Answer was corrent!!!!","Pat now has 3 Gold Coins.","Manuel is the current player","They have rolled a 2","Manuel's new location is 4","The category is Pop","Pop Question 0","Answer was corrent!!!!","Manuel now has 3 Gold Coins.","Chet is the current player",
            "They have rolled a 5","Chet is getting out of the penalty box","Chet's new location is 4","The category is Pop","Pop Question 1","Answer was correct!!!!","Chet now has 3 Gold Coins.","Pat is the current player","They have rolled a 5","Pat's new location is 3","The category is Rock","Rock Question 2","Answer was corrent!!!!","Pat now has 4 Gold Coins.",
            "Manuel is the current player","They have rolled a 2","Manuel's new location is 6","The category is Sports","Sports Question 3","Answer was corrent!!!!","Manuel now has 4 Gold Coins.","Chet is the current player","They have rolled a 5","Chet is getting out of the penalty box","Chet's new location is 9","The category is Science","Science Question 3",
            "Answer was correct!!!!","Chet now has 4 Gold Coins.","Pat is the current player","They have rolled a 5","Pat's new location is 8","The category is Pop","Pop Question 2","Question was incorrectly answered","Pat was sent to the penalty box","Manuel is the current player","They have rolled a 2","Manuel's new location is 8","The category is Pop","Pop Question 3",
            "Answer was corrent!!!!","Manuel now has 5 Gold Coins.","Chet is the current player","They have rolled a 2","Chet is not getting out of the penalty box","Question was incorrectly answered","Chet was sent to the penalty box","Pat is the current player","They have rolled a 4","Pat is not getting out of the penalty box","Question was incorrectly answered",
            "Pat was sent to the penalty box","Manuel is the current player","They have rolled a 2","Manuel's new location is 10","The category is Sports","Sports Question 4","Answer was corrent!!!!","Manuel now has 6 Gold Coins."};
    }

    private static List<int> RollsFor3PlayersGame()
    {
        return new List<int>() { 3, 1, 1, 3, 4, 1, 5, 5, 2, 5, 5, 2, 5, 5, 2, 2, 4, 2 };
    }

    private List<bool> IsWrongAnswersFor3PlayersGame()
    {
        return new List<bool>()
        {
            false, false, false, false, false, false, true, false, false, false, false, false, false, true, false, true,
            true, false
        };
    }

    private class GameForTesting : Game
    {
        private readonly List<bool> _isWrongAnswerList;
        private readonly List<int> _rollDieList;
        public List<string> Notifications = new();
        private int _rollsNumber;
        private int _answersNumber;

        public GameForTesting(List<bool> isWrongAnswerList, List<int> rollDieList)
        {
            _isWrongAnswerList = isWrongAnswerList;
            _rollDieList = rollDieList;
            _rollsNumber = 0;
            _answersNumber = 0;
        }

        protected override bool IsWrongAnswer(Random rand)
        {
            var isWrongAnswer = _isWrongAnswerList[_answersNumber];
            _answersNumber++;
            return isWrongAnswer;
        }

        protected override int RollDie(Random rand)
        {
            var roll = _rollDieList[_rollsNumber];
            _rollsNumber++;
            return roll;
        }

        protected override void Notify(string message)
        {
            Notifications.Add(message);
        }
    }
}

