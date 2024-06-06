using SportsMatchScoring.Shared.Models;

namespace SportsMatchScoring.Shared.Utilities
{
    public class PredictorHelper
    {
        public static bool ValidateLimitSet(string[] scoresForSets, int scoreLimit)
        {
            int scoresLimit = 0;
            foreach (string s in scoresForSets)
            {
                var count = s.ToCharArray().Where(x => x == '0').Select(z => z).Count();
                scoresLimit = count > scoresLimit ? count : scoresLimit;
            }
            foreach (string s in scoresForSets)
            {
                var count = s.ToCharArray().Where(x => x == '1').Select(z => z).Count();
                scoresLimit = count > scoresLimit ? count : scoresLimit;
            }

            return scoresLimit >= scoreLimit;
        }

        public static void UpdateSetScore(int homeScore, int awayScore, MatchResult matchResult)
        {
            var setScore = new int[] { homeScore, awayScore };
            matchResult.SetsScores.Add(setScore);
        }
    }
}
