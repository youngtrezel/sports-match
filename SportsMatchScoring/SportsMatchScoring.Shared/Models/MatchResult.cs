using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsMatchScoring.Shared.Models
{
    public class MatchResult
    {
        public MatchResult() 
        {
            SetsScores = new List<int[]>();
            WinningTeam =  String.Empty;
        }    

        public string WinningTeam {  get; set; }
        public List<int[]> SetsScores { get; set; }
        public bool Draw {  get; set; }
    }
}
