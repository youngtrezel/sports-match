using SportsMatchScoring.Shared.Models;
using SportsMatchScoring.Shared.Utilities;

namespace SportsMatchScoring.Tests.Shared
{
    public class PredictorHelperTests
    {
        public PredictorHelperTests() { }

        [Fact]
        public void VaildateLimitSet_ReturnsFalse()
        {
            // Arrange 
            string[] scoresForSet = ["1001010101111011101111"];
            int limit = 16;

            // Act 
            var result = PredictorHelper.ValidateLimitSet(scoresForSet, limit);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void VaildateLimitSet_ReturnsTrue()
        {
            // Arrange 
            string[] scoresForSet = ["1001010101111011101111"];
            int limit = 15;

            // Act 
            var result = PredictorHelper.ValidateLimitSet(scoresForSet, limit);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void UpdateSetScoreUpdatesMatchResult()
        {
            // Arrange 
            MatchResult matchResult = new MatchResult();
            int homeScore = 7;
            int awayScore = 15;

            // Act 
            PredictorHelper.UpdateSetScore(homeScore, awayScore, matchResult);

            // Assert
            Assert.NotEmpty(matchResult.SetsScores);
            Assert.Equal(homeScore, matchResult.SetsScores[0][0]);
            Assert.Equal(awayScore, matchResult.SetsScores[0][1]);
        }
    }
}
