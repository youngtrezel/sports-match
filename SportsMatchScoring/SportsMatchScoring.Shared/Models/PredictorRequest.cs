using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsMatchScoring.Shared.Models
{
    public record PredictorRequest
    {
        public string[]? Scores { get; set; }
        //public int? ScoreLimit { get; set; }
    }
}
