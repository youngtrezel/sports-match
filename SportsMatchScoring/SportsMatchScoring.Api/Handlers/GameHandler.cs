using SportsMatchScoring.Api.Interfaces;
using SportsMatchScoring.Shared.Interfaces;
using SportsMatchScoring.Shared.Models;
using SportsMatchScoring.Shared.Models.DbModels;
using SportsMatchScoring.Shared.Services;
using SportsMatchScoring.Shared.Utilities;

namespace SportsMatchScoring.Api.Handlers
{
    public class GameHandler : IGameHandler
    {
        private Match? match;
        private MatchResult? matchResult;
        private GameRequest? _gameRequest;
        private MatchService? _matchService;

        public void SetupGame (GameRequest gameRequest) {

            _gameRequest = gameRequest;
            matchResult = new MatchResult();
            match = new Match()
            {
                MatchResult = matchResult
            };
            _matchService = new();

            Team homeTeam = new()
            {
                Game = _gameRequest.Game,
                TeamName = _gameRequest.HomeTeamName
            };

            Team awayTeam = new()
            {
                Game = _gameRequest.Game,
                TeamName = _gameRequest.AwayTeamName
            };

            match.AwayTeam = awayTeam;
            match.HomeTeam = homeTeam;
            match.Game = _gameRequest.Game;
            match.MatchResult = DetermineWinningTeam();
        }

        public string GetResultsMessage()
        {
            MatchProcessor matchProcessor = new();
            return matchProcessor.ProcessMatch(match).Item2;
        }

        public Match GetMatchDetails()
        {
            MatchProcessor matchProcessor = new();
            return matchProcessor.ProcessMatch(match).Item1;
        }
        private MatchResult DetermineWinningTeam()
        {
            PredictorRequest request = new()
            {
                Scores = _gameRequest.Scores
            };

            if(_gameRequest.Game == Games.VolleyBall)
            {
                VolleyballScorePredictor volleyballScorePredictor = new ();
                matchResult = _matchService.GetMatchResult (volleyballScorePredictor, request);
            }
            if(_gameRequest.Game == Games.Squash)
            {
                SquashScorePredictor squashScorePredictor = new ();
                matchResult = _matchService.GetMatchResult (squashScorePredictor, request);
            }      
            
            return matchResult;
        }

    }
}
