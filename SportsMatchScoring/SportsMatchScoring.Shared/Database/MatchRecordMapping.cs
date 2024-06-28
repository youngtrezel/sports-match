using SportsMatchScoring.Shared.Models;
using SportsMatchScoring.Shared.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsMatchScoring.Shared.Database
{
    public static class MatchRecordMapping
    {
        public static MatchRecord Map(DbMatchRecord record)
        {
            return new MatchRecord
            {
                Id = Guid.NewGuid(),
                MatchDate = DateTimeOffset.Now,
                AwaySets = record.AwaySets,
                HomeSets = record.HomeSets,
                HomeTeam = record.HomeTeam,
                AwayTeam = record.AwayTeam,
                Game = record.Game,
                Result = record.Result
            };
        }
    }
}

