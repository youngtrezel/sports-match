using SportsMatchScoring.Shared.Interfaces;
using SportsMatchScoring.Shared.Models;
using SportsMatchScoring.Shared.Utilities;

namespace SportsMatchScoring.Shared.Models
{
    public class SquashScorePredictor : IPredictScore
    {
        private readonly int Limit = 11;
        private readonly int Sets = 3;
        private string? _score;
        private int _scoreLimit;
        private int _sets;
        private string[]? _scoresForSets;
        private int[] setsPlayed = new int[2];
        private MatchResult? _matchResult;
        private int winningSetTotal;
        private bool suddenDeath = false;

        public SquashScorePredictor(){ }

        public MatchResult PredictScore(string[] scoresForSets)
        {

            _scoresForSets = scoresForSets;
            _sets = scoresForSets.Length;
            _matchResult = new MatchResult();

            _scoreLimit = Limit;
            _sets = _sets > 0 ? _sets : Sets;
            winningSetTotal = _sets > 2 ? _sets - 1 : _sets;
            setsPlayed[0] = 0;
            setsPlayed[1] = 0;

            foreach (string set in _scoresForSets)
            {

                if (setsPlayed[0] == winningSetTotal || setsPlayed[1] == winningSetTotal)
                {
                    _matchResult.WinningTeam = setsPlayed[0] > setsPlayed[1] ? "HOME" : "AWAY";
                    return _matchResult;
                }

                _score = set;

                int[] count = new int[2];
                int i;
                for (i = 0; i < _score.Length; i++)
                {
                    // increase count using hexideciaml value for 0 or 1
                    count[_score[i] - '0']++;

                    // check losing condition
                    if (count[0] == _scoreLimit && count[1] < _scoreLimit - 1)
                    {                       
                        PredictorHelper.UpdateSetScore(count[1], count[0], _matchResult);

                        if (setsPlayed[0] == winningSetTotal || setsPlayed[1] == winningSetTotal)
                        {
                            _matchResult.WinningTeam = setsPlayed[0] > setsPlayed[1] ? "HOME" : "AWAY";
                            return _matchResult;
                        }
                        setsPlayed[1]++;
                        break;
                    }

                    // check winning condition
                    if (count[1] == _scoreLimit && count[0] < _scoreLimit - 1)
                    {                       
                        PredictorHelper.UpdateSetScore(count[1], count[0], _matchResult);

                        if (setsPlayed[0] == winningSetTotal || setsPlayed[1] == winningSetTotal)
                        {
                            _matchResult.WinningTeam = setsPlayed[0] > setsPlayed[1] ? "HOME" : "AWAY";
                            return _matchResult;
                        }
                        setsPlayed[0]++;
                        break;
                    }

                    if (count[0] == _scoreLimit - 1 && count[1] == _scoreLimit - 1)
                    {
                        suddenDeath = true;
                        break;
                        
                    }

                    if (i == _score.Length - 1)
                    {
                        PredictorHelper.UpdateSetScore(count[1], count[0], _matchResult);
                        _matchResult.WinningTeam = "DRAW";
                        _matchResult.Draw = true;
                        return _matchResult;
                    }

                }

                for (i++; i < _score.Length; i++)
                {
                    // break if arrived here from winning. 
                    if  (!suddenDeath && count[0] == _scoreLimit && count[1] != _scoreLimit -1 || 
                        !suddenDeath && count[1] == _scoreLimit && count[0] != _scoreLimit - 1)
                    {
                        break;
                    }

                        // increase count
                    count[_score[i] - '0']++;

                    // check for 2 point lead
                    if (Math.Abs(count[0] - count[1]) == 2)
                    {
                        // condition of lost
                        if (count[0] > count[1])
                        {
                            setsPlayed[1]++;
                            PredictorHelper.UpdateSetScore(count[1], count[0], _matchResult);
                            if (setsPlayed[0] == winningSetTotal || setsPlayed[1] == winningSetTotal)
                            {
                                _matchResult.WinningTeam = setsPlayed[0] > setsPlayed[1] ? "HOME" : "AWAY";
                                return _matchResult;
                            }
                            break;
                        }
                        // condition of win
                        else
                        {
                            setsPlayed[0]++;
                            PredictorHelper.UpdateSetScore(count[1], count[0], _matchResult);
                            if (setsPlayed[0] == winningSetTotal || setsPlayed[1] == winningSetTotal)
                            {
                                _matchResult.WinningTeam = setsPlayed[0] > setsPlayed[1] ? "HOME" : "AWAY";
                                return _matchResult;
                            }
                            break;
                        }
                    } 
                    if (i == _score.Length - 1)
                    {
                        PredictorHelper.UpdateSetScore(count[1], count[0], _matchResult);
                        if (Math.Abs(count[0] - count[1]) == 0 && _sets == 1)
                        {
                            _matchResult.WinningTeam = "DRAW";
                            _matchResult.Draw = true;
                            return _matchResult;
                        }                
                    }
                }
                _sets--;
            }

            if (setsPlayed[0] == setsPlayed[1])
            {
                _matchResult.WinningTeam = "DRAW";
                _matchResult.Draw = true;
                return _matchResult;
            } else
            {
                _matchResult.WinningTeam = setsPlayed[0] > setsPlayed[1] ? "HOME" : "AWAY";
                return _matchResult;
            }

                
        }
    }
}
