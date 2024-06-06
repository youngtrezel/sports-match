
using SportsMatchScoring.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsMatchScoring.Tests.Shared
{
    public class SquashPredictorTests
    {
        [Fact]
        public void ReturnsHomeWin_ThreeSetsWinsInThird()
        {
            // Arrange 
            string[] scoresForSet = ["110101010111101101", "00000000000101101", "01101010111111110001"];

            SquashScorePredictor squashScorePredictor = new();

            // Act 
            var result = squashScorePredictor.PredictScore(scoresForSet);

            // Assert
            Assert.Equal("HOME", result.WinningTeam);
            Assert.Equal([11, 5], result.SetsScores[0]);
            Assert.Equal([0, 11], result.SetsScores[1]);
            Assert.Equal([11, 4], result.SetsScores[2]);
        }
        [Fact]
        public void ReturnsAwayWin_ThreeSetsWinsInThird()
        {
            // Arrange 
            string[] scoresForSet = ["001010101000010010", "11111111111010010", "10010101000000001110"];

            SquashScorePredictor squashScorePredictor = new();

            // Act 
            var result = squashScorePredictor.PredictScore(scoresForSet);

            // Assert
            Assert.Equal("AWAY", result.WinningTeam);
            Assert.Equal([5, 11], result.SetsScores[0]);
            Assert.Equal([11, 0], result.SetsScores[1]);
            Assert.Equal([4, 11], result.SetsScores[2]);
        }
        [Fact]
        public void ReturnsHomeWin_ThreeSetsWinsInFirstTwo()
        {
            // Arrange 
            string[] scoresForSet = ["100101010111101101", "100101010111101101", "011010101000010001"];

            SquashScorePredictor squashScorePredictor = new();

            // Act 
            var result = squashScorePredictor.PredictScore(scoresForSet);

            // Assert
            Assert.Equal("HOME", result.WinningTeam);
            Assert.Equal([11, 7], result.SetsScores[0]);
            Assert.Equal([11, 7], result.SetsScores[1]);
        }

        [Fact]
        public void ReturnsAwayWin_ThreeSetsWinsInFirstTwo()
        {
            // Arrange 
            string[] scoresForSet = ["011010101000010010", "011010101000010010", "100101010111101110"];

            SquashScorePredictor squashScorePredictor = new();

            // Act 
            var result = squashScorePredictor.PredictScore(scoresForSet);

            // Assert
            Assert.Equal("AWAY", result.WinningTeam);
            Assert.Equal([7, 11], result.SetsScores[0]);
            Assert.Equal([7, 11], result.SetsScores[1]);
        }

        [Fact]
        public void ReturnsHomeWin_StraightSetsWinWith11PointsToZero()
        {
            // Arrange 
            string[] scoresForSet = ["111111111110000000", "111111111110000000", "011010101000010001"];

            SquashScorePredictor squashScorePredictor = new();

            // Act 
            var result = squashScorePredictor.PredictScore(scoresForSet);

            // Assert
            Assert.Equal("HOME", result.WinningTeam);
            Assert.Equal([11, 0], result.SetsScores[0]);
            Assert.Equal([11, 0], result.SetsScores[1]);

        }

        [Fact]
        public void ReturnsAwayWin_StraightSetsWinWith11PointsToZero()
        {
            // Arrange 
            string[] scoresForSet = ["0000000000000000", "000000000000000", "011010101000010001"];

            SquashScorePredictor squashScorePredictor = new();

            // Act 
            var result = squashScorePredictor.PredictScore(scoresForSet);

            // Assert
            Assert.Equal("AWAY", result.WinningTeam);
            Assert.Equal([0, 11], result.SetsScores[0]);
            Assert.Equal([0, 11], result.SetsScores[1]);

        }

        [Fact]
        public void ReturnsHomeWinIn2Sets_FromSuddenDeathAfterBothTeamsReach10Points()
        {
            // Arrange 
            string[] scoresForSet = ["10101010101010101010111111", "10101010101010101010111111", "111111111110000000"];

            SquashScorePredictor squashScorePredictor = new();

            // Act 
            var result = squashScorePredictor.PredictScore(scoresForSet);

            // Assert
            Assert.Equal("HOME", result.WinningTeam);
            Assert.Equal([12, 10], result.SetsScores[0]);
            Assert.Equal([12, 10], result.SetsScores[1]);
        }

        [Fact]
        public void ReturnsAwayWinIn2Sets_FromSuddenDeathAfterBothTeamsReach10Points()
        {
            // Arrange 
            string[] scoresForSet = ["10101010101010101010000000", "10101010101010101010000000", "111111111110000000"];

            SquashScorePredictor squashScorePredictor = new();

            // Act 
            var result = squashScorePredictor.PredictScore(scoresForSet);

            // Assert
            Assert.Equal("AWAY", result.WinningTeam);
            Assert.Equal([10, 12], result.SetsScores[0]);
            Assert.Equal([10, 12], result.SetsScores[1]);
        }


        [Fact]
        public void ReturnsDraw()
        {
            // Arrange 
            string[] scoresForSet = ["101010101010101010101010", "101010101010101010101010", "101010101010101010101010"];

            SquashScorePredictor squashScorePredictor = new();

            // Act 
            var result = squashScorePredictor.PredictScore(scoresForSet);

            // Assert
            Assert.Equal("DRAW", result.WinningTeam);
            Assert.Equal([12, 12], result.SetsScores[0]);
            Assert.Equal([12, 12], result.SetsScores[1]);
            Assert.Equal([12, 12], result.SetsScores[2]);
            Assert.True( result.Draw);
        }

    }
}
