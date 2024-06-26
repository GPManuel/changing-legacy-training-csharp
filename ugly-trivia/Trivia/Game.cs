namespace Trivia
{
    public class Game
    {
        public List<bool> _answersWasWrong = new();
        public List<int> _rollsDie = new();
        public List<string> _notifications = new();

        private readonly List<string> _players = new();
        private readonly int[] _places = new int[6];
        private readonly int[] _purses = new int[6];
        private readonly bool[] _inPenaltyBox = new bool[6];
        private int _currentPlayer;
        private bool _isGettingOutOfPenaltyBox;

        private readonly LinkedList<string> _popQuestions = new();
        private readonly LinkedList<string> _scienceQuestions = new();
        private readonly LinkedList<string> _sportsQuestions = new();
        private readonly LinkedList<string> _rockQuestions = new();

        public Game()
        {
            for (var i = 0; i < 50; i++)
            {
                _popQuestions.AddLast("Pop Question " + i);
                _scienceQuestions.AddLast(("Science Question " + i));
                _sportsQuestions.AddLast(("Sports Question " + i));
                _rockQuestions.AddLast(CreateRockQuestion(i));
            }
        }

        public void Run()
        {
            bool notAWinner;

            //input indirecto
            var rand = new Random();

            do
            {
                //TODO extract & override 
                Roll(RollDie(rand));

                if (IsWrongAnswer(rand))
                {
                    notAWinner = WrongAnswer();
                }
                else
                {
                    notAWinner = WasCorrectlyAnswered();
                }
            } while (notAWinner);
        }

        private string CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool Add(string playerName)
        {
            _players.Add(playerName);
            _places[HowManyPlayers()] = 0;
            _purses[HowManyPlayers()] = 0;
            _inPenaltyBox[HowManyPlayers()] = false;

            Notify(playerName + " was added");
            Notify("They are player number " + _players.Count);
            return true;
        }


        private int HowManyPlayers()
        {
            return _players.Count;
        }

        private void Roll(int roll)
        {
            Notify(_players[_currentPlayer] + " is the current player");
            Notify("They have rolled a " + roll);

            if (_inPenaltyBox[_currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    Notify(_players[_currentPlayer] + " is getting out of the penalty box");
                    _places[_currentPlayer] = _places[_currentPlayer] + roll;
                    if (_places[_currentPlayer] > 11)
                    {
                        _places[_currentPlayer] = _places[_currentPlayer] - 12;
                    }

                    Notify(_players[_currentPlayer]
                                      + "'s new location is "
                                      + _places[_currentPlayer]);
                    Notify("The category is " + CurrentCategory());
                    AskQuestion();
                }
                else
                {
                    Notify(_players[_currentPlayer] + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                _places[_currentPlayer] = _places[_currentPlayer] + roll;
                if (_places[_currentPlayer] > 11)
                {
                    _places[_currentPlayer] = _places[_currentPlayer] - 12;
                }

                Notify(_players[_currentPlayer]
                                  + "'s new location is "
                                  + _places[_currentPlayer]);
                Notify("The category is " + CurrentCategory());
                AskQuestion();
            }
        }

        private void AskQuestion()
        {
            if (CurrentCategory() == "Pop")
            {
                Notify(_popQuestions.First());
                _popQuestions.RemoveFirst();
            }

            if (CurrentCategory() == "Science")
            {
                Notify(_scienceQuestions.First());
                _scienceQuestions.RemoveFirst();
            }

            if (CurrentCategory() == "Sports")
            {
                Notify(_sportsQuestions.First());
                _sportsQuestions.RemoveFirst();
            }

            if (CurrentCategory() == "Rock")
            {
                Notify(_rockQuestions.First());
                _rockQuestions.RemoveFirst();
            }
        }

        private string CurrentCategory()
        {
            if (_places[_currentPlayer] == 0) return "Pop";
            if (_places[_currentPlayer] == 4) return "Pop";
            if (_places[_currentPlayer] == 8) return "Pop";
            if (_places[_currentPlayer] == 1) return "Science";
            if (_places[_currentPlayer] == 5) return "Science";
            if (_places[_currentPlayer] == 9) return "Science";
            if (_places[_currentPlayer] == 2) return "Sports";
            if (_places[_currentPlayer] == 6) return "Sports";
            if (_places[_currentPlayer] == 10) return "Sports";
            return "Rock";
        }

        private bool DidPlayerWin()
        {
            return !(_purses[_currentPlayer] == 6);
        }

        private bool WrongAnswer()
        {
            Notify("Question was incorrectly answered");
            Notify(_players[_currentPlayer] + " was sent to the penalty box");
            _inPenaltyBox[_currentPlayer] = true;

            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;
            return true;
        }

        private bool WasCorrectlyAnswered()
        {
            if (_inPenaltyBox[_currentPlayer])
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    Notify("Answer was correct!!!!");
                    _purses[_currentPlayer]++;
                    Notify(_players[_currentPlayer]
                                      + " now has "
                                      + _purses[_currentPlayer]
                                      + " Gold Coins.");

                    var winner = DidPlayerWin();
                    _currentPlayer++;
                    if (_currentPlayer == _players.Count) _currentPlayer = 0;

                    return winner;
                }
                else
                {
                    _currentPlayer++;
                    if (_currentPlayer == _players.Count) _currentPlayer = 0;
                    return true;
                }
            }
            else
            {
                Notify("Answer was corrent!!!!");
                _purses[_currentPlayer]++;
                Notify(_players[_currentPlayer]
                                  + " now has "
                                  + _purses[_currentPlayer]
                                  + " Gold Coins.");

                var winner = DidPlayerWin();
                _currentPlayer++;
                if (_currentPlayer == _players.Count) _currentPlayer = 0;

                return winner;
            }
        }

        protected virtual bool IsWrongAnswer(Random rand)
        {
            var isWrongAnswer = rand.Next(9) == 7;
            _answersWasWrong.Add(isWrongAnswer);
            return isWrongAnswer;
        }

        protected virtual int RollDie(Random rand)
        {
            var rollDie = rand.Next(5) + 1;
            _rollsDie.Add(rollDie);
            return rollDie;
        }
        protected virtual void Notify(string message)
        {
            _notifications.Add(message);
            Console.WriteLine(message);
        }
    }
}