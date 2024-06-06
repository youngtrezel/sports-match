using SportsMatchScoring.Shared.Models;
using SportsMatchScoring.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsMatchScoring.Tests.Shared
{
    public class MatchResultMapperTests
    {
        [Fact]
        public void GetResultReturnsVolleyballHomeWin()
        {
            // Arrange 
            string[] scoresForSet = ["1001010101111011101111", "0110101010000100010000", "1001010101111011101111"];

            VolleyballScorePredictor volleyballScorePredictor = new VolleyballScorePredictor();

            Match match = new Match
            {
                //Id = new Guid(),
                Game = Games.VolleyBall,
                HomeTeam = new Team { Game = Games.VolleyBall, TeamName = "Ravens" },
                AwayTeam = new Team { Game = Games.VolleyBall, TeamName = "Broncos" },
                IsBestOf = true,
                MatchResult = volleyballScorePredictor.PredictScore(scoresForSet)
            };

            // Act 
            MatchProcessor matchProcessor = new MatchProcessor();
            var result = matchProcessor.ProcessMatch(match).Item2;

            // Assert
            Assert.Equal("Ravens beat Broncos (2 - 1) Scores: 15-7, 7-15, 15-7.", result);
        }

        [Fact]
        public void GetResultReturnsSquashHomeWin()
        {
            // Arrange 
            string[] scoresForSet = ["110101010111101101", "00000000000101101", "01101010111111110001"];

            SquashScorePredictor squashScorePredictor = new();

            Match match = new Match
            {
                //Id = new Guid(),
                Game = Games.Squash,
                HomeTeam = new Team { Game = Games.Squash, TeamName = "Ravens" },
                AwayTeam = new Team { Game = Games.Squash, TeamName = "Broncos" },
                IsBestOf = true,
                MatchResult = squashScorePredictor.PredictScore(scoresForSet)
            };

            // Act 
            MatchProcessor matchProcessor = new MatchProcessor();
            var result = matchProcessor.ProcessMatch(match).Item2;

            // Assert
            Assert.Equal("Ravens beat Broncos (2 - 1) Scores: 11-5, 0-11, 11-4.", result);
        }

        [Fact]
        public void GetResultReturnsSquashDraw()
        {
            // Arrange 
            string[] scoresForSet = ["101010101010101010101010", "101010101010101010101010", "101010101010101010101010"];

            SquashScorePredictor squashScorePredictor = new();

            Match match = new Match
            {
                //Id = new Guid(),
                Game = Games.Squash,
                HomeTeam = new Team { Game = Games.Squash, TeamName = "Ravens" },
                AwayTeam = new Team { Game = Games.Squash, TeamName = "Broncos" },
                IsBestOf = true,
                MatchResult = squashScorePredictor.PredictScore(scoresForSet)
            };

            // Act 
            MatchProcessor matchProcessor = new MatchProcessor();
            var result = matchProcessor.ProcessMatch(match).Item2;

            // Assert
            Assert.Equal("Ravens and Broncos draw (0 - 0) Scores: 12-12, 12-12, 12-12.", result);
        }

        [Fact]
        public void GetResultReturnsVolleyballDraw()
        {
            // Arrange 
            string[] scoresForSet = ["1010101010101010101010101010101010", "1010101010101010101010101010101010", "1010101010101010101010101010101010"];

            VolleyballScorePredictor volleyballScorePredictor = new VolleyballScorePredictor();

            Match match = new Match
            {
                //Id = new Guid(),
                Game = Games.Squash,
                HomeTeam = new Team { Game = Games.Squash, TeamName = "Ravens" },
                AwayTeam = new Team { Game = Games.Squash, TeamName = "Broncos" },
                IsBestOf = true,
                MatchResult = volleyballScorePredictor.PredictScore(scoresForSet)
            };

            // Act 
            MatchProcessor matchProcessor = new MatchProcessor();
            var result = matchProcessor.ProcessMatch(match).Item2;

            // Assert
            Assert.Equal("Ravens and Broncos draw (0 - 0) Scores: 17-17, 17-17, 17-17.", result);
        }

        [Fact]
        public void GetResultReturnsVolleyball2()
        {
            // Arrange 
            string[] scoresForSet = ["111111111100000000000001111011","111111111000000000000000", "1110001111000011111111"];

            VolleyballScorePredictor volleyballScorePredictor = new VolleyballScorePredictor();

            Match match = new Match
            {
                //Id = new Guid(),
                Game = Games.VolleyBall,
                HomeTeam = new Team { Game = Games.Squash, TeamName = "Broncos" },
                AwayTeam = new Team { Game = Games.Squash, TeamName = "Ravens" },
                IsBestOf = true,
                MatchResult = volleyballScorePredictor.PredictScore(scoresForSet)
            };

            // Act 
            MatchProcessor matchProcessor = new MatchProcessor();
            var result = matchProcessor.ProcessMatch(match).Item2;

            // Assert
            Assert.Equal("Broncos beat Ravens (2 - 1) Scores: 16-14, 9-15, 15-7.", result);
        }
    }
}
