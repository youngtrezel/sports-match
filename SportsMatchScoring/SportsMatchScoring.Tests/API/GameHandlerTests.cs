using SportsMatchScoring.Api.Handlers;
using SportsMatchScoring.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsMatchScoring.Tests.API
{
    public class GameHandlerTests
    {
        [Fact]
        public void DetermineWinningTeamSelectsVolleyballPredictor()
        {
            // Arrange
            GameRequest request = new()
            {
                HomeTeamName = "Ravens",
                AwayTeamName = "Broncos",               
                Game = Games.VolleyBall,
                Scores = ["1001010101111011101111", "0110101010000100010000", "1001010101111011101111"]
            };

            // Act
            GameHandler handler = new GameHandler();
            handler.SetupGame(request);
            var result = handler.GetResultsMessage();

            //Assert
            Assert.Equal("Ravens beat Broncos (2 - 1) Scores: 15-7, 7-15, 15-7.", result);

        }

        [Fact]
        public void DetermineWinningTeamSelectsSquashPredictor()
        {
            // Arrange
            GameRequest request = new()
            {
                HomeTeamName = "Ravens",
                AwayTeamName = "Broncos",
                Game = Games.Squash,
                Scores = ["110101010111101101", "00000000000101101", "01101010111111110001"]
            };

            // Act
            GameHandler handler = new GameHandler();
            handler.SetupGame(request);
            var result = handler.GetResultsMessage();

            //Assert
            Assert.Equal("Ravens beat Broncos (2 - 1) Scores: 11-5, 0-11, 11-4.", result);

        }
    }
}
