using SportsMatchScoring.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Match = SportsMatchScoring.Shared.Models.Match;

namespace SportsMatchScoring.Shared.Utilities
{
    public class MatchProcessor
    {
        private Match _match;
        private MatchResult _matchResult;
        private int[] winSets = new int[20];
        private int[] lossSets = new int[20];
        private int winsScore = 0;
        private int homeScore = 0;
        private int awayScore = 0;
        private int lossScore = 0;
        string winningTeam = "";
        string losingTeam = "";
        string result = "";


        public Tuple<Match, string> ProcessMatch(Match match)
        {
            _match = match;
            _matchResult = match.MatchResult;

            if (_matchResult.Draw == false)
            {
                winningTeam = _matchResult.WinningTeam == "HOME" ? _match.HomeTeam.TeamName : _match.AwayTeam.TeamName;
                losingTeam = _matchResult.WinningTeam == "HOME" ? _match.AwayTeam.TeamName : _match.HomeTeam.TeamName;
                match.Winner = _matchResult.WinningTeam == "HOME" ? _match.HomeTeam : _match.AwayTeam;
            }

            // potential issue if team names are too long 
            var scores = new StringBuilder("", 100);

            for (int i = 0; i < _matchResult.SetsScores.Count; i++)
            {
                winSets[i] = _matchResult.SetsScores[i][0];
                homeScore = _matchResult.SetsScores[i][0] > _matchResult.SetsScores[i][1] ? homeScore + 1 : homeScore;
            }
            for (int i = 0; i < _matchResult.SetsScores.Count; i++)
            {
                lossSets[i] = _matchResult.SetsScores[i][1];
                awayScore = _matchResult.SetsScores[i][1] > _matchResult.SetsScores[i][0] ? awayScore + 1 : awayScore;
            }

            for (int i = 0; i < _matchResult.SetsScores.Count; i++)
            {
                if (i == _matchResult.SetsScores.Count - 1)
                {
                    scores.Append($"{winSets[i]}-{lossSets[i]}.");
                }
                else
                {
                    scores.Append($"{winSets[i]}-{lossSets[i]}, ");
                }
            }

            if (_matchResult.Draw == true)
            {

                result = $"{match.HomeTeam.TeamName} and {match.AwayTeam.TeamName} draw ({homeScore} - {awayScore}) Scores: {scores.ToString()}";
            }
            else
            {
                winsScore = homeScore > awayScore ? homeScore : awayScore;
                lossScore = homeScore < awayScore ? homeScore : awayScore;
                result = $"{winningTeam} beat {losingTeam} ({winsScore} - {lossScore}) Scores: {scores.ToString()}";
            }

            _match.HomeSets = homeScore;
            _match.AwaySets = awayScore;

            return Tuple.Create(_match, result);
        }

    }
}
