using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SportsMatchScoring.Shared.Models
{
    public class GameRequest 
    {
        public required string HomeTeamName { get; set; } = "";
        public required string AwayTeamName { get; set; } = "";

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public required Games Game { get; set; }
        public required string[]? Scores { get; set; }

    }
}
