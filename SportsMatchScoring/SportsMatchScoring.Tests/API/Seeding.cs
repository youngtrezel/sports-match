using SportsMatchScoring.Data;
using SportsMatchScoring.Shared.Models;

namespace SportsMatchScoring.Tests.API
{
    public class Seeding
    {
        public static void SeedDb(SportsMatchContext context)
        {
            context.MatchRecords.AddRange(CreateMatchRecords());
            context.SaveChanges();
        }

        private static List<MatchRecord> CreateMatchRecords()
        {
            var records = new List<MatchRecord>();

            for (int i = 0; i < 20; i++)
            {
                records.Add(new MatchRecord
                {
                    Id = Guid.NewGuid(),
                    HomeTeam = $"Ravens{i}",
                    AwayTeam = $"Broncos{i}",
                    HomeSets = 2,
                    AwaySets = 1,
                    Game = "Volleyball",
                    Result = $"Ravens{i} beat Broncos{i} (2 - 1) Scores: 15-7, 7-15, 15-7."
                });

            };

            var manUtd = new MatchRecord
            {
                Id = Guid.NewGuid(),
                HomeTeam = $"Manchester",
                AwayTeam = $"Broncos",
                HomeSets = 2,
                AwaySets = 1,
                Game = "Volleyball",
                Result = $"Manchester beat Broncos (2 - 1) Scores: 15-7, 7-15, 15-7."
            };

            records.Add(manUtd);

            return records;
        }
    }
}
