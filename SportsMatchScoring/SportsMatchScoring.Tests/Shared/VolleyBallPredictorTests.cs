using SportsMatchScoring.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsMatchScoring.Tests.Shared
{
    public class VolleyBallPredictorTests
    {

        public VolleyBallPredictorTests() { }

        [Fact]
        public void ReturnsHomeWin_AfterOneSet()
        {
            // Arrange 
            string[] scoresForSet = ["1001010101111011101111"];

            VolleyballScorePredictor volleyballScorePredictor = new();

            // Act 
            var result = volleyballScorePredictor.PredictScore(scoresForSet);

            // Assert
            Assert.Equal("HOME", result.WinningTeam);
            Assert.Equal([15, 7], result.SetsScores[0]);

        }
        [Fact]
        public void ReturnsAwayWin_AfterOneSet()
        {
            // Arrange 
            string[] scoresForSet = ["0110101010000100010000"];

            VolleyballScorePredictor volleyballScorePredictor = new();

            // Act 
            var result = volleyballScorePredictor.PredictScore(scoresForSet);

            // Assert
            Assert.Equal("AWAY", result.WinningTeam);
            Assert.Equal([7, 15], result.SetsScores[0]);

        }

        [Fact]
        public void ReturnsHomeWin_AfterThreeSet()
        {
            // Arrange 
            string[] scoresForSet = ["1001010101111011101111", "0110101010000100010000", "1001010101111011101111"];

            VolleyballScorePredictor volleyballScorePredictor = new();

            // Act 
            var result = volleyballScorePredictor.PredictScore(scoresForSet);

            // Assert
            Assert.Equal("HOME", result.WinningTeam);
            Assert.Equal([15, 7], result.SetsScores[0]);
            Assert.Equal([7, 15], result.SetsScores[1]);
            Assert.Equal([15, 7], result.SetsScores[2]);
        }
        [Fact]
        public void ReturnsAwayWin_AfterThreeSet()
        {
            // Arrange 
            string[] scoresForSet = ["0110101010000100010000", "1001010101111011101111", "0110101010000100010000"];

            VolleyballScorePredictor volleyballScorePredictor = new();

            // Act 
            var result = volleyballScorePredictor.PredictScore(scoresForSet);

            // Assert
            Assert.Equal("AWAY", result.WinningTeam);
            Assert.Equal([7, 15], result.SetsScores[0]);
            Assert.Equal([15, 7], result.SetsScores[1]);
            Assert.Equal([7, 15], result.SetsScores[2]);
        }
        [Fact]
        public void ReturnsHomeWin_AfterThreeSetWhitewash()
        {
            // Arrange 
            string[] scoresForSet = ["111111111111111", "111111111111111", "111111111111111"];

            VolleyballScorePredictor volleyballScorePredictor = new();

            // Act 
            var result = volleyballScorePredictor.PredictScore(scoresForSet);

            // Assert
            Assert.Equal("HOME", result.WinningTeam);
            Assert.Equal([15, 0], result.SetsScores[0]);
            Assert.Equal([15, 0], result.SetsScores[1]);
            Assert.Equal([15, 0], result.SetsScores[2]);
        }
        [Fact]
        public void ReturnsAwayWin_AfterThreeSetWhitewash()
        {
            // Arrange 
            string[] scoresForSet = ["000000000000000", "000000000000000", "000000000000000"];

            VolleyballScorePredictor volleyballScorePredictor = new();

            // Act 
            var result = volleyballScorePredictor.PredictScore(scoresForSet);

            // Assert
            Assert.Equal("AWAY", result.WinningTeam);
            Assert.Equal([0, 15], result.SetsScores[0]);
            Assert.Equal([0, 15], result.SetsScores[1]);
            Assert.Equal([0, 15], result.SetsScores[2]);
        }
        [Fact]
        public void ReturnDrawAfterOneSet()
        {
            string[] scoresForSet = ["1010101010101010101010101010101010"];

            VolleyballScorePredictor volleyballScorePredictor = new();

            // Act 
            var result = volleyballScorePredictor.PredictScore(scoresForSet);

            // Assert
            Assert.Equal("DRAW", result.WinningTeam);
            Assert.Equal([17, 17], result.SetsScores[0]);
            Assert.True(result.Draw);
        }
        [Fact]
        public void ReturnDrawAfterThreeSets()
        {
            string[] scoresForSet = ["1010101010101010101010101010101010", "1010101010101010101010101010101010", "1010101010101010101010101010101010"];

            VolleyballScorePredictor volleyballScorePredictor = new();

            // Act 
            var result = volleyballScorePredictor.PredictScore(scoresForSet);

            // Assert
            Assert.Equal("DRAW", result.WinningTeam);
            Assert.Equal([17, 17], result.SetsScores[0]);
            Assert.Equal([17, 17], result.SetsScores[1]);
            Assert.Equal([17, 17], result.SetsScores[2]);
            Assert.True(result.Draw);

        }

        [Fact]
        public void ReturnsHomeWin_AfterThreeSet2()
        {
            // Arrange 
            string[] scoresForSet = ["1110001111000011111111", "111111111000000000000000", "111111111100000000000001111011"];

            VolleyballScorePredictor volleyballScorePredictor = new();

            // Act 
            var result = volleyballScorePredictor.PredictScore(scoresForSet);

            // Assert
            Assert.Equal("HOME", result.WinningTeam);
            Assert.Equal([15, 7], result.SetsScores[0]);
            Assert.Equal([9, 15], result.SetsScores[1]);
            Assert.Equal([16,14], result.SetsScores[2]);
        }

        [Fact]
        public void ReturnDrawAfterThreeSetsOneDrawn()
        {
            string[] scoresForSet = ["1010101010101010101010101010101010101010", "111111111111111", "000000000000000"];

            VolleyballScorePredictor volleyballScorePredictor = new();

            // Act 
            var result = volleyballScorePredictor.PredictScore(scoresForSet);

            // Assert
            Assert.Equal("DRAW", result.WinningTeam);
            Assert.Equal([20, 20], result.SetsScores[0]);
            Assert.Equal([15, 0], result.SetsScores[1]);
            Assert.Equal([0, 15], result.SetsScores[2]);
            Assert.True(result.Draw);

        }

    }
}
