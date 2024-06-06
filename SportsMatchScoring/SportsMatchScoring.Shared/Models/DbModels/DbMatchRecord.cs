using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsMatchScoring.Shared.Models.DbModels
{
    public class DbMatchRecord
    {
        public int Id { get; set; }
        public string HomeTeam { get; set; } = string.Empty;
        public string AwayTeam { get; set; } = string.Empty;
        public int HomeSets { get; set; }
        public int AwaySets { get; set; }
        public DateTimeOffset MatchDate { get; set; }
        public string Game { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
    }
}
