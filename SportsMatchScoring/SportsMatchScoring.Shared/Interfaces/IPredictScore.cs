using SportsMatchScoring.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsMatchScoring.Shared.Interfaces
{
    public interface IPredictScore
    {
        public MatchResult PredictScore(string[] scoresForSets);
    }
}
