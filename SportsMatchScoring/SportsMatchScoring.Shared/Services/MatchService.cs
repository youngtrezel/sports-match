using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsMatchScoring.Shared.Interfaces;
using SportsMatchScoring.Shared.Models;

namespace SportsMatchScoring.Shared.Services
{
    public class MatchService
    {

        public MatchService() { }
    

        public MatchResult GetMatchResult(IPredictScore scorePredictor, PredictorRequest request)
        {
            return scorePredictor.PredictScore(request.Scores);
        }
    }
}
