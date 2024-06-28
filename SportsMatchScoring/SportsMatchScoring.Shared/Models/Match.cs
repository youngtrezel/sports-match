using SportsMatchScoring.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsMatchScoring.Shared.Models
{
    public class Match
    {
        public Guid Id { get; set; }
        public Games Game { get; set; }
        public Team? HomeTeam { get; set; }
        public  Team? AwayTeam { get; set; }
        public Team? Winner { get; set; }
        public bool IsBestOf { get; set;}
        public required MatchResult? MatchResult { get; set; }
        public int HomeSets { get; set; }
        public int AwaySets { get; set;}

    }
}
