using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsMatchScoring.Shared.Models
{
    public class Team
    {
        public int Id { get; set; }
        public required string TeamName { get; set; }
        public Games Game { get; set; }  
        public IEnumerable<Match> GamesPlayed { get; set; } = [];
    }
}
